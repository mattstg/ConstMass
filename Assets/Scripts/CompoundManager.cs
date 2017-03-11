using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompoundManager  {
   
    #region Singleton
    private static CompoundManager instance;

    private CompoundManager() { }

    public static CompoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new CompoundManager();
            }
            return instance;
        }
    }
    #endregion

    List<Compound> activeCompounds;
    Transform compoundParent;
    Compound selectedCompound;
    bool compoundIsSelected = false; //doing if(selectedCompound) has minute costs, but ipad...
    //This class pretty much does everything in game

    public void CreateAndInitializeLevel(int lvl)
    {
        activeCompounds = new List<Compound>();
        GameObject go = MonoBehaviour.Instantiate(Resources.Load("Prefabs/Levels/level" + lvl)) as GameObject;
        compoundParent = go.transform;
        foreach(Transform t in compoundParent)
        {
            Compound newCompound = t.GetComponent<Compound>();
            newCompound.Initialize();
            activeCompounds.Add(newCompound);
        }
    }

    public void UnloadCurrentLevel()
    {
        MonoBehaviour.Destroy(compoundParent.gameObject);
        activeCompounds = new List<Compound>();
        compoundParent = null;
    }

    public void Update(float dt)
    {
        foreach(Compound c in activeCompounds)
            c.UpdateMovement(dt);

        //int curCount 
        //Make each active compound float, bounce if near walls, snap back in if out of bounds
        //make any nearby that are not "held" or "onCooldown" merge
            //merge produces new ones, in different directions

    }

    private void CompoundSelected(Compound _selectedCompound)
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
    //Mouse handling

    public void MouseClicked(Vector2 loc)
    {
        if(compoundIsSelected)
            LaunchCurrentSelectedCompound();

        foreach(Compound c in activeCompounds)
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

}
