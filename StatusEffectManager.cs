using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffectManager : MonoBehaviour
{
    private Enemy enemyObject;
    private Color baseColor;

    [Header("Regular Ball")]
    [SerializeField] private float colorChangeTimer;
    [SerializeField] private int regularDamage;

    [Header("Fire Ball")]
    [SerializeField] private int fireDamageToEnemy;
    public float damageTimer;
    public List<int> burnTickTimer = new List<int>();
    private bool withChildren = true;
    public ParticleSystem fireSystem;

    [Header("Ice Ball")]
    [SerializeField] private float freezeTimer;
    [SerializeField] private int iceDamage;
    public ParticleSystem iceSystem;

    [Header("Reverse Time Ball")]
    [SerializeField] private float reverseTimer;
    [SerializeField] private int timeDamage;


    // Start is called before the first frame update
    void Start()
    {
        enemyObject = GetComponent<Enemy>();
        baseColor = gameObject.GetComponent<MeshRenderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Fire Effects and Damage
    public void ApplyBurn(int ticks)
    {
        if(burnTickTimer.Count <= 0)
        {
            burnTickTimer.Add(ticks);
            StartCoroutine(Burn());
        }
        else
        {
            burnTickTimer.Add(ticks);
        }
    }

    IEnumerator Burn()
    {
        enemyObject.GetComponent<MeshRenderer>().material.color = Color.red;
        while(burnTickTimer.Count > 0)
        {
            for(int i = 0; i < burnTickTimer.Count; i++)
            {
                burnTickTimer[i]--;
            }
            enemyObject.health -= fireDamageToEnemy;

            ParticleSystem newFireSystem = Instantiate(fireSystem, enemyObject.transform.position, Quaternion.identity);
            newFireSystem.transform.parent = gameObject.transform;
            burnTickTimer.RemoveAll(tickCount => tickCount == 0);

            yield return new WaitForSeconds(damageTimer);
            enemyObject.GetComponent<MeshRenderer>().material.color = baseColor;
            newFireSystem.Stop(withChildren, ParticleSystemStopBehavior.StopEmitting);
        }
    }
    #endregion

    #region Ice Effects and Damage
    public void FreezeEnemy()
    {
        StartCoroutine(Freeze());
        enemyObject.health -= iceDamage;
    }

    IEnumerator Freeze()
    {
        ParticleSystem newIceSystem = Instantiate(iceSystem, enemyObject.transform.position, Quaternion.identity);
        newIceSystem.transform.parent = gameObject.transform;
        enemyObject.GetComponent<MeshRenderer>().material.color = Color.cyan;
        enemyObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

        yield return new WaitForSeconds(freezeTimer);
        enemyObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        enemyObject.GetComponent<MeshRenderer>().material.color = baseColor;
        newIceSystem.Stop(withChildren, ParticleSystemStopBehavior.StopEmitting);
    }
    #endregion

    #region Base Effects and Damage
    public void ChangeColor()
    {
        StartCoroutine(ColorChange());
        enemyObject.health -= regularDamage;
    }

    IEnumerator ColorChange()
    {
        enemyObject.GetComponent<MeshRenderer>().material.color = Color.red;

        yield return new WaitForSeconds(colorChangeTimer);
        enemyObject.GetComponent<MeshRenderer>().material.color = baseColor;
    }
    #endregion

    #region Time Effects and Damage
    public void ReverseTime()
    {
        StartCoroutine(TimeReverse());
        enemyObject.health -= timeDamage;
    }

    IEnumerator TimeReverse()
    {
        if(enemyObject.GetComponent<EnemyMovement>().movingTowards == true)
        {
            enemyObject.GetComponent<EnemyMovement>().movingTowards = false;
            enemyObject.GetComponent<MeshRenderer>().material.color = Color.yellow;
        }
        

        yield return new WaitForSeconds(reverseTimer);
        if(enemyObject.GetComponent<EnemyMovement>().movingTowards == false)
        {
            enemyObject.GetComponent<EnemyMovement>().movingTowards = true;
            enemyObject.GetComponent<MeshRenderer>().material.color = baseColor;
        }
    }
    #endregion
}

