using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cameroscrolling : MonoBehaviour {

    public float scrollSpeed;
    public float scrollDistance;
    public GameObject background;
    public int depassement;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float mousePosX = Input.mousePosition.x;
        float mousePosY = Input.mousePosition.y;
        
        if (mousePosX < scrollDistance && (background.transform.position.x - this.transform.position.x)<depassement)
        {
            transform.Translate(Vector3.right * -scrollSpeed * Time.deltaTime);
        }

        if (mousePosX >= Screen.width - scrollDistance && (this.transform.position.x - background.transform.position.x) < depassement)
        {
            transform.Translate(Vector3.right * scrollSpeed * Time.deltaTime);
        }

        if (mousePosY < scrollDistance && (background.transform.position.y - this.transform.position.y) < depassement)
        {
            transform.Translate(transform.up * -scrollSpeed * Time.deltaTime);
        }

        if (mousePosY >= Screen.height - scrollDistance && (this.transform.position.y - background.transform.position.y) < depassement)
        {
            transform.Translate(transform.up * scrollSpeed * Time.deltaTime);
        }

        var d = Input.GetAxis("Mouse ScrollWheel");
        if (d > 0f) {
            if(this.GetComponent<Camera>().orthographicSize > 8)
                this.GetComponent<Camera>().orthographicSize /= 1.1f;
        } else if (d < 0f) {
            if (this.GetComponent<Camera>().orthographicSize < 40)
                this.GetComponent<Camera>().orthographicSize *= 1.1f;
        }
    }
}
