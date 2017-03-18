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

    public static List<Molecule> activeCompounds; //to access everywhere, gets filled by MergeManager
    Molecule selectedCompound;
    bool compoundIsSelected = false; //doing if(selectedCompound) has minute costs, but ipad...
    bool levelLoaded = false;
    //This class pretty much does everything in game

    public void CreateAndInitializeLevel(int lvl)
    {
        if (levelLoaded)
            UnloadCurrentLevel();
        activeCompounds = new List<Molecule>();
        GameObject go = MonoBehaviour.Instantiate(Resources.Load("Prefabs/Levels/level" + lvl)) as GameObject;
        Debug.Log("load: " + "Prefabs/Levels/level" + lvl);
        foreach(Transform t in go.transform)
            MergeManager.Instance.CreateMolecule(t.GetComponent<MoleculeEditorLoader>().moleculeType, t.position);
        MonoBehaviour.Destroy(go);
        levelLoaded = true;
    }

    public void UnloadCurrentLevel()
    {
        foreach (Transform t in GV.MoleculeParent)
            MonoBehaviour.Destroy(t.gameObject);
        activeCompounds = new List<Molecule>();
        MergeManager.Instance.ClearMergeManager();
        levelLoaded = false;
    }

    public void Update(float dt)
    {
        //foreach(Compound c in activeCompounds)
           

        //int curCount 
        //Make each active compound float, bounce if near walls, snap back in if out of bounds
        //make any nearby that are not "held" or "onCooldown" merge
            //merge produces new ones, in different directions

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
            if (MathHelper.ApproxDist(c.transform.position, loc) < GV.Mouse_Selection_Distance)
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
