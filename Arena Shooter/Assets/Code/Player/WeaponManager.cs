using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] RangedWeaponsScriptable startingWeapon;
    public RangedWeaponsScriptable Weapon2;
    [HideInInspector]public RangedWeaponsScriptable currentweapon;
    public GameObject currentWeaponBody;
    GameObject weaponHolster;
    Gun gun;
    PlayerMovement ply;

    private void Awake() {
        FindWeaponHolster();
        PlaceWeapon(startingWeapon);
        ply = GetComponent<PlayerMovement>();
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.R)){
            ChooseWeapon();
        }
    }

    public void StartShooting(){
        ply.PushBack(currentweapon.recoil);
        gun.Shoot();
    }

    public void FindWeaponHolster(){
        for(int x=0;x<transform.childCount;x++){
            if(transform.GetChild(x).name == "Weapon"){
                weaponHolster = transform.GetChild(x).gameObject;
                return;
            }
        }
        Debug.LogError("Weapon Holster not found (Checking for name Weapon)");
    }
    

    void PlaceWeapon(RangedWeaponsScriptable weapon){
        RemoveCurrentWeapon();
        currentweapon = weapon;
        GameObject gO = Instantiate(weapon.gunBody,transform.position,Quaternion.identity);
        gO.transform.rotation = transform.rotation;
        gO.transform.parent = weaponHolster.transform;
        currentWeaponBody = gO;
        gun = currentWeaponBody.GetComponent<Gun>();
        currentWeaponBody.GetComponent<Gun>().SetAttributes(weapon);
        currentWeaponBody.GetComponent<Gun>().SetTag("Friendly");
    }

    void RemoveCurrentWeapon(){
        Destroy(currentWeaponBody);
    }

    void ChooseWeapon(){
        RangedWeaponsScriptable temp = Weapon2;
        Weapon2 = startingWeapon;
        PlaceWeapon(temp);
    }

    public RangedWeaponsScriptable GetCurrentWeapon(){
        return currentweapon;
    }

}
