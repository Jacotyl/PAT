using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cleanup : MonoBehaviour
{
    public List<GameObject> riichiSticks = new List<GameObject>();

    public GameObject roundCounter;

    public GameObject manager;

    public GameObject riichiCounter;

    public List<GameObject> dealerIcons = new List<GameObject>();

    public List<bool> tenpaiStatuses = new List<bool>();

    public List<GameObject> tenpaiBoxes = new List<GameObject>();

    public GameObject exhaustiveDrawButton;

    public GameObject exhaustiveDrawButton2;

    public GameObject settings;

    public List<GameObject> buttons;

    public Sprite tileBack;


    public void winCleanup() {
        manager.GetComponent<GameManager>().closedHandIndex = 0;
        manager.GetComponent<GameManager>().closedKanIndex = 0;
        manager.GetComponent<GameManager>().openKanIndex = 0;
        manager.GetComponent<GameManager>().chiiIndex = 0;
        manager.GetComponent<GameManager>().ponIndex = 0;
        manager.GetComponent<GameManager>().closedWinningHand.Clear();
        manager.GetComponent<GameManager>().winningClosedKan.Clear();
        manager.GetComponent<GameManager>().winningOpenKan.Clear();
        manager.GetComponent<GameManager>().winningPon.Clear();
        manager.GetComponent<GameManager>().winningChii.Clear();
        manager.GetComponent<GameManager>().winningTile = 0;
        manager.GetComponent<GameManager>().hanCount = 0;
        manager.GetComponent<GameManager>().yakumanCount = 0;
        manager.GetComponent<GameManager>().fuCount = 0;
        manager.GetComponent<GameManager>().pair.Clear();
        manager.GetComponent<GameManager>().group1.Clear();
        manager.GetComponent<GameManager>().group2.Clear();
        manager.GetComponent<GameManager>().group3.Clear();
        manager.GetComponent<GameManager>().group4.Clear();
        manager.GetComponent<GameManager>().flattenedHand.Clear();
        manager.GetComponent<GameManager>().groups.Clear();
        manager.GetComponent<GameManager>().convertedGroups.Clear();
        manager.GetComponent<GameManager>().yakuList.Clear();
        manager.GetComponent<GameManager>().ron = false;
        for(int i = 0; i < manager.GetComponent<GameManager>().payoutValues.Count; i++) {
            manager.GetComponent<GameManager>().payoutValues[i] = 0;
        }
        
        if(manager.GetComponent<GameManager>().dealerID == manager.GetComponent<GameManager>().winnerID) { //dealer win
            manager.GetComponent<GameManager>().repeatCount++;
        }
        else {
            if(manager.GetComponent<GameManager>().dealerID == 4) {
                manager.GetComponent<GameManager>().dealerID = 1;
                manager.GetComponent<GameManager>().roundCount++;
             
            }
            else{
                manager.GetComponent<GameManager>().dealerID++;
                manager.GetComponent<GameManager>().roundCount++;
                
            }

        }

        manager.GetComponent<GameManager>().riichiCount = 0;
        manager.GetComponent<GameManager>().riichiCountTemp = 0;

        
        updateDealer();
        updateRound();
        updateRiichi();
        hideSticks();
        settings.SetActive(true);


    }

    public void exhaustiveDraw() {
        settings.SetActive(false);
        hideButtons();
        for(int i = 0; i < tenpaiBoxes.Count; i++) {
            tenpaiBoxes[i].SetActive(true);
        }
        exhaustiveDrawButton.SetActive(false);
        exhaustiveDrawButton2.SetActive(true);
    }
    public void exhaustiveDrawCleanup() {
        for(int i = 0; i < tenpaiBoxes.Count; i++) {
            Debug.Log(tenpaiBoxes[i].GetComponent<Toggle>().isOn);
        }
        int tenpaiCount = 0;
        for(int i = 0; i < tenpaiBoxes.Count; i++) {
            tenpaiStatuses.Add(tenpaiBoxes[i].GetComponent<Toggle>().isOn);
        }
        for(int i = 0; i < tenpaiStatuses.Count; i++) {
            if(tenpaiStatuses[i]) {
                tenpaiCount++;
            }
        }
        Debug.Log("tenpai count: " + tenpaiCount);
        if(tenpaiCount != 4 && tenpaiCount != 0) {
            for(int i = 0; i < tenpaiStatuses.Count; i++) {
                if(!tenpaiStatuses[i]) {
                    manager.GetComponent<GameManager>().playerScores[i] -= (3000 / (4 - tenpaiCount));
                }
                else {
                    manager.GetComponent<GameManager>().playerScores[i] += (3000 / tenpaiCount);
                }
            }
        }
    

        manager.GetComponent<GameManager>().updateScores();
        


        
        if(!tenpaiStatuses[manager.GetComponent<GameManager>().dealerID-1]) {
            if(manager.GetComponent<GameManager>().dealerID == 4) {
                manager.GetComponent<GameManager>().dealerID = 1;
                manager.GetComponent<GameManager>().roundCount++;
                manager.GetComponent<GameManager>().repeatCount = 0;
            }
            else {
                manager.GetComponent<GameManager>().dealerID++;
                manager.GetComponent<GameManager>().roundCount++;
                manager.GetComponent<GameManager>().repeatCount = 0;
            }
            
        }
        else {
            manager.GetComponent<GameManager>().repeatCount++;
        }

        for(int i = 0; i < tenpaiBoxes.Count; i++) {
            tenpaiBoxes[i].SetActive(false);
        }

        tenpaiStatuses.Clear();

        updateDealer();
        updateRound();
        updateRiichi();
        hideSticks();

        exhaustiveDrawButton.SetActive(true);
        exhaustiveDrawButton2.SetActive(false);
        settings.SetActive(true);
        showButtons();
    }

    public void updateDealer() {
        for(int i = 0; i < dealerIcons.Count; i++) {
            if(i == manager.GetComponent<GameManager>().dealerID - 1) {
                dealerIcons[i].SetActive(true);
            }
            else {
                dealerIcons[i].SetActive(false);
            }
        }  
    }

    public void updateRound() {
        roundCounter.GetComponent<TMPro.TextMeshProUGUI>().text = "Current Round: " + manager.GetComponent<GameManager>().roundCount + " r:" + manager.GetComponent<GameManager>().repeatCount;
    }

    public void updateRiichi() {
        manager.GetComponent<GameManager>().riichiCount = manager.GetComponent<GameManager>().riichiCountTemp;
        riichiCounter.GetComponent<TMPro.TextMeshProUGUI>().text = "Riichi Count: " + manager.GetComponent<GameManager>().riichiCount;
    }

    public void hideSticks() {
        for(int i = 0; i < riichiSticks.Count; i++) {
            riichiSticks[i].SetActive(false);
            manager.GetComponent<GameManager>().riichiStatus[i] = 0;
        }
    }

    public void updateSticks() { //only used for load data functionality
        for(int i = 0; i < riichiSticks.Count; i++) {
            if(manager.GetComponent<GameManager>().riichiStatus[i] == 1) {
                riichiSticks[i].SetActive(true);
            }
            else {
                riichiSticks[i].SetActive(false);
            }
        }
    }

    public void hideButtons() {
        for(int i = 0; i < buttons.Count; i++) {
            buttons[i].SetActive(false);
        }
    }

    public void showButtons() {
        for(int i = 0; i < buttons.Count; i++) {
            buttons[i].SetActive(true);
        }
    }

    public void resetTiles() {
        for(int i = 0; i < manager.GetComponent<GameManager>().closeWinTiles.Length; i++) {
            manager.GetComponent<GameManager>().closeWinTiles[i].GetComponent<winningTile>().updateImage(tileBack);
        }
        for(int i = 0; i < manager.GetComponent<GameManager>().closeKanTiles.Length; i++) {
            manager.GetComponent<GameManager>().closeKanTiles[i].GetComponent<winningTile>().updateImage(tileBack);
        }
        for(int i = 0; i < manager.GetComponent<GameManager>().openKanTiles.Length; i++) {
            manager.GetComponent<GameManager>().openKanTiles[i].GetComponent<winningTile>().updateImage(tileBack);
        }
        for(int i = 0; i < manager.GetComponent<GameManager>().ponTiles.Length; i++) {
            manager.GetComponent<GameManager>().ponTiles[i].GetComponent<winningTile>().updateImage(tileBack);
        }
        for(int i = 0; i < manager.GetComponent<GameManager>().chiiTiles.Length; i++) {
            manager.GetComponent<GameManager>().chiiTiles[i].GetComponent<winningTile>().updateImage(tileBack);
        }
    }
}
