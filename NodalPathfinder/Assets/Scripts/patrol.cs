using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class patrol : MonoBehaviour {
    public GameObject dest1, dest2;
    public float speed;
    private Vector2 dest;
	// Use this for initialization
	void Start () {
        dest = dest1.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        this.Move(dest);
        if(((Vector2)this.transform.position - dest).SqrMagnitude() < 1E-2f) {
            if (dest == (Vector2)dest1.transform.position)
                dest = dest2.transform.position;
            else
                dest = dest1.transform.position;
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
