using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RepairObject : MonoBehaviour
{
    public float maxDamageAmount = 100f;
    public float damageAmount;
    public Slider damageSlider;

    // Start is called before the first frame update
    void Start()
    {
        damageSlider.value = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void sendRepairs(float repairAmount){
        damageAmount -= repairAmount;
        damageAmount = Mathf.Max(damageAmount, 0);
        damageSlider.value = GetDamagePercentage();
    }

    public virtual void DamageByPercentage(float percent){
        //Mathf.Max makes sure that it can be damaged at 0 damageAmount
        damageAmount = Mathf.Min(damageAmount+percent*maxDamageAmount, maxDamageAmount);
        damageSlider.value = GetDamagePercentage();
    }
    public virtual void DamageByFlatAmount(float amount){
        damageAmount+=amount;
        damageSlider.value = GetDamagePercentage();
    }
    public virtual float GetDamagePercentage(){
        return damageAmount/maxDamageAmount;
    }
}
