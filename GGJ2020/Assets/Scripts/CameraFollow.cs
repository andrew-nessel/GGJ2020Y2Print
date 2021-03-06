﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] GameObject player = null;
    [SerializeField] float rotateSpeed = 100f;
    [SerializeField] 
    [Range(0,1)] float CameraSpeed = .1f;
    Vector3 offset;
    float inputDirection;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(player == null){
            return;
        }
        HandleInput();
    }

    void FixedUpdate(){
        if(player == null){
            return;
        }
        transform.position = Vector3.Lerp(transform.position, player.transform.position+offset, CameraSpeed);
        transform.LookAt(player.transform.position);
    }

    void HandleInput(){
        offset = Quaternion.AngleAxis (Input.GetAxis("Mouse X") * rotateSpeed, Vector3.up) * offset;
    }
}
