using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public AudioSource menuMusic;
    public AudioClip music1;
    public AudioClip music2;
    public GameObject mainMenuPanel;
    public GameObject howToPanel;


    // Start is called before the first frame update
    void Start()
    {
        howToPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
        menuMusic.clip = music1;
        menuMusic.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (!menuMusic.isPlaying)
        {
            menuMusic.clip = music2;
            menuMusic.Play();
            menuMusic.loop = true;
        }
        
    }

    public void quitGame(){
        Application.Quit();
    }

    public void startGame(){
        SceneManager.LoadScene("PrinterLevel");
    }

    public void showHowToPlay(){
        howToPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
    }
}
