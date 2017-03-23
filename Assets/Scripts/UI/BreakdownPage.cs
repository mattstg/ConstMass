using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LoLSDK;

public class BreakdownPage : Page
{
    public RectTransform entriesParent;
    public BreakdownEntry preTotalEntry;
    public BreakdownEntry postTotalEntry;

    public int level = 0;
    public bool preGame = true;

    protected List<BreakdownMolecule> preMolecules = new List<BreakdownMolecule>();
    protected List<BreakdownMolecule> postMolecules = new List<BreakdownMolecule>();
    protected List<BreakdownEntry> entries;
    protected string entryPrefabPath = "Prefabs/UI/Elements/BreakdownEntry";

    public void SetLevel(int level, bool preGame)
    {
        this.level = level;
        this.preGame = preGame;
        UpdateMolecules();
        UpdateChart();
    }

    public void UpdateMolecules()
    {
        List<GV.MoleculeType> pre = LevelMoleculeTracker.Instance.GetMoleculeList(level, true);
        List<GV.MoleculeType> post = LevelMoleculeTracker.Instance.GetMoleculeList(level, false);
        for (int i = 0; i < pre.Count; i++)
        {
            BreakdownMolecule bm = new BreakdownMolecule();
            bm.SetMoleculeType(pre[i]);
            preMolecules.Add(bm);
        }
        for (int i = 0; i < post.Count; i++)
        {
            BreakdownMolecule bm = new BreakdownMolecule();
            bm.SetMoleculeType(post[i]);
            postMolecules.Add(bm);
        }
    }

    public void UpdateChart()
    {
        ClearEntries();
        List<BreakdownMolecule> entriesMolecules;
        if (preGame)
            entriesMolecules = preMolecules;
        else
            entriesMolecules = postMolecules;
        foreach (BreakdownMolecule bm in entriesMolecules)
        {
            GameObject go = Object.Instantiate(Resources.Load(entryPrefabPath)) as GameObject;
            go.transform.SetParent(entriesParent, false);
            BreakdownEntry breakdownEntry = go.GetComponent<BreakdownEntry>();
            breakdownEntry.bm = bm;
            breakdownEntry.UpdateText();
            entries.Add(breakdownEntry);
        }
        UpdateTotalBefore();
        if (preGame)
        {
            postTotalEntry.gameObject.SetActive(false);
            foreach (Transform t in preTotalEntry.transform)
            {
                Text text = t.GetComponent<Text>();
                if (text)
                    text.font = Resources.Load<Font>("Graphics/UI/Fonts/s.ui.semibold");
            }
        }
        else
        {
            UpdateTotalAfter();
            postTotalEntry.gameObject.SetActive(true);
            foreach (Transform t in postTotalEntry.transform)
            {
                Text text = t.GetComponent<Text>();
                if (text)
                    text.font = Resources.Load<Font>("Graphics/UI/Fonts/s.ui.semibold");
            }
        }
    }

    public void UpdateTotalBefore()
    {
        BreakdownMolecule total = new BreakdownMolecule();
        foreach (BreakdownMolecule bm in preMolecules)
        {
            total.mass += bm.mass;
            total.totalAtoms += bm.totalAtoms;
            total.hydrogen += bm.hydrogen;
            total.carbon += bm.carbon;
            total.oxygen += bm.oxygen;
            total.sodium += bm.sodium;
            total.chlorine += bm.chlorine;
            total.potassium += bm.potassium;
        }
        preTotalEntry.bm = total;
        preTotalEntry.UpdateText();
    }

    public void UpdateTotalAfter()
    {
        BreakdownMolecule total = new BreakdownMolecule();
        foreach (BreakdownMolecule bm in postMolecules)
        {
            total.mass += bm.mass;
            total.totalAtoms += bm.totalAtoms;
            total.hydrogen += bm.hydrogen;
            total.carbon += bm.carbon;
            total.oxygen += bm.oxygen;
            total.sodium += bm.sodium;
            total.chlorine += bm.chlorine;
            total.potassium += bm.potassium;
        }
        postTotalEntry.bm = total;
        postTotalEntry.UpdateText();
    }

    public void ClearEntries()
    {
        entries = new List<BreakdownEntry>();
        foreach (Transform t in entriesParent)
            Destroy(t.gameObject);
    }
}
