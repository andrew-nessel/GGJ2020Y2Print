using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

  public AudioSource MusicSource1;
  public AudioSource MusicSource2;
  public AudioClip Music1;
  public AudioClip Music2;

    // Start is called before the first frame update
    void Start()
    {
      MusicSource1.clip = Music1;
      MusicSource1.loop = true;
      MusicSource1.volume = 0.2f;
      MusicSource1.Play();
    }




    // Update is called once per frame
    void Update()
    {

    }
}
