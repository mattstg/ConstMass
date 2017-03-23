using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GV {

    public enum AtomType { H, C, O, Na, Cl, K }
    public enum MoleculeType { H2, H2O, HCl, CO2, Na2O, NaHCO3, NaOH, NaCl, Cl2, K2O, KOH, KCl }
    public static readonly int Molecule_Enum_Count = 12;
    //WS
    public static Transform MoleculeParent;
    public static GameFlow gameFlow;
    
    

    public static readonly float Compound_Immunity = 1;  //How long after a reaction before can react again
    public static readonly Vector4 Game_Bounds = new Vector4(3f,4.7f,-6.3f,-4.7f);  //top right xy, bottom left xy
    public static readonly float Mouse_Selection_Distance = .3f;
    public static readonly float Start_Element_Speed = 2f;
    public static float Temperature = 1;
    public static float Temperature_Force_Per_Degree = 1;

    public static readonly float Molecule_Scale = .7f; //scales all new molecules
    public static readonly float Molecule_Speed_Start = 1;
    public static readonly float Molecule_Speed_Min = .33f;
    public static readonly float Molecule_Speed_Max = 4f;
    public static bool Molecule_Text_Active = false;

    //Chemical
    public static float Merge_Time = .3f; //time it takes for molecules to merge
    public static float Merge_Safe_Zone_Radius = .3f; //size of invisible merge protector

    //Main Game
    public static readonly float[] Game_Length = new float[] { 30, 40, 50, 50, 80 };
    public static readonly int Game_Lesson_Max = 5;

    //launch vars
    public static readonly float Launch_Min_Dist = .5f;
    public static readonly float Launch_Max_Velo = 5f;
    public static readonly float Launch_Velo_Per_Dist = 1.2f;
    public static readonly float Launch_Elastic_Time = .32f; //time to elastic launch


    public static string MoleculeFormula(MoleculeType mtype)
    {
        string formula = mtype.ToString();
        switch (mtype)
        {
            case MoleculeType.H2:
                formula = "H" + S(2);
                break;
            case MoleculeType.Cl2:
                formula = "Cl" + S(2);
                break;
            case MoleculeType.HCl:
                formula = "HCl";
                break;
            case MoleculeType.H2O:
                formula = "H" + S(2) + "O";
                break;
            case MoleculeType.CO2:
                formula = "CO" + S(2);
                break;
            case MoleculeType.NaCl:
                formula = "NaCl";
                break;
            case MoleculeType.NaOH:
                formula = "NaOH";
                break;
            case MoleculeType.KOH:
                formula = "KOH";
                break;
            case MoleculeType.KCl:
                formula = "KCl";
                break;
            case MoleculeType.NaHCO3:
                formula = "NaHCO" + S(3);
                break;
            case MoleculeType.Na2O:
                formula = "Na" + S(2) + "O";
                break;
            case MoleculeType.K2O:
                formula = "K" + S(2) + "O";
                break;
            default:
                break;
        }
        return formula;
    }

    public static char S(int subscript)
    {
        switch (subscript)
        {
            case 0:
                return '\u2080';
            case 1:
                return '\u2081';
            case 2:
                return '\u2082';
            case 3:
                return '\u2083';
            case 4:
                return '\u2084';
            case 5:
                return '\u2085';
            case 6:
                return '\u2086';
            case 7:
                return '\u2087';
            case 8:
                return '\u2088';
            case 9:
                return '\u2089';
            default:
                return ' ';
        }
    }
}
