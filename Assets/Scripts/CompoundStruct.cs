using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompoundStruct
{
    public GV.CompoundType ctype;
    public Vector2 startPos;

    public CompoundStruct(GV.CompoundType _ctype, Vector2 _startPos)
    {
        ctype = _ctype;
        startPos = _startPos;
    }
}
