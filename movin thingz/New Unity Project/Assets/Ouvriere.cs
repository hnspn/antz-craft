using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ouvriere : Ant {

    public int strength;
    public int foodCarried;
    public GameObject bouffe;
    
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

    void OnTriggerEnter2D(Collider2D other)
    {
        print("trigger triggered !!!!!!!!!!!!");
        if (other.gameObject.CompareTag("bouffe"))
        {
            TakeFood(other.gameObject);
            setNewDestination(fourmilliere);
        }

        if (other.gameObject.CompareTag("home"))
        {
            DropFood(other.gameObject);
            if (this.bouffe != null)
                setNewDestination(bouffe);
            else
                idle = true;
                
        }



    }

    private void DropFood(GameObject gameObject)
    {
        this.fourmilliere.GetComponent<Fourmilliere>().foodStored += foodCarried;
        foodCarried = 0;
    }

    void TakeFood(GameObject food)
    {
        int foodQuantity = food.GetComponent<bouffe>().quantiteDeNourriture;
        print(foodQuantity);
        if (foodQuantity >= strength)
        {
            print("case1");
            food.GetComponent<bouffe>().quantiteDeNourriture -= strength;
            foodCarried = strength;
        } else if (foodQuantity > 0)
        {
            print("case2");
            foodCarried = foodQuantity;
            food.GetComponent<bouffe>().quantiteDeNourriture = 0;
        }
        if (food.GetComponent<bouffe>().quantiteDeNourriture <= 0)
            this.bouffe = null;
        this.setNewDestination(this.fourmilliere);


    }

}
