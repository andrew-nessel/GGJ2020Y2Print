﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] float jumpPower = 50f;
    [SerializeField] float gravity = 100f;
    CharacterController cc;
    Vector3 movementDirection;
    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    void Update(){
        movementDirection = HandleInput();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        MovePlayer(movementDirection);
    }


    Vector3 HandleInput(){
        Vector3 direction = Vector3.zero;
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.z = Input.GetAxisRaw("Vertical");
        //scales movement to camera direction
        direction.Normalize();
        //comment out line below to stop character from rotating w/ camera
        transform.forward = new Vector3(Camera.main.transform.forward.x, transform.forward.y, Camera.main.transform.forward.z);
        direction = Camera.main.transform.TransformDirection(direction);
        direction = direction*speed;
        direction.y = GetYAxis();
        return direction;
    }


    //current jumping is too variable, should prob be changed later oops
    float GetYAxis(){
        float y = -gravity*Time.fixedDeltaTime;
        if(cc.isGrounded && Input.GetAxisRaw("Jump")==1){
            y += jumpPower;
        }
        return y;
    }

    public void MovePlayer(Vector3 velocity){
        cc.Move(velocity*Time.fixedDeltaTime);
    }



}