using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {
    public GameObject food, scrap, science;
    private float foodN, scrapN, scienceN;
	// Use this for initialization
	void Start () {
        foodN = 0;
        scrapN = 0;
        scienceN = 0;
        food.GetComponent<UnityEngine.UI.Text>().text = ""+foodN;
        scrap.GetComponent<UnityEngine.UI.Text>().text = "" + scrapN;
        science.GetComponent<UnityEngine.UI.Text>().text = "" + scienceN;
    }
	
	// Update is called once per frame
	void Update () {
        foodN += 10 * Time.deltaTime;
        scrapN += 5 * Time.deltaTime;
        scienceN += 1 * Time.deltaTime;

        food.GetComponent<UnityEngine.UI.Text>().text = "" + foodN.ToString("N0");
        scrap.GetComponent<UnityEngine.UI.Text>().text = "" + scrapN.ToString("N0");
        science.GetComponent<UnityEngine.UI.Text>().text = "" + scienceN.ToString("N0");
    }
}
