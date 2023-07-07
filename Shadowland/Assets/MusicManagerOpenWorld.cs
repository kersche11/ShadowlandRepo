using CUAS.MMT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManagerOpenWorld : MonoBehaviour
{
   
    [SerializeField] private float PlayNextVideoTime = 0.1f;

    float changeClipTime;
    private AudioSource[] audioSources;
    bool isFirstClip;

    // Start is called before the first frame update
    private void Start()
    {
        audioSources = GetComponents<AudioSource>();

       isFirstClip = true;
        PlayFirstClip();
        changeClipTime= audioSources[0].clip.length * PlayNextVideoTime;
    }
    private void Update()
    {
        float time;
        if (isFirstClip)
        {
          time = audioSources[0].clip.length - audioSources[0].time / audioSources[0].pitch;
        }
        else
        {
            time = audioSources[1].clip.length - audioSources[1].time / audioSources[1].pitch;
        }
       
       
        if (time < changeClipTime)
        {
            if (isFirstClip)
            {
                isFirstClip = false;
                PlaySecondClip();
            }
            else
            {
                isFirstClip = true;
                PlayFirstClip();
            }
            
        }
    }
    private void PlayFirstClip()
    {

        audioSources[0].Play();

       
       
    }

    private void PlaySecondClip()
    {

        audioSources[1].Play();
    }


    public void StartGameMusic()
    {
        audioSources[0].Stop();
        audioSources[1].Stop();
        audioSources[2].Play();
    }
}

