using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : MonoBehaviour
{
    private bool isThrowing;
    private GameObject player;
    private GameObject boomerang;
    private WeaponHandler weaponHandler;
    //public GameObject boomerangHolder;
    Transform boomerangRotate;
    Vector3 locationInFrontOfPlayer;
    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController = gameObject.GetComponent<PlayerController>();
        isThrowing = false;
        player = GameObject.Find("Test_Player");
        boomerang = GameObject.Find("Boomerang");
        //boomerangHolder = GameObject.Find("BoomerangTest(Clone)");
        
        weaponHandler = gameObject.GetComponent<WeaponHandler>();
        //boomerang.GetComponent<MeshRenderer>().enabled = false;
        //boomerangRotate = gameObject.transform.GetChild(0);
        locationInFrontOfPlayer = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z) + player.transform.forward * 10f;

        //StartCoroutine(BoomerangThrow());
    }

    public IEnumerator BoomerangThrow()
    {
        locationInFrontOfPlayer = new Vector3(player.transform.position.x, player.transform.position.y + 1, player.transform.position.z) + player.transform.forward * 10f;
        isThrowing = true;
        yield return new WaitForSeconds(1.5f);
        isThrowing = false;
        
    }

    // Update is called once per frame
    void Update()
    {       
            //StartCoroutine(BoomerangThrow());
        
            

            if (isThrowing)
        {
            transform.position = Vector3.MoveTowards(transform.position, locationInFrontOfPlayer, Time.deltaTime * 40);
            //boomerangRotate.transform.Rotate(0, Time.deltaTime * 500, 0);
        }
            if (!isThrowing)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z), Time.deltaTime * 40);
        }

            if (!isThrowing && Vector3.Distance(player.transform.position, transform.position) < 1.5)
        {
            //boomerang.GetComponent<MeshRenderer>().enabled = true;
            //weaponHandler.currentWeaponPrefab.transform.localPosition = weaponHandler.currentWeapon.weaponOffset;
            //weaponHandler.currentWeaponPrefab.transform.Rotate(-45.0f, 90.0f, 0.0f);
            //Destroy(this.gameObject);
            InputBoom();
        }

    }
    private void InputBoom()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            StartCoroutine(BoomerangThrow());
        }
    }
}
