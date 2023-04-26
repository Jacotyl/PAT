using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class riichiButton : MonoBehaviour
{
    public int playerID;

    public GameObject riichi;

    public GameObject manager;

    public void clickRiichi() {
        riichi.SetActive(true);
        manager.GetComponent<GameManager>().riichiStatus[playerID-1] = 1;
        manager.GetComponent<GameManager>().riichiCountTemp += 1;
        manager.GetComponent<GameManager>().playerScores[playerID-1] -= 1000;
        manager.GetComponent<GameManager>().updateScores();
    }    
}
