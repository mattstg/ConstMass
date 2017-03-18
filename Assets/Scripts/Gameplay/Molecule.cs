using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Molecule : MonoBehaviour
{
    bool isLocked = false;
    bool isMerging = false;
    Rigidbody2D rb2d;
    public GV.MoleculeType mtype;
    
    float immunityCoutner = GV.Compound_Immunity;

    public void Initialize(GV.MoleculeType _mtype)
    {
        mtype = _mtype;
        Initialize();
    }


    public void Initialize()
    {
        rb2d = GetComponent<Rigidbody2D>();
        Vector2 launchDir = new Vector2(Random.Range(-1, 1f), Random.Range(-1, 1f)).normalized;
        rb2d.AddForce(launchDir * GV.Temperature * GV.Temperature_Force_Per_Degree,ForceMode2D.Impulse);
        //initialize the UI, values already initialized
    }

    public void SetLock(bool _isLocked)
    {
        isLocked = _isLocked;
        rb2d.constraints = (isLocked)? RigidbodyConstraints2D.FreezeAll : RigidbodyConstraints2D.None;
    }

    public void Launch(Vector2 dir, float speed)
    {
        rb2d.velocity = dir.normalized * speed;
        //rb2d.AddForce();
    }

    public void Launch(Vector2 speed)
    {
        rb2d.velocity = speed;
    }

    public void DestroyRbAndColi()
    {
        foreach (Collider2D c in GetComponentsInChildren<Collider2D>())
        {
            c.enabled = false;
            MonoBehaviour.Destroy(c);
        }
        MonoBehaviour.Destroy(rb2d);
        rb2d = null;
    }

    public void OnTriggerEnter2D(Collider2D coli)
    {
        if (coli.CompareTag("Walls"))
        {
            Rebound(coli.GetComponent<WallRebound>().reboundVector);
        }
    }

    public void OnCollisionEnter2D(Collision2D coli)
    {
        if (!isLocked && !isMerging && coli.transform.CompareTag("Molecule"))
        {
            Molecule m = coli.gameObject.GetComponent<Molecule>();
            if(!m.isLocked && !m.isMerging && MoleculeDict.Instance.CanReact(mtype, m.mtype))
            {
                m.isMerging = isMerging = true;
                MergeManager.Instance.MergeTwoMolecules(this, m);
            }
        }
    }

    public void Rebound(Vector2 reboundVector)
    {
        //Debug.Log("current velo: " + rb2d.velocity + " mult by " + reboundVector + " result is: " + new Vector2(rb2d.velocity.x * reboundVector.x, rb2d.velocity.y * reboundVector.y));
        //rb2d.velocity = new Vector2(rb2d.velocity.x * reboundVector.x, rb2d.velocity.y * reboundVector.y);    cannot do, double colision is a thing
        if(reboundVector.x == -1)
            rb2d.velocity = new Vector2(Mathf.Abs(rb2d.velocity.x) * -1, rb2d.velocity.y);
        else if(reboundVector.x == 1)
            rb2d.velocity = new Vector2(Mathf.Abs(rb2d.velocity.x), rb2d.velocity.y);
        else if (reboundVector.y == -1)
            rb2d.velocity = new Vector2(rb2d.velocity.x, Mathf.Abs(rb2d.velocity.y)*-1);
        else if (reboundVector.y == 1)
            rb2d.velocity = new Vector2(rb2d.velocity.x, Mathf.Abs(rb2d.velocity.y));
    }

    /* public void BoundsCorrection()
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
   }*/
}
