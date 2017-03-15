﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GV {

    public enum MoleculeType { Hydrogen, Hydrogen2 }

    //WS
    public static Transform MoleculeParent;

    public static readonly float Compound_Immunity = 1;  //How long after a reaction before can react again
    public static readonly Vector4 Game_Bounds = new Vector4(3f,4.7f,-6.3f,-4.7f);  //top right xy, bottom left xy
    public static readonly float Mouse_Selection_Distance = .3f;
    public static readonly float Start_Element_Speed = 2f;
    public static float Temperature = 2;
    public static float Temperature_Force_Per_Degree = 1;

    //Chemical
    public static float Merge_Time = .3f; //time it takes for molecules to merge
    public static float Merge_Safe_Zone_Radius = .3f; //size of invisible merge protector

    //Main Game
    public static float[] Game_Length = new float[] { 30, 40, 50, 50, 80 };
    public static int Game_Lesson_Max = 5;

    public static string CompoundToString(GV.MoleculeType ctype)
    {
        switch(ctype)
        {
            case MoleculeType.Hydrogen:
                return "H";
            case MoleculeType.Hydrogen2:
                return "H2";
            default:
                return "Err";
        }
    }

    public static Color CompoundToColor(GV.MoleculeType ctype)
    {
        switch (ctype)
        {
            case MoleculeType.Hydrogen:
                return Color.white;
            case MoleculeType.Hydrogen2:
                return Color.blue;
            default:
                return Color.white;
        }
    }


}
