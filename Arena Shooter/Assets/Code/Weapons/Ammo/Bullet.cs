using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public Vector2 direction;
    RangedWeaponsScriptable firedFrom;
    [SerializeField] Rigidbody2D rb;
    Vector2 pos;

    int permeated;

    public void InitializeProperties(RangedWeaponsScriptable weaponUsed, Vector2 _direction, string _tag, Color color){
        firedFrom = weaponUsed;
        speed = firedFrom.travelSpeed;
        direction = _direction;
        transform.tag = _tag;
        pos = transform.position;
        GetComponent<SpriteRenderer>().color = color;
    }

    void Update()
    {
        rb.velocity = direction.normalized * speed;
        if(Vector2.Distance(pos, transform.position)>= firedFrom.range)
            DestroyBullet();
    }

    public float GetDamage(){
        return firedFrom.damage;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.GetComponent<Rigidbody2D>() != null && !(other.CompareTag("Player") || other.CompareTag(transform.tag))){
            permeated++;
            other.GetComponent<IInteractions>().OnBulletCollison(transform, firedFrom);
            if(permeated >= firedFrom.permeation)
                DestroyBullet();
        }
    }

    void DestroyBullet(){
        if(firedFrom.AOE>0.4f){
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position,firedFrom.AOE);
            foreach(Collider2D col in colliders){
                if(col.CompareTag("Hostile"))
                    col.GetComponent<IInteractions>().OnBulletCollison(transform, firedFrom);
            }
        }
        Destroy(gameObject);
    }

}
