using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanel : MonoBehaviour {

    public List<ReactionEntry> reactions;
    public ScoreText scoreText;
    public Text levelText;
    public Text goalText;
    public Text progressText;
    public CanvasButton atomTextButton;
    public CanvasButton pauseButton;
    int visibleFields;

    public void SetupLevel(int curLevel)
    {
        visibleFields = GV.visibleFormulas[curLevel];
        SetFormulasVisible(curLevel);
        UpdateReactionTexts();
        foreach (ReactionEntry r in reactions)
            r.InitiateHighlight();
        levelText.text = "LEVEL " + (curLevel + 1);
        //StateGoal(curLevel);
        GV.SelectedMolecule = GV.MoleculeType.None;
        if (pauseButton && pauseButton.IsSelected() != GV.Paused)
            pauseButton.SetSelected(GV.Paused, false);
        if (atomTextButton && atomTextButton.IsSelected() != GV.Atom_Text_Active)
            atomTextButton.SetSelected(GV.Atom_Text_Active, false);
    }

    private void SetFormulasVisible(int curLevel)
    {
        for(int i = 0; i < reactions.Count; i++)
            reactions[i].gameObject.SetActive(i < visibleFields);
    }

    public void UpdateProgressText(int progress, int maxProgress)
    {
        progressText.text = "PROGRESS:  " + progress + " / " + maxProgress;
    }

    public void SetTimeRemaining(float time)
    {
        scoreText.SetScoreTime(time);
    }

    // integrated with GameManager.SetupGameWinCondition()
    /*private void StateGoal(int lvl) //Yes, this could be generic
    {
        string toOut = "GOAL:  ";
        switch(lvl)
        {
            case 0:
                GV.GoalMolecule = GV.MoleculeType.HCl;
                toOut += "Make 8 " + GV.MoleculeFormula(goalMolecule);
                break;
            case 1:
                GV.GoalMolecule = GV.MoleculeType.H2O;
                toOut += "Make 4 " + GV.MoleculeFormula(goalMolecule);
                break;
            case 2:
                GV.GoalMolecule = GV.MoleculeType.NaCl;
                toOut += "Make 6 " + GV.MoleculeFormula(goalMolecule);
                break;
            case 3:
                GV.GoalMolecule = GV.MoleculeType.H2O;
                toOut += "Make 3 " + GV.MoleculeFormula(goalMolecule);
                break;
        }
        goalText.text = toOut;
    }*/

    public string MoleculeText(GV.MoleculeType m, bool isProduct, int coefficient = 1)
    {
        string openMarkup = "";
        string closeMarkup = "";
        string moleculeFormula = (coefficient > 1) ? coefficient.ToString() : "";
        moleculeFormula += GV.MoleculeFormula(m);

        if (m == GV.SelectedMolecule && !isProduct)
        {
            openMarkup = "<color=#8DC6FF>";
            closeMarkup = "</color>";
        }
        if (m == GV.GoalMolecule && isProduct)
        {
            openMarkup = "<color=#77FF77>";
            closeMarkup = "</color>";
        }
        return openMarkup + moleculeFormula + closeMarkup;
    }

    public string ReactionText(int id)
    {
        string formula = "";
        switch (id)
        {
            case 0:     //* H₂ + Cl₂  →  2HCl
                formula = string.Format("{0} + {1}  →  {2}",
                    MoleculeText(GV.MoleculeType.H2, false), MoleculeText(GV.MoleculeType.Cl2, false), MoleculeText(GV.MoleculeType.HCl, true, 2));
                break;
            case 1:     //  NaHCO₃ + HCl  →  NaCl + CO₂ + H₂O
                formula = string.Format("{0} + {1}  →\n{2} + {3} + {4}",
                    MoleculeText(GV.MoleculeType.NaHCO3, false), MoleculeText(GV.MoleculeType.HCl, false), MoleculeText(GV.MoleculeType.NaCl, true), MoleculeText(GV.MoleculeType.CO2, true), MoleculeText(GV.MoleculeType.H2O, true));
                break;
            case 2:     //  Na₂O + H₂O  →  2NaOH
                formula = string.Format("{0} + {1}  →\n{2}",
                    MoleculeText(GV.MoleculeType.Na2O, false), MoleculeText(GV.MoleculeType.H2O, false), MoleculeText(GV.MoleculeType.NaOH, true, 2));
                break;
            case 3:     //  NaOH + HCl  →  NaCl + H₂O
                formula = string.Format("{0} + {1}  →\n{2} + {3}",
                    MoleculeText(GV.MoleculeType.NaOH, false), MoleculeText(GV.MoleculeType.HCl, false), MoleculeText(GV.MoleculeType.NaCl, true), MoleculeText(GV.MoleculeType.H2O, true));
                break;
            case 4:     //* H₂O + K₂O  →  2KOH
                formula = string.Format("{0} + {1}  →  {2}",
                    MoleculeText(GV.MoleculeType.H2O, false), MoleculeText(GV.MoleculeType.K2O, false), MoleculeText(GV.MoleculeType.KOH, true, 2));
                break;
            case 5:     //  KOH + HCl  →  KCl + H₂O
                formula = string.Format("{0} + {1}  →\n{2} + {3}",
                    MoleculeText(GV.MoleculeType.KOH, false), MoleculeText(GV.MoleculeType.HCl, false), MoleculeText(GV.MoleculeType.KCl, true), MoleculeText(GV.MoleculeType.H2O, true));
                break;
            default:
                break;
        }
        return formula;
    }

    public void UpdateReactionTexts()
    {
        for (int i = 0; i < visibleFields; i++)
        {
            reactions[i].text.text = ReactionText(i);
        }
    }

    public void UpdateReactionHighlights(float dt)
    {
        for (int i = 0; i < visibleFields; i++)
        {
            reactions[i].UpdateHighlight(dt);
        }
    }

    public void ReactionOccurrence(GV.MoleculeType m1, GV.MoleculeType m2)
    {
        if ((m1 == GV.MoleculeType.H2 && m2 == GV.MoleculeType.Cl2) || (m2 == GV.MoleculeType.H2 && m1 == GV.MoleculeType.Cl2))
            reactions[0].newOccurrence = true;
        else if ((m1 == GV.MoleculeType.NaHCO3 && m2 == GV.MoleculeType.HCl) || (m2 == GV.MoleculeType.NaHCO3 && m1 == GV.MoleculeType.HCl))
            reactions[1].newOccurrence = true;
        else if ((m1 == GV.MoleculeType.Na2O && m2 == GV.MoleculeType.H2O) || (m2 == GV.MoleculeType.Na2O && m1 == GV.MoleculeType.H2O))
            reactions[2].newOccurrence = true;
        else if ((m1 == GV.MoleculeType.NaOH && m2 == GV.MoleculeType.HCl) || (m2 == GV.MoleculeType.NaOH && m1 == GV.MoleculeType.HCl))
            reactions[3].newOccurrence = true;
        else if ((m1 == GV.MoleculeType.H2O && m2 == GV.MoleculeType.K2O) || (m2 == GV.MoleculeType.H2O && m1 == GV.MoleculeType.K2O))
            reactions[4].newOccurrence = true;
        else if ((m1 == GV.MoleculeType.KOH && m2 == GV.MoleculeType.HCl) || (m2 == GV.MoleculeType.KOH && m1 == GV.MoleculeType.HCl))
            reactions[5].newOccurrence = true;
    }
}
