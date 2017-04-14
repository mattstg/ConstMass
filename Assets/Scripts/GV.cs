using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GV {

    public enum AtomType { H, C, O, Na, Cl, K }
    public enum MoleculeType { H2, H2O, HCl, CO2, Na2O, NaHCO3, NaOH, NaCl, Cl2, K2O, KOH, KCl, None }
    public static readonly int Molecule_Enum_Count = 12;
    //WS
    public static Transform MoleculeParent;
    public static GameFlow gameFlow;
    
    

    public static readonly float Compound_Immunity = 1;  //How long after a reaction before can react again
    public static readonly Vector4 Game_Bounds = new Vector4(3f,4.7f,-6.3f,-4.7f);  //top right xy, bottom left xy
    public static readonly Vector4 Start_Screen_Bounds = new Vector4(6.46f,4.6f,-6.46f,-4.6f);
    public static readonly float Mouse_Selection_Distance = .5f;
    public static readonly float Start_Element_Speed = 2f;
    public static float Temperature = 1;
    public static float Temperature_Force_Per_Degree = 1;

    public static readonly float Molecule_Scale = .5f; //scales all new molecules
    public static readonly float Molecule_Speed_Start = 1;
    public static readonly float Molecule_Speed_Min = .33f;
    public static readonly float Molecule_Speed_Max = 3.5f;
    public static bool Molecule_Text_Active = false;
    public static bool Atom_Text_Active = true;

    //Chemical
    public static float Merge_Time = .3f; //time it takes for molecules to merge
    public static float Merge_Safe_Zone_Radius = .3f; //size of invisible merge protector

    //Main Game
    public static readonly float[] Game_Length = new float[] { 45, 90, 150, 210 };
    public static readonly int[] visibleFormulas = new int[] { 1, 2, 4, 6 };
    public static readonly int Game_Lesson_Max = 4;
    public static readonly float Completion_Delay = 2;
    public static bool Paused = false;
    public static MoleculeType GoalMolecule = MoleculeType.None;
    public static MoleculeType SelectedMolecule = MoleculeType.None;

    //Score
    public static readonly float Time_Score_Perc_For_Max = .2f; //which means 80% of score
    public static readonly float Score_Time_Max = 500;
    public static readonly float Score_Success_Max = 1000;
    public static readonly float Score_Quiz_Max = 1000;

    //launch vars
    public static readonly float Launch_Min_Dist = .7f;
    public static readonly float Launch_Max_Velo = 5f;
    public static readonly float Launch_Elastic_Max_Dist = 2.4f;
    public static readonly float Launch_Velo_Per_Dist = 1.2f;
    public static readonly float Launch_Elastic_Time = .32f; //time to elastic launch

    public static int Current_Flow_Index = 0;

    public static bool overrideCascadeSpeed = true;
    public static float globalCascadeSpeed = 80f;

    public static float MaxScore(ProgressTracker.ScoreType st)
    {
        switch (st)
        {
            case ProgressTracker.ScoreType.Success:
                return Score_Success_Max;
            case ProgressTracker.ScoreType.Time:
                return Score_Time_Max;
            case ProgressTracker.ScoreType.Quiz:
                return Score_Quiz_Max;
            default:
                return 0;
        }
    }

    public static string MoleculeFormula(MoleculeType mtype)
    {
        string formula = mtype.ToString();
        switch (mtype)
        {
            case MoleculeType.H2:
                formula = "H" + SubscriptNumeral(2);
                break;
            case MoleculeType.Cl2:
                formula = "Cl" + SubscriptNumeral(2);
                break;
            case MoleculeType.HCl:
                formula = "HCl";
                break;
            case MoleculeType.H2O:
                formula = "H" + SubscriptNumeral(2) + "O";
                break;
            case MoleculeType.CO2:
                formula = "CO" + SubscriptNumeral(2);
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
                formula = "NaHCO" + SubscriptNumeral(3);
                break;
            case MoleculeType.Na2O:
                formula = "Na" + SubscriptNumeral(2) + "O";
                break;
            case MoleculeType.K2O:
                formula = "K" + SubscriptNumeral(2) + "O";
                break;
            default:
                break;
        }
        return formula;
    }

    public static char SubscriptNumeral(int subscript)
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
                Debug.Log("GV.SubscriptNumeral() error, argument out of bounds.");
                return 'X';
        }
    }
}
