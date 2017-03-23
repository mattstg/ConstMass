using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LoLSDK;

public class BreakdownEntry : MonoBehaviour
{
    public Text formulaText;
    public Image image;
    public Text massText;
    public Text totalAtomsText;
    public Text hydrogenText;
    public Text carbonText;
    public Text oxygenText;
    public Text sodiumText;
    public Text chlorineText;
    public Text potassiumText;

    public BreakdownMolecule bm = new BreakdownMolecule();

    public void SetMoleculeType(GV.MoleculeType type)
    {
        bm.SetMoleculeType(type);
        UpdateText();
    }

    public void UpdateText()
    {
        if (formulaText)
            formulaText.text = GV.MoleculeFormula(bm.moleculeType);

        massText.text = bm.mass.ToString();
        totalAtomsText.text = bm.totalAtoms.ToString();

        hydrogenText.text = (bm.hydrogen != 0) ? bm.hydrogen.ToString() : "";
        carbonText.text = (bm.carbon != 0) ? bm.carbon.ToString() : "";
        oxygenText.text = (bm.oxygen != 0) ? bm.oxygen.ToString() : "";
        sodiumText.text = (bm.sodium != 0) ? bm.sodium.ToString() : "";
        chlorineText.text = (bm.chlorine != 0) ? bm.chlorine.ToString() : "";
        potassiumText.text = (bm.potassium != 0) ? bm.potassium.ToString() : "";
    }
}
