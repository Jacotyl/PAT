using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tileInfo : MonoBehaviour
{
   public string textValue;
   public int numValue;

   public GameObject data;

 
   public GameObject closedParentTile;

   public GameObject chiiParent;

   public GameObject ponParent;

   public GameObject openKanParent;

   public GameObject closedKanParent;

   public void onClick() {
      Sprite mySprite = this.GetComponent<Image>().sprite;
      if(closedParentTile.activeSelf) {
         
         int index = data.GetComponent<GameManager>().closedHandIndex; //get closed tile index
         GameObject currentWinTile = data.GetComponent<GameManager>().closeWinTiles[index]; //get tile object at index
         currentWinTile.GetComponent<winningTile>().updateImage(mySprite); //update image
         data.GetComponent<GameManager>().closedWinningHand.Add(this.numValue); //add value to closedWinningHand list
         data.GetComponent<GameManager>().closedHandIndex++; //add 1 to index
      }

      else if(chiiParent.activeSelf) {
         int index = data.GetComponent<GameManager>().chiiIndex;
         GameObject currentWinTile = data.GetComponent<GameManager>().chiiTiles[index];
         currentWinTile.GetComponent<winningTile>().updateImage(mySprite);
         data.GetComponent<GameManager>().winningChii.Add(this.numValue);
         data.GetComponent<GameManager>().chiiIndex++;
      }
      else if(ponParent.activeSelf) {
         int index = data.GetComponent<GameManager>().ponIndex;
         GameObject currentWinTile = data.GetComponent<GameManager>().ponTiles[index];
         currentWinTile.GetComponent<winningTile>().updateImage(mySprite);
         data.GetComponent<GameManager>().winningPon.Add(this.numValue);
         data.GetComponent<GameManager>().ponIndex++;
      }
      else if(openKanParent.activeSelf) {
         int index = data.GetComponent<GameManager>().openKanIndex;
         GameObject currentWinTile = data.GetComponent<GameManager>().openKanTiles[index];
         currentWinTile.GetComponent<winningTile>().updateImage(mySprite);
         data.GetComponent<GameManager>().winningOpenKan.Add(this.numValue);
         data.GetComponent<GameManager>().openKanIndex++;
      }
      else if(closedKanParent.activeSelf) {
         int index = data.GetComponent<GameManager>().closedKanIndex;
         GameObject currentWinTile = data.GetComponent<GameManager>().closeKanTiles[index];
         currentWinTile.GetComponent<winningTile>().updateImage(mySprite);
         data.GetComponent<GameManager>().winningClosedKan.Add(this.numValue);
         data.GetComponent<GameManager>().closedKanIndex++;
      }
     


   }
}
