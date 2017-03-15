using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeManager
{
    #region Singleton
    private static MergeManager instance;

    private MergeManager() { }

    public static MergeManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new MergeManager();
            }
            return instance;
        }
    }
    #endregion

    List<MergeStruct> activeMergers = new List<MergeStruct>();

    public void MergeTwoMolecules(Molecule m1, Molecule m2)
    {
        MergeStruct ms = new MergeStruct(m1, m2);
        activeMergers.Add(ms);
    }

    public void UpdateMerger(float dt)
    {
        for(int i = activeMergers.Count - 1; i >= 0; i--)
        {
            if (!activeMergers[i].completed)
                activeMergers[i].UpdateMerge(dt); 
            else
                activeMergers.RemoveAt(i);
        }
    }

    public void SplitMolecule(Molecule m) //from heat
    {

    }

    private void MergeComplete(GV.MoleculeType m1, GV.MoleculeType m2, Vector2 pos)
    {
        List<GV.MoleculeType> toCreate = MoleculeDict.Instance.GetProducts(m1, m2);
        foreach (GV.MoleculeType mtype in toCreate)
            CreateMolecule(mtype, pos);
    }

    public void CreateMolecule(GV.MoleculeType mtype, Vector2 pos)
    {
        GameObject newMole = MonoBehaviour.Instantiate(Resources.Load("Prefabs/Molecules/" + mtype)) as GameObject;
        newMole.transform.position = pos;
        newMole.transform.SetParent(GV.MoleculeParent);
        Molecule toRet = newMole.GetComponent<Molecule>();
        GameManager.activeCompounds.Add(toRet);
        toRet.Initialize(mtype);
    }

    public void ClearMergeManager() //level finished
    {
        activeMergers = new List<MergeStruct>();
    }

    private class MergeStruct
    {
        Molecule m1, m2;
        Vector2 opos1, opos2, center; //original pos
        GameObject mergeProtector;
        float timeRemaining = GV.Merge_Time;
        public bool completed = false;

        public MergeStruct(Molecule _m1, Molecule _m2)
        {
            m1 = _m1;
            m2 = _m2;
            opos1 = m1.transform.position;
            opos2 = m2.transform.position;
            center = Vector2.Lerp(opos1, opos2, .5f);
            m1.DestroyRbAndColi();
            m2.DestroyRbAndColi();
            mergeProtector = new GameObject();
            CircleCollider2D c2d = mergeProtector.AddComponent<CircleCollider2D>();
            c2d.radius = GV.Merge_Safe_Zone_Radius;
            mergeProtector.transform.position = center;
        }

        public void UpdateMerge(float dt) //do only during fixed update?
        {
            if (completed)
                return;

            timeRemaining -= dt;
            if (timeRemaining > 0)
            {
                m1.transform.position = Vector2.Lerp(center, opos1 , timeRemaining / GV.Merge_Time);
                m2.transform.position = Vector2.Lerp(center, opos2, timeRemaining / GV.Merge_Time);
            }
            else
            {
                MergeManager.instance.MergeComplete(m1.mtype, m2.mtype, center);
                GameManager.activeCompounds.Remove(m1);
                GameManager.activeCompounds.Remove(m2);
                MonoBehaviour.Destroy(m1.gameObject);
                MonoBehaviour.Destroy(m2.gameObject);
                MonoBehaviour.Destroy(mergeProtector);
                mergeProtector = null;
                completed = true;
            }

        }
    }
}
