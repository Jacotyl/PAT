using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttons : MonoBehaviour
{
   public GameObject gameManager;

   public GameObject closedParentTile;

   public GameObject chiiParent;

   public GameObject ponParent;

   public GameObject openKanParent;

   public GameObject closedKanParent;

   public Sprite tileBack;
   

   public void chii() {
    closedParentTile.SetActive(false);
    ponParent.SetActive(false);
    chiiParent.SetActive(true); //this one
    openKanParent.SetActive(false);
    closedKanParent.SetActive(false);
   }

   public void pon() {
    closedParentTile.SetActive(false);
    ponParent.SetActive(true); //this one
    chiiParent.SetActive(false);
    openKanParent.SetActive(false);
    closedKanParent.SetActive(false);
   }

   public void openKan() {
    closedParentTile.SetActive(false);
    ponParent.SetActive(false);
    chiiParent.SetActive(false);
    openKanParent.SetActive(true); //this one
    closedKanParent.SetActive(false);
   }

   public void closedKan() {
    closedParentTile.SetActive(false);
    ponParent.SetActive(false);
    chiiParent.SetActive(false);
    openKanParent.SetActive(false);
    closedKanParent.SetActive(true); //this one
   }

   public void closedHand() {
    closedParentTile.SetActive(true); //this one
    ponParent.SetActive(false);
    chiiParent.SetActive(false);
    openKanParent.SetActive(false);
    closedKanParent.SetActive(false); 
   }

   

   public void del() {
    if(closedParentTile.activeSelf) {
        int index = gameManager.GetComponent<GameManager>().closedHandIndex;
        
        if (index != 0) {
            gameManager.GetComponent<GameManager>().closedHandIndex--;
            gameManager.GetComponent<GameManager>().closedWinningHand.RemoveAt(gameManager.GetComponent<GameManager>().closedWinningHand.Count-1);
            GameObject currentWinTile = gameManager.GetComponent<GameManager>().closeWinTiles[index-1];
            currentWinTile.GetComponent<winningTile>().updateImage(tileBack);
        }
        Debug.Log(gameManager.GetComponent<GameManager>().closedWinningHand.Count);
        Debug.Log(index);


    }
    else if(chiiParent.activeSelf) {
        int index = gameManager.GetComponent<GameManager>().chiiIndex;
       
        if (index != 0) {
            gameManager.GetComponent<GameManager>().chiiIndex--;
            gameManager.GetComponent<GameManager>().winningChii.RemoveAt(gameManager.GetComponent<GameManager>().winningChii.Count-1);
            GameObject currentWinTile = gameManager.GetComponent<GameManager>().chiiTiles[index-1];
            currentWinTile.GetComponent<winningTile>().updateImage(tileBack);
        }
   
    }
    else if(ponParent.activeSelf) {
        int index = gameManager.GetComponent<GameManager>().ponIndex;
       
        if (index != 0) {
            gameManager.GetComponent<GameManager>().ponIndex--;
            gameManager.GetComponent<GameManager>().winningPon.RemoveAt(gameManager.GetComponent<GameManager>().winningPon.Count-1);
            GameObject currentWinTile = gameManager.GetComponent<GameManager>().ponTiles[index-1];
            currentWinTile.GetComponent<winningTile>().updateImage(tileBack);
        }

    }
    else if(openKanParent.activeSelf) {
        int index = gameManager.GetComponent<GameManager>().openKanIndex;
       
        if (index != 0) {
            gameManager.GetComponent<GameManager>().openKanIndex--;
            gameManager.GetComponent<GameManager>().winningOpenKan.RemoveAt(gameManager.GetComponent<GameManager>().winningOpenKan.Count-1);
            GameObject currentWinTile = gameManager.GetComponent<GameManager>().openKanTiles[index-1];
            currentWinTile.GetComponent<winningTile>().updateImage(tileBack);
        }

    }
    else if(closedKanParent.activeSelf) {
        int index = gameManager.GetComponent<GameManager>().closedKanIndex;
       
        if (index != 0) {
            gameManager.GetComponent<GameManager>().closedKanIndex--;
            gameManager.GetComponent<GameManager>().winningClosedKan.RemoveAt(gameManager.GetComponent<GameManager>().winningClosedKan.Count-1);
            GameObject currentWinTile = gameManager.GetComponent<GameManager>().closeKanTiles[index-1];
            currentWinTile.GetComponent<winningTile>().updateImage(tileBack);
        }

    }    

   }

   public void calc() {
   }
}
