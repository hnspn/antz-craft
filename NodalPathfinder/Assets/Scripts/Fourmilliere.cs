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
    public GameObject food, scrap, science;


    public float foodStored = 0;
    public float scrapStored = 0;
    public float sciencePoints = 0;
    public int workerFoodCost;
    public int workerScrapCost;
    public int scoutFoodCost;
    public int scoutScrapCost;
    public int soldierFoodCost;
    public int soldierScrapCost;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("q"))
        {
            UnityEngine.Debug.Log(foodStored);
            if (foodStored >= workerFoodCost && scrapStored >= workerScrapCost) {
                GameObject ant = CreateOuvriere();
                ant.SetActive(true);
                foodStored -= workerFoodCost;
                scrapStored -= workerScrapCost;
            }
        }
        if (Input.GetKeyDown("w"))
        {
            if (foodStored >= scoutFoodCost && scrapStored >= scoutScrapCost) {
                GameObject ant = CreateScout();
                ant.SetActive(true);
                foodStored -= scoutFoodCost;
                scrapStored -= scoutScrapCost;
            }
        }
        if (Input.GetKeyDown("e"))
        {
            if (foodStored >= soldierFoodCost && scrapStored >= soldierScrapCost) {
                GameObject ant = CreateSoldat();
                ant.SetActive(true);
                foodStored -= soldierFoodCost;
                scrapStored -= soldierScrapCost;
            }

        }

        /*
        if (Input.GetMouseButtonDown(0)) {
            print("adsad");
            selectedStuff = Selectioning();
        }*/
        List<Ant> selectedObjects = RectangleSelection.selectedObjects;
        if (selectedObjects != null) {
            float i = 0;
            foreach (Ant unit in selectedObjects) {
                if (Input.GetMouseButtonDown(1) && unit != null) {
                    print("click droit");
                    GameObject newDest = Selectioning();
                    Collider2D col = (unit.GetComponent<Collider2D>());
                    if (col.CompareTag("Unit")) {
                        unit.GetComponent<Ant>().setTarget(newDest);
                        unit.GetComponent<Ant>().idle = false;
                        if (newDest == null) {
                            unit.GetComponent<Ant>().setPath((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition)
                                + new Vector2(
                                    i % (selectedObjects.ToArray().Length / 4.0f) - (selectedObjects.ToArray().Length / 4.0f),
                                    i - selectedObjects.ToArray().Length / 2.0f)
                                    );
                        } else {
                            if (newDest.CompareTag("Food"))
                                unit.GetComponent<Ouvriere>().bouffe = newDest;
                        }
                    }
                }
                i++;
            }
        }
        foodStored += 10 * Time.deltaTime;
        scrapStored += 5 * Time.deltaTime;
        sciencePoints += 1 * Time.deltaTime;

        food.GetComponent<UnityEngine.UI.Text>().text = "" + foodStored.ToString("N0");
        scrap.GetComponent<UnityEngine.UI.Text>().text = "" + scrapStored.ToString("N0");
        science.GetComponent<UnityEngine.UI.Text>().text = "" + sciencePoints.ToString("N0");
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
