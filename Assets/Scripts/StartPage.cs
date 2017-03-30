using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartPage : MonoBehaviour {

    public void StartClicked()
    {
        GameManager.Instance.UnloadCurrentLevel();
        SceneManager.LoadScene("MainScene");

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
