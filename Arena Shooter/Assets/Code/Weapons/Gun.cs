using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class Gun : MonoBehaviour
{

    GameObject bullet;
    [SerializeField] Transform[] barrels;
    [Range(1,10f)]
    [SerializeField] float shakeMagnitude = 1.2f;


    float time;

    public RangedWeaponsScriptable currentWeapon;
    WeaponManager weaponManager;
    public AudioSource audioSource;

    string bulletTag;

    Color color;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0.5f;
        
    }

    private void Update() {
        time += Time.deltaTime;
        if(Input.GetKeyDown(KeyCode.E))
            Shoot();
    }

    public void SetAttributes(RangedWeaponsScriptable weaponsScriptable){

        currentWeapon = weaponsScriptable;
        audioSource.clip = currentWeapon.audioClip;
    } 

    public void Shoot(){
        if(time>=currentWeapon.fireRate){
            // SetSpread();
            GameObject bullet;
            foreach(Transform barrel in barrels){
                bullet = Instantiate(currentWeapon.bullet, transform.position, barrel.rotation);
                bullet.GetComponent<Bullet>().InitializeProperties(currentWeapon,BulletDirection(barrel), bulletTag, color);
                bullet.transform.parent = BulletManager.Instance.transform;

            }
            audioSource.Play();
            ShakeCamera();
            time = 0;
        }

    }

    Vector2 BulletDirection(Transform barrel){
        float error = Random.Range(0f,1);
        print(error);
        Vector2 spreadDirection = barrel.right;
        if(error>currentWeapon.accuracy){
            color = Color.red;
            float spreadAmmount = Random.Range(-currentWeapon.spread,currentWeapon.spread);
            spreadDirection += new Vector2(spreadDirection.x+spreadAmmount, spreadDirection.y+spreadAmmount);
        }
        return spreadDirection;
    }

    public void SetTag(string tg){
        bulletTag = tg;
    }

    void ShakeCamera(){
        CameraShaker.Instance.ShakeOnce((currentWeapon.recoil+1)*shakeMagnitude, 2f, 0f, 0.1f);
    }

}
