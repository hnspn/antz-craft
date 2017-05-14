using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class RectangleSelection : MonoBehaviour {


    bool isSelecting = false;
    Vector3 mousePosition1;
    public static List<Ant> selectedObjects;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.timeScale > 0) {
            // If we press the left mouse button, begin selection and remember the location of the mouse
            if (Input.GetMouseButtonDown(0)) {
                selectedObjects = new List<Ant>();
                isSelecting = true;
                mousePosition1 = Input.mousePosition;


                // foreach (var selectableObject in FindObjectsOfType<Ant>())
                // {
                //     if (selectableObject.selectionCircle != null)
                //     {
                //         Destroy(selectableObject.selectionCircle.gameObject);
                //         selectableObject.selectionCircle = null;
                //     }
                // }
            }
            // If we let go of the left mouse button, end selection
            if (Input.GetMouseButtonUp(0)) {
                if ((mousePosition1 - Input.mousePosition).sqrMagnitude < 1) {
                    Vector2 origin = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
                                          Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
                    RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.zero, 0f);
                    if (hit) {
                        if (hit.transform.gameObject.tag == "Unit" && hit.transform.gameObject.GetComponent<Ant>().team == 0) {
                            selectedObjects.Add(hit.transform.gameObject.GetComponent<Ant>());
                            hit.transform.gameObject.GetComponent<Ant>().selectionCircle.GetComponent<Renderer>().enabled = true;
                        }
                    }
                } else {
                    foreach (var selectableObject in FindObjectsOfType<Ant>()) {
                        if (IsWithinSelectionBounds(selectableObject.gameObject) && selectableObject.gameObject.GetComponent<Ant>().team == 0) {
                            selectedObjects.Add(selectableObject);
                        }
                    }

                    var sb = new StringBuilder();
                    sb.AppendLine(string.Format("Selecting [{0}] Units", selectedObjects.Count));
                    foreach (var selectedObject in selectedObjects)
                        sb.AppendLine("-> " + selectedObject.gameObject.name);
                    //Debug.Log(sb.ToString());
                }
                isSelecting = false;

            }

            // Highlight all objects within the selection box

            if (isSelecting) {
                foreach (var selectableObject in FindObjectsOfType<Ant>()) {
                    if (IsWithinSelectionBounds(selectableObject.gameObject) && selectableObject.team == 0) {
                        selectableObject.GetComponent<Ant>().selectionCircle.GetComponent<Renderer>().enabled = true;
                    } else {
                        selectableObject.GetComponent<Ant>().selectionCircle.GetComponent<Renderer>().enabled = false;
                    }

                }
            } 
        }
    }

    private void OnGUI() {
        if (Time.timeScale > 0) {
            if (isSelecting) {
                // Create a rect from both mouse positions
                var rect = Utils.GetScreenRect(mousePosition1, Input.mousePosition);
                Utils.DrawScreenRect(rect, new Color(0.8f, 0.8f, 0.95f, 0.25f));
                Utils.DrawScreenRectBorder(rect, 2, new Color(0.8f, 0.8f, 0.95f));
            }
        }
    }

    public bool IsWithinSelectionBounds(GameObject gameObject)
    {
        if (!isSelecting)
            return false;

        var camera = Camera.main;
        var viewportBounds =
            Utils.GetViewportBounds(camera, mousePosition1, Input.mousePosition);

        return viewportBounds.Contains(
            camera.WorldToViewportPoint(gameObject.transform.position));
    }


}
