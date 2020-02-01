﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrinterManager : MonoBehaviour
{
    List<RepairObject> pieces;
    List<float> timers;
    //To do: actually balance the numbers oops...
    [SerializeField] float maxTimeToBreak = 15f;
    [SerializeField] float minTimeToBreak = 5f;
    [SerializeField] float printTime = 2f;
    float timePassed = 0.0f;
    LineManager lineManager;
    int counter = 0;
    // Start is called before the first frame update
    void Start()
    {
        timers = new List<float>();
        lineManager = GameObject.FindObjectOfType<LineManager>();
        pieces = new List<RepairObject>(FindObjectsOfType<RepairObject>());
        foreach(var piece in pieces){
            timers.Add(Random.Range(minTimeToBreak, maxTimeToBreak));
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckBreak();
        CheckPrint();
    }
    void CheckBreak(){
        for(int i = 0; i < pieces.Count; i++){
            timers[i]-=Time.deltaTime;
            if(timers[i]<=0){
                pieces[i].DamageByPercentage(Random.Range(0.0f,1.0f));
                timers[i] = Random.Range(minTimeToBreak, maxTimeToBreak);
            }
        }
    }

    void CheckPrint(){
        //won't print/pass time if there's a jam
        if(CheckJam()){
            return;
        }
        timePassed += Time.deltaTime;
        //might change the if statement below to be more dependent on the broken parts than just based on a flat time
        if(timePassed >= printTime){
            lineManager.PrintLine(GetResult(), counter);
            timePassed = 0.0f;
            counter++;
        }
    }
    
    LineManager.Result GetResult(){
        float percentageBad = 0.0f;
        foreach(var piece in pieces){
            percentageBad+=piece.GetDamagePercentage();
        }
        percentageBad = percentageBad/pieces.Count;
        if(percentageBad<.50f){
            return LineManager.Result.Terrible;
        }
        else if(percentageBad<.90f){
            return LineManager.Result.Bad;
        }
        else{
            return LineManager.Result.Good;
        }
    }

    bool CheckJam(){
        //To do: change so that it actually checks if there is a jam lol
        return false;
    }

}
