using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WASDMovement : MonoBehaviour
{
    public bool overrideMovespeed = false;

    [Tooltip("movespeed override var that only applies if overrideMovespeed is true.")]
    public float moveSpeed = 5f;

    // Update is called once per frame
    void FixedUpdate()
    {
        // Get input from WASD keys
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // Calculate the movement direction
        Vector3 movementDirection = new Vector3(horizontalInput, verticalInput, 0f).normalized;

        // Move the player
        if (overrideMovespeed)
        {
            transform.position += movementDirection * moveSpeed * Time.deltaTime;
        }
        else
        {
            transform.position += movementDirection * PlayerData.instance.playerParty.partySpeed * Time.deltaTime;
        }

        
    }
}
