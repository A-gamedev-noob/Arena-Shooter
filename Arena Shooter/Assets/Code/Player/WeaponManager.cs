using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] RangedWeaponsScriptable startingWeapon;
    RangedWeaponsScriptable currentweapon;
    GameObject currentWeaponBody;
    GameObject weaponHolster;
    Gun gun;

    private void Awake() {
        FindWeaponHolster();
        PlaceWeapon(startingWeapon);
        gun = currentWeaponBody.GetComponent<Gun>();
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Space)){
            PlaceWeapon(startingWeapon);
        }
        if(Input.GetKeyDown(KeyCode.Q)){
            RemoveCurrentWeapon();
        }
    }

    public void StartShooting(){
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
        Quaternion angle = Quaternion.Euler(transform.right);
        GameObject gO = Instantiate(weapon.gunBody,transform.position,Quaternion.identity);
        gO.transform.rotation = transform.rotation;
        gO.transform.parent = weaponHolster.transform;
        currentWeaponBody = gO;
        currentWeaponBody.GetComponent<Gun>().SetAttributes(weapon);
        currentWeaponBody.GetComponent<Gun>().SetTag("Friendly");
    }

    void RemoveCurrentWeapon(){for (int x = 0; x < transform.childCount; x++){
            if (transform.GetChild(x).name == "Weapon")
            { 
                Destroy(transform.GetChild(x).transform.GetChild(0).gameObject);
                return;
            }
        }
    }

    public RangedWeaponsScriptable GetCurrentWeapon(){
        return currentweapon;
    }

}
