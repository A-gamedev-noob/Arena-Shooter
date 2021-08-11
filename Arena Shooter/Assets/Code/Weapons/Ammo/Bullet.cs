using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float speed;
    Vector2 direction;
    RangedWeaponsScriptable firedFrom;
    [SerializeField] Rigidbody2D rb;

    public void InitializeProperties(RangedWeaponsScriptable weaponUsed, Vector2 _direction, string _tag){
        firedFrom = weaponUsed;
        speed = firedFrom.travelSpeed;
        direction = _direction;
        transform.tag = _tag;
    }

    void Update()
    {
        rb.velocity = direction * speed;
    }

    public float GetDamage(){
        return firedFrom.damage;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.GetComponent<Rigidbody2D>() != null){
            other.GetComponent<IInteractions>().OnBulletCollison(transform, firedFrom);
        }
    }

}
