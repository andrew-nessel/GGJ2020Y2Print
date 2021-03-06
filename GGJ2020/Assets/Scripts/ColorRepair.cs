﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorRepair : RepairObject
{
    [SerializeField] float cyanEmpty = 0;
    [SerializeField] float magentaEmpty = 0;
    [SerializeField] float yellowEmpty = 0;
    [SerializeField] float blackEmpty = 0;
    InkAmount currentCart = null;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void sendRepairs(float repairAmount){
        cyanEmpty -= repairAmount;
        magentaEmpty -= repairAmount;
        yellowEmpty -= repairAmount;
        blackEmpty -= repairAmount;
    }

    public void LoadCartridge(InkAmount newCart){
        currentCart = newCart;
        cyanEmpty = currentCart.cyanEmpty;
        magentaEmpty = currentCart.magentaEmpty;
        yellowEmpty = currentCart.yellowEmpty;
        blackEmpty = currentCart.blackEmpty;
        currentCart.UpdateColorRepair(this);
    }

    public void RemoveCartridge(){
        currentCart = null;
        cyanEmpty = maxDamageAmount;
        magentaEmpty = maxDamageAmount;
        yellowEmpty = maxDamageAmount;
        blackEmpty = maxDamageAmount;
    }

    public override void DamageByPercentage(float percent){
        //since theres three different values percent is multipled by 3 and capped at 1
        if(currentCart == null){
            return;
        }
        percent = Mathf.Min(1, percent*3);
        float rand = Random.Range(0.0f, 1.0f);
        if(rand<.5f){
            cyanEmpty = Mathf.Min(cyanEmpty+percent*maxDamageAmount, maxDamageAmount);
        }
        else if(rand<.75f){
            magentaEmpty = Mathf.Min(magentaEmpty+percent*maxDamageAmount, maxDamageAmount);
        }
        else if(rand<.825){
            yellowEmpty = Mathf.Min(yellowEmpty+percent*maxDamageAmount, maxDamageAmount);
        }
        else{
            blackEmpty = Mathf.Min(blackEmpty+percent*maxDamageAmount, maxDamageAmount);
        }
        currentCart.UpdateInk(cyanEmpty, magentaEmpty, yellowEmpty, blackEmpty);

    }

    public override void DamageByFlatAmount(float amount){
        cyanEmpty += amount;
        magentaEmpty += amount;
        yellowEmpty += amount;
        blackEmpty +=amount;
    }

    public override float GetDamagePercentage(){
        return (cyanEmpty+magentaEmpty+yellowEmpty)/(3*maxDamageAmount);
    }

    public float GetCyanPercentage(){
        return 1-(cyanEmpty/maxDamageAmount);
    }

    public float GetMagentaPercentage(){
        return 1-(magentaEmpty/maxDamageAmount);
    }

    public float GetYellowPercentage(){
        return 1-(yellowEmpty/maxDamageAmount);
    }

    public float GetBlackPercentage(){
        return 1-(blackEmpty/maxDamageAmount);
    }


}
