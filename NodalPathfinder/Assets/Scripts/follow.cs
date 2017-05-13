using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class follow : MonoBehaviour {
    public BreadCrumb bc;
    public float speed;

    private Grid grid;
    private GameObject target;
	// Use this for initialization
	void Start () {
        grid = GameObject.Find("Grid").GetComponent<Grid>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (target != null) {
            Point playerPos = grid.WorldToGrid(this.transform.position);
            if (!grid.Nodes[playerPos.X, playerPos.Y].BadNode) {
            bc = PathFinder.FindPath(grid, grid.WorldToGrid(this.transform.position), grid.WorldToGrid(target.transform.position));
            int count = 0;
            LineRenderer lr = this.GetComponent<LineRenderer>();
            lr.SetVertexCount(100);  //Need a higher number than 2, or crashes out
            lr.SetWidth(0.1f, 0.1f);
            lr.SetColors(Color.yellow, Color.yellow);

            //Draw out our path
            BreadCrumb bc1 = bc;
            while (bc1 != null) {
                lr.SetPosition(count, Grid.GridToWorld(bc1.position));
                bc1 = bc1.next;
                count += 1;
            }
            lr.SetVertexCount(count);
            if (bc != null)
                if (bc.next != null)
                    bc = bc.next;
        }
        }
        //UnityEngine.Debug.Log(target.transform.position);
        if (bc == null)
            return;
        if(((Vector2)this.transform.position - Grid.GridToWorld(bc.position)).SqrMagnitude() < 1E-2f) {
            if(bc.next != null)
                bc = bc.next; 
        } else {
            this.Move(Grid.GridToWorld(bc.position));
        if (target != null) {
            //UnityEngine.Debug.Log(bc);
        }
        }
	}
    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
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
        }
        
    }
    void Move(Vector2 destination) {
        Vector2 currentPosition = this.transform.position;
        Vector3 direction = (destination - currentPosition).normalized;
        this.transform.position = Time.deltaTime * speed * direction + this.transform.position;


        //adjusting orientation to face destination
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        this.transform.rotation = Quaternion.RotateTowards(transform.rotation, q, Time.deltaTime * speed * 1000);


    }
}
