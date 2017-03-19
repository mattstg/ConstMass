using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GV {

    public enum AtomType { H, O, C, Na, K, Cl }
    public enum MoleculeType { H2 , Cl2, HCl, H2O, CO2, NaCl, NaOH, KOH, KCl, NaHCO3, Na2O, K2O }
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

    public static readonly float Molecule_Scale = .5f; //scales all new molecules
    public static readonly float Molecule_Speed_Start = 1;
    public static readonly float Molecule_Speed_Min = .33f;
    public static readonly float Molecule_Speed_Max = 5f;

    //Chemical
    public static float Merge_Time = .3f; //time it takes for molecules to merge
    public static float Merge_Safe_Zone_Radius = .3f; //size of invisible merge protector

    //Main Game
    public static readonly float[] Game_Length = new float[] { 30, 40, 50, 50, 80 };
    public static readonly int Game_Lesson_Max = 5;

    //launch vars
    public static readonly float Launch_Min_Dist = .5f;
    public static readonly float Launch_Max_Velo = 5f;
    public static readonly float Launch_Velo_Per_Dist = 2;
    public static readonly float Launch_Elastic_Time = .32f; //time to elastic launch

}
