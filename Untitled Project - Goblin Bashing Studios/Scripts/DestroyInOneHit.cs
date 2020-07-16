using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyInOneHit : MonoBehaviour, IDamagable
{
    //Destroys object in one hit.
    public void DealDamage(int damage)
    {
        Destroy(gameObject);
    }
}
