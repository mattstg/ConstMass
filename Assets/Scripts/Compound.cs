using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compound : MonoBehaviour
{
    public SpriteRenderer sr;
    public Transform lockChild;
    public GV.CompoundType ctype;
    public Vector2 curDir;
    public float curSpeed;
    [HideInInspector]
    bool isLocked = false;
    float immunityCoutner = GV.Compound_Immunity;

    public void Initialize(GV.CompoundType _ctype)
    {
        ctype = _ctype;
        Initialize();
    }


    public void Initialize()
    {
        curDir = new Vector2(Random.Range(-1, 1f), Random.Range(-1, 1f)).normalized;
        curSpeed = GV.Start_Element_Speed;
        sr.color = GV.CompoundToColor(ctype);
        lockChild = transform.GetChild(0);
        //initialize the UI, values already initialized
    }

    public void UpdateMovement(float dt)
    {
        if(isLocked)
        {
            
        }
        else
        {
            Vector2 curPos = transform.position;
            transform.position = Vector2.MoveTowards(curPos, curPos + curDir, curSpeed * dt);
            BoundsCorrection();
        }
    }


    public void BoundsCorrection()
    {
        Vector2 curPos = transform.position;
        //Returns resulting direction, bounces if hits walls
        if (curPos.x < GV.Game_Bounds.z)
        {
            curDir.x *= -1;
            curPos.x = GV.Game_Bounds.z;
        }
        if (curPos.x > GV.Game_Bounds.x)
        {
            curDir.x *= -1;
            curPos.x = GV.Game_Bounds.x;
        }
        if (curPos.y > GV.Game_Bounds.y)
        {
            curDir.y *= -1;
            curPos.y = GV.Game_Bounds.y;
        }
        if (curPos.y < GV.Game_Bounds.w)
        {
            curDir.y *= -1;
            curPos.y = GV.Game_Bounds.w;
        }
        transform.position = curPos;
    }

    public void SetLock(bool _isLocked)
    {
        isLocked = _isLocked;
        lockChild.gameObject.SetActive(isLocked);
    }

    public void Launch(Vector2 dir, float speed)
    {
        curDir = dir.normalized;
        curSpeed = speed;
    }
}
