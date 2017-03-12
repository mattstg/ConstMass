using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WS : MonoBehaviour {


    public Transform MoleculeParent;

	void Awake ()
    {
        GV.MoleculeParent = MoleculeParent;
	}
	
}
