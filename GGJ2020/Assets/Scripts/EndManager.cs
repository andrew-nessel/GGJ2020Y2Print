using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class EndManager : MonoBehaviour
{
    [SerializeField] TextMeshPro endText = null;
    [SerializeField] Text scoreText = null;
    // Start is called before the first frame update
    void Start()
    {
        endText.text = PlayerPrefs.GetString("FinishedText", "Error");
        scoreText.text = "Final Score: " + ((2-(PlayerPrefs.GetFloat("Score", 3)-1))/2)*100 + "%";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToMainMenu(){
        SceneManager.LoadScene("MainMenu");
    }

}
