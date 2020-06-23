using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flipper : MonoBehaviour
{
    [SerializeField] private float restPosition = 0f;
    [SerializeField] private float pressedPosition = 45f;
    [SerializeField] private float hitStrength = 10000f;
    [SerializeField] private float flipperDamper = 150;
    [SerializeField] private float holdTime = 0;
    [SerializeField] private KeyCode flipperInputKey;
    public StrenghtMeter strengthMeter;
    HingeJoint hinge;
    public bool isPressed = false;
    public bool quickSwing;
    public bool showMeter;

    [Header("Health Settings")]
    [SerializeField] private Slider HealthBar;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentHealth;

    //Kat Added 4/2/2020
    public Animator anim;

    //Going to use this bool for the added animation stuff, if it's not checked, the animation variables should not cause any errors
    public bool usingAnim;
    //End Add

    private void Start()
    {
        hinge = GetComponent<HingeJoint>();
        hinge.useSpring = true;
        currentHealth = maxHealth;
        showMeter = true;


        //Kat Addeed 4/2/2020 - Should prevent any error messages if we decide not to use animation/testing without animation
        if(usingAnim == true)
        {
            anim = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>();
        }
        else
        {
            anim = null;
        }
        //End Add
    }

    private void Update()
    {
        JointSpring spring = new JointSpring();
        spring.spring = hitStrength;
        spring.damper = flipperDamper;

        //Kat Added 4/2/2020 - Sets the animation bool to the flipper bool, only if we are using anim
        if(usingAnim == true)
        {
            anim.SetBool("Hit", isPressed);
        }
        //End Add

        if (Input.GetKey(KeyCode.S))
        {
            quickSwing = true;
            hitStrength = 10000;
            isPressed = true;
            spring.targetPosition = pressedPosition;
            //Debug.Log("quick swing");
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            isPressed = false;
            spring.targetPosition = restPosition;
        }

        if((Input.GetKey(flipperInputKey) || (Input.touchCount > 0) ))
        {
            
                quickSwing = false;
                holdTime += Time.deltaTime;
            //if player holds swing for longer than 0.33 seconds, display the swing power meter
            if (holdTime > 0.13f)
            {
                //power meter appears
                //power meter increases gradually
                //hitStrenght increases gradually
                if (hitStrength < 40000)
                {
                    hitStrength += 10000 * Time.deltaTime;
                }
                if (showMeter)
                {
                    strengthMeter.IncreaseStrengthMeter();
                }

            }
            
            //when the player presses the "Swing" key,
            //if it is a quick tap, just swing bat at 10000 hitStrength
            //if the player holds for more than a 1/3 of second, show the power charge up meter and increase hitStrength
            //we need a quick swing animation
            //isPressed = true;
                      
        }

        if (Input.GetKeyUp(flipperInputKey))
        {
            isPressed = true;
            showMeter = false;
            strengthMeter.HideStrengthMeter();
            //if player releases the key quickly enough, just do a quick swing at 10,000 hit strength
            if (holdTime <= 0.13f)
            {
                hitStrength = 10000;
                holdTime = 0.33f;
            }
            if (holdTime > 0.13f)
            {
                holdTime = 0.33f;
            }
            
            
        }
        if(isPressed == true && quickSwing == false)
        {
            spring.targetPosition = pressedPosition;  
            holdTime -= Time.deltaTime;
            if(holdTime <= 0)
            {
                //Debug.Log("back to rest");
                isPressed = false;
                spring.targetPosition = restPosition;
                hitStrength = 10000;
                showMeter = true;
                
            }
        }

        /*else
        {   isPressed = false;
            spring.targetPosition = restPosition;            
        }*/
        
        hinge.spring = spring;
        hinge.useLimits = true;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        HealthBar.value = currentHealth;
        if(currentHealth <= 0)
        {
            currentHealth = 0;
            FindObjectOfType<GameManager>().EndGame();
        }
    }
}
