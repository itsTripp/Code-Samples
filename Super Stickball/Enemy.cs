using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _currentHealth;
    public int health
    {
        get
        {
            return _currentHealth;
        }
        set
        {
            if (value > 0)
                _currentHealth = value;
            else
                Die();
        }
    }
    [SerializeField] private int maxHealth;
    [SerializeField] private int minFireTickValue;
    [SerializeField] private int maxFireTickVale;
    private int randomFireTickValue;

    private AudioSource audioSource;
    public AudioClip death;

    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        health = maxHealth;
    }
    
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.tag == "Ball")
        {
            gameObject.GetComponent<StatusEffectManager>().ChangeColor();
            checkHit();
        }

        if (collision.gameObject.tag == "Fireball")
        {
                if (gameObject.GetComponent<StatusEffectManager>() != null)
                {
                    randomFireTickValue = Random.Range(minFireTickValue, maxFireTickVale);
                    gameObject.GetComponent<StatusEffectManager>().ApplyBurn(randomFireTickValue);
                }
            checkHit();
        }

        if (collision.gameObject.tag == "Iceball")
        {
            gameObject.GetComponent<StatusEffectManager>().FreezeEnemy();
            checkHit();
        }

        if (collision.gameObject.tag == "ReverseTimeBall")
        {
            gameObject.GetComponent<StatusEffectManager>().ReverseTime();
            checkHit();
        }
    }

    public void checkHit()
    {
        audioSource.Play();
        
        if (health <= 0)
        {
            health = 0;
            Die();
        }
    }

    public void Die()
    {
        AudioSource.PlayClipAtPoint(death, transform.position);
        Destroy(gameObject);
    }
}
