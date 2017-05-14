using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Progress : MonoBehaviour {
    public float progress; //current progress
    public Vector2 pos;
    public Vector2 size;
    public Texture2D emptyTex;
    public Texture2D fullTex;

    void OnGUI() {

        float posX = Screen.width * pos.x;
        float posY = Screen.height * pos.y;

        //draw the background:
        GUI.BeginGroup(new Rect(posX, posY, size.x, size.y));
        GUI.DrawTexture(new Rect(0, 0, size.x, size.y), emptyTex);

        //draw the filled-in part:
        //GUI.BeginGroup(new Rect(0, 0, size.x * progress, size.y));
        //GUI.DrawTexture(new Rect(0,0, size.x, size.y), fullTex);
        //draw the filled-in part FLIPPED:
        int xProg = (int)(size.x * progress);
        GUI.BeginGroup(new Rect(size.x - xProg, 0, xProg, size.y));
        GUI.DrawTexture(new Rect(-size.x + xProg, 0, size.x, size.y), fullTex);

        GUI.EndGroup();
        GUI.EndGroup();
    }

    // Use this for initialization
    void Start() {
    }

    // Update is called once per frame
    void Update() {
    }
}