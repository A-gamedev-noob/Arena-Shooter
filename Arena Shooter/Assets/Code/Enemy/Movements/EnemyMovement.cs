using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    Transform player;
    
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        LookTowards(player.position);
    }

    void LookTowards(Vector2 target){
        Vector2 TDirection = target - (Vector2)transform.position;
        transform.right = TDirection;
    }

}
