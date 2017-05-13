using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bouffe : MonoBehaviour {

    public int quantiteDeNourriture;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (quantiteDeNourriture <= 0)
            this.gameObject.GetComponent<Renderer>().enabled = false;
	}
}
