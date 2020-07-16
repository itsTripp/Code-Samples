using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnTouch : MonoBehaviour
{

    [SerializeField] string ObjectToDestroyTag = "Enemy";
    GameManager manager;

    private void Start()
    {
        manager = GameObject.FindObjectOfType<GameManager>();
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == ObjectToDestroyTag)
        {
            if(ObjectToDestroyTag != "Enemy")
            {
                Destroy(collision.gameObject);
            }
            else
            {
                collision.gameObject.GetComponentInParent<AI_Base>().DestroyThis();
                manager.DecrementNumEnemies();
                Debug.Log("Done");
            }
        }
    }

}
