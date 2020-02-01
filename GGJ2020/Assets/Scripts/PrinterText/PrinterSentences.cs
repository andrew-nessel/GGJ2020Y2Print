using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrinterSentences : MonoBehaviour
{
    [SerializeField] TextAsset file;
    List<List<string>> possibleSentences;
    // Start is called before the first frame update
    void Start()
    {
        possibleSentences = OrganizeSentences.ReadFromCSV(file);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ChangeTextFile(TextAsset newFile){
        file = newFile;
    }
    public void LoadTextFile(TextAsset loadFile){
        possibleSentences = OrganizeSentences.ReadFromCSV(loadFile);
    }
    public void LoadCurrentFile(){
        LoadTextFile(file);
    }
    public string GetWorstAtIndex(int index){
        return GetSentence(index, 2);
    }
    public string GetBadAtIndex(int index){
        return GetSentence(index, 1);
    }
    public string GetGoodAtIndex(int index){
        return GetSentence(index, 0);
    }
    //takes in the index of the sentence collection and the index of which sentence to use and returns that sentence
    //returns empty string if index or the sentence index is out of bounds
    string GetSentence(int index, int sentIndex){
        if(CheckEnd(index)){
            return "";
        }
        if(sentIndex >= possibleSentences[index].Count){
            return "";
        }
        return possibleSentences[index][sentIndex];
    }

    public bool CheckEnd(int index){
        return index >= possibleSentences.Count;
    }
}
