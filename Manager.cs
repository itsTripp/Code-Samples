using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager Instance { set; get; }

    public GameObject[] playerBats = new GameObject[10];
    public GameObject[] ballTrails = new GameObject[10];

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }

    public int curentLevel = 0;  //Used when changing from menu to game scene
    public int menuFocus = 0;   //Used when entering the menu scene, to know which menu to focus.


}
