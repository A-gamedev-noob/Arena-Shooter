using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class Gun : MonoBehaviour
{
    string gunName;
    float damage;
    float range;
    float fireRate;
    public float recoil;
    float spread;
    float AOE;
    GameObject bullet;
    [SerializeField] Transform[] barrels;
    [Range(1,10f)]
    [SerializeField] float shakeMagnitude = 1.2f;


    float time;

    RangedWeaponsScriptable currentWeapon;
    WeaponManager weaponManager;
    public AudioSource audioSource;

    string bulletTag;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0.5f;
    }

    private void Update() {
        time += Time.deltaTime;
    }

    public void SetAttributes(RangedWeaponsScriptable weaponsScriptable){

        currentWeapon = weaponsScriptable;

        gunName = weaponsScriptable.gunName;
        damage = weaponsScriptable.damage;
        range = weaponsScriptable.range;
        fireRate = weaponsScriptable.fireRate;
        recoil = weaponsScriptable.recoil;
        spread = weaponsScriptable.spread;
        AOE = weaponsScriptable.AOE;
        bullet = weaponsScriptable.bullet;
        audioSource.clip = weaponsScriptable.audioClip;
    } 

    public void Shoot(){
        if(time>=fireRate){
            GameObject bullet;
            foreach(Transform barrel in barrels){
                bullet = Instantiate(currentWeapon.bullet, barrels[0].position, barrel.rotation);
                bullet.GetComponent<Bullet>().InitializeProperties(currentWeapon,barrel.right, bulletTag);
                bullet.transform.parent = BulletManager.Instance.transform;

            }
            audioSource.Play();
            ShakeCamera();
            time = 0;
        }

    }

    public void SetTag(string tg){
        bulletTag = tg;
    }

    void ShakeCamera(){
        CameraShaker.Instance.ShakeOnce((recoil+1)*shakeMagnitude, 2f, 0f, 0.1f);
    }

}
