using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
    // When A Ball Collides With The Out Of Bounds Trigger, It Will Destroy, Count Down The Amount Of Balls In Scene
    // And Spawn Another Ball After Timer Is Complete.
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ball" || collision.gameObject.tag == "Fireball" || collision.gameObject.tag == "Iceball" || collision.gameObject.tag == "ReverseTimeBall")
        {
            Destroy(collision.gameObject);
            FindObjectOfType<GameManager>().currentNumberOfBalls--;
            collision.rigidbody.velocity = Vector3.zero;
            collision.transform.position = FindObjectOfType<GameManager>().startingPosition;

            StartCoroutine(SpawnBall());
        }
    }

    IEnumerator SpawnBall()
    {
        yield return new WaitForSeconds(FindObjectOfType<GameManager>().respawnTimer);
        FindObjectOfType < GameManager>().BallCheck();
    }
}
