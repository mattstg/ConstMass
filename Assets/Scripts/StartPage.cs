using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; using LoLSDK;

public class StartPage : MonoBehaviour {
    bool soundActive = true;
    public GameObject muteButton;

    void Start()
    {
        LOLSDK.Init("com.Pansimula.DanceOfAtoms");
        LOLAudio.Instance.PlayAudio("GameMusic.mp3",true);
        GameManager.Instance.InitializeStartLevel();
    }

    public void StartClicked()
    {
        GameManager.Instance.UnloadCurrentLevel();
        GameManager.Instance.currentLevelIsStart = false;
        SceneManager.LoadScene("MainScene");
    }

    public void MutePressed()
    {
        soundActive = !soundActive;
        LOLAudio.Instance.soundIsActive = soundActive;
        if (soundActive)
        {
            muteButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("Graphics/UI/Sound");
            LOLAudio.Instance.PlayAudio("GameMusic.mp3", true);
        }
        else
        {
            muteButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("Graphics/UI/Mute");
            LOLAudio.Instance.StopAudio("GameMusic.mp3");
        }
    }
	
	// Update is called once per frame
	void Update () {
        float dt = Time.deltaTime;
        GameManager.Instance.StartScreenUpdate(dt);
    }
}
