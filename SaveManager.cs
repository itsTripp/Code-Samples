using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { set; get; }
    public SaveState state;

    private void Awake()
    {
        ResetSave();
        DontDestroyOnLoad(gameObject);
        Instance = this;
        Load();
    }

    //Save to the player pref
    public void Save()
    {
        PlayerPrefs.SetString("save", Helper.Serialize<SaveState>(state));
    }

    //Load the previous save from player pref
    public void Load()
    {
        if(PlayerPrefs.HasKey("save"))
        {
            state = Helper.Deserialize<SaveState>(PlayerPrefs.GetString("save"));
        }
        else
        {
            state = new SaveState();
            Save();
            Debug.Log("No save file found, creating a new one.");
        }
    }

    //Check if the bat was bought
    public bool IsBatOwned(int index)
    {
        //Check if the bat is set, if so the bat is owned.
        return (state.batOwned & (1 << index)) != 0;
    }

    //Check if the trail was bought
    public bool IsTrailOwned(int index)
    {
        //Check if the bit is set, if so the trail is owned.
        return (state.trailOwned & (1 << index)) != 0;
    }

    //Attempt buying a bat, return true/false
    public bool BuyBat(int index, int cost)
    {
        if(state.gold >= cost)
        {
            //Enough money, remove from the current gold stack
            state.gold -= cost;
            UnlockBat(index);

            //Save progress
            Save();
            return true;
        }
        else
        {
            //Not enough money, return false
            return false;
        }
    }

    //Attempt buying a trail, return true/false
    public bool BuyTrail(int index, int cost)
    {
        if (state.gold >= cost)
        {
            //Enough money, remove from the current gold stack
            state.gold -= cost;
            UnlockTrail(index);

            //Save progress
            Save();
            return true;
        }
        else
        {
            //Not enough money, return false
            return false;
        }
    }

    //Unlock a bat in the "batOwned" int
    public void UnlockBat(int index)
    {
        //Toggle on the bit at index
        state.batOwned |= 1 << index;
    }

    //Unlock a trail in the "trailOwned" int
    public void UnlockTrail(int index)
    {
        //Toggle on the bit at index
        state.trailOwned |= 1 << index;
    }

    //Complete Level
    public void CompleteLevel(int index)
    {
        //If this is the current active level
        if(state.completedLevel == index)
        {
            state.completedLevel++;
            Save();
        }
    }

    //Resets the whole save file
    public void ResetSave()
    {
        PlayerPrefs.DeleteKey("save");
    }
}
