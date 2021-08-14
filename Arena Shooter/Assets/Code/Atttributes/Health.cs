using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IInteractions
{
    
    [SerializeField]float health = 50f;
    SpriteRenderer spriteRender;
    public string selfTag;
    Color originalColor;

    [SerializeField] GameObject deathVfx;

    void Start()
    {
        spriteRender = GetComponent<SpriteRenderer>();
        originalColor = spriteRender.color;
    }

    // Update is called once per frame
    void Update()
    {
        if(health<=0)
            Death();
    }

    public void HealthDown(float dmg){
        health -= dmg;
        StopCoroutine(FlashEffect(Color.red, 2));
        StartCoroutine(FlashEffect(Color.red,2));
    }

    IEnumerator FlashEffect(Color color, int turns){
        for(int x=1; x<=turns; x++){
            spriteRender.color = color;
            yield return new WaitForSeconds(0.1f);
            spriteRender.color = originalColor;
            yield return new WaitForSeconds(0.1f);
        }
        // spriteRender.color = originalColor;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        HealthDown(15);
    }

    public void Death(){
        GameObject Vfx = Instantiate(deathVfx,transform.position,Quaternion.identity);
        Vfx.GetComponent<ParticleSystem>().Play();
        Destroy(gameObject);
    }

    public void OnBulletCollison(Transform col, RangedWeaponsScriptable weapon){
        if(!col.CompareTag(selfTag)){
            HealthDown(weapon.damage);
        }
    }
}
