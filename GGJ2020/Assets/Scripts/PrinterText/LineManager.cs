using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LineManager : MonoBehaviour
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
    float epsilon = .0001f;
    //can be used to see how long the player took
    float startTime;
    float timePassed = 0.0f;
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

    }

    public void PrintLine(Result result, int counter){
        if(sentences.CheckEnd(counter)){
            //end condition? 
            return;
        }
        string newSentence;
        //check w/e has the amount of shit wrong w/ it and determine which line 2 print here
        if(result == Result.Good){
            newSentence = sentences.GetGoodAtIndex(counter);
        }
        else if(result == Result.Bad){
            newSentence = sentences.GetBadAtIndex(counter);
        }
        else{
            newSentence = sentences.GetWorstAtIndex(counter);
        }
        Debug.Log(newSentence);
        printedSentences.Add(newSentence, result);
        //To do: pass the current printer color percentages to GetTextColor
        string color = GetTextColor(1f,1f,1f,1f);
        printerText.text = "<color=#"+color+">"+newSentence+"</color>"+'\n'+printerText.text;
    }

    string GetTextColor(float cyan, float magnenta, float yellow, float black){
        Color result;
        if(black-cyan<epsilon&&black-magnenta<epsilon&&black-yellow<epsilon){
            result = new Color(1-black,1-black,1-black);
            return ColorUtility.ToHtmlStringRGB(result);
        }
        if(cyan<black){
            black = cyan;
        }
        result = new Color(1-Mathf.Min(cyan+black,1),1-Mathf.Min(magnenta+black,1),1-Mathf.Min(yellow+black,1));
            return ColorUtility.ToHtmlStringRGB(result);
    }

    public bool CheckEnd(int counter){
        return sentences.CheckEnd(counter);
    }
}
