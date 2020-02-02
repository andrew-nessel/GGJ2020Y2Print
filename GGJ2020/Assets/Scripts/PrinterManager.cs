using System.Collections;
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
    GM gm;
    // Start is called before the first frame update
    void Start()
    {
        timers = new List<float>();
        lineManager = GameObject.FindObjectOfType<LineManager>();
        pieces = new List<RepairObject>(FindObjectsOfType<RepairObject>());
        foreach(var piece in pieces){
            timers.Add(Random.Range(minTimeToBreak, maxTimeToBreak));
        }
        gm = GameObject.FindObjectOfType<GM>();
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
                pieces[i].DamageByPercentage(CalucalatePercentage());
                timers[i] = Random.Range(minTimeToBreak, maxTimeToBreak);
            }
        }
    }

    float CalucalatePercentage(){
        float percent = Random.Range(0.0f, 0.5f);
        return percent;
    }

    void CheckPrint(){
        //won't print/pass time if there's a jam
        if(CheckJam()){
            return;
        }
        if(lineManager.CheckEnd(counter)){
            return;
        }
        timePassed += Time.deltaTime;
        //might change the if statement below to be more dependent on the broken parts than just based on a flat time
        if(timePassed >= printTime){
            lineManager.PrintLine(GetResult(), counter);
            timePassed = 0.0f;
            counter++;
            if(lineManager.CheckEnd(counter)){
                if(gm != null){
                    gm.EndScreen(lineManager.GetCurrentOverallResult(), lineManager.GetFinishedText());
                }
            }
        }
    }
    
    LineManager.Result GetResult(){
        float percentageBad = 0.0f;
        foreach(var piece in pieces){
            percentageBad+=piece.GetDamagePercentage();
        }
        percentageBad = percentageBad/pieces.Count;
        if(percentageBad<.10f){
            return LineManager.Result.Good;
        }
        else if(percentageBad<.50f){
            return LineManager.Result.Bad;
        }
        else{
            return LineManager.Result.Terrible;
        }
    }

    bool CheckJam(){
        //To do: change so that it actually checks if there is a jam lol
        return false;
    }

}
