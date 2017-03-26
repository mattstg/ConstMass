using System.Collections;
using System.Collections.Generic;
using UnityEngine; using LoLSDK;

public class SDKInitializer : MonoBehaviour {
    //Has to be called only once and at start of first scene in _init
    void Start()
    {
        LOLSDK.Init("com.Pansimula.BidForLife");
        Destroy(this);
    }

    
}
