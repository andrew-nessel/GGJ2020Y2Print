using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractCollider : MonoBehaviour
{

    public float playerRepairAmount;
    public float playerRepairCooldown;
    public float currentCooldown = 0f;


    private void OnTriggerStay(Collider other){
        GameObject otherGO = other.gameObject;
        if(string.Equals(otherGO.tag, "Repairable")){
            if(Input.GetButton("Repair")){
                if(currentCooldown <= 0f){
                    RepairObject repairO = otherGO.GetComponent<RepairObject>();
                    repairO.sendRepairs(playerRepairAmount);
                    currentCooldown = playerRepairCooldown;
                }else{
                    currentCooldown -= Time.deltaTime;
                }
            }
        }
    }
}
