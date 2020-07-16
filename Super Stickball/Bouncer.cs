using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncer : MonoBehaviour
{
    public float bounceStrength = 100f;
    public float bounceUpwards = 0.5f;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
       if(collision.gameObject.tag == "Ball" || collision.gameObject.tag == "Fireball" || collision.gameObject.tag == "Iceball" || collision.gameObject.tag == "ReverseTimeBall")
        {
            collision.rigidbody.AddExplosionForce(bounceStrength, this.transform.position, 5);
            PlayAudio();
        }
       if(collision.gameObject.tag == "PlayerBody")
        {
            collision.rigidbody.AddExplosionForce(bounceStrength, this.transform.position, 5, bounceUpwards);
            PlayAudio();
        }
        
    }

    private void PlayAudio()
    {
        audioSource.Play();
    }
}
