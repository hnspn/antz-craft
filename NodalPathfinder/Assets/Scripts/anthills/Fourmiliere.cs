using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Fourmiliere : MonoBehaviour {

    public List<GameObject> listScout = new List<GameObject>();
    public List<GameObject> listOuvriere = new List<GameObject>();
    public List<GameObject> listSoldat = new List<GameObject>();

    public GameObject ScoutPrefab;
    public GameObject OuvrierePrefab;
    public GameObject SoldatPrefab;
    public GameObject enemyBase;

    public int team;
    public int health;
    

    public float foodStored = 0;
    public float scrapStored = 0;
    public float sciencePoints = 0;
    public int workerFoodCost;
    public int workerScrapCost;
    public int scoutFoodCost;
    public int scoutScrapCost;
    public int soldierFoodCost;
    public int soldierScrapCost;

    public int workerStrength;
    public int workerMaxHealth;
    public int workerSpeed;
    public int scoutMaxHealth;
    public int scoutSpeed;
    public int soldierAttack;
    public int soldierMaxHealth;
    public int soldierSpeed;


    // Use this for initialization
    void Start () {
       
	}
	
	// Update is called once per frame
	public void Update () {
        
    }

    public GameObject CreateSoldat()
    {
        GameObject ant = Instantiate(SoldatPrefab);

        ant.SetActive(true);
        listSoldat.Add(ant);
        foodStored -= soldierFoodCost;
        scrapStored -= soldierScrapCost;
        return ant;
    }

    public GameObject CreateScout()
    {
        GameObject ant = Instantiate(ScoutPrefab);

        ant.SetActive(true);
        listScout.Add(ant);
        foodStored -= scoutFoodCost;
        scrapStored -= scoutScrapCost;
        return ant;
    }

    public GameObject CreateOuvriere()
    {
        GameObject ant = Instantiate(OuvrierePrefab);

        ant.SetActive(true);
        listOuvriere.Add(ant);
        foodStored -= workerFoodCost;
        scrapStored -= workerScrapCost;
        return ant;
    }


   



}
