﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GM : MonoBehaviour
{
    public bool isPaused = false;
    public GameObject UI;

    // Start is called before the first frame update
    void Start()
    {
        unPause();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Pause")){
            if(isPaused){
                unPause();
            }else{
                Pause();
            }
        }
    }

    public void unPause(){
        UI.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        isPaused = false;
    }

    public void Pause(){
        UI.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.Confined;
        isPaused = true;
    }

    public void goToMenu(){
        SceneManager.LoadScene("MainMenu");
    }

    public void EndScreen(float numberScore, string finishedText){
        Cursor.lockState = CursorLockMode.None;
        PlayerPrefs.SetFloat("Score", numberScore);
        PlayerPrefs.SetString("FinishedText", finishedText);
        SceneManager.LoadScene("EndScene");
    }
}
