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
        //Make each active compound float, bounce if near walls, snap back in if out of bounds
        //make any nearby that are not "held" or "onCooldown" merge
            //merge produces new ones, in different directions

    }

    private void UpdateCompoundPosition(Compound compound)
    {

    }

}
