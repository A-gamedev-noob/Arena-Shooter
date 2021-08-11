using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapons", menuName = "ScriptableObjects/Ranged", order = 1)]
public class RangedWeaponsScriptable : ScriptableObject
{
    public string gunName;
    public float damage;
    public float travelSpeed;
    public float range;
    public float fireRate;
    public float recoil;
    public float spread;
    public float AOE;
    [Header("Prefabs")]
    public GameObject bullet;
    public GameObject gunBody;

    [Header("Sounds")]
    public AudioClip audioClip;
}
