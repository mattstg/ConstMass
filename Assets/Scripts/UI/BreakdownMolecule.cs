using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LoLSDK;

public class BreakdownMolecule
{
    public GV.MoleculeType moleculeType;

    public double mass = 0;
    public int totalAtoms = 0;
    public int hydrogen = 0;
    public int carbon = 0;
    public int oxygen = 0;
    public int sodium = 0;
    public int chlorine = 0;
    public int potassium = 0;

    public void SetMoleculeType(GV.MoleculeType type)
    {
        moleculeType = type;
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
    }
}
