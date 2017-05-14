using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ressource : MonoBehaviour {

    public int quantiteDeRessource;
    public int typeRessources;//0 for food, 1 for wood
                              // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (quantiteDeRessource <= 0)
            this.gameObject.GetComponent<Renderer>().enabled = false;
	}
}
