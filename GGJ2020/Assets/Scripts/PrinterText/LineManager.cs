using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LineManager : MonoBehaviour
{
    public enum Result{
    Good,
    Bad,
    Terrible
    }

    Dictionary<string, Result> printedSentences;
    PrinterSentences sentences;
    float totalSentenceResults = 0.0f;
    [SerializeField] TextMeshPro printerText = null;
    ColorRepair colors;
    [SerializeField] GameObject paper = null;
    [SerializeField] Vector3 paperShuffleDirection = Vector3.right;
    [SerializeField] float paperMovementAmount = 2f; 
    // Start is called before the first frame update
    void Start()
    {
        paperShuffleDirection.Normalize();
        printedSentences = new Dictionary<string, Result>();
        printerText.text = "";
        sentences = GetComponent<PrinterSentences>();
        if(sentences == null){
            sentences = GameObject.FindObjectOfType<PrinterSentences>();
        }
        colors = GameObject.FindObjectOfType<ColorRepair>();
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
        totalSentenceResults += ResultToFloat(result);
        string color;
        if(colors!=null){
            color = GetTextColor(colors.GetCyanPercentage(),colors.GetMagentaPercentage(),colors.GetYellowPercentage(), colors.GetBlackPercentage());
        }
        else{
            color = GetTextColor(1f,1f,1f,1f);
        }
        printerText.text = printerText.text+"\n<color=#"+color+">"+newSentence+"</color>";
        if(paper!=null){
            ShufflePaper();
        }
    }

    void ShufflePaper(){
        paper.transform.position += paperShuffleDirection*paperMovementAmount;
    }

    string GetTextColor(float cyan, float magnenta, float yellow, float black){
        Color result;
        result = new Color((1-Mathf.Min(cyan,1))*(1-Mathf.Min(black, 1)),(1-Mathf.Min(magnenta,1))*(1-Mathf.Min(black, 1)),(1-Mathf.Min(yellow,1))*(1-Mathf.Min(black, 1)));
            return ColorUtility.ToHtmlStringRGB(result);
    }

    public bool CheckEnd(int counter){
        return sentences.CheckEnd(counter);
    }

    public float ResultToFloat(Result result){
        if(result == Result.Good){
            return 1f;
        }
        else if(result == Result.Bad){
            return 2f;
        }
        else{
            return 3f;
        }
    }

    public float GetCurrentOverallResult(){
        return totalSentenceResults/(float)printedSentences.Count;
    }

    public string GetFinishedText(){
        return printerText.text;
    }
}
