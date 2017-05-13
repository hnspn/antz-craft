using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ant : MonoBehaviour {


    public GameObject destination;
    public float speed;
    public float health;
    public float sightRange;
    public Boolean idle;
    public GameObject fourmilliere;
    public string team;


	// Use this for initialization
	public void Start () {
        this.transform.position = fourmilliere.transform.position+ new Vector3(UnityEngine.Random.Range(-0.5f,0.5f), UnityEngine.Random.Range(-0.5f, 0.5f),0f);
        idle = true;
	}
	
	// Update is called once per frame
	public void Update () {
        if (destination == null)
            idle = true;
        if (!idle)
            this.Move();
        if (this.health <= 0)
            Destroy(this.gameObject);

	}

    public void setNewDestination(GameObject dest) {
        destination = dest;
    }


    void Move() {
        Vector2 currentPosition = this.transform.position;
        Vector2 destination = this.destination.transform.position;
        Vector3 direction = (destination - currentPosition).normalized;
        this.transform.position = Time.deltaTime*speed*direction + this.transform.position;


        //adjusting orientation to face destination
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg -90f;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        this.transform.rotation = Quaternion.RotateTowards(transform.rotation, q, Time.deltaTime * speed*1000);


    }


   



}
