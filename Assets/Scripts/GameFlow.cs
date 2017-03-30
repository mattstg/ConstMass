using System.Collections;
using System.Collections.Generic;
using UnityEngine; using LoLSDK;

public class GameFlow : MonoBehaviour {

	
	public bool canTimeOut = true;
	
    protected AudioLooper audioLooper;
    PanelManager panelManager;
    public InfoPanel infoPanel;
    public Transform gameParent;
    public UnityEngine.UI.InputField levelSelected; //temp to restart/force load level

    protected int stage = 0;
    protected bool gameForceEnded = false;

	public void Start()
	{
        audioLooper = new AudioLooper();
        GV.gameFlow = this;
        GameManager.Instance.LinkInfoPanel(infoPanel, gameParent);
        panelManager = GameObject.FindObjectOfType<PanelManager>();
        StartNextLoadout();
    }

    private void StartNextLoadout()
    {
        //Start the LessonManager
        panelManager.LoadPanel((PanelID)GV.Current_Flow_Index, LessonFinished);
    }

    public void LessonFinished()
    {
        if(GV.Current_Flow_Index >= GV.Game_Lesson_Max)
            EndOfGameFlow();
        else
            LoadLevel(GV.Current_Flow_Index);
    }

    private void EndOfGameFlow()
    {
        //Stop playing music
        //Send onComplete call
    }

    public void GameFinished(float sucessScore, float timeScore)
    {
        ProgressTracker.Instance.SetScore(ProgressTracker.ScoreType.Success,  GV.Current_Flow_Index, sucessScore);
        ProgressTracker.Instance.SetScore(ProgressTracker.ScoreType.Time, GV.Current_Flow_Index, timeScore);
        ProgressTracker.Instance.SubmitProgress((GV.Current_Flow_Index * 2) + 1); 
        //Then start next level sequence
        InputManager.gameInputActivate = false;
        GameManager.Instance.RecordCurrentLevel();
        GameManager.Instance.UnloadCurrentLevel();
        GV.Current_Flow_Index++;
        StartNextLoadout();
        Debug.Log(string.Format("game finished, [SucessScore,TimeScore]:[{0},{1}] ",sucessScore,timeScore));
    }


    private void LoadLevel(int lvl)
    {
        GameManager.Instance.CreateAndInitializeLevel(lvl);
        InputManager.gameInputActivate = true;
    }


    public void RestartLevelButton()
    {
        int index = int.Parse(levelSelected.text);
        if (index % 2 == 0)     // even: panel
        {
            GV.Current_Flow_Index = (index / 2);
            if (GV.Current_Flow_Index > 0)
            {
                GV.Current_Flow_Index--;
                LoadLevel(GV.Current_Flow_Index);
                GameFinished(1f, 1f);
            }
            else
            {
                GameManager.Instance.UnloadCurrentLevel();
                StartNextLoadout();
            }
        }
        else                    // odd: level
        {
            GV.Current_Flow_Index = (index - 1) / 2;
            panelManager.ClosePanel();
            LoadLevel(GV.Current_Flow_Index);
        }
    }

    public void ToggleNamePressed()
    {
        GV.Molecule_Text_Active = !GV.Molecule_Text_Active;
    }

    public void Update()
    {
        float dt = Time.deltaTime;
        GameManager.Instance.Update(dt);

        if (audioLooper != null)
            audioLooper.Update();

    }


    /*protected float GetTimedRoundScore()
    {
        return Mathf.Clamp01( 1 - ((roundTime - roundTimeToGetFullScore) / (GV.Game_Length[currentLevel] - roundTimeToGetFullScore)));
    }*/
	

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
