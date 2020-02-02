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


  //FOOTSTEP SFX
    public AudioSource steps;
    public AudioClip leftfoot;
    public AudioClip rightfoot;
    private bool foot = false;
    public float footstepcooldown = 0.15f;
    public float currentfootstepcooldown;


    public void setFootstep(){

      if (foot){
        steps.clip = leftfoot;
      } else {
        steps.clip = rightfoot;
      }
    }

    private void Footsteps(Vector3 movementDirection){
      if(currentfootstepcooldown <= 0f){
        if((Mathf.Abs(movementDirection.x) + Mathf.Abs(movementDirection.z)) > 0.2f){
          setFootstep();
          steps.Play();
          foot = !foot;
          currentfootstepcooldown = footstepcooldown;
        }
      } else {
        currentfootstepcooldown -= Time.fixedDeltaTime;
      }


    }


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
        Footsteps(movementDirection);
        Debug.Log(movementDirection);
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
        direction.y = CheckJump();
        return direction;
    }


    //current jumping is too variable, should prob be changed later oops
    float CheckJump(){
        Vector3 newVelocity = new Vector3(cc.velocity.x, cc.velocity.y-gravity*Time.fixedDeltaTime, cc.velocity.z);
        if(cc.isGrounded && Input.GetAxisRaw("Jump")==1){
            newVelocity.y += jumpPower;
        }
        return newVelocity.y;
    }

    public void MovePlayer(Vector3 velocity){
        cc.Move(velocity*Time.fixedDeltaTime);
    }



}
