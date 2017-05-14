using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ouvriere : Ant {

    public int strength;
    public int foodCarried;
    public int scrapCarried;
    public GameObject Ressource;
    public string task;

    // Use this for initialization
    new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Food") || other.gameObject.CompareTag("Scrap")) {
            //print("trigger triggered ressourcde !!!!!!!!!!!!");
            TakeRessource(other.gameObject);
            setPath(fourmiliere.transform.position);
        }

        else if (other.gameObject.CompareTag("Home")) {
            //print("trigger triggered home !!!!!!!!!!!!");
            if (this.Ressource != null)
                setPath(Ressource.transform.position);
            else
                idle = true;
            DropRessources(other.gameObject);

        }



    }

    private void DropRessources(GameObject gameObject) {
        this.fourmiliere.GetComponent<Fourmiliere>().foodStored += foodCarried;
        this.fourmiliere.GetComponent<Fourmiliere>().scrapStored += scrapCarried;
        foodCarried = 0;
        scrapCarried = 0;
    }

    void TakeRessource(GameObject food) {
        if (foodCarried == 0) {
            int foodQuantity = food.GetComponent<Ressource>().quantiteDeRessource;
            //print(foodQuantity);
            if (foodQuantity >= strength) {
                //print("case1");
                food.GetComponent<Ressource>().quantiteDeRessource -= strength;
                foodCarried = strength;
            } else if (foodQuantity > 0) {
                //print("case2");
                foodCarried = foodQuantity;
                food.GetComponent<Ressource>().quantiteDeRessource = 0;
            }
            if (food.GetComponent<Ressource>().quantiteDeRessource <= 0)
                this.Ressource = null;
            this.setPath(this.fourmiliere.transform.position);
        }

    }
}
