using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GV {

    public enum CompoundType { Oxygen }


    public static readonly float Compound_Immunity = 1;  //How long after a reaction before can react again
    public static readonly Vector4 Game_Bounds = new Vector4(3f,4.7f,-6.3f,-4.7f);  //top right xy, bottom left xy
    public static readonly float Mouse_Selection_Distance = .3f;
    public static readonly float Start_Element_Speed = 2f;


    public static string CompoundToString(GV.CompoundType ctype)
    {
        switch(ctype)
        {
            case CompoundType.Oxygen:
                return "O";
            default:
                return "Err";
        }
    }

    public static Color CompoundToColor(GV.CompoundType ctype)
    {
        switch (ctype)
        {
            case CompoundType.Oxygen:
                return Color.red;
            default:
                return Color.white;
        }
    }


}
