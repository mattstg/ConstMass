using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LoLSDK;

public class ReactionEntry : MonoBehaviour
{
    public Image highlightImage;
    public Text text;
    private bool highlightVisible = false;
    private float highlightClock = 0;
    

    public void SetHighlightAlpha(float alpha)
    {
        if (highlightImage)
        {
            if (highlightImage.color.a != alpha)
            {
                Color c = highlightImage.color;
                c.a = alpha;
                highlightImage.color = c;
            }
        }
    }

    public void InitiateHighlight()
    {
        SetHighlightAlpha(0);
        highlightVisible = false;
        highlightClock = 0;
    }

    public void NewOccurrence()
    {
        highlightVisible = true;
        highlightClock = 0;
    }

    public void UpdateHighlight(float dt)
    {
        if (highlightVisible)
        {
            SetHighlightAlpha(AlphaCurve(highlightClock / GV.Reaction_Highlight_Duration));
            if (highlightClock >= GV.Reaction_Highlight_Duration)
                highlightVisible = false;
            highlightClock += dt;
        }
    }

    private float AlphaCurve(float x)
    {
        return CurveIntegral(-1 * (x - 1));
    }

    private float CurveIntegral(float x)
    {
        float y;

        if (x < 0)
            y = 0;
        else if (x > 1)
            y = 1;
        else if (x <= 0.5f)
            y = x * CurveVelocity(x) / 2;
        else
            y = 0.5f + ((x - 0.5f) * (CurveVelocity(x) + 2) / 2);

        return y;
    }

    private float CurveVelocity(float x)
    {
        if (x >= 0 && x <= 1)
            return -4 * Mathf.Abs(x - 0.5f) + 2;
        else
            return 0;
    }
}
