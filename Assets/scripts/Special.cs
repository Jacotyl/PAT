using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Special : MonoBehaviour
{


    public Toggle HaiteRaoyue;

    public Toggle ChanKan;

    public Toggle RinshanKaihou;

    public Toggle HouteiRaoyui;

    public Toggle Ippatsu;

    public GameObject Ron;

    public Toggle ryanmen;

    public Toggle kanchan;

    public Toggle penchan;

    public Toggle tanki;

    public GameObject manager;

    public GameObject specialCanvas;

    public GameObject tileInputCanvas;

    public GameObject settings;

    



    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void onClick() {
        if(Ron.GetComponent<TMPro.TMP_Dropdown>().value > 0) {
            manager.GetComponent<GameManager>().roneeID = Ron.GetComponent<TMPro.TMP_Dropdown>().value;
            manager.GetComponent<GameManager>().ron = true;
        }
        else {
            manager.GetComponent<GameManager>().ron = false;
        }
        manager.GetComponent<GameManager>().haiteRaoyue = HaiteRaoyue.isOn;
        manager.GetComponent<GameManager>().Chankan = ChanKan.isOn;
        manager.GetComponent<GameManager>().rinshanKaihou = RinshanKaihou.isOn;
        manager.GetComponent<GameManager>().houteiRaoyui = HouteiRaoyui.isOn;
        manager.GetComponent<GameManager>().ippatsu = Ippatsu.isOn;

        manager.GetComponent<GameManager>().ryanmen = ryanmen.isOn;
        manager.GetComponent<GameManager>().kanchan = kanchan.isOn;
        manager.GetComponent<GameManager>().penchan = penchan.isOn;
        manager.GetComponent<GameManager>().tanki = tanki.isOn;

        specialCanvas.SetActive(false);
        tileInputCanvas.SetActive(true);
        settings.SetActive(false);
    
    }

    void Update() {
        //Debug.Log(Ron.GetComponent<TMPro.TMP_Dropdown>().value);
    }
    
}
