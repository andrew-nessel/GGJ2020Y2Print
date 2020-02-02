using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

public static class OrganizeSentences
{

    public static List<List<string>> ReadFromCSV(TextAsset text){
        List<List<string>> resultSentences = new List<List<string>>();
        string original = GetString(text);
        string[] groups = original.Split('\n');
        int counter = 0;
        foreach(string group in groups){
            string[] removeChars = {"\",\""};
            //gets rid of extra white space
            string edittedGroup = group.Trim();
            string[] sentence = edittedGroup.Split(removeChars,StringSplitOptions.RemoveEmptyEntries);
            resultSentences.Add(new List<string>());
            foreach (string sent in sentence)
            {
                string sentenceToAdd = sent;
                if(sentenceToAdd[0]=='\"'){
                    sentenceToAdd = sentenceToAdd.Substring(1, sent.Length-1);
                }
                if(sentenceToAdd[sentenceToAdd.Length-1]=='\"'){
                    sentenceToAdd = sentenceToAdd.Substring(0, sent.Length-2);
                }
                resultSentences[counter].Add(sentenceToAdd);
            }
            counter++;
        }
        while(resultSentences[resultSentences.Count-1].Count==0){
            resultSentences.RemoveAt(resultSentences.Count-1);
        }
        return resultSentences;
    }

    public static string GetString(TextAsset text){
        return text.text;
    }
}
