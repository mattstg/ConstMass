using System.Collections;
using System.Collections.Generic;
using UnityEngine; using LoLSDK;

public class GameFlow : MonoBehaviour {

	
	public bool canTimeOut = true;
	
    protected AudioLooper audioLooper;
    public ScoreText scoreText; //maybe moved to canvas?
    public UnityEngine.UI.InputField levelSelected; //temp to restart/force load level

    private int _score = 0;
	public int score {set{ChangeScore (value); }get{return _score; }}
	public bool trackScoreAsTime = false;
	protected bool roundTimerActive = false;
	protected float roundTime = 0;
    protected float roundTimeToGetFullScore = 0;
    protected int stage = 0;
    protected bool gameForceEnded = false;

    private int currentLevel = 0;
    

	public void Start()
	{
        audioLooper = new AudioLooper();
        
	}

    private void StartNextLoadout()
    {
        //Start the LessonManager
        
    }

    public void LessonFinished()
    {
        //Then play current level:game
        if(currentLevel >= GV.Game_Lesson_Max)
        {
            //Was the final lesson, not followed by a game, load scorescreen or end the game
        }
        else
            LoadLevel(currentLevel);
    }

    public void GameFinished()
    {
        //Then start next level sequence
        InputManager.gameInputActivate = false;
        GameManager.Instance.UnloadCurrentLevel();
        currentLevel++;
        StartNextLoadout();
        
    }

    private void TimeRanOut()
    {
        GameFinished();
    }

    private void LoadLevel(int lvl)
    {
        GameManager.Instance.CreateAndInitializeLevel(lvl);
        InputManager.gameInputActivate = true;
        roundTime = 0;

    }

    public void ChangeScore(int newScore)
	{
		scoreText.SetScore (newScore);
		_score = newScore;
	}


    public void RestartLevelButton()
    {
        LoadLevel(int.Parse(levelSelected.text));
    }


    public void Update()
    {
        float dt = Time.deltaTime;

        GameManager.Instance.Update(dt);
        MergeManager.Instance.UpdateMerger(dt);

        if (audioLooper != null)
            audioLooper.Update();

        if (roundTimerActive)
            roundTime += Time.deltaTime;

        if (trackScoreAsTime)
            scoreText.SetScoreTime(roundTime);

        if (roundTime > GV.Game_Length[currentLevel])
            TimeRanOut();

    }


    protected float GetTimedRoundScore()
    {
        return Mathf.Clamp01( 1 - ((roundTime - roundTimeToGetFullScore) / (GV.Game_Length[currentLevel] - roundTimeToGetFullScore)));
    }
	

    protected void StartMusic()
    {
        /*LOLAudio.Instance.PlayAudio(lessonType + "Music.mp3", true);
        if(audioLooper != null)
            audioLooper.StartAudioLooper(lessonType);*/
    }

    protected void CloseMusic()
    {
       /* LOLAudio.Instance.StopAudio(lessonType + "Music.mp3");
        if (audioLooper != null)
            audioLooper.CloseAudioLooper();*/
    }
}
