using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GV {

    public enum CompoundType { Oxygen }


    public static readonly float Compound_Immunity = 1;  //How long after a reaction before can react again


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
}
