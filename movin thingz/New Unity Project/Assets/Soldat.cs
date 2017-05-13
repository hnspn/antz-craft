using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldat : Ant {

    public float attack;

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
        if (other.gameObject.CompareTag("scout") || other.gameObject.CompareTag("soldat") || other.gameObject.CompareTag("ouvriere"))
        {
            print("allo");
            if (other.gameObject.GetComponent<Ant>().team != this.team)
            {

                print("a lassaut !!!!!!!!!!!!");
                Attack(other.gameObject);
            }
            
        }
    }


    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("scout") || other.gameObject.CompareTag("soldat") || other.gameObject.CompareTag("ouvriere"))
        {
            print("allo");
            if (other.gameObject.GetComponent<Ant>().team != this.team)
            {

                print("a lassaut !!!!!!!!!!!!");
                Attack(other.gameObject);
            }

        }
    }

    public void Attack(GameObject enemy)
    {
        this.setNewDestination(enemy);
        enemy.GetComponent<Ant>().health -= this.attack;
    }


}
