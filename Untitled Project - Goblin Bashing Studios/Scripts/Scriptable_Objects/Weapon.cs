using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapons")]
public class Weapon : ScriptableObject
{    
    public string weaponName;
    public string weaponSlot;
    public GameObject weaponPrefab;
    public Vector3 weaponOffset;
    public Quaternion weaponRotation;
    
    [Header("Stats")]
    public int minimumDamage;
    public int maximumDamage;
    public float maximumRange;
}
