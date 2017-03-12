using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeFlow : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        GameManager.Instance.CreateAndInitializeLevel(0);	
	}
	
    void Update()
    {
        float dt = Time.deltaTime;
        GameManager.Instance.Update(dt);
        MergeManager.Instance.UpdateMerger(dt);
    }
}
