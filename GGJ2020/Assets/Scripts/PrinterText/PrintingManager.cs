using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PrintingManager : MonoBehaviour
{
    public enum Result{
    Good,
    Bad,
    Terrible
    }
    Dictionary<string, Result> printedSentences;
    PrinterSentences sentences;
    [SerializeField] TextMesh printerText = null;
    [SerializeField] float printTime = 2f;
    //can be used to see how long the player took
    float startTime;
    float timePassed = 0.0f;
    int counter = 0;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        printedSentences = new Dictionary<string, Result>();
        printerText.text = "";
        sentences = GetComponent<PrinterSentences>();
        if(sentences == null){
            sentences = GameObject.FindObjectOfType<PrinterSentences>();
        }
    }
    // Update is called once per frame
    void Update()
    {
        CheckPrint();
    }

    void CheckPrint(){
        //won't print/pass time if there's a jam
        if(CheckJam()){
            return;
        }
        timePassed+=Time.deltaTime;
        //might change the if statement below to be more dependent on the broken parts than just based on a flat time
        if(timePassed>=printTime){
            PrintLine();
            //To do: add possible effects either here or in print line like different colors, "invisible" ink
            timePassed = 0.0f;
        }
    }

    bool CheckJam(){
        //To do: change so that it actually checks if there is a jam lol
        return false;
    }

    void PrintLine(){
        if(sentences.CheckEnd(counter)){
            //end condition? 
            return;
        }
        string newSentence;
        Result result;
        //check w/e has the amount of shit wrong w/ it and determine which line 2 print here
        if(true){
            result = Result.Good;
            newSentence = sentences.GetGoodAtIndex(counter);
        }
        else if(false){
            result = Result.Bad;
            newSentence = sentences.GetBadAtIndex(counter);
        }
        else{
            result = Result.Terrible;
            newSentence = sentences.GetWorstAtIndex(counter);
        }
        Debug.Log(newSentence);
        printedSentences.Add(newSentence, result);
        printerText.text = newSentence+'\n'+printerText.text;
        counter++;
    }
}
