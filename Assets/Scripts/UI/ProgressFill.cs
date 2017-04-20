using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LoLSDK;

public class ProgressFill : MonoBehaviour
{
    private float fadeDuration = 1;
    private float fadeProgress = 1;
    private float sourceFill = 0;
    private float targetFill = 0;
    private Image progressBar;

    void Awake()
    {
        if (fadeDuration <= 0)
            fadeDuration = 1;
        fadeProgress = fadeDuration;
        if (!progressBar)
            progressBar = gameObject.GetComponent<Image>();
    }

    public void UpdateProgress(float dt)
    {
        float presentFill = GetPresentFill();
        if (presentFill != targetFill)
        {
            if (fadeDuration <= 0)
                fadeDuration = 1;

            float integral = GV.CurveIntegral(fadeProgress / fadeDuration);
            float newFill = targetFill * integral + sourceFill * (1 - integral);
            fadeProgress += dt;
            if (fadeProgress > fadeDuration)
                fadeProgress = fadeDuration;
            ApplyFill(newFill);
        }
    }

    public void SetTargetFill(float target, float duration)
    {
        sourceFill = GetPresentFill();
        targetFill = target;
        fadeProgress = 0;
        fadeDuration = duration;
    }

    public void SetPresentFill(float fill)
    {
        sourceFill = fill;
        targetFill = fill;
        fadeProgress = fadeDuration;
        ApplyFill(targetFill);
    }

    public void ApplyFill(float newFill)
    {
        if (!progressBar)
            progressBar = gameObject.GetComponent<Image>();
        progressBar.fillAmount = newFill;
    }

    public float GetPresentFill()
    {
        if (!progressBar)
            progressBar = gameObject.GetComponent<Image>();
        return progressBar.fillAmount;
    }
}
