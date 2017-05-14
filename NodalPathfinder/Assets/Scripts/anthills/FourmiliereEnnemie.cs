using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class FourmiliereEnnemie : Fourmiliere
{

   

    public bool WorkerCreationRunning;
    public bool SoldierCreationRunning;

    public GameObject Wintext;


    public string mode;
    public string atkmode;
    public bool ForceAtk;

    // Use this for initialization
    void Start()
    {
        WorkerCreationRunning = false;
        SoldierCreationRunning = false;
        ForceAtk = false;
        Wintext.SetActive(false);
    }

    // Update is called once per frame
    new void Update()
    {
        if (health <= 0)
        {
            print("win cond entered");
            WinCondition(); 
        }
        Updateresources();

        if ((Time.time < 60 || foodStored < 100 || scrapStored < 100) && !ForceAtk)
            mode = "ECO";
        else if (true)
            mode = "ATK";


        if (mode == "ATK")
            Attack();
        else if (mode == "ECO" && !WorkerCreationRunning)
        {
            print("starting routine");
            StartCoroutine("Expand");
            
        }
            
        else if (mode == "TECH")
            throw new System.Exception("not imple;ented yet");
        
    }


    public void WinCondition()
    {
        Time.timeScale = 0;
        Wintext.SetActive(true);

    }
  

    void Attack()
    {
        print("them with " + enemyBase.GetComponent<Fourmiliere>().listSoldat.Count + " vs us with "+ listSoldat.Count);
        if (enemyBase.GetComponent<Fourmiliere>().listSoldat.Count  < listSoldat.Count)
            atkmode = "RushB(ase)";
        else
            atkmode = "TuerLaForceOuvriere";

        if (!SoldierCreationRunning)
            StartCoroutine("ExpandArmy");


        if (atkmode == "RushB(ase)")
        {
            foreach (GameObject ant in listSoldat)
            {

                ant.GetComponent<Ant>().setTarget(enemyBase);
            }
        } else if (atkmode == "TuerLaForceOuvriere")
        {
            int n = enemyBase.GetComponent<Fourmiliere>().listOuvriere.Count;
            int i = 0;
            foreach (GameObject ant in listSoldat)
            {
                if(ant.GetComponent<Ant>().target == null)
                    ant.GetComponent<Ant>().setTarget(enemyBase.GetComponent<Fourmiliere>().listOuvriere[i++%n]);

            }
        }

    }


   

    private IEnumerator Expand()
    {
        for(;;) 
        {
            print("inroutine !");
            if (foodStored >= workerFoodCost && scrapStored >= workerScrapCost)
            {
                GameObject a = CreateOuvriere();

                a.GetComponent<Ant>().team = 1;
                listOuvriere.Add(a);
                SendWorkersOnRessource(0.8f);
            }
            WorkerCreationRunning = true;
            yield return new WaitForSeconds(1f);
        }
    }

    private IEnumerator ExpandArmy()
    {
        for (;;)
        {
            print("inroutine !");
            if (foodStored >= soldierFoodCost && scrapStored >= soldierScrapCost)
            {
                GameObject a = CreateSoldat();

                a.GetComponent<Ant>().team = 1;
                listSoldat.Add(a);
            }
            SoldierCreationRunning = true;
            yield return new WaitForSeconds(1f);
        }
    }



    public GameObject SelectClosestFoodSource()
    {
        GameObject res = null;
        float dist = Mathf.Infinity;
        foreach(Ressource a in FindObjectsOfType<Ressource>())
        {
            if( a.typeRessources == 0 && a.gameObject.activeInHierarchy)
            {
                float d = Vector3.Distance(a.transform.position, transform.position);
                if (d <= dist)
                {
                    dist = d;
                    res = a.gameObject;
                }
           
            }
        }
        return res;
    }

    public GameObject SelectClosestScrapSource()
    {
        GameObject res = null;
        float dist = Mathf.Infinity;
        foreach (Ressource a in FindObjectsOfType<Ressource>())
        {
            if (a.typeRessources == 1)
            {
                float d = Vector3.Distance(a.transform.position, transform.position);
                if (d <= dist)
                {
                    dist = d;
                    res = a.gameObject;
                }

            }
        }
        return res;
    }


    public void SendWorkersOnRessource(float ratioFoodies)
    {
        GameObject foodlocal = SelectClosestFoodSource();
        GameObject scraplocal = SelectClosestScrapSource();

        if (foodlocal == null)
            ratioFoodies = 0f;
        if (scraplocal == null)
            ratioFoodies = 1;
        if (foodlocal == null && scraplocal == null)
        {
            print("No more ressources on the map");
            ForceAtk = true;
            return;
        }

        float foodies = 0f;
        float scrapies = 0f;
        int i = 0;
        float ratio = 0f;
        foreach(GameObject ant in listOuvriere)
        {
            print(++i + " ant");
            if(ant.GetComponent<Ouvriere>().Ressource == null)
            {
                if (ratio <= ratioFoodies)
                {
                    print("asd");
                    ant.GetComponent<Ant>().setPath(foodlocal.transform.position);
                    ant.GetComponent<Ouvriere>().Ressource = foodlocal;
                    ant.GetComponent<Ouvriere>().task = "food";
                }
                else
                {
                    print("ddd");
                    ant.GetComponent<Ant>().setPath(scraplocal.transform.position);
                    ant.GetComponent<Ouvriere>().Ressource = scraplocal;
                    ant.GetComponent<Ouvriere>().task = "scrap";
                }
            }
            //else if (ant.GetComponent<Ouvriere>().foodCarried != 0  || ant.GetComponent<Ouvriere>().target.GetComponent<Ressource>().typeRessources == 0)
            //    foodies++;
            //else if (ant.GetComponent<Ouvriere>().scrapCarried != 0  || ant.GetComponent<Ouvriere>().target.GetComponent<Ressource>().typeRessources == 1)
            //    scrapies++;
            //

            if (ant.GetComponent<Ouvriere>().task == "food")
                foodies++;
            else if (ant.GetComponent<Ouvriere>().task == "scrap")
                scrapies++;

            if (scrapies == 0f && foodies == 0f)
                ratio = 0f;
            else
                ratio = foodies / (foodies+scrapies);
            print("ratio = " + ratio);
            
        }
    }

    public void Updateresources()
    {
        foodStored += 10 * Time.deltaTime;
        scrapStored += 5 * Time.deltaTime;
        sciencePoints += 1 * Time.deltaTime;
    }

}
