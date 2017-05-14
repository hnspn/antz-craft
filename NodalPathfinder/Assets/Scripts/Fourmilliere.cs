using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Fourmilliere : MonoBehaviour {

    public List<GameObject> listScout = new List<GameObject>();
    public List<GameObject> listOuvriere = new List<GameObject>();
    public List<GameObject> listSoldat = new List<GameObject>();

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

    public int soldierAttack;
    public int soldierMaxHealth;
    public int soldierSpeed;
    public int scoutMaxHealth;
    public int scoutSpeed;
    public int workerStrength;
    public int workerMaxHealth;
    public int workerSpeed;
    public int unitCreateTime;

    private Queue<int> q = new Queue<int>();
    private float queueTimer = 0;
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

        print(q.Count);
        if (q.Count > 0) {
            if (queueTimer <= 0) {
                queueTimer = unitCreateTime;
                switch (q.Dequeue()) {
                    case 0: {
                            GameObject ant = Instantiate(OuvrierePrefab);

                            listOuvriere.Add(ant);
                            ant.SetActive(true);
                            ant.GetComponent<Ant>().health = workerMaxHealth;
                            ant.GetComponent<Ant>().speed = workerSpeed;
                            ant.GetComponent<Ouvriere>().strength = workerStrength;
                        }
                        break;
                    case 1: {
                            GameObject ant = Instantiate(ScoutPrefab);

                            listScout.Add(ant);
                            ant.SetActive(true);
                            ant.GetComponent<Ant>().health = scoutMaxHealth;
                            ant.GetComponent<Ant>().speed = scoutSpeed;
                        }
                        break;
                    case 2: {
                            GameObject ant = Instantiate(SoldatPrefab);

                            listSoldat.Add(ant);
                            ant.SetActive(true);
                            ant.GetComponent<Ant>().health = soldierMaxHealth;
                            ant.GetComponent<Ant>().speed = soldierSpeed;
                            ant.GetComponent<Soldat>().attack = soldierAttack;
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        if (queueTimer > 0)
            queueTimer -= Time.deltaTime;

        if (Input.GetKeyDown("q")) {
                        foodStored += workerFoodCost;
                        scrapStored += workerScrapCost;
                        CreateOuvriere();
                    }
                    if (Input.GetKeyDown("w")) {
                        foodStored += scoutFoodCost;
                        scrapStored += scoutScrapCost;
                        CreateScout();
                    }
                    if (Input.GetKeyDown("e")) {
                        foodStored += soldierFoodCost;
                        scrapStored += soldierScrapCost;
                        CreateSoldat();
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
                    //print("click droit");
                    GameObject newDest = Selectioning();
                    Collider2D col = (unit.GetComponent<Collider2D>());
                    if (newDest == null) {
                        unit.GetComponent<Ant>().setPath((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition)
                            + new Vector2(
                                i % (selectedObjects.ToArray().Length / 4.0f) - (selectedObjects.ToArray().Length / 4.0f),
                                i - selectedObjects.ToArray().Length / 2.0f)
                                );
                    } else {
                        if (col.CompareTag("Unit")) {
                            unit.GetComponent<Ant>().setPath(newDest.transform.position);
                            unit.GetComponent<Ant>().idle = false;
                            if ((newDest.CompareTag("Food") || newDest.CompareTag("Scrap")) && unit.GetComponent<Ouvriere>() != null)
                                unit.GetComponent<Ouvriere>().Ressource = newDest;
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

    public bool CreateSoldat() {
        if (foodStored >= soldierFoodCost && scrapStored >= soldierScrapCost) {
            q.Enqueue(2);
            foodStored -= soldierFoodCost;
            scrapStored -= soldierScrapCost;

            return true;
        }
        return false;
    }

    public bool CreateScout() {
        if (foodStored >= scoutFoodCost && scrapStored >= scoutScrapCost) {
            q.Enqueue(1);
            foodStored -= scoutFoodCost;
            scrapStored -= scoutScrapCost;

            return true;
        }
        return false;
    }

    public bool CreateOuvriere() {
        if (foodStored >= workerFoodCost && scrapStored >= workerScrapCost) {
            q.Enqueue(0);
            foodStored -= workerFoodCost;
            scrapStored -= workerScrapCost;
            return true;
        }
        return false;
    }


    public GameObject Selectioning() {
        Vector2 origin = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
                                       Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.zero, 0f);
        if (hit) {
            print(hit.transform.gameObject.tag);
            return hit.transform.gameObject;
        }
        return null;
    }



}
