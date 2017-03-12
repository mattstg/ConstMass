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
    Transform compoundParent;
    Molecule selectedCompound;
    bool compoundIsSelected = false; //doing if(selectedCompound) has minute costs, but ipad...
    //This class pretty much does everything in game

    public void CreateAndInitializeLevel(int lvl)
    {
        activeCompounds = new List<Molecule>();
        GameObject go = MonoBehaviour.Instantiate(Resources.Load("Prefabs/Levels/level" + lvl)) as GameObject;
        compoundParent = go.transform;
        foreach(Transform t in compoundParent)
            MergeManager.Instance.CreateMolecule(t.GetComponent<MoleculeEditorLoader>().moleculeType, t.position);
        MonoBehaviour.Destroy(go);
    }

    public void UnloadCurrentLevel()
    {
        MonoBehaviour.Destroy(compoundParent.gameObject);
        activeCompounds = new List<Molecule>();
        compoundParent = null;
    }

    public void Update(float dt)
    {
        //foreach(Compound c in activeCompounds)
           

        //int curCount 
        //Make each active compound float, bounce if near walls, snap back in if out of bounds
        //make any nearby that are not "held" or "onCooldown" merge
            //merge produces new ones, in different directions

    }

    private void CompoundSelected(Molecule _selectedCompound)
    {
        selectedCompound = _selectedCompound;
        selectedCompound.SetLock(true);
        compoundIsSelected = true;
        //Setup the arrow and stuff here
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
        if(compoundIsSelected)
            LaunchCurrentSelectedCompound();

        foreach(Molecule c in activeCompounds)
        {
            if (MathHelper.ApproxDist(c.transform.position, loc) < GV.Mouse_Selection_Distance)
            {
                CompoundSelected(c);
                break;
            }
        }
    }
    

    public void MouseHeld(Vector2 loc)
    {
        if(compoundIsSelected)
        {
            //Do the arrow stuff
        }
    }

    public void MouseReleased(Vector2 loc)
    {
        if (compoundIsSelected)
            LaunchCurrentSelectedCompound();
    }
    #endregion

}
