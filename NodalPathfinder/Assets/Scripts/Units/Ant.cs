using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ant : MonoBehaviour {
    public Sprite[] sprites;
    public GameObject selectionCircle;

    public Vector2 destination;
    public float speed;
    public float health;
    public float sightRange;
    public Boolean idle;
    public GameObject fourmiliere;
    public int team;
    public int updateTargetFrequency = 60;

    public BreadCrumb bc;

    private Grid grid;
    public GameObject target;
    private int counter = 0, spr = 0, cntTrgt = 0;
    public int type;

    public void Awake () {
        this.GetComponent<SpriteRenderer>().sprite = sprites[0];
        grid = GameObject.Find("Grid").GetComponent<Grid>();
        this.transform.position = fourmiliere.transform.position+ new Vector3(UnityEngine.Random.Range(-0.5f,0.5f), UnityEngine.Random.Range(-0.5f, 0.5f),0f);
        this.destination = this.transform.position;
        idle = true;
	}

    // Use this for initialization
    public void Start() {

    }

    void FixedUpdate() {
        if(destination == null) {
            destination = transform.position;
        }
        if (health <= 0)
            Destroy(gameObject);
        if (target != null) {
            if (cntTrgt >= updateTargetFrequency) {
                cntTrgt = 0;
                Point playerPos = grid.WorldToGrid(transform.position);
                if (!grid.Nodes[playerPos.X, playerPos.Y].BadNode) {
                    bc = PathFinder.FindPath(grid, grid.WorldToGrid(transform.position), grid.WorldToGrid(target.transform.position));
                    if (bc != null)
                        if (bc.next != null)
                            bc = bc.next;
                } 
            }
        }
        cntTrgt++;
        //UnityEngine.Debug.Log(target.transform.position);
        if (bc == null) {
            if(((Vector2)this.transform.position - destination).SqrMagnitude() > 1E-2f)
                Move(destination);
            return;
        }
        if (((Vector2)this.transform.position - Grid.GridToWorld(bc.position)).SqrMagnitude() < 1E-2f) {
            if (bc.next != null)
                bc = bc.next;
        } else {
            this.Move(Grid.GridToWorld(bc.position));
            if (target != null) {
                //UnityEngine.Debug.Log(bc);
            }
        }
    }

    // Update is called once per frame
    public void Update () {
        /*if (Input.GetMouseButtonDown(1) && sel) {
            //Convert mouse click point to grid coordinates
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero, 0f);

            if (hit && hit.collider.tag == "Unit")
                target = hit.transform.gameObject;
            else
                target = null;
            Point gridPos = grid.WorldToGrid(worldPos);
            if (gridPos != null) {

                if (gridPos.X > 0 && gridPos.Y > 0 && gridPos.X < grid.Width && gridPos.Y < grid.Height) {

                    //Convert player point to grid coordinates
                    Point playerPos = grid.WorldToGrid(this.transform.position);
                    grid.Nodes[playerPos.X, playerPos.Y].SetColor(Color.blue);

                    //Find path from player to clicked position
                    bc = PathFinder.FindPath(grid, playerPos, gridPos);
                }
            }
        }*/

    }

    public void setTarget(GameObject target) {
        this.target = target;
    }

    public void setPath(Vector2 destination) {
        target = null;
        RaycastHit2D hit = Physics2D.Raycast(destination, (Vector2)this.transform.position - destination);
        //if(hit) {
            //if (hit.collider.gameObject.GetComponent<Ant>() == null || hit.collider.gameObject.GetComponent<Ant>() != this) {
                Point gridPos = grid.WorldToGrid(destination);
                if (gridPos != null) {
                    if (gridPos.X > 0 && gridPos.Y > 0 && gridPos.X < grid.Width && gridPos.Y < grid.Height) {

                        //Convert player point to grid coordinates
                        Point playerPos = grid.WorldToGrid(this.transform.position);
                        //grid.Nodes[playerPos.X, playerPos.Y].SetColor(Color.blue);

                        //Find path from player to clicked position
                        bc = PathFinder.FindPath(grid, playerPos, gridPos);
                    }
               // }
           // }
      //  } else {
            //bc = null;
            //print(bc);
           // this.destination = destination;
            //print(destination);
        } 
        
    }


    void Move(Vector2 destination) {
        animate();
        Vector2 currentPosition = this.transform.position;
        Vector3 direction = (destination - currentPosition).normalized;
        this.transform.position = Time.deltaTime * speed * direction + this.transform.position;


        //adjusting orientation to face destination
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        this.transform.rotation = Quaternion.RotateTowards(transform.rotation, q, Time.deltaTime * speed * 1000);


    }

    void animate() {
        counter++;
        if(counter % 3 == 0) {
            this.GetComponent<SpriteRenderer>().sprite = sprites[spr++];
            spr %= sprites.Length;
        }
    }




}
