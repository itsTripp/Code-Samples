using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] private List<Weapon> weapons = new List<Weapon>();
    
    
    public GameObject currentWeaponPrefab;
    public Weapon currentWeapon;
    public GameObject boomerangHolder;
    private Transform cameraTransform;    
    public PlayerController playerController;

    private void Start()
    {
        playerController = GetComponentInParent<PlayerController>();
        cameraTransform = Camera.main.transform;
        currentWeaponPrefab = Instantiate(weapons[0].weaponPrefab, transform);
        currentWeapon = weapons[0];
        currentWeaponPrefab.transform.localPosition = currentWeapon.weaponOffset;
        currentWeaponPrefab.transform.Rotate(0.0f, -90.0f, 0.0f);
    }

    private void Update()
    {
        CheckForShooting();
        if(Input.GetKeyDown(playerController.weaponSwitch1Key))
        {
            Destroy(currentWeaponPrefab);
            currentWeaponPrefab = Instantiate(weapons[0].weaponPrefab, transform);
            currentWeapon = weapons[0];
            currentWeaponPrefab.transform.localPosition = currentWeapon.weaponOffset;
            currentWeaponPrefab.transform.Rotate(0.0f, -90.0f, 0.0f);
        }
        else if(Input.GetKeyDown(playerController.weaponSwitch2Key))
        {
            Destroy(currentWeaponPrefab);
            currentWeaponPrefab = Instantiate(weapons[1].weaponPrefab, transform);
            currentWeapon = weapons[1];
            currentWeaponPrefab.transform.localPosition = currentWeapon.weaponOffset;
            currentWeaponPrefab.transform.Rotate(0.0f, -90.0f, 0.0f);
        }
        else if(Input.GetKeyDown(playerController.meleeAttackKey)) //Needs Animation when input pressed, then goes back to previously equipped weapon.
        {
            Destroy(currentWeaponPrefab);
            currentWeaponPrefab = Instantiate(weapons[2].weaponPrefab, transform);
            currentWeapon = weapons[2];
            currentWeaponPrefab.transform.localPosition = currentWeapon.weaponOffset;
        }
        else if(Input.GetKeyDown(playerController.boomerangKey))
        {
            Destroy(currentWeaponPrefab);
            currentWeaponPrefab = Instantiate(weapons[3].weaponPrefab, transform);
            currentWeapon = weapons[3];
            currentWeaponPrefab.transform.localPosition = currentWeapon.weaponOffset;
            currentWeaponPrefab.transform.Rotate(-45.0f, 90.0f, 0.0f);
        }

    }

    private void CheckForShooting()
    {
        if (Input.GetKeyDown(playerController.primaryFireKey))
        {
            RaycastHit whatIsHit;
            //Sends a raycast in the forward direction forever, until it hits an object.
            if (Physics.Raycast(cameraTransform.position, transform.forward, out whatIsHit, Mathf.Infinity))
            {
                //The raycast checks if the object has the IDamagable interface applied to it.
                IDamagable damagable = whatIsHit.collider.GetComponent<IDamagable>();
                //If the object does have the IDamageable interface, then it will deal damage. If it does not, nothing will happen.
                if (damagable != null)
                {
                    damagable.DealDamage(currentWeapon.maximumDamage);
                }
                //Print name of object that raycast has hit.
                Debug.Log(whatIsHit.collider.name);
            }
        }
        
    }
}
