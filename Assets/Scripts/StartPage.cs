﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; using LoLSDK;

public class StartPage : MonoBehaviour {
    string songChosen = "GameMusicNew.mp3";
    public GameObject muteButton;

    void Start()
    {
        LOLSDK.Init("com.Pansimula.DanceOfAtoms");
        GV.Sound_Active = true;
        LOLAudio.Instance.AddToBanList("CorrectMolecule.wav");
        LOLAudio.Instance.AddToBanList("Seperate.wav");
        LOLAudio.Instance.AddToBanList("Wobble.wav");
        AudioLooper.Instance.StartAudioLooper(songChosen, GetSongLength(songChosen));
        GV.Atom_Text_Active = false;
        GV.Molecule_Text_Active = false;
        GameManager.Instance.InitializeStartLevel();
    }

    public void StartClicked()
    {
        GameManager.Instance.UnloadMainMenu();
        GameManager.Instance.currentLevelIsStart = false;
        LOLAudio.Instance.ClearBanList();
        GV.Atom_Text_Active = true;
        GV.Molecule_Text_Active = true;
        SceneManager.LoadScene("MainScene");
    }

    /*public void ChoseSong(int songID)
    {
        string newsong = (songID == 0) ? "GameMusicNew.mp3" : "GameMusicRetro.mp3";
        if(songChosen != newsong && GV.Sound_Active)
        {
            AudioLooper.Instance.CloseAudioLooper();
            AudioLooper.Instance.StartAudioLooper(newsong, GetSongLength(newsong));
            songChosen = newsong;
        }
    }*/

    public float GetSongLength(string songName) //shouldnt be here but w.e
    {
        if (songName == "GameMusicNew.mp3")
            return 32;
        return 20;
    }

    public void MutePressed()
    {
        GV.Sound_Active = !GV.Sound_Active;
        if (GV.Sound_Active)
        {
            muteButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("Graphics/UI/Sound");
            AudioLooper.Instance.StartAudioLooper(songChosen, GetSongLength(songChosen));
        }
        else
        {
            muteButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("Graphics/UI/Mute");
            AudioLooper.Instance.CloseAudioLooper();
        }
    }
	
	// Update is called once per frame
	void Update () {
        float dt = Time.deltaTime;
        GameManager.Instance.StartScreenUpdate(dt);
        AudioLooper.Instance.Update(dt);
    }
}
