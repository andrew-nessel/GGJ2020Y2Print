using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 10f;
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
        MovePlayer(movementDirection*speed);
    }

    Vector3 HandleInput(){
        Vector3 direction = Vector3.zero;
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.z = Input.GetAxisRaw("Vertical");
        direction = Camera.main.transform.TransformDirection(direction);
        direction.Normalize();
        direction.y = -1;
        return direction;
    }

    public void MovePlayer(Vector3 velocity){
        cc.Move(velocity*Time.deltaTime);
    }



}
