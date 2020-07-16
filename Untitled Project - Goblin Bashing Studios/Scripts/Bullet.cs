using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    GameManager manager;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 1.0f);
        manager = GameObject.FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            GameObject temp = collision.gameObject;
            temp.GetComponentInParent<AI_Base>().DestroyThis();
            manager.DecrementNumEnemies();
            Debug.Log("GG");
        }
        Destroy(gameObject);
    }

}
