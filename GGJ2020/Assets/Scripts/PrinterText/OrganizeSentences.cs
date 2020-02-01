using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrganizeSentences : MonoBehaviour
{
    List<List<string>> sentences;
    [SerializeField] ReadTextFile reader;
    // Start is called before the first frame update
    void Start()
    {
        sentences = new List<List<string>>();
        ReadFromCSV();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ReadFromCSV(){
        string original = reader.GetString();
        string[] groups = original.Split('\n');
        int counter = 0;
        foreach(string group in groups){
            char[] removeChars = {','};
            string[] sentence = group.Split(removeChars);
            sentences.Add(new List<string>());
            foreach (string sent in sentence)
            {
                sentences[counter].Add(sent.Substring(1,sent.Length-2));
                Debug.Log(sent.Substring(1,sent.Length-2));
            }
        }
    }
}
