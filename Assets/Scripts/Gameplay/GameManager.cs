using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager  {
   
    #region Singleton
    private static GameManager instance;

    private GameManager() { }

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameManager();
            }
            return instance;
        }
    }
    #endregion

    public InfoPanel infoPanel;
    Transform gameParent;

    public static List<Molecule> activeCompounds = new List<Molecule>(); //to access everywhere, gets filled by MergeManager
    Molecule selectedCompound;
    bool gameRunning = false;
    bool completionDelaying = false;
    float completionDelay = 0;
    int curLoadedLevel = 0;
    protected float roundTime = 0;
    public bool currentLevelIsStart = false;
    //This class pretty much does everything in game

    int goalMoleculeCount = 0;
    int goalMoleculeRequired = 0;


    public void LinkInfoPanel(InfoPanel _infoPanel, Transform _gameParent)
    {
        infoPanel = _infoPanel;
        gameParent = _gameParent;
    }

    public void MoleculeCreated(Molecule m)
    {
        if (!currentLevelIsStart && m.mtype == GV.GoalMolecule)
        {
            LOLAudio.Instance.PlayAudio("CorrectMolecule.wav");
            goalMoleculeCount++;
            infoPanel.UpdateProgressText(goalMoleculeCount, goalMoleculeRequired);
        }
        
        activeCompounds.Add(m);

    }

    public void MoleculeRemoved(Molecule m)
    {

        if (!currentLevelIsStart && m.mtype == GV.GoalMolecule) //if you are bored try commenting out "!currentLevelIsStart &&"
        {
            goalMoleculeCount--;
            infoPanel.UpdateProgressText(goalMoleculeCount, goalMoleculeRequired);
        }
        GameManager.activeCompounds.Remove(m);
        MonoBehaviour.Destroy(m.gameObject);
    }

    public void RecordCurrentLevel()
    {
        List<GV.MoleculeType> endLevelMoleList = new List<GV.MoleculeType>();
        foreach(Molecule m in activeCompounds)
            endLevelMoleList.Add(m.mtype);
        endLevelMoleList.Sort();
        LevelMoleculeTracker.Instance.RecordLevel(GV.Current_Flow_Index, endLevelMoleList);
    }

    public void RecordProgressAndTime()
    {
        ProgressTracker.Instance.SetProgressAndTime(GV.Current_Flow_Index, goalMoleculeCount, roundTime);
    }

    public void InitializeStartLevel()
    {
        currentLevelIsStart = true;
        activeCompounds = new List<Molecule>();
        GameObject go = MonoBehaviour.Instantiate(Resources.Load("Prefabs/Levels/StartLevel")) as GameObject;
        foreach (Transform t in go.transform)
            MergeManager.Instance.CreateMolecule(t.GetComponent<MoleculeEditorLoader>().moleculeType, t.position);
        MonoBehaviour.Destroy(go);
        gameRunning = true;
        

    }

    public void CreateAndInitializeLevel(int lvl)
    {
        if (gameRunning)
            UnloadCurrentLevel();
        activeCompounds = new List<Molecule>();
        GameObject go = MonoBehaviour.Instantiate(Resources.Load("Prefabs/Levels/level" + lvl)) as GameObject;
        foreach(Transform t in go.transform)
            MergeManager.Instance.CreateMolecule(t.GetComponent<MoleculeEditorLoader>().moleculeType, t.position);
        MonoBehaviour.Destroy(go);
        gameRunning = true;
        curLoadedLevel = GV.Current_Flow_Index;
        SetupGameWinCondition(GV.Current_Flow_Index);        
        infoPanel.gameObject.SetActive(true);
        gameParent.gameObject.SetActive(true);
        infoPanel.SetupLevel(curLoadedLevel);
        infoPanel.UpdateProgressText(goalMoleculeCount, goalMoleculeRequired);
    }

    public void UnloadCurrentLevel()
    {
        foreach (Transform t in GV.MoleculeParent)
            MonoBehaviour.Destroy(t.gameObject);
        activeCompounds = new List<Molecule>();
        MergeManager.Instance.ClearMergeManager();
        Launcher.Instance.ClearLauncher();
        gameRunning = false;
    }

    public void UnloadMainMenu() //
    {
        activeCompounds = new List<Molecule>();
        MergeManager.Instance.ClearMergeManager();
        gameRunning = false;
    }

    public void Update(float dt)
    {
        if (gameRunning)
        {
            Launcher.Instance.Update(dt);

            foreach (Molecule c in activeCompounds)
                c.UpdateMolecule();

            MergeManager.Instance.UpdateMerger(dt);

            if (!GV.Paused)
            {
                if (!completionDelaying)
                {
                    roundTime -= dt;
                    roundTime = Mathf.Clamp(roundTime, 0, 512);
                    infoPanel.SetTimeRemaining(roundTime);
                    GameEndCheck(dt);
                }
                else
                {
                    completionDelay -= dt;
                    if (completionDelay <= 0)
                    {
                        completionDelaying = false;
                        EndGame();
                    }
                }
            }
        }
    }

    public void StartScreenUpdate(float dt)
    {
        if (gameRunning)
        {
            foreach (Molecule c in activeCompounds)
                c.UpdateMolecule();

            MergeManager.Instance.UpdateMerger(dt);
        }
    }

    private void GameEndCheck(float dt)
    {
        if (goalMoleculeCount >= goalMoleculeRequired)
        {
            RecordCompletion();
            completionDelay = GV.Completion_Delay;
            completionDelaying = true;
        }
        else if (roundTime <= 0)
        {
            RecordCompletion();
            EndGame();
        }
    }

    private void RecordCompletion()
    {
        float gameScore = Mathf.Clamp01((float)goalMoleculeCount / (float)goalMoleculeRequired);
        float timeScore = CalculateTimeScore(roundTime);
        RecordProgressAndTime();
        GV.gameFlow.GameCompleted(gameScore, timeScore);
    }

    private void EndGame()
    {
        gameRunning = false;
        roundTime = 99;
        infoPanel.gameObject.SetActive(false);
        gameParent.gameObject.SetActive(false);
        GV.gameFlow.UnloadGame();
    }

    public void SetupGameWinCondition(int lvl)
    {
        roundTime = GV.Game_Length[lvl];
        goalMoleculeCount = 0;
        switch (lvl)
        {
            case 0:
                GV.GoalMolecule = GV.MoleculeType.HCl;
                goalMoleculeRequired = 8;
                break;
            case 1:
                GV.GoalMolecule = GV.MoleculeType.H2O;
                goalMoleculeRequired = 4;
                break;
            case 2:
                GV.GoalMolecule = GV.MoleculeType.NaCl;
                goalMoleculeRequired = 6;
                break;
            case 3:
                GV.GoalMolecule = GV.MoleculeType.H2O;
                goalMoleculeRequired = 3;
                break;
        }
        infoPanel.goalText.text = string.Format("GOAL:  Make {0} {1}", goalMoleculeRequired, GV.MoleculeFormula(GV.GoalMolecule));
    }

    public float CalculateTimeScore(float _roundTime)
    {
        float secondsFor100 = GV.Time_Score_Perc_For_Max * GV.Game_Length[GV.Current_Flow_Index];
        float perc = Mathf.Clamp01(_roundTime/secondsFor100);
        return perc;
    }


    private void LaunchCurrentSelectedCompound()
    {
        selectedCompound.SetLock(false);
        selectedCompound = null;
    }

    //Mouse Code
    #region MouseHandling

    public void MouseClicked(Vector2 loc)
    {
        foreach(Molecule c in activeCompounds)
        {
            if (MathHelper.ApproxDist(c.transform.position, loc) <= GV.Mouse_Selection_Distance && c.IsSelectable())
            {
                Launcher.Instance.SetupLaunch(c);
                break;
            }
        }
    }
    

    public void MouseHeld(Vector2 loc)
    {
        Launcher.Instance.UpdateMousePosition(loc);
    }

    public void MouseReleased(Vector2 loc)
    {
        Launcher.Instance.ReleaseMouse(loc);
    }
    #endregion
  
}
