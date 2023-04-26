using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class settings : MonoBehaviour
{
   public GameObject mainCanvas;

   public GameObject settingsCanvas;


   public void onClick() {
    if(mainCanvas.activeSelf) {
        mainCanvas.SetActive(false);
        settingsCanvas.SetActive(true);

    }
    else {
        mainCanvas.SetActive(true);
        settingsCanvas.SetActive(false);
    }
   } 
}
