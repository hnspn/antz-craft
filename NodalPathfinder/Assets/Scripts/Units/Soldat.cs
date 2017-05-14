using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldat : Ant {

    public int attack;

    // Use this for initialization
    new void Start() {
        base.Start();

    }



    // Update is called once per frame
    new void Update() {
        if (this.health <= 0)
            fourmiliere.GetComponent<Fourmiliere>().listSoldat.Remove(this.gameObject);
        base.Update();
    }

    void OnTriggerEnter2D(Collider2D other) {
        print("trigger atk collifder");
        LaunchAttack(other.gameObject);
    }

    void LaunchAttack(GameObject target) {
        if (target == null) throw new System.Exception("does not work, target is null");
        if (target.CompareTag("Unit")) {
            if (target.GetComponent<Ant>().team != this.team) {

                print("a lassaut !!!!!!!!!!!!");
                Attack(target, false);
            }

        } else if (target.CompareTag("Home")) {
            print("atk home");
            if (target.GetComponent<FourmiliereEnnemie>().team != this.team) {


                Attack(target, true);
            }
        }

    }

    void OnTriggerStay2D(Collider2D other) {
        LaunchAttack(other.gameObject);
    }

    public void Attack(GameObject enemy, bool isBase) {
        this.setTarget(enemy);
        if (isBase) {
            print("a lassaut de la base !! hp = " + enemy.GetComponent<FourmiliereEnnemie>().health);
            enemy.GetComponent<FourmiliereEnnemie>().health -= this.attack;
        } else {
            enemy.GetComponent<Ant>().health -= this.attack;
            print("a lassaut de lenemy !! hp = " + enemy.GetComponent<Ant>().health);
        }

    }


}
