using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public AudioSource menuMusic;
    public AudioClip music1;
    public AudioClip music2;

    // Start is called before the first frame update
    void Start()
    {
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
}
