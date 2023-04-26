using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

/*
SAVE DATA INFO:
0 P1 score
1 P2 score
2 P3 score
3 P4 score
4 Round Count
5 Riichi Count
6 Dealer ID
7 P1 riichi
8 p2 riichi
9 p3 riichi
10 p4 riichi
11 repeat count
*/

public class fileIO : MonoBehaviour
{
    public GameObject manager;

    public GameObject cleaner;

    

    public void load() {
        List<int>loadData = new List<int>();
        StreamReader reader = new StreamReader(Application.dataPath + "/save.maj", false);
        for(int i = 0; i <= 11; i++) { //12 lines in save data
            loadData.Add((int.Parse(reader.ReadLine())));
            Debug.Log("Loading data" + loadData[i]);
        }
        reader.Close();
        
        for(int i = 0; i < manager.GetComponent<GameManager>().playerScores.Count; i++){
            manager.GetComponent<GameManager>().playerScores[i] = loadData[i];
        }
        manager.GetComponent<GameManager>().roundCount = loadData[4];
        manager.GetComponent<GameManager>().riichiCount = loadData[5];
        manager.GetComponent<GameManager>().riichiCountTemp = loadData[5];
        manager.GetComponent<GameManager>().dealerID = loadData[6];
        Debug.Log("Riichistatus.count " + manager.GetComponent<GameManager>().riichiStatus.Count);
        Debug.Log("LoadData.count " + loadData.Count);

        manager.GetComponent<GameManager>().riichiStatus[0] = loadData[7];
        manager.GetComponent<GameManager>().riichiStatus[1] = loadData[8];
        manager.GetComponent<GameManager>().riichiStatus[2] = loadData[9];
        manager.GetComponent<GameManager>().riichiStatus[3] = loadData[10];

        manager.GetComponent<GameManager>().repeatCount = loadData[11];

        manager.GetComponent<GameManager>().updateScores();
        cleaner.GetComponent<cleanup>().updateDealer();
        cleaner.GetComponent<cleanup>().updateRiichi();
        cleaner.GetComponent<cleanup>().updateRound();
        //cleaner.GetComponent<cleanup>().hideSticks();
        cleaner.GetComponent<cleanup>().updateSticks();


    }

    public void save() {
        List<int> savedata = new List<int>();
        for(int i = 0; i < manager.GetComponent<GameManager>().playerScores.Count; i++) {
            savedata.Add(manager.GetComponent<GameManager>().playerScores[i]);
        }
        savedata.Add(manager.GetComponent<GameManager>().roundCount);
        savedata.Add(manager.GetComponent<GameManager>().riichiCount);
        savedata.Add(manager.GetComponent<GameManager>().dealerID);
        for(int i = 0; i < manager.GetComponent<GameManager>().riichiStatus.Count; i++) {
            savedata.Add(manager.GetComponent<GameManager>().riichiStatus[i]);
        }
        savedata.Add(manager.GetComponent<GameManager>().repeatCount);
        Debug.Log("Writing to file" + Application.dataPath + "/sav.maj");
        //File.WriteAllText(Application.dataPath + "/" + "sav.maj", savedata[0] + "\n" + savedata[1] + "\n" + savedata[2] + "\n" + savedata[3] + "\n" + savedata[4] + "\n" + savedata[5] + "\n");
        StreamWriter writer = new StreamWriter(Application.dataPath + "/save.maj", false);
        writer.WriteLine(savedata[0] + "\n" + savedata[1] + "\n" + savedata[2] + "\n" + savedata[3] + "\n" + savedata[4] + "\n" + savedata[5] + "\n" + savedata[6] + "\n" + savedata[7] + "\n" + savedata[8] + "\n" + savedata[9] + "\n" + savedata[10] + "\n" + savedata[11]);
        writer.Close();
    }
}
