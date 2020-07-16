using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu_Scene : MonoBehaviour
{
    private CanvasGroup fadeGroup;
    private float fadeInSpeed = 0.33f;

    public RectTransform menuContainer;
    public Transform levelPanel;
    public Transform batPanel;
    public Transform trailPanel;
    public Transform optionsPanel;
    public Transform creditsPanel;

    public Text batBuySetText;
    public Text trailBuySetText;
    public Text goldText;

    private MenuCamera menuCam;

    private int[] batCost = new int[] { 0, 5, 5, 5, 10, 10, 10, 15, 15, 10 };
    private int[] trailCost = new int[] { 0, 20, 40, 40, 60, 60, 80, 80, 100, 100 };

    private int selectedBatIndex;
    private int selectedTrailIndex;
    private int activeBatIndex;
    private int activeTrailIndex;

    private Vector3 desiredMenuPosition;

    private GameObject currentTrail;
    private GameObject currentBat;

    public AnimationCurve enteringLevelZoomCurve;
    private bool isEnteringLevel = false;
    private float zoomDuration = 3.0f;
    private float zoomTransition;

    private Texture previousTrail;
    private GameObject lastPreviewObject;

    public Transform trailPreviewObject;
    public RenderTexture trailPreviewTexture;

    private void Start()
    {
        //Find the only MenuCamera and assign it
        menuCam = FindObjectOfType<MenuCamera>();

        //$$ Temporary
        SaveManager.Instance.state.gold = 999;

        //Position our camera on the focused menu
        SetCameraTo(Manager.Instance.menuFocus);

        //Tell our gold text how much we currently have
        UpdateGoldText();
        //Grab only CanvasGroup in the scene
        fadeGroup = FindObjectOfType<CanvasGroup>();
        Debug.Log("fadegroup");

        //Start with solid screen
        fadeGroup.alpha = 1;

        //Add button on-click events to shop buttons
        InitializeShop();

        //Add button on-click events to levels
        InitializeLevel();

        //Set player's preferences for both bat and trail
        OnBatSelect(SaveManager.Instance.state.activeBat);
        SetBat(SaveManager.Instance.state.activeBat);

        OnTrailSelect(SaveManager.Instance.state.activeTrail);
        SetTrail(SaveManager.Instance.state.activeTrail);

        //Make buttons bigger for selected item
        batPanel.GetChild(SaveManager.Instance.state.activeBat).GetComponent<RectTransform>().localScale = Vector3.one * 1.125f;
        trailPanel.GetChild(SaveManager.Instance.state.activeTrail).GetComponent<RectTransform>().localScale = Vector3.one * 1.125f;

        //Create the trail preview
        lastPreviewObject = GameObject.Instantiate(Manager.Instance.ballTrails[SaveManager.Instance.state.activeTrail]) as GameObject;
        lastPreviewObject.transform.SetParent(trailPreviewObject);
        lastPreviewObject.transform.localPosition = Vector3.zero;
    }

    private void Update()
    {
        //Fade In
        fadeGroup.alpha = 1 - Time.timeSinceLevelLoad * fadeInSpeed;

        //Menu navigation (smooth)
        menuContainer.anchoredPosition3D = Vector3.Lerp(menuContainer.anchoredPosition3D, desiredMenuPosition, 0.1f);

        //Entering level zoom
        if(isEnteringLevel)
        {
            //Add to the zoomTransition float
            zoomTransition += (1 / zoomDuration) * Time.deltaTime;

            //Change the scale, following the animation curve
            menuContainer.localScale = Vector3.Lerp(Vector3.one, Vector3.one * 5, enteringLevelZoomCurve.Evaluate(zoomTransition));

            //Change the desired position of the canvas so it can follow the scaler
            //This will zoom in the center
            Vector3 newDesiredPosition = desiredMenuPosition * 5;
            //This adds to the specific position of the level on the canvas
            RectTransform levelRectTransform = levelPanel.GetChild(Manager.Instance.curentLevel).GetComponent<RectTransform>();
            newDesiredPosition -= levelRectTransform.anchoredPosition3D * 5;

            //This line will override the previous position update
            menuContainer.anchoredPosition3D = Vector3.Lerp(desiredMenuPosition, newDesiredPosition, enteringLevelZoomCurve.Evaluate(zoomTransition));

            //Fade to white screen, this will override the first line of the update
            fadeGroup.alpha = zoomTransition;

            //Are we done with the animation
            if(zoomTransition >= 1)
            {
                //Enter the level
                SceneManager.LoadScene("One_Flipper3D_No_Exit");
            }

        }
    }

    private void InitializeShop()
    {
        //Assign references
        if (batPanel == null || trailPanel == null)
        {
            Debug.Log("You didn't assign the bat/trail panel references");
        }

        //For each bat panel child, find the button and add onClick.
        int i = 0;
        foreach (Transform batTransform in batPanel)
        {
            int currentIndex = i;

            Button batButton = batTransform.GetComponent<Button>();
            batButton.onClick.AddListener(() => OnBatSelect(currentIndex));

            // Set the bat of the image, based on if owned or not.
            Image img = batTransform.GetComponent<Image>();
            img.color = SaveManager.Instance.IsBatOwned(i) ? Color.white : new Color(0.7f, 0.7f, 0.7f);

            i++;
        }
        //Reset index
        i = 0;
        //For each trail panel child, find the button and add onClick.
        foreach (Transform trailTransform in trailPanel)
        {
            int currentIndex = i;

            Button trailButton = trailTransform.GetComponent<Button>();
            trailButton.onClick.AddListener(() => OnTrailSelect(currentIndex));

            // Set the color of the image based if owned or not.
            RawImage img = trailTransform.GetComponent<RawImage>();
            img.color = SaveManager.Instance.IsTrailOwned(i) ? Color.white : new Color(0.7f, 0.7f, 0.7f);

            i++;
        }

        //Set the previous trail to prevent bug when swapping
        previousTrail = trailPanel.GetChild(SaveManager.Instance.state.activeTrail).GetComponent<RawImage>().texture;
    }

    private void InitializeLevel()
    {
        //Assign references
        if (levelPanel == null || trailPanel == null)
        {
            Debug.Log("You didn't assign the level panel references");
        }

        //For each level panel child, find the button and add onClick.
        int i = 0;
        foreach (Transform levelTransform in levelPanel)
        {
            int currentIndex = i;

            Button levelButton = levelTransform.GetComponent<Button>();
            levelButton.onClick.AddListener(() => OnLevelSelect(currentIndex));

            Image img = levelTransform.GetComponent<Image>();

            //Is it unlocked?
            if(i <= SaveManager.Instance.state.completedLevel)
            {
                //It is unlocked
                if(i == SaveManager.Instance.state.completedLevel)
                {
                    //It is not completed
                    img.color = Color.white;
                }
                else
                {
                    //Level is already completed
                    img.color = Color.green;
                }
            }
            else
            {
                //Level isnt unlocked, disable the button
                levelButton.interactable = false;

                //Set to a dark color
                img.color = Color.gray;
            }

            i++;
        }
    }

    private void SetCameraTo(int menuIndex)
    {
        NavigateTo(menuIndex);
        menuContainer.anchoredPosition3D = desiredMenuPosition;
    }

    private void NavigateTo(int menuIndex)
    {
        switch (menuIndex)
        {
            //Case 0 and Default = Main Menu
            default:
            case 0:
                desiredMenuPosition = Vector3.zero;
                menuCam.BackToMainMenu();
                break;
            //Case 1 = LevelSelect
            case 1:
                desiredMenuPosition = Vector3.right * 1080;
                menuCam.MoveToLevel();
                break;
            //Case 2 = Shop
            case 2:
                desiredMenuPosition = Vector3.left * 1080;
                menuCam.MoveToShop();
                break;
            //Case 3 = Options
            case 3:
                desiredMenuPosition = Vector3.down * -1922;
                break;
            //Case 4 = Credits
            case 4:
                desiredMenuPosition = new Vector3(-1080, 1922, 0);
                break;
        }
    }

    private void SetBat(int index)
    {
        //Set the active index
        activeBatIndex = index;
        SaveManager.Instance.state.activeBat = index;

        //Change the bat
        if (currentBat != null)
            Destroy(currentBat);

        //Create a new bat
        currentBat = Instantiate(Manager.Instance.playerBats[index]) as GameObject;

        //Set it as a child of the player
        currentBat.transform.SetParent(FindObjectOfType<BatHolder>().transform);

        //Position/Rotation
        currentBat.transform.localPosition = new Vector3(0.104f, 0.048f, 0.039f);
        currentBat.transform.localRotation = Quaternion.Euler(-56.128f, -12.406f, 258.001f);

        //Change buy/set button text
        batBuySetText.text = "Current";

        //Remember Preferences
        SaveManager.Instance.Save();
    }

    private void SetTrail(int index)
    {
        //Set the active index
        activeTrailIndex = index;
        SaveManager.Instance.state.activeTrail = index;

        //Change the trail on the model
        if (currentTrail != null)
            Destroy(currentTrail);

        //Create a new trail
        currentTrail = Instantiate(Manager.Instance.ballTrails[index]) as GameObject;

        //Set it as a child of the ball
        currentTrail.transform.SetParent(FindObjectOfType<Menu_Ball>().transform);

        //Position/Rotation/Scaling
        currentTrail.transform.localPosition = Vector3.zero;
        currentTrail.transform.localRotation = Quaternion.Euler(0, 0, 90);
        currentTrail.transform.localScale = Vector3.one;

        //Change buy/set button text
        trailBuySetText.text = "Current";

        //Remember Preferences
        SaveManager.Instance.Save();
    }

    private void UpdateGoldText()
    {
        goldText.text = SaveManager.Instance.state.gold.ToString();
    }
    

    //Buttons
    public void OnPlayClick()
    {
        NavigateTo(1);
        Debug.Log("Play Button Has Been Clicked");
    }

    public void OnShopClick()
    {
        NavigateTo(2);
        Debug.Log("Shop Button Has Been Clicked");
    }

    public void OnBackClick()
    {
        NavigateTo(0);
        Debug.Log("Back Button has been clicked");
    }

    public void OnOptionsClick()
    {
        NavigateTo(3);
        Debug.Log("Options Button has been clicked");
    }

    public void OnCreditsClick()
    {
        NavigateTo(4);
        Debug.Log("Credits Button has been clicked");
    }

    public void OnCreditsBackClick()
    {
        NavigateTo(3);
        Debug.Log("Back Button has been clicked");
    }

    private void OnTrailSelect(int currentIndex)
    {
        Debug.Log("Selecting trail button : " + currentIndex);

        //If the button clicked is already selected, exit
        if (selectedTrailIndex == currentIndex)
        {
            return;
        }

        //Preview Trail
        //Get the image of the preview button
        trailPanel.GetChild(selectedTrailIndex).GetComponent<RawImage>().texture = previousTrail;
        //Keep the new trails preview image in the previous trail
        previousTrail = trailPanel.GetChild(currentIndex).GetComponent<RawImage>().texture;
        //Set the new trail preview image to the other camera.
        trailPanel.GetChild(currentIndex).GetComponent<RawImage>().texture = trailPreviewTexture;

        //Change the physical object of the trail preview
        if(lastPreviewObject != null)
            Destroy(lastPreviewObject);
        lastPreviewObject = GameObject.Instantiate(Manager.Instance.ballTrails[currentIndex]) as GameObject;
        lastPreviewObject.transform.SetParent(trailPreviewObject);
        lastPreviewObject.transform.localPosition = Vector3.zero;



        //Make the icon slightly bigger
        trailPanel.GetChild(currentIndex).GetComponent<RectTransform>().localScale = Vector3.one * 1.125f;
        //Put the previous icon on normal scale
        trailPanel.GetChild(selectedTrailIndex).GetComponent<RectTransform>().localScale = Vector3.one;

        //Set the selected trail
        selectedTrailIndex = currentIndex;

        //Change the content of the Buy/Set Button, depending on the state of the trail. Owned or not
        if (SaveManager.Instance.IsTrailOwned(currentIndex))
        {
            //Trail is owned
            //Is it already our current trail?
            if (activeTrailIndex == currentIndex)
            {
                trailBuySetText.text = "Current";
            }
            else
            {
                trailBuySetText.text = "Select";
            }
        }
        else
        {
            //Trail isn't owned
            trailBuySetText.text = "Buy: " + trailCost[currentIndex].ToString();
        }
    }

    private void OnBatSelect(int currentIndex)
    {
        Debug.Log("Selecting bat button : " + currentIndex);

        //If the button clicked is already selected, exit
        if(selectedBatIndex == currentIndex)
        {
            return;
        }

        //Make the icon slightly bigger
        batPanel.GetChild(currentIndex).GetComponent<RectTransform>().localScale = Vector3.one * 1.125f;
        //Put the previous icon on normal scale
        batPanel.GetChild(selectedBatIndex).GetComponent<RectTransform>().localScale = Vector3.one;

        //Set the selected bat
        selectedBatIndex = currentIndex;

        //Change the content of the Buy/Set Button, depending on the state of the bat. Owned or not
        if(SaveManager.Instance.IsBatOwned(currentIndex))
        {
            //Bat is owned
            //Is it already our current bat?
            if (activeBatIndex == currentIndex)
            {
                batBuySetText.text = "Current";
            }
            else
            {
                batBuySetText.text = "Select";
            }
        }
        else
        {
            //Bat isn't owned
            batBuySetText.text = "Buy: " + batCost[currentIndex].ToString();
        }
    }

    private void OnLevelSelect(int currentIndex)
    {
        Manager.Instance.curentLevel = currentIndex;
        isEnteringLevel = true;
        Debug.Log("Selecting level : " + currentIndex);
    }

    public void OnBatBuySet()
    {
        Debug.Log("Buy/Set bat");

        //Is selected bat owned.
        if(SaveManager.Instance.IsBatOwned(selectedBatIndex))
        {
            //Set the bat
            SetBat(selectedBatIndex);
        }
        else
        {
            //Attempt to buy the bat
            if(SaveManager.Instance.BuyBat(selectedBatIndex, batCost[selectedBatIndex]))
            {
                // Success
                SetBat(selectedBatIndex);

                //Change color of the button
                batPanel.GetChild(selectedBatIndex).GetComponent<Image>().color = Color.white;

                //Update the gold text
                UpdateGoldText();
            }
            else
            {
                //Not enough gold
                //Play Sound feedback
                Debug.Log("Not enough gold");
            }

        }
    }

    public void OnTrailBuySet()
    {
        Debug.Log("Buy/Set trail");

        //Is selected trail owned.
        if (SaveManager.Instance.IsTrailOwned(selectedTrailIndex))
        {
            //Set the trail
            SetTrail(selectedTrailIndex);
        }
        else
        {
            //Attempt to buy the trail
            if (SaveManager.Instance.BuyTrail(selectedTrailIndex, trailCost[selectedTrailIndex]))
            {
                // Success
                SetTrail(selectedTrailIndex);

                //Change color of the button
                trailPanel.GetChild(selectedTrailIndex).GetComponent<RawImage>().color = Color.white;

                //Update the gold text
                UpdateGoldText();
            }
            else
            {
                //Not enough gold
                //Play Sound feedback
                Debug.Log("Not enough gold");
            }

        }
    }

}
