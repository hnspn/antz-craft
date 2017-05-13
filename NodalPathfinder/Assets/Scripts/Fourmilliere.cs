using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Fourmilliere : MonoBehaviour {

    private List<GameObject> listScout = new List<GameObject>();
    private List<GameObject> listOuvriere = new List<GameObject>();
    private List<GameObject> listSoldat = new List<GameObject>();

    public GameObject ScoutPrefab;
    public GameObject OuvrierePrefab;
    public GameObject SoldatPrefab;

    GameObject selectedStuff = null;
    public int foodStored = 0;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("q"))
        {
            GameObject ant = CreateOuvriere();
            ant.SetActive(true);
        }
        if (Input.GetKeyDown("w"))
        {
            GameObject ant = CreateScout();
            ant.SetActive(true);
        }
        if (Input.GetKeyDown("e"))
        {
            GameObject ant = CreateSoldat();
            ant.SetActive(true);
        }

        
        if (Input.GetMouseButtonDown(0)) {
            print("adsad");
            selectedStuff = Selectioning();
        }
        
        if (Input.GetMouseButtonDown(1)&&selectedStuff != null)
        {
            print("click droit");
            GameObject newDest = Selectioning();
            Collider2D col = (selectedStuff.GetComponent<Collider2D>());
            if (col.CompareTag("Unit")) {
                selectedStuff.GetComponent<Ant>().setTarget(newDest);
                selectedStuff.GetComponent<Ant>().idle = false;
                if (newDest == null) {
                    selectedStuff.GetComponent<Ant>().setPath(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                } else {
                    if (newDest.CompareTag("Food"))
                        selectedStuff.GetComponent<Ouvriere>().bouffe = newDest;
                }
            }
        }

    }

    public GameObject CreateSoldat() {
        GameObject ant = Instantiate(SoldatPrefab);
        listSoldat.Add(ant);
        return ant;
    }

    public GameObject CreateScout()
    {
        GameObject ant = Instantiate(ScoutPrefab);
        listSoldat.Add(ant);
        return ant;
    }

    public GameObject CreateOuvriere()
    {
        GameObject ant = Instantiate(OuvrierePrefab);
        listSoldat.Add(ant);
        return ant;
    }


    public GameObject Selectioning()
    {
        Vector2 origin = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
                                       Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.zero, 0f);
        if (hit)
        {
            print(hit.transform.gameObject.tag);
            return hit.transform.gameObject;
        }
        return null;
    }



}
