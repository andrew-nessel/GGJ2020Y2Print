  ﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RepairObject : MonoBehaviour
{
    public float maxDamageAmount = 100f;
    public float damageAmount;
    public Slider damageSlider;

    public AudioSource Damage;
    public AudioClip[] damageSFX;
    bool needsRepair = false;
    public ParticleSystem particles;
    public bool particlesOn = false;

    private void playDamageSFX(){
      if (needsRepair == false){
        if(GetDamagePercentage() > 0.5f){
          needsRepair = true;
          Damage.loop = true;
          Damage.clip = damageSFX[0];
          Damage.Play();
        }
        else if (GetDamagePercentage() > 0.75f) {
              needsRepair = true;
              Damage.loop = true;
              Damage.clip = damageSFX[1];
              Damage.Play();
            }
          } else if (GetDamagePercentage() < 0.5f) {
            needsRepair = false;
            Damage.clip = damageSFX[2];
            Damage.loop = false;
            Damage.Play();
         }
      }

    // Start is called before the first frame update
    void Start()
    {
        particles.Stop();
        damageSlider.value = 0f;
        Damage.spatialBlend = 1.0f;

    }

    // Update is called once per frame
    void Update()
    {
      playDamageSFX();
        
        if((GetDamagePercentage() > .75f) && (!particlesOn)){
            particles.Play();
            particlesOn = true;
        }else{
            particles.Stop();
            particlesOn = false;
        }
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
