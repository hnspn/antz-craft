﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void play() {
        SceneManager.LoadScene("Main");
    }

    public void credits() {

    }

    public void leave() {

    }

    public void tutorial() {

    }
}
