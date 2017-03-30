using System.Collections;
using System.Collections.Generic;
using UnityEngine; using LoLSDK;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour {

	Text textScore;
	// Use this for initialization
	void Awake () {
		textScore = GetComponent<Text> ();	
	}

	public void SetScore(int newScore)
	{
        if(gameObject.activeInHierarchy)
		    textScore.text = newScore.ToString ();
	}

	public void SetScoreTime(float totalTime)
	{
        if (gameObject.activeInHierarchy)
            textScore.text = TimeAsTimerString (totalTime); 
	}

    public string TimeAsTimerString(float totalTime, bool showMilliseconds = false)
    {
        string min = (((int)totalTime / 60).ToString()).PadLeft(1, '0');
        string sec = (((int)totalTime % 60).ToString()).PadLeft(2, '0');
        string ms = (((int)((totalTime - (int)totalTime) * 100)).ToString()).PadLeft(2, '0');
        string colon = ":";

        if (showMilliseconds)
            return min + colon + sec + colon + ms;
        else
            return min + colon + sec;
    }
}
