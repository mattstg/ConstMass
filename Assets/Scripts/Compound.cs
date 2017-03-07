using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compound : MonoBehaviour
{
    public SpriteRenderer sr;
    public GV.CompoundType ctype;
    public Vector2 floatingSpeed;
    [HideInInspector]
    public bool isHeld = false;
    float immunityCoutner = GV.Compound_Immunity;

    public void Initialize(GV.CompoundType _ctype)
    {
        ctype = _ctype;
        Initialize();
    }


    public void Initialize()
    {
        //initialize the UI, values already initialized
    }


}
