using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossDungeon : MonoBehaviour
{


    private AudioSource audiosource;

    public AudioClip loopClip;

    private void Start()
    {
        audiosource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!audiosource.isPlaying)
        {
            audiosource.loop = true;
            audiosource.clip = loopClip;
            audiosource.Play();
        }
    }
}



