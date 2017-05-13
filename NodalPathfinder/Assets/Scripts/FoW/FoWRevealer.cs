using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FoWRevealer : MonoBehaviour {
    public int radius;

    private void Start() {
        FoWManager.Instance.RegisterRevealer(this);
    }
}

