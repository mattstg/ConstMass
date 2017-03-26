using System.Collections;
using System.Collections.Generic;
using UnityEngine; using LoLSDK;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public static bool Sound_Active = true;
    public GameObject muteButton;

    public void StartPressed()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("IntroScene");
        //LOLAudio.Instance.StopAudio("IntroMusic.mp3");
    }

    public void MutePressed()
    {
        Sound_Active = !Sound_Active;
        if (Sound_Active)
        {
            LOLAudio.Instance.PlayAudio("IntroMusic.mp3");
            muteButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Sound");
        }
        else
        {
            LOLAudio.Instance.StopAudio("IntroMusic.mp3");
            muteButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Mute");
        }
    }
}
