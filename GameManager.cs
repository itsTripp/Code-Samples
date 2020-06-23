using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    [Header("Game Over Display")]
    public Text gameOverDisplay;

    [Header("Timmy and His Bat")]
    public Renderer TimmysBat;
    public List<Material> bats = new List<Material>();
    private GameObject currentTrail;
    private Transform currentBall;

    public List<GameObject> trails = new List<GameObject>();

    [Header("Buttons")]
    public Button pauseButton;
    public Button flipperSwitchButton;
    public bool canSpawnBall = true;
    private int whichBallIsSelected = 1;

    [Header("Regular Ball")]
    public GameObject regularBall;
    public Toggle regularBallToggle;
    public bool regularBallOn;

    [Header("Fire Ball")]
    public GameObject fireBall;
    public Toggle fireBallToggle;
    public bool fireBallOn;

    [Header("Ice Ball")]
    public GameObject iceBall;
    public Toggle iceBallToggle;
    public bool iceBallOn;

    [Header("Reverse Time Ball")]
    public GameObject reverseTimeBall;
    public Toggle timeBallToggle;
    public bool reverseTimeBallOn;

    [Header("Lightning Ball")]
    public GameObject lightningBall;
    public Toggle lightningToggle;
    public bool lightningBallOn;

    [Header("Starting Ball Position")]
    public Transform ballPosition;
    public Vector3 startingPosition;

    [Header("Ball Respawn")]
    public float respawnTimer;
    public int currentNumberOfBalls;
    

    private void Start()
    {
       startingPosition = ballPosition.position;
       canSpawnBall = true;
       SetBallMesh(SaveManager.Instance.state.activeBat, SaveManager.Instance.state.activeTrail);
    }

    private void Update()
    {
        
    }
    private void FixedUpdate()
    {
        
    }

    public void SetBallMesh(int batIndex, int trailIndex)
    {
        TimmysBat.material = bats[batIndex];
    }
     public void SetTrailMesh(Transform ballTransform)
    {
        Instantiate(trails[SaveManager.Instance.state.activeTrail], ballTransform);

    }
    // These Functions Control Which Ball Will Be Instantiated.
    public void RegularBall()
    {
        if (currentNumberOfBalls < 1 && canSpawnBall == true)
        {
            currentNumberOfBalls++;
            GameObject newBall = Instantiate(regularBall, startingPosition, Quaternion.identity);
            SetTrailMesh(newBall.transform);
            //currentBall = regularBall;
        }
        regularBallOn = true;
        fireBallOn = false;
        iceBallOn = false;
        reverseTimeBallOn = false;
        lightningBallOn = false;
    }

    public void FireBall()
    {
        if (currentNumberOfBalls < 1 && canSpawnBall == true)
        {
            currentNumberOfBalls++;
            GameObject newBall = Instantiate(fireBall, startingPosition, Quaternion.identity);
            SetTrailMesh(newBall.transform);
        }
        regularBallOn = false;
        fireBallOn = true;
        iceBallOn = false;
        reverseTimeBallOn = false;
        lightningBallOn = false;
    }

    public void IceBall()
    {
        if (currentNumberOfBalls < 1 && canSpawnBall == true)
        {
            currentNumberOfBalls++;
            GameObject newBall = Instantiate(iceBall, startingPosition, Quaternion.identity);
            SetTrailMesh(newBall.transform);
        }
        regularBallOn = false;
        fireBallOn = false;
        iceBallOn = true;
        reverseTimeBallOn = false;
        lightningBallOn = false;
    }

    public void TimeReversalBall()
    {
        if (currentNumberOfBalls < 1 && canSpawnBall == true)
        {
            currentNumberOfBalls++;
            GameObject newBall = Instantiate(reverseTimeBall, startingPosition, Quaternion.identity);
            SetTrailMesh(newBall.transform);
        }
        regularBallOn = false;
        fireBallOn = false;
        iceBallOn = false;
        reverseTimeBallOn = true;
        lightningBallOn = false;
    }

    public void LightingBall()
    {
        if (currentNumberOfBalls < 1 && canSpawnBall == true)
        {
            currentNumberOfBalls++;
            GameObject newBall = Instantiate(lightningBall, startingPosition, Quaternion.identity);
            SetTrailMesh(newBall.transform);
        }
        regularBallOn = false;
        fireBallOn = false;
        iceBallOn = false;
        reverseTimeBallOn = false;
        lightningBallOn = true;
    }

    // Checks Which Toggle Button Is On for A Ball To Spawn.
    public void BallCheck()
    {
        if(regularBallOn == true)
        {
            RegularBall();
        }

        else if(fireBallOn == true)
        {
            FireBall();
        }

        else if(iceBallOn == true)
        {
            IceBall();
        }

        else if(reverseTimeBallOn == true)
        {
            TimeReversalBall();
        }

        else if (lightningBallOn == true)
        {
            LightingBall();
        }

        return;
    }

    public void EndGame()
    {
        //Enables The Game Over Display
        gameOverDisplay.gameObject.SetActive(true);

        //Disable Pause Button and Flippper Button
        pauseButton.gameObject.SetActive(false);
        flipperSwitchButton.gameObject.SetActive(false);

        // Pauses Game
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        gameOverDisplay.gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }
}
