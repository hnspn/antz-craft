using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourmiliereAlliee : Fourmiliere {
    public GameObject food, scrap, science;
    public int unitCreateTime;
    public GameObject progressBar;
    public GameObject[] qUnits;
    public Sprite[] sprites;

    private Queue<GameObject> q = new Queue<GameObject>();
    private float queueTimer = 0;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	new void Update () {
        GameObject[] qTemp = q.ToArray();
        for (int i = 0; i < q.Count; i++) {
            qUnits[i].GetComponent<UnityEngine.UI.Image>().sprite = sprites[qTemp[i].GetComponent<Ant>().type];
        }
        for (int i = q.Count; i < 5; i++) {
            qUnits[i].GetComponent<UnityEngine.UI.Image>().sprite = sprites[3];
        }
        if (q.Count > 0) {
            progressBar.GetComponent<Progress>().progress = queueTimer/(float)unitCreateTime;
            if (queueTimer <= 0) {
                queueTimer = unitCreateTime;
                progressBar.GetComponent<Progress>().progress = 1;
                GameObject ant = Instantiate(q.Dequeue());
                switch (ant.GetComponent<Ant>().type) {
                    case 0: {
                            listOuvriere.Add(ant);
                            ant.SetActive(true);
                            ant.GetComponent<Ant>().team = this.team;
                            ant.GetComponent<Ant>().health = workerMaxHealth;
                            ant.GetComponent<Ant>().speed = workerSpeed;
                            ant.GetComponent<Ouvriere>().strength = workerStrength;
                        }
                        break;
                    case 1: {
                            listScout.Add(ant);
                            ant.SetActive(true);
                            ant.GetComponent<Ant>().team = this.team;
                            ant.GetComponent<Ant>().health = scoutMaxHealth;
                            ant.GetComponent<Ant>().speed = scoutSpeed;
                        }
                        break;
                    case 2: {
                            listSoldat.Add(ant);
                            ant.SetActive(true);
                            ant.GetComponent<Ant>().team = this.team;
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
            GameObject ant = Instantiate(OuvrierePrefab);
            listOuvriere.Add(ant);
            ant.SetActive(true);
            ant.GetComponent<Ant>().team = this.team;
            ant.GetComponent<Ant>().health = workerMaxHealth;
            ant.GetComponent<Ant>().speed = workerSpeed;
            ant.GetComponent<Ouvriere>().strength = workerStrength;



        }
        if (Input.GetKeyDown("w")) {
            GameObject ant = Instantiate(ScoutPrefab);
            listScout.Add(ant);
            ant.SetActive(true);
            ant.GetComponent<Ant>().team = this.team;
            ant.GetComponent<Ant>().health = scoutMaxHealth;
            ant.GetComponent<Ant>().speed = scoutSpeed;
        }
        if (Input.GetKeyDown("e")) {
            GameObject ant = Instantiate(SoldatPrefab);
            listSoldat.Add(ant);
            ant.SetActive(true);
            ant.GetComponent<Ant>().team = this.team;
            ant.GetComponent<Ant>().health = soldierMaxHealth;
            ant.GetComponent<Ant>().speed = soldierSpeed;
            ant.GetComponent<Soldat>().attack = soldierAttack;
        }

        /*
        if (Input.GetMouseButtonDown(0)) {
            print("adsad");
            selectedStuff = Selectioning();
        }*/
        List<Ant> selectedObjects = RectangleSelection.selectedObjects;
        if (selectedObjects != null) {
            if (Input.GetKeyDown(KeyCode.Delete)) {
                foreach(Ant ant in selectedObjects) {
                    ant.health = 0;
                }
                selectedObjects = new List<Ant>();
            }
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
    
        base.Update();
	}

    public new bool CreateSoldat() {
        if (foodStored >= soldierFoodCost && scrapStored >= soldierScrapCost && q.Count < 5) {
            if(q.Count==0)
                queueTimer = unitCreateTime;
            q.Enqueue(SoldatPrefab);
            foodStored -= soldierFoodCost;
            scrapStored -= soldierScrapCost;
            return true;
        }
        return false;
    }

    public new bool CreateScout() {
        if (foodStored >= scoutFoodCost && scrapStored >= scoutScrapCost && q.Count < 5) {
            if (q.Count == 0)
                queueTimer = unitCreateTime;
            q.Enqueue(ScoutPrefab);
            foodStored -= scoutFoodCost;
            scrapStored -= scoutScrapCost;

            return true;
        }
        return false;
    }

    public new bool CreateOuvriere() {
        if (foodStored >= workerFoodCost && scrapStored >= workerScrapCost && q.Count < 5) {
            if (q.Count == 0)
                queueTimer = unitCreateTime;
            q.Enqueue(OuvrierePrefab);
            foodStored -= workerFoodCost;
            scrapStored -= workerScrapCost;
            return true;
        }
        return false;
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
