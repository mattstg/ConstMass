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

    InfoPanel infoPanel;
    Transform gameParent;

    public static List<Molecule> activeCompounds = new List<Molecule>(); //to access everywhere, gets filled by MergeManager
    Molecule selectedCompound;
    bool compoundIsSelected = false; //doing if(selectedCompound) has minute costs, but ipad...
    bool gameRunning = false;
    int curLoadedLevel = 0;
    protected float roundTime = 0;
    public bool currentLevelIsStart = true;
    //This class pretty much does everything in game

    GV.MoleculeType goalMoleculeType;
    int goalMoleculeCount = 0;
    int goalMoleculeRequired = 0;


    public void LinkInfoPanel(InfoPanel _infoPanel, Transform _gameParent)
    {
        infoPanel = _infoPanel;
        gameParent = _gameParent;
    }

    public void MoleculeCreated(Molecule m)
    {
        if (!currentLevelIsStart && m.mtype == goalMoleculeType)
        {
            goalMoleculeCount++;
            infoPanel.UpdateProgressText(goalMoleculeCount, goalMoleculeRequired);
        }
        
        activeCompounds.Add(m);

    }

    public void MoleculeRemoved(Molecule m)
    {

        if (!currentLevelIsStart && m.mtype == goalMoleculeType) //if you are bored try commenting out "!currentLevelIsStart &&"
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
        if(!currentLevelIsStart)  //it activates the launcher then deletes when scene changes, causes issue
            Launcher.Instance.ClearLauncher();
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

            roundTime -= dt;
            roundTime = Mathf.Clamp(roundTime, 0, 512);
            infoPanel.SetTimeRemaining(roundTime);

            GameEndCheck(dt);
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
       if (goalMoleculeCount >= goalMoleculeRequired || roundTime <= 0)
           EndGame();
    }

    private void EndGame()
    {
        gameRunning = false;
        float gameScore = Mathf.Clamp01((float)goalMoleculeCount / (float)goalMoleculeRequired);
        float timeScore = CalculateTimeScore(roundTime);
        RecordProgressAndTime();
        roundTime = 99;
        infoPanel.gameObject.SetActive(false);
        gameParent.gameObject.SetActive(false);
        GV.gameFlow.GameFinished(gameScore, timeScore);
    }

    public void SetupGameWinCondition(int lvl)
    {
        roundTime = GV.Game_Length[lvl];
        goalMoleculeCount = 0;
        switch (lvl)
        {
            case 0:
                goalMoleculeType = GV.MoleculeType.HCl;
                goalMoleculeRequired = 8;
                break;
            case 1:
                goalMoleculeType = GV.MoleculeType.H2O;
                goalMoleculeRequired = 4;
                break;
            case 2:
                goalMoleculeType = GV.MoleculeType.NaCl;
                goalMoleculeRequired = 6;
                break;
            case 3:
                goalMoleculeType = GV.MoleculeType.H2O;
                goalMoleculeRequired = 3;
                break;
        }
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
        compoundIsSelected = false;
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
