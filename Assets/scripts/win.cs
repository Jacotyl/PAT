using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class win : MonoBehaviour
{
    public int playerID;

    public GameObject tileInput;

    public GameObject mainScreen;

    public GameObject manager;

    public GameObject settings;

    public void onClick() {
        manager.GetComponent<GameManager>().winnerID = playerID;
        settings.SetActive(false);
        mainScreen.SetActive(false);
        tileInput.SetActive(true);
        manager.GetComponent<cleanup>().resetTiles();
    }
}
