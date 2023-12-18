using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// We should rename this script to PlayerMovement and try to feed in stats for movespeed
// from the playerControllerStats script so that we can modify it with abilities.



public class movement : MonoBehaviour
{
    Rigidbody d_Rigidbody;
    CharacterController d_Controller;
    Animator d_Animator;

    private float d_Speed;
    
    public Camera d_Camera;

    public float d_Walk = 5f;
    public float d_Run = 8f;

    private Vector3 moveDircetion;


    void Start()
    {   
        d_Controller = GetComponent<CharacterController>();
        d_Rigidbody = GetComponent<Rigidbody>();
        d_Animator = GetComponentInChildren<Animator>();
    }   

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
         
    }
    
    void Move()
    {
        float moveZ=Input.GetAxis("Vertical");
        float moveX=Input.GetAxis("Horizontal");
        moveDircetion = new Vector3 (moveX,0,moveZ);

        if (moveDircetion !=Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
        {
            d_Speed = d_Walk;
            if (moveZ <= 0)
            {
                d_Animator.SetFloat("Speed",-0.5f,0.1f,Time.deltaTime);
            }
            else 
            {
                d_Animator.SetFloat("Speed",0.5f,0.1f,Time.deltaTime);
            }
        }
        else if (moveDircetion !=Vector3.zero && Input.GetKey(KeyCode.LeftShift))
        {
            d_Speed = d_Run;
            if (moveZ <= 0)
            {
                d_Animator.SetFloat("Speed",-1f,0.1f,Time.deltaTime);
            }
            else
            {
                d_Animator.SetFloat("Speed",1f,0.1f,Time.deltaTime);
            }
        }
        
        else if (moveDircetion ==Vector3.zero)
        {
            d_Animator.SetFloat("Speed",0,0.1f,Time.deltaTime);
        }
        
        
        moveDircetion *=d_Speed;
        d_Controller.Move(moveDircetion*Time.deltaTime);

        
        
    }
    void Idle()
    {
        d_Animator.SetFloat("Speed",0,0.1f,Time.deltaTime);
    }
    void Walk()
    {
        d_Speed = d_Walk;
        d_Animator.SetFloat("Speed",0.5f,0.1f,Time.deltaTime);
        
    }

    void Run()
    {
        d_Speed = d_Run;
        d_Animator.SetFloat("Speed",1,0.1f,Time.deltaTime);
    }

}
