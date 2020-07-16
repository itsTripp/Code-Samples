using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlipperSwitchButton : MonoBehaviour
{
    public GameObject LeftFlipper;
    public GameObject RightFlipper;
    int whichFlipperIsOn = 1;

    private void Start()
    {
        LeftFlipper.gameObject.SetActive(true);
        RightFlipper.gameObject.SetActive(false);
    }

    public void SwitchFlipper()
    {
        switch (whichFlipperIsOn)
        {
            case 1:
                
                whichFlipperIsOn = 2;

                LeftFlipper.gameObject.SetActive(false);
                RightFlipper.gameObject.SetActive(true);
                break;

            case 2:
                
                whichFlipperIsOn = 1;

                LeftFlipper.gameObject.SetActive(true);
                RightFlipper.gameObject.SetActive(false);
                break;
        }
    }
}
