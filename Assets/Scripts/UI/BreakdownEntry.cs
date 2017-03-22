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

    public GV.MoleculeType moleculeType;

    [HideInInspector]
    public double mass = 0;
    [HideInInspector]
    public int totalAtoms = 0;
    [HideInInspector]
    public int hydrogen = 0;
    [HideInInspector]
    public int carbon = 0;
    [HideInInspector]
    public int oxygen = 0;
    [HideInInspector]
    public int sodium = 0;
    [HideInInspector]
    public int chlorine = 0;
    [HideInInspector]
    public int potassium = 0;

    public void SetMoleculeType(GV.MoleculeType type)
    {
        moleculeType = type;
        SetFormula(GV.MoleculeFormula(moleculeType));
        switch (moleculeType)
        {
            case GV.MoleculeType.Cl2:
                SetValues(0, 0, 0, 0, 2, 0);
                break;
            case GV.MoleculeType.CO2:
                SetValues(0, 1, 2, 0, 0, 0);
                break;
            case GV.MoleculeType.H2:
                SetValues(2, 0, 0, 0, 0, 0);
                break;
            case GV.MoleculeType.H2O:
                SetValues(2, 0, 1, 0, 0, 0);
                break;
            case GV.MoleculeType.HCl:
                SetValues(1, 0, 0, 0, 1, 0);
                break;
            case GV.MoleculeType.K2O:
                SetValues(0, 0, 1, 0, 0, 2);
                break;
            case GV.MoleculeType.KCl:
                SetValues(0, 0, 0, 0, 1, 1);
                break;
            case GV.MoleculeType.KOH:
                SetValues(1, 0, 1, 0, 0, 1);
                break;
            case GV.MoleculeType.Na2O:
                SetValues(0, 0, 1, 2, 0, 0);
                break;
            case GV.MoleculeType.NaCl:
                SetValues(0, 0, 0, 1, 1, 0);
                break;
            case GV.MoleculeType.NaHCO3:
                SetValues(1, 1, 3, 1, 0, 0);
                break;
            case GV.MoleculeType.NaOH:
                SetValues(1, 0, 1, 1, 0, 0);
                break;
            default:
                break;
        }
    }

    public void SetFormula(string f)
    {
        formulaText.text = f;
    }

    public void SetValues(int h, int c, int o, int na, int cl, int k)
    {
        hydrogen = h;
        carbon = c;
        oxygen = o;
        sodium = na;
        chlorine = cl;
        potassium = k;
        totalAtoms = hydrogen + carbon + oxygen + sodium + chlorine + potassium;
        mass = hydrogen * 1.008 + carbon * 12.011 + oxygen * 15.999 + sodium * 29.990 + chlorine * 35.45 + potassium * 39.098;
        UpdateText();
    }

    public void UpdateText()
    {
        hydrogenText.text = hydrogen.ToString();
        carbonText.text = carbon.ToString();
        oxygenText.text = oxygen.ToString();
        sodiumText.text = sodium.ToString();
        chlorineText.text = chlorine.ToString();
        potassiumText.text = potassium.ToString();
        totalAtomsText.text = totalAtoms.ToString();
        massText.text = mass.ToString();
    }
}
