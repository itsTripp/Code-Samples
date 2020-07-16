using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody enemyRigidbody;

    [SerializeField] private float movementSpeed;

    [SerializeField] private GameObject flipper;
    [SerializeField] private GameObject navPoint;

    private Enemy enemy;
    [SerializeField] int damageToPlayer;

    public bool movingTowards;
    

    // Start is called before the first frame update
    void Start()
    {
        flipper = GameObject.FindGameObjectWithTag("Flipper");
        navPoint = GameObject.FindGameObjectWithTag("NavPoint");
        enemyRigidbody = GetComponent<Rigidbody>();
        enemy = GetComponent<Enemy>();
        movingTowards = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        MoveInDirection();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Flipper")
        {
            flipper.GetComponent<Flipper>().TakeDamage(damageToPlayer);
            enemy.checkHit();
            enemy.Die();
        }
    }

    public void MoveInDirection()
    {
        if (movingTowards == true)
        {
            MoveEnemy();
        }
        if (movingTowards == false)
        {
            ReverseEnemy();
        }
    }

    public void MoveEnemy()
    {
       movingTowards = true;
        transform.LookAt(navPoint.transform.position);
        enemyRigidbody.velocity = (transform.forward * movementSpeed);
    }

    public void ReverseEnemy()
    {
        movingTowards = false;
        transform.LookAt(navPoint.transform.position);
        enemyRigidbody.velocity = (-transform.forward * movementSpeed);
    }

    
}
