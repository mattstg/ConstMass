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
    float winTimeChecker = 3;
    protected float roundTime = 0;
    //This class pretty much does everything in game

    public void LinkInfoPanel(InfoPanel _infoPanel, Transform _gameParent)
    {
        infoPanel = _infoPanel;
        gameParent = _gameParent;
    }

    public void RecordCurrentLevel()
    {
        List<GV.MoleculeType> endLevelMoleList = new List<GV.MoleculeType>();
        foreach(Molecule m in activeCompounds)
            endLevelMoleList.Add(m.mtype);
        endLevelMoleList.Sort();
        LevelMoleculeTracker.Instance.RecordLevel(GV.Current_Flow_Index, endLevelMoleList);
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
        roundTime = GV.Game_Length[lvl];
        infoPanel.gameObject.SetActive(true);
        gameParent.gameObject.SetActive(true);
        infoPanel.SetupLevel(curLoadedLevel);
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

    private void GameEndCheck(float dt)
    {
        if (winTimeChecker <= 0)
        {
            if (GameManager.Instance.IsGameOver())
            {
                EndGame();
                return;
            }
            winTimeChecker = 3;
        }
        else
        {
            winTimeChecker -= dt;
        }

        if(roundTime <= 0)
        {
            EndGame();
            return;
        }
    }

    private void EndGame()
    {
        gameRunning = false;
        float gameScore = CalculateGameScore();
        float timeScore = CalculateTimeScore(roundTime);
        roundTime = 99;
        infoPanel.gameObject.SetActive(false);
        gameParent.gameObject.SetActive(false);
        GV.gameFlow.GameFinished(gameScore, timeScore);
    }

    public bool IsGameOver()
    {
        int[] moleculeCount = new int[GV.Molecule_Enum_Count];
        foreach(Molecule c in activeCompounds)
            moleculeCount[(int)c.mtype]++;

        switch(GV.Current_Flow_Index)
        {
            case 0:
                return moleculeCount[(int)GV.MoleculeType.HCl] >= 8;
            case 1:
                return moleculeCount[(int)GV.MoleculeType.H2O] >= 4;
            case 2:
                return moleculeCount[(int)GV.MoleculeType.NaCl] >= 6;
            case 3:
                return moleculeCount[(int)GV.MoleculeType.H2O] >= 3;
        }
        return false;
    }

    public float CalculateGameScore()
    {
        int[] moleculeCount = new int[GV.Molecule_Enum_Count];
        foreach (Molecule c in activeCompounds)
            moleculeCount[(int)c.mtype]++;

        switch (GV.Current_Flow_Index)
        {
            case 0:
                return moleculeCount[(int)GV.MoleculeType.HCl] / 8f;
            case 1:
                return moleculeCount[(int)GV.MoleculeType.H2O] / 4f;
            case 2:
                return moleculeCount[(int)GV.MoleculeType.NaCl] / 6f;
            case 3:
                return moleculeCount[(int)GV.MoleculeType.H2O] / 3f;
        }
        return 0;
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
