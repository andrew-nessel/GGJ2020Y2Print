using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairObject : MonoBehaviour
{
    public float maxDamageAmount = 100f;
    public float damageAmount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void sendRepairs(float repairAmount){
        damageAmount -= repairAmount;
    }

    public void DamageByPercentage(float percent){
        //Mathf.Max makes sure that it can be damaged at 0 damageAmount
        damageAmount = Mathf.Min(Mathf.Max((1 + percent)*damageAmount, percent*maxDamageAmount), maxDamageAmount);
    }
    public void DamageByFlatAmount(float amount){
        damageAmount+=amount;
    }
    public float GetDamagePercentage(){
        return damageAmount/maxDamageAmount;
    }
}
