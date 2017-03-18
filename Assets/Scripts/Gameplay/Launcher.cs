using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher
{
    #region Singleton
    private static Launcher instance;

    private Launcher() { Setup(); }

    public static Launcher Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Launcher();
            }
            return instance;
        }
    }
    #endregion
    bool elasticGraphicEnabled = false;
    GameObject elasticGraphics;

    bool moleSelected = false;
    Molecule toLaunch;
    Vector2 moleInitialVelo;
    Vector2 curMousePos;
    Vector2 molePos;
    float elasticDist = 0;
    //Launch_Min_Dist = .25f;
    //Launch_Max_Velo = 5f;
    //Launch_Velo_Per_Dist = 2;
    //Launch_Elastic_Time = .6f;

    private void Setup()
    {
        elasticGraphics = MonoBehaviour.Instantiate(Resources.Load("Prefabs/Elastic")) as GameObject;
        elasticGraphics.SetActive(false);
        elasticGraphicEnabled = false;
    }

    public void SetupLaunch(Molecule _toLaunch)
    {
        if (toLaunch)
            LaunchCurrentMolecule();
        toLaunch = _toLaunch;
        moleInitialVelo = toLaunch.GetComponent<Rigidbody2D>().velocity;
        toLaunch.SetLock(true);
        molePos = curMousePos = toLaunch.transform.position;
        moleSelected = true;
        elasticDist = 0;
    }

    public void UpdateMousePosition(Vector2 pos)
    {
        if (moleSelected)
        {
            curMousePos = pos;
            elasticDist = MathHelper.ApproxDist(curMousePos, molePos);
            float ang = Mathf.Atan2(curMousePos.y - molePos.y, curMousePos.x - molePos.x) * 180 / Mathf.PI;
            if (elasticDist >= GV.Launch_Min_Dist)
            {
                if (!elasticGraphicEnabled)
                {
                    elasticGraphics.SetActive(true);
                    elasticGraphicEnabled = true;
                }
                elasticGraphics.transform.position = molePos;
                elasticGraphics.transform.eulerAngles = new Vector3(0,0,ang);
                elasticGraphics.transform.localScale = new Vector2(elasticDist, 1);
            }
            else
            {
                if (elasticGraphicEnabled)
                {
                    elasticGraphics.SetActive(false);
                    elasticGraphicEnabled = false;
                }
            }
        }
        else
        {
            if (elasticGraphicEnabled)
            {
                elasticGraphics.SetActive(false);
                elasticGraphicEnabled = false;
            }
        }
    }

    public void ReleaseMouse(Vector2 pos)
    {
        if (moleSelected)
            LaunchCurrentMolecule();
    }

    private void LaunchCurrentMolecule()
    {
        if (moleSelected)
        {
            if (elasticGraphicEnabled)
            {
                float launchAng = Mathf.Atan2(curMousePos.y - molePos.y, curMousePos.x - molePos.x) * 180 / Mathf.PI;
                float launchSpeed = Mathf.Clamp(elasticDist * GV.Launch_Velo_Per_Dist, 0, GV.Launch_Max_Velo);
                Vector2 launchVector = MathHelper.DegreeToVector2(launchAng + 180).normalized;
                toLaunch.Launch(launchVector * launchSpeed);
            }
            else
            {
                toLaunch.Launch(moleInitialVelo);
            }
            //Vector2 launchParam = 

            toLaunch.SetLock(false);
            toLaunch = null;
            moleInitialVelo = new Vector2();
            moleSelected = false;
            molePos = new Vector2();
            elasticDist = 0;
            elasticGraphics.SetActive(false);
            elasticGraphicEnabled = false;
        }
    }
    
    

}
