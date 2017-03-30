using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartPage : MonoBehaviour {
    bool soundActive = true;
    public GameObject muteButton;

    public void StartClicked()
    {
        GameManager.Instance.UnloadCurrentLevel();
        GameManager.Instance.currentLevelIsStart = false;
        SceneManager.LoadScene("MainScene");

    }

    public void MutePressed()
    {
        soundActive = !soundActive;
        if (soundActive)
        {
            muteButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("Graphics/UI/Sound");
            LOLAudio.Instance.soundIsActive = true;
        }
        else
        {
            muteButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("Graphics/UI/Mute");
            LOLAudio.Instance.soundIsActive = false;
        }
    }

    // Use this for initialization
    void Start () {
        GameManager.Instance.InitializeStartLevel();
	}
	
	// Update is called once per frame
	void Update () {
        float dt = Time.deltaTime;
        GameManager.Instance.StartScreenUpdate(dt);
    }
}
