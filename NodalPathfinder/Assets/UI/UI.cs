using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour {
    public FourmiliereAlliee fourmiliere;
    public Sprite[] sprites;
    public GameObject image,hp, upgrades, settings;
    private bool upgradesOn = false, settingsOn = false, errorMessage;

    public void Start() {
        UnityEngine.Debug.Log("Start of program");
    }

    public void showUpgrades() {
        if (upgradesOn ) {
            upgrades.SetActive(true);
            Time.timeScale = 0;
            upgradesOn = false;
        } else {
            upgrades.SetActive(false);
            Time.timeScale = 1;
            upgradesOn = true;
        }
    }

    public void showSettings() {
        if (settingsOn ) {
            settings.SetActive(true);
            Time.timeScale = 0;
            settingsOn = false;
        } else {
            settings.SetActive(false);
            Time.timeScale = 1;
            settingsOn = true;
        }
    }
    public void createUnit(string unitType) {
        switch (unitType) {
            case "worker": {
                    fourmiliere.CreateOuvriere();
                }break;
            case "scout": {
                    fourmiliere.CreateScout();
                }break;
            case "soldier": {
                    fourmiliere.CreateSoldat();
                }break;
            default:break;
        }
    }

    public void Update() {
        if (RectangleSelection.selectedObjects != null && RectangleSelection.selectedObjects.Count >= 1) {
            Ant ant = RectangleSelection.selectedObjects[0];
            string name = ant.name;
            switch (name) {
                case "AntWorker(Clone)": {
                        image.GetComponent<UnityEngine.UI.Image>().sprite = sprites[0];
                        hp.GetComponent<UnityEngine.UI.Text>().text = ant.health.ToString("N0");
                    }
                    break;
                case "AntScout(Clone)": {
                        image.GetComponent<UnityEngine.UI.Image>().sprite = sprites[1];
                        hp.GetComponent<UnityEngine.UI.Text>().text = ant.health.ToString("N0");
                    }
                    break;
                case "AntSoldier(Clone)": {
                        image.GetComponent<UnityEngine.UI.Image>().sprite = sprites[2];
                        hp.GetComponent<UnityEngine.UI.Text>().text = ant.health.ToString("N0");
                    }
                    break;
                default: break;
            }
        } else {
            image.GetComponent<UnityEngine.UI.Image>().sprite = sprites[3];
        }
    }

}
