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

        if(lookJoystick.Direction.magnitude >= shootSensetivity)
            weaponManager.StartShooting();
        
        transform.right = lookDirection;
    }

    void Movement(){   
        rb.velocity = direction * speed;
    }


}
