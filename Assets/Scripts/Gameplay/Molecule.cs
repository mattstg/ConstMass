using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Molecule : MonoBehaviour
{
    bool isLocked = false;
    bool isMerging = false;
    Rigidbody2D rb2d;
    public GV.MoleculeType mtype;
    Transform textMesh;
    bool textIsActive;

    public void Initialize(GV.MoleculeType _mtype)
    {
        mtype = _mtype;
        Initialize();
    }

    public bool IsSelectable()
    {
        return !isLocked && !isMerging;
    }

    public void Initialize()
    {
        rb2d = GetComponent<Rigidbody2D>();
        Vector2 launchDir = new Vector2(Random.Range(-1, 1f), Random.Range(-1, 1f)).normalized;
        rb2d.velocity = launchDir * GV.Molecule_Speed_Start;
        gameObject.AddComponent<RandomSpin>();
        GameObject tm = Instantiate(Resources.Load("Prefabs/MoleText")) as GameObject;
        tm.GetComponent<Renderer>().sortingLayerName = "MoleculeText";
        tm.GetComponent<TextMesh>().text = GV.MoleculeFormula(mtype);
        textMesh = tm.transform;
        textMesh.SetParent(transform);
        textMesh.localPosition = new Vector3();
        textIsActive = GV.Molecule_Text_Active;
        textMesh.gameObject.SetActive(textIsActive);
        //rb2d.AddForce(GV.Temperature_Force_Per_Degree,ForceMode2D.Impulse);
        //initialize the UI, values already initialized
    }

    public void SetLock(bool _isLocked)
    {
        isLocked = _isLocked;
        //rb2d.constraints = (isLocked)? RigidbodyConstraints2D.FreezeAll : RigidbodyConstraints2D.None;
        rb2d.constraints = (isLocked) ? RigidbodyConstraints2D.FreezePosition : RigidbodyConstraints2D.None;
    }

    public void Launch(Vector2 dir, float speed)
    {
        rb2d.velocity = dir.normalized * speed;
        //rb2d.AddForce();
    }

    public void UpdateMolecule()
    {
        if (!isLocked && !isMerging)
        {
            float speed = rb2d.velocity.magnitude;
            if (speed > GV.Molecule_Speed_Max)
            {
                rb2d.velocity = rb2d.velocity * (GV.Molecule_Speed_Max / speed);
            }
            else if(speed < GV.Molecule_Speed_Min)
            {
                rb2d.velocity = rb2d.velocity * (GV.Molecule_Speed_Min / speed);
            }

            BoundsCorrection();
        }
        if(textIsActive != GV.Molecule_Text_Active)
        {
            textIsActive = GV.Molecule_Text_Active;
            textMesh.gameObject.SetActive(textIsActive);
        }
        if (textIsActive)
            textMesh.transform.rotation = Quaternion.identity;
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
        else
        {
            LOLAudio.Instance.PlayAudio("Wobble.wav");
        }
    }

    public void Rebound(Vector2 reboundVector)
    {
        //Debug.Log("current velo: " + rb2d.velocity + " mult by " + reboundVector + " result is: " + new Vector2(rb2d.velocity.x * reboundVector.x, rb2d.velocity.y * reboundVector.y));
        //rb2d.velocity = new Vector2(rb2d.velocity.x * reboundVector.x, rb2d.velocity.y * reboundVector.y);    cannot do, double colision is a thing
        if (reboundVector.x == -1)
        {
            rb2d.velocity = new Vector2(Mathf.Abs(rb2d.velocity.x) * -1, rb2d.velocity.y);
            if (rb2d.velocity.x > -GV.Molecule_Speed_Min)
                rb2d.velocity = new Vector2(-GV.Molecule_Speed_Min, rb2d.velocity.y);
        }
        else if (reboundVector.x == 1)
        {
            rb2d.velocity = new Vector2(Mathf.Abs(rb2d.velocity.x), rb2d.velocity.y);
            if (rb2d.velocity.x < GV.Molecule_Speed_Min)
                rb2d.velocity = new Vector2(GV.Molecule_Speed_Min, rb2d.velocity.y);
        }
        else if (reboundVector.y == -1)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, Mathf.Abs(rb2d.velocity.y) * -1);
            if (rb2d.velocity.y > -GV.Molecule_Speed_Min)
                rb2d.velocity = new Vector2(rb2d.velocity.x, -GV.Molecule_Speed_Min);
        }
        else if (reboundVector.y == 1)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, Mathf.Abs(rb2d.velocity.y));
            if (rb2d.velocity.y < GV.Molecule_Speed_Min)
                rb2d.velocity = new Vector2(rb2d.velocity.x, GV.Molecule_Speed_Min);
        }
    }

   public void BoundsCorrection()
   {
        bool playBounceAudio = false;
       Vector2 curPos = transform.position;
        //Returns resulting direction, bounces if hits walls
        if (!GameManager.Instance.currentLevelIsStart)
        {
            if (curPos.x < GV.Game_Bounds.z)
            {
                rb2d.velocity = new Vector2(Mathf.Abs(rb2d.velocity.x), rb2d.velocity.y);
                curPos.x = GV.Game_Bounds.z;
                playBounceAudio = true;
            }
            if (curPos.x > GV.Game_Bounds.x)
            {
                rb2d.velocity = new Vector2(Mathf.Abs(rb2d.velocity.x) * -1, rb2d.velocity.y);
                curPos.x = GV.Game_Bounds.x;
                playBounceAudio = true;
            }
            if (curPos.y > GV.Game_Bounds.y)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, Mathf.Abs(rb2d.velocity.y) * -1);
                curPos.y = GV.Game_Bounds.y;
                playBounceAudio = true;
            }
            if (curPos.y < GV.Game_Bounds.w)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, Mathf.Abs(rb2d.velocity.y));
                curPos.y = GV.Game_Bounds.w;
                playBounceAudio = true;
            }
        }
        else if(GameManager.Instance.currentLevelIsStart)
        {
            if (curPos.x < GV.Start_Screen_Bounds.z)
            {
                rb2d.velocity = new Vector2(Mathf.Abs(rb2d.velocity.x), rb2d.velocity.y);
                curPos.x = GV.Start_Screen_Bounds.z;
                playBounceAudio = true;
            }
            if (curPos.x > GV.Start_Screen_Bounds.x)
            {
                rb2d.velocity = new Vector2(Mathf.Abs(rb2d.velocity.x) * -1, rb2d.velocity.y);
                curPos.x = GV.Start_Screen_Bounds.x;
                playBounceAudio = true;
            }
            if (curPos.y > GV.Start_Screen_Bounds.y)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, Mathf.Abs(rb2d.velocity.y) * -1);
                curPos.y = GV.Start_Screen_Bounds.y;
                playBounceAudio = true;
            }
            if (curPos.y < GV.Start_Screen_Bounds.w)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, Mathf.Abs(rb2d.velocity.y));
                curPos.y = GV.Start_Screen_Bounds.w;
                playBounceAudio = true;
            }
        }
        if (playBounceAudio)
            LOLAudio.Instance.PlayAudio("Wobble.wav");
       transform.position = curPos;
   }
}
