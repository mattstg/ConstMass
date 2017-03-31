using System.Collections;
using System.Collections.Generic;
using UnityEngine; using LoLSDK;

public class AudioLooper {
    //For future game, should be a manager that holds many AudioLoopers, more generic setup with args instead of reading a switch
    #region Singleton
    private static AudioLooper instance;

    private AudioLooper() { }

    public static AudioLooper Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new AudioLooper();
            }
            return instance;
        }
    }
    #endregion

    private string audioName;
    private float trackLength;
    private bool isPlaying = false;
    float[] curTime = new float[2];
    bool[] playingAudio = new bool[2];
    private float fadeTime = 3;

    public void StartAudioLooper(string _audioName, float _trackLength)
    {
        curTime[0] = curTime[1] = 0;
        playingAudio[0] = playingAudio[1] = false;
        trackLength = _trackLength;
        audioName = _audioName;
        isPlaying = true;
        PlayAudio(0);
    }

    public void Update(float dt)
    {
        if(isPlaying)
        {
            for(int i = 0; i < 2; i++)
            {
                if(playingAudio[i])
                {
                    curTime[i] += dt;
                    if(!playingAudio[GOI(i)] && curTime[i] + fadeTime >= trackLength)
                    {
                        PlayAudio(GOI(i));
                    }
                    if(curTime[i] >= trackLength)
                    {
                        curTime[i] = 0;
                        playingAudio[i] = false;
                    }
                }
            }
        }
    }

    private void PlayAudio(int i)
    {
        curTime[i] = 0;
        playingAudio[i] = true;
        LOLAudio.Instance.PlayAudio(audioName, false); 
    }

    public void CloseAudioLooper()
    {
        LOLAudio.Instance.StopAudio(audioName);
        LOLAudio.Instance.StopAudio(audioName);
        isPlaying = false;
    }


    private int GOI(int i) //Get Other Index
    {
        return (i + 1) % 2;
    }
    
}
