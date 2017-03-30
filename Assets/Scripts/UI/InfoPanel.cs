using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanel : MonoBehaviour {

    public Transform reactionParentList;
    public ScoreText scoreText;
    public Text levelText;
    public Text goalText;
    public Text progressText;

    public void SetupLevel(int curLevel)
    {
        SetFormulasVisible(curLevel);
        levelText.text = "LEVEL " + curLevel;
    }

    private void SetFormulasVisible(int curLevel)
    {
        int visibleFields = GV.visibleFormulas[curLevel];
        for(int i = 0; i < reactionParentList.childCount; i++)
            reactionParentList.GetChild(i).gameObject.SetActive(i < visibleFields);
    }

    public void SetTimeRemaining(float time)
    {
        scoreText.SetScoreTime(time);
    }

    private string StateGoal(int lvl) //Yes, this could be generic
    {
        string toOut = "GOAL: ";
        switch(lvl)
        {
            case 0:
                toOut += "Make 8 HCl";
                break;
            case 1:
                toOut += "Make 4 " + GV.MoleculeFormula(GV.MoleculeType.H2O);
                break;
            case 2:
                toOut += "Make 6 NaCl";
                break;
            case 3:
                toOut += "Make 3  " + GV.MoleculeFormula(GV.MoleculeType.H2O);
                break;
        }
        return toOut;
    }
}
