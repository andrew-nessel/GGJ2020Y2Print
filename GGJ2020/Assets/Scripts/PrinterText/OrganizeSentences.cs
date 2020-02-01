﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrganizeSentences : MonoBehaviour
{
    List<List<string>> sentences;
    [SerializeField] TextAsset textFile;
    // Start is called before the first frame update
    void Start()
    {
        sentences = ReadFromCSV(textFile);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static List<List<string>> ReadFromCSV(TextAsset text){
        List<List<string>> resultSentences = new List<List<string>>();
        string original = GetString(text);
        string[] groups = original.Split('\n');
        int counter = 0;
        foreach(string group in groups){
            char[] removeChars = {','};
            //gets rid of extra white space
            string edittedGroup = group.Trim();
            string[] sentence = edittedGroup.Split(removeChars);
            resultSentences.Add(new List<string>());
            foreach (string sent in sentence)
            {
                resultSentences[counter].Add(sent.Substring(1,sent.Length-2));
            }
        }
        return resultSentences;
    }

    public static string GetString(TextAsset text){
        return text.text;
    }

    public void LoadTextFile(TextAsset file){
        textFile = file;
    }
}
