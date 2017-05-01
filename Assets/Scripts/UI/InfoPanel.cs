using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanel : MonoBehaviour {

    public List<ReactionEntry> reactions;
    public ScoreText scoreText;
    public Text goalText;
    public Text progressText;
    public ProgressFill progressBar;
    public CanvasButton atomTextButton;
    public CanvasButton pauseButton;
    int visibleFields;

    public void SetupLevel(int curLevel)
    {
        visibleFields = GV.visibleFormulas[curLevel];
        SetFormulasVisible(curLevel);
        UpdateReactionTexts();
        progressBar.SetPresentFill(0);
        foreach (ReactionEntry r in reactions)
            r.InitiateHighlight();
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

    public void UpdateProgress(int progress, int maxProgress)
    {
        progressText.text = progress.ToString();
        float progressQuotient = (float)progress / (float)maxProgress;
        progressBar.SetTargetFill(progressQuotient, GV.Progress_Bar_Duration);
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

    public string MoleculeFormula(GV.MoleculeType m, bool isProduct, int coefficient = 1)
    {
        return GV.ColoredMoleculeFormula(m, isProduct, coefficient);
    }

    public string ReactionFormula(int id)
    {
        string formula = "";
        switch (id)
        {
            case 0:     //* H₂ + Cl₂  →  2HCl
                formula = string.Format("{0} + {1}   →   {2}",
                    MoleculeFormula(GV.MoleculeType.H2, false), MoleculeFormula(GV.MoleculeType.Cl2, false), MoleculeFormula(GV.MoleculeType.HCl, true, 2));
                break;
            case 1:     //  NaHCO₃ + HCl  →  NaCl + CO₂ + H₂O
                formula = string.Format("{0} + {1}   →\n{2} + {3} + {4}",
                    MoleculeFormula(GV.MoleculeType.NaHCO3, false), MoleculeFormula(GV.MoleculeType.HCl, false), MoleculeFormula(GV.MoleculeType.NaCl, true), MoleculeFormula(GV.MoleculeType.CO2, true), MoleculeFormula(GV.MoleculeType.H2O, true));
                break;
            case 2:     //  Na₂O + H₂O  →  2NaOH
                formula = string.Format("{0} + {1}   →\n{2}",
                    MoleculeFormula(GV.MoleculeType.Na2O, false), MoleculeFormula(GV.MoleculeType.H2O, false), MoleculeFormula(GV.MoleculeType.NaOH, true, 2));
                break;
            case 3:     //  NaOH + HCl  →  NaCl + H₂O
                formula = string.Format("{0} + {1}   →\n{2} + {3}",
                    MoleculeFormula(GV.MoleculeType.NaOH, false), MoleculeFormula(GV.MoleculeType.HCl, false), MoleculeFormula(GV.MoleculeType.NaCl, true), MoleculeFormula(GV.MoleculeType.H2O, true));
                break;
            case 4:     //* H₂O + K₂O  →  2KOH
                formula = string.Format("{0} + {1}   →   {2}",
                    MoleculeFormula(GV.MoleculeType.H2O, false), MoleculeFormula(GV.MoleculeType.K2O, false), MoleculeFormula(GV.MoleculeType.KOH, true, 2));
                break;
            case 5:     //  KOH + HCl  →  KCl + H₂O
                formula = string.Format("{0} + {1}   →\n{2} + {3}",
                    MoleculeFormula(GV.MoleculeType.KOH, false), MoleculeFormula(GV.MoleculeType.HCl, false), MoleculeFormula(GV.MoleculeType.KCl, true), MoleculeFormula(GV.MoleculeType.H2O, true));
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
            reactions[i].text.text = ReactionFormula(i);
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
            reactions[0].NewOccurrence();
        else if ((m1 == GV.MoleculeType.NaHCO3 && m2 == GV.MoleculeType.HCl) || (m2 == GV.MoleculeType.NaHCO3 && m1 == GV.MoleculeType.HCl))
            reactions[1].NewOccurrence();
        else if ((m1 == GV.MoleculeType.Na2O && m2 == GV.MoleculeType.H2O) || (m2 == GV.MoleculeType.Na2O && m1 == GV.MoleculeType.H2O))
            reactions[2].NewOccurrence();
        else if ((m1 == GV.MoleculeType.NaOH && m2 == GV.MoleculeType.HCl) || (m2 == GV.MoleculeType.NaOH && m1 == GV.MoleculeType.HCl))
            reactions[3].NewOccurrence();
        else if ((m1 == GV.MoleculeType.H2O && m2 == GV.MoleculeType.K2O) || (m2 == GV.MoleculeType.H2O && m1 == GV.MoleculeType.K2O))
            reactions[4].NewOccurrence();
        else if ((m1 == GV.MoleculeType.KOH && m2 == GV.MoleculeType.HCl) || (m2 == GV.MoleculeType.KOH && m1 == GV.MoleculeType.HCl))
            reactions[5].NewOccurrence();
    }
}
