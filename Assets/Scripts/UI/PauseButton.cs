using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LoLSDK;

public class PauseButton : CanvasButton
{
    public Sprite selectedPressedSprite;
    protected Sprite defaultPressedSprite;

    protected override void Awake()
    {
        if (!canvasManager & clickAudio)
            canvasManager = GetComponentInParent<CanvasManager>();
        button = gameObject.GetComponent<Button>();
        image = gameObject.GetComponent<Image>();
        text = gameObject.GetComponentInChildren<Text>();
        defaultSprite = image.sprite;
        defaultPressedSprite = button.spriteState.pressedSprite;
        SetMode(true, true);
        InitializeState(isSelected, isEnabled);
    }

    public override void OnClick()
    {
        if (isSelected != GV.Paused)
            isSelected = GV.Paused;
        SetPause(!isSelected, true);
    }

    public void SetPause(bool becomesSelected, bool directAction)
    {
        if (isSelected != becomesSelected)
        {
            if (becomesSelected)
            {
                image.sprite = selectedSprite;
                SpriteState spriteState = new SpriteState();
                spriteState = button.spriteState;
                spriteState.pressedSprite = selectedPressedSprite;
                button.spriteState = spriteState;
                isSelected = true;
                BecameSelected(directAction);
            }
            else
            {
                image.sprite = defaultSprite;
                SpriteState spriteState = new SpriteState();
                spriteState = button.spriteState;
                spriteState.pressedSprite = defaultPressedSprite;
                button.spriteState = spriteState;
                isSelected = false;
                BecameDeselected(directAction);
            }
        }
    }
}
