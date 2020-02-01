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

    public virtual void sendRepairs(float repairAmount){
        damageAmount -= repairAmount;
        damageAmount = Mathf.Max(damageAmount, 0);
    }

    public virtual void DamageByPercentage(float percent){
        //Mathf.Max makes sure that it can be damaged at 0 damageAmount
        damageAmount = Mathf.Min(Mathf.Max((1 + percent)*damageAmount, percent*maxDamageAmount), maxDamageAmount);
    }
    public virtual void DamageByFlatAmount(float amount){
        damageAmount+=amount;
    }
    public virtual float GetDamagePercentage(){
        return damageAmount/maxDamageAmount;
    }
}
