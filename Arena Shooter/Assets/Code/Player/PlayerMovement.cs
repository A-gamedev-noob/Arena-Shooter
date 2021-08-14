using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Header("Essential GameObjects")]
    [SerializeField] Joystick moveJoystick;
    [SerializeField] Joystick lookJoystick;
    [SerializeField]Camera cam;
    WeaponManager weaponManager;

    [Header("Variables")]
    [SerializeField] float speed = 5f;
    [SerializeField] float moveSensetivity = 0.4f;
    [SerializeField] float shootSensetivity = 0.5f;

    [Header("Private vARiables")]
    Vector2 movement;
    Rigidbody2D rb;
    Vector2 direction;
    Vector2 lookDirection;
    bool dynamic = true;

    [Header("Debug")]
    public Text debugText;
   
    void Start()
    {
        if(rb == null)
            rb = gameObject.GetComponent<Rigidbody2D>();

        weaponManager = GetComponent<WeaponManager>();
        
        lookDirection = Vector2.up;
        
    }

    // Update is called once per frame
    void Update()
    {
        Aim();
        TakeInput();
        debugText.text = lookJoystick.Direction.ToString();
    }

    void TakeInput(){
        
        direction = moveJoystick.Direction;
        if(moveJoystick.Direction.magnitude>= moveSensetivity)
            direction = direction.normalized;
            
    }

    private void FixedUpdate() {
        Movement();
    }

    void Aim(){
        if(lookJoystick.Direction != Vector2.zero)
            lookDirection = lookJoystick.Direction;

        if(lookJoystick.Direction.magnitude >= shootSensetivity){
            PushBack(weaponManager.currentweapon.recoil);
            weaponManager.StartShooting();
        }
        
        transform.right = lookDirection;
    }

    public void Movement(){   
        if(direction.magnitude != 0f){
            rb.velocity = direction  * speed;
            dynamic = false;
        }
        else if(direction.magnitude == 0f && !dynamic){
            rb.velocity = direction * 0f;
            dynamic = true;
        }
    }

    public void PushBack(float amount){
        Vector2 forceDir = transform.right * amount * -1;
        rb.velocity = forceDir;
        // StopCoroutine(StopPlayer());
        StartCoroutine(StopPlayer());
    }

    IEnumerator StopPlayer(){
        yield return new WaitForSeconds(0.1f);
        dynamic = false;
    }

}
