using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] int MaxEnemiesAliveAtOnce = 20;
    int EnemiesAlive = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncrementNumEnemies()
    {
        EnemiesAlive++;
    }

    public void DecrementNumEnemies()
    {
        EnemiesAlive--;
    }

    public int GetNumEnemies()
    {
        return EnemiesAlive;
    }

    public int GetMaxNumEnemiesAliveAtOnce()
    {
        return MaxEnemiesAliveAtOnce;
    }

}
