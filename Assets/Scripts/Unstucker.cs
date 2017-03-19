using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unstucker : MonoBehaviour {

	public float unstuckTimer = 1f;
	// Update is called once per frame
	void Update ()
    {
        unstuckTimer -= Time.deltaTime;
        if (unstuckTimer <= 0)
            Destroy(this.gameObject);
	}
}
