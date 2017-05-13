using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour {


    private int food;
    private int scrap;
    private int science;

    private bool upgradesOn = false;

    public void Start() {
        Debug.Log("Start of program");
    }

    public void showUpgrades() {
            SceneManager.LoadScene("Upgrades", LoadSceneMode.Additive);


    }
}
