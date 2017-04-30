using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Molecule : MonoBehaviour
{
    bool isLocked = false;
    bool isMerging = false;
    bool isPaused = false;
    Rigidbody2D rb2d;
    public GV.MoleculeType mtype;
    Transform moleText;
    List<Transform> atomTexts;
    bool moleTextIsActive;
    bool atomTextIsActive;

    public void Initialize(GV.MoleculeType _mtype)
    {
        mtype = _mtype;
        Initialize();
    }

    public bool IsSelectable()
    {
        return !isLocked && !isMerging && !isPaused;
    }

    public void Initialize()
    {
        rb2d = GetComponent<Rigidbody2D>();
        Vector2 launchDir = new Vector2(Random.Range(-1, 1f), Random.Range(-1, 1f)).normalized;
        rb2d.velocity = launchDir * GV.Molecule_Speed_Start;
        gameObject.AddComponent<RandomSpin>();
        atomTexts = new List<Transform>();
        atomTextIsActive = GV.Atom_Text_Active;
        foreach (Transform t in transform)
        {
            switch (t.name)
            {
                case "Hydrogen":
                    GameObject hydrogenTextObject = Instantiate(Resources.Load("Prefabs/Text/HydrogenText")) as GameObject;
                    Transform hydrogenTextTransform = hydrogenTextObject.transform;
                    hydrogenTextTransform.SetParent(t, false);
                    hydrogenTextTransform.GetComponentInChildren<Renderer>().sortingLayerName = "AtomText";
                    hydrogenTextTransform.gameObject.SetActive(atomTextIsActive);
                    atomTexts.Add(hydrogenTextTransform);
                    break;
                case "Carbon":
                    GameObject carbonTextObject = Instantiate(Resources.Load("Prefabs/Text/CarbonText")) as GameObject;
                    Transform carbonTextTransform = carbonTextObject.transform;
                    carbonTextTransform.SetParent(t, false);
                    carbonTextTransform.GetComponentInChildren<Renderer>().sortingLayerName = "AtomText";
                    carbonTextTransform.gameObject.SetActive(atomTextIsActive);
                    atomTexts.Add(carbonTextTransform);
                    break;
                case "Oxygen":
                    GameObject oxygenTextObject = Instantiate(Resources.Load("Prefabs/Text/OxygenText")) as GameObject;
                    Transform oxygenTextTransform = oxygenTextObject.transform;
                    oxygenTextTransform.SetParent(t, false);
                    oxygenTextTransform.GetComponentInChildren<Renderer>().sortingLayerName = "AtomText";
                    oxygenTextTransform.gameObject.SetActive(atomTextIsActive);
                    atomTexts.Add(oxygenTextTransform);
                    break;
                case "Sodium":
                    GameObject sodiumTextObject = Instantiate(Resources.Load("Prefabs/Text/SodiumText")) as GameObject;
                    Transform sodiumTextTransform = sodiumTextObject.transform;
                    sodiumTextTransform.SetParent(t, false);
                    sodiumTextTransform.GetComponentInChildren<Renderer>().sortingLayerName = "AtomText";
                    sodiumTextTransform.gameObject.SetActive(atomTextIsActive);
                    atomTexts.Add(sodiumTextTransform);
                    break;
                case "Chlorine":
                    GameObject chlorineTextObject = Instantiate(Resources.Load("Prefabs/Text/ChlorineText")) as GameObject;
                    Transform chlorineTextTransform = chlorineTextObject.transform;
                    chlorineTextTransform.SetParent(t, false);
                    chlorineTextTransform.GetComponentInChildren<Renderer>().sortingLayerName = "AtomText";
                    chlorineTextTransform.gameObject.SetActive(atomTextIsActive);
                    atomTexts.Add(chlorineTextTransform);
                    break;
                case "Potassium":
                    GameObject potassiumTextObject = Instantiate(Resources.Load("Prefabs/Text/PotassiumText")) as GameObject;
                    Transform potassiumTextTransform = potassiumTextObject.transform;
                    potassiumTextTransform.SetParent(t, false);
                    potassiumTextTransform.GetComponentInChildren<Renderer>().sortingLayerName = "AtomText";
                    potassiumTextTransform.gameObject.SetActive(atomTextIsActive);
                    atomTexts.Add(potassiumTextTransform);
                    break;
                default:
                    break;
            }
        }
        GameObject tm = Instantiate(Resources.Load("Prefabs/Text/MoleText")) as GameObject;
        tm.GetComponentInChildren<Renderer>().sortingLayerName = "MoleculeText";
        tm.GetComponentInChildren<TextMesh>().text = GV.ColoredMoleculeFormula(mtype, true);
        moleText = tm.transform;
        moleText.SetParent(transform);
        moleText.localPosition = new Vector3();
        moleTextIsActive = GV.Molecule_Text_Active;
        moleText.gameObject.SetActive(moleTextIsActive);
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
        if (isPaused != GV.Paused)
        {
            isPaused = GV.Paused;
            if (!isMerging)
            {
                if (isPaused)
                {
                    SetMotion sm = gameObject.AddComponent<SetMotion>();
                    sm.isActive = false;
                    sm.velocity = rb2d.velocity;
                    sm.angularVelocity = rb2d.angularVelocity;
                    sm.constraints = rb2d.constraints;
                    sm.setVelocity = !Launcher.Instance.IsAnimating(this);
                    rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
                }
                else
                {
                    SetMotion sm = gameObject.GetComponent<SetMotion>();
                    if (sm)
                        sm.isActive = true;
                }
            }
        }

        if (!isPaused && !isLocked && !isMerging)
        {
            float speed = rb2d.velocity.magnitude;
            if (speed != 0)
            {
                if (speed > GV.Molecule_Speed_Max)
                {
                    rb2d.velocity = rb2d.velocity * (GV.Molecule_Speed_Max / speed);
                }
                else if (speed < GV.Molecule_Speed_Min)
                {
                    rb2d.velocity = rb2d.velocity * (GV.Molecule_Speed_Min / speed);
                }

                BoundsCorrection();
            }
        }
        if(moleTextIsActive != GV.Molecule_Text_Active)
        {
            moleTextIsActive = GV.Molecule_Text_Active;
            moleText.gameObject.SetActive(moleTextIsActive);
        }
        if (moleTextIsActive)
            moleText.transform.rotation = Quaternion.identity;

        if (atomTextIsActive != GV.Atom_Text_Active)
        {
            atomTextIsActive = GV.Atom_Text_Active;
            foreach (Transform t in atomTexts)
            {
                t.gameObject.SetActive(atomTextIsActive);
            }
        }
        if (atomTextIsActive)
        {
            foreach (Transform t in atomTexts)
            {
                t.rotation = Quaternion.identity;
            }
        }
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
                if(!GameManager.Instance.currentLevelIsStart)
                    GameManager.Instance.infoPanel.ReactionOccurrence(mtype, m.mtype);
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
