using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ReadTextFile : MonoBehaviour
{
    [SerializeField] TextAsset textFile;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public string GetString(){
        return textFile.text;
    }
}
