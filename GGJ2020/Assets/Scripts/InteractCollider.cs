using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractCollider : MonoBehaviour
{

    public float playerRepairAmount;
    public float playerRepairCooldown;
    public float playerPickupCooldown;
    public float currentRepairCooldown = 0f;
    public float currentPickupCooldown = 0f;
    public GameObject pickupLocation;
    public bool isHolding;
    public GameObject heldObject;
    public GameObject snapLocation;

    public AudioSource Pickup;
    public AudioSource Repair;
    public AudioClip pickupSFX;
    public AudioClip[] repairSFX;

    void Start(){
      Pickup.clip = pickupSFX;
    }

    void Update(){
        if(isHolding){
            if(Input.GetButton("Pickup") && (currentPickupCooldown <= 0f)){
                if(snapLocation == null){
                    heldObject.transform.position = pickupLocation.transform.position + Vector3.forward + new Vector3(0f, 1.5f, 0f);
                    heldObject.transform.parent = null;
                    heldObject.GetComponent<Rigidbody>().isKinematic = false;
                    heldObject = null;
                    isHolding = false;
                    currentPickupCooldown = playerPickupCooldown;
                }else{
                    heldObject.transform.position = snapLocation.transform.position;
                    snapLocation.GetComponent<MeshRenderer>().enabled = false;
                    heldObject.transform.parent = null;
                    heldObject.GetComponent<Rigidbody>().isKinematic = true;
                    heldObject = null;
                    isHolding = false;
                    currentPickupCooldown = playerPickupCooldown;
                }
            }
        }
        if(currentRepairCooldown > 0f){
            currentRepairCooldown -= Time.deltaTime;
        }

        if(currentPickupCooldown > 0f){
            currentPickupCooldown -= Time.deltaTime;
        }

    }


    private void OnTriggerStay(Collider other){
        GameObject otherGO = other.gameObject;
        if(string.Equals(otherGO.tag, "Repairable")){
            if(Input.GetButton("Repair") && (!isHolding)){
                if(currentPickupCooldown <= 0f){
                    Repair.clip = repairSFX[Random.Range(0,repairSFX.Length)];
                    Repair.Play();
                    RepairObject repairO = otherGO.GetComponent<RepairObject>();
                    repairO.sendRepairs(playerRepairAmount);
                    currentPickupCooldown = playerRepairCooldown;
                }
            }
        }
        if(string.Equals(otherGO.tag, "Pickupable")){
            if(Input.GetButton("Pickup") && (!isHolding) && (currentPickupCooldown <= 0f)){
                otherGO.transform.position = pickupLocation.transform.position;
                otherGO.transform.parent = pickupLocation.transform;
                heldObject = otherGO;
                heldObject.GetComponent<Rigidbody>().isKinematic = true;
                isHolding = true;
                Pickup.Play();
                currentPickupCooldown = playerPickupCooldown;
            }
        }
    }

    private void OnTriggerEnter(Collider other){
        GameObject otherGO = other.gameObject;
        if(string.Equals(otherGO.tag, "PlaceLocation")){
            if(isHolding){
                otherGO.GetComponent<MeshRenderer>().enabled = true;
                snapLocation = otherGO;
            }
        }
    }


    private void OnTriggerExit(Collider other){
        GameObject otherGO = other.gameObject;
        if(string.Equals(otherGO.tag, "PlaceLocation")){
            if(isHolding){
                otherGO.GetComponent<MeshRenderer>().enabled = false;
                snapLocation = null;
            }
        }
    }
}
