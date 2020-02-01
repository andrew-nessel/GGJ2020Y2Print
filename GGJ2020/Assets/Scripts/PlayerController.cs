using System.Collections;
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

    // Update is called once per frame
    void Update()
    {
        movementDirection = HandleInput();
        MovePlayer(movementDirection);
    }


    Vector3 HandleInput(){
        Vector3 direction = Vector3.zero;
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.z = Input.GetAxisRaw("Vertical");
        //scales movement to camera direction
        direction = Camera.main.transform.TransformDirection(direction);
        direction.Normalize();
        direction = direction*speed;
        direction.y = GetYAxis();
        return direction;
    }


    //current jumping is too variable, should prob be changed later oops
    float GetYAxis(){
        float y = -gravity*Time.deltaTime;
        if(cc.isGrounded && Input.GetAxisRaw("Jump")==1){
            y += jumpPower;
        }
        return y;
    }

    public void MovePlayer(Vector3 velocity){
        cc.Move(velocity*Time.deltaTime);
    }



}
