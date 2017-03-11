using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    bool compoundSelected = false;

    public void Update()
    {
        //Mouse Pressed
        if (Input.GetMouseButtonDown(0))
        {
            _MouseClicked(GetRealMouse(Input.mousePosition));
        }
        else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            _MouseClicked(GetRealMouse(Input.GetTouch(0).position));
        }

        //Mouse Up
        else if (Input.GetMouseButtonUp(0))
        {
            _MouseUp(GetRealMouse(Input.mousePosition));
        }
        else if (Input.touchCount > 0 && (Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetTouch(0).phase == TouchPhase.Canceled))
        {
            _MouseUp(GetRealMouse(Input.GetTouch(0).position));
        }

        //Mouse dragged or held
        else if (Input.GetMouseButton(0))
        {
            _MouseHeld(GetRealMouse(Input.mousePosition));
        }
        else if (Input.touchCount > 0 && (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(0).phase == TouchPhase.Stationary))
        {
            _MouseHeld(GetRealMouse(Input.GetTouch(0).position));
        }

    }

    private Vector2 GetRealMouse(Vector3 clickPosRaw)
    {
        return Camera.main.ScreenToWorldPoint(clickPosRaw);
    }

    private void _MouseUp(Vector2 clickPos)
    {
        CompoundManager.Instance.MouseReleased(clickPos);
    }

    private void _MouseClicked(Vector2 clickPos)
    {
        CompoundManager.Instance.MouseClicked(clickPos);
    }

    private void _MouseHeld(Vector2 clickPos)
    {
        CompoundManager.Instance.MouseHeld(clickPos);
    }

  
   
}
