using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Base : MonoBehaviour
{

    [SerializeField] GameObject ForwardObj;
    [SerializeField] float MoveSpeed;
    [SerializeField] Vector3 TargetOffset = Vector3.zero;
    [SerializeField] float AttackRadius = 5.0f;

    Collider collider;
    GameObject player;

    Player playerScript;
    GameManager manager;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<Player>();
        manager = GameObject.FindObjectOfType<GameManager>();
        manager.IncrementNumEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * MoveSpeed * Time.deltaTime);
        transform.LookAt(player.transform.position + TargetOffset);

        if(Vector3.Distance(transform.position, player.transform.position) <= AttackRadius)
        {
            playerScript.TakeDamage();
            manager.DecrementNumEnemies();
            Destroy(gameObject);
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, AttackRadius);
    }

    public void DestroyThis()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {

            Destroy(gameObject);
            manager.DecrementNumEnemies();
        }
    }

}
