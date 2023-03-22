using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class winningTile : MonoBehaviour
{
   public void updateImage(Sprite inputSprite) {
        this.GetComponent<Image>().sprite = inputSprite;
   }
}
