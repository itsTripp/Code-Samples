using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{

    GameManager manager;
    [SerializeField] GameObject[] Enemies;
    [SerializeField] float XDisplacement = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Mathf.Sin(Time.time) * XDisplacement, transform.position.y, transform.position.z);

        if(manager.GetNumEnemies() < manager.GetMaxNumEnemiesAliveAtOnce())
        {
            int Rand = Random.Range(0, Enemies.Length);

            Instantiate(Enemies[Rand], transform.position, transform.rotation);
        }
    }
}
