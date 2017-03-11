using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeFlow : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        CompoundManager.Instance.CreateAndInitializeLevel(0);	
	}
	
    void Update()
    {
        CompoundManager.Instance.Update(Time.deltaTime);
    }
}
