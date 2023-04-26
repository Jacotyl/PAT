using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

//TODO: Seperate Winning Tile from rest of hand

public class GameManager : MonoBehaviour
{

    public int[] tileID = {
        1, //man
        2,
        3,
        4,
        5,
        6,
        7,
        8,
        9, 
        10,//pin
        11,
        12,
        13,
        14,
        15,
        16,
        17,
        18,
        19, //sou
        20,
        21,
        22,
        23,
        24,
        25,
        26,
        27,
        28, //winds
        29,
        30,
        31,
        32, //dragons
        33,
        34,
        35, //akadora
        36,
        37     
    };

    public string[] tileName = {
        "1m", //1
        "2m", //2
        "3m", //3
        "4m", //4
        "5m", //5
        "6m", //6
        "7m", //7
        "8m", //8
        "9m", //9
        "1p", //10
        "2p", //11
        "3p", //12
        "4p", //13
        "5p", //14
        "6p", //15
        "7p", //16
        "8p", //17
        "9p", //18
        "1s", //19
        "2s", //20
        "3s", //21
        "4s", //22
        "5s", //23
        "6s", //24
        "7s", //25
        "8s", //26
        "9s", //27
        "t", //east 28
        "n", //north 29
        "s", //west 30
        "p", //south 31
        "w", //white dragon 32
        "g", //green dragon 33
        "r", //red dragon 34
        "5mr", //akadora tiles 35
        "5pr", //36
        "5sr" //37
    };

    public int[] tileNumbers = {
        1, //man
        2,
        3,
        4,
        5,
        6,
        7,
        8,
        9, 
        1,//pin
        2,
        3,
        4,
        5,
        6,
        7,
        8,
        9,
        1, //sou
        2,
        3,
        4,
        5,
        6,
        7,
        8,
        9,
        28, //winds
        29,
        30,
        31,
        32, //dragons
        33,
        34,
        35, //akadora
        36,
        37  
    };

    public int[] terminals = { //This contains both terminals AND honors
        1,9,10,18,19,27,28,29,30,31,32,33,34 //honors begin at index 6
    };

    public int[] dragons = {
        32,33,34
    };

    public int[] winds = {
        28,29,30,31
    };

   

    //TILE INDEXES
    public int closedHandIndex = 0; //Will never go above 13 (14 tiles)

    public int closedKanIndex = 0;

    public int openKanIndex = 0;

    public int chiiIndex = 0;

    public int ponIndex = 0;


    //TILE LISTS (THESE HOLD VALUES)
    public List<int> closedWinningHand = new List<int>();
    public List<int> winningClosedKan = new List<int>();
    public List<int> winningOpenKan = new List<int>();

    public List<int> winningPon = new List<int>();
    public List<int> winningChii = new List<int>();

    public int winningTile;

    //Arrays of the tiles (these show up when you select tiles for win)
    public GameObject[] closeWinTiles;

    public GameObject[] closeKanTiles;

    public GameObject[] openKanTiles;

    public GameObject[] ponTiles;

    public GameObject[] chiiTiles;

   

    //scoring

    public int hanCount;
    public int yakumanCount;

    public int fuCount;


    //grouping

    bool handOpen;
    public List<int> pair = new List<int>();
    bool pairOpen;
    public List<int> group1 = new List<int>();
    bool group1open;
    bool group1Sequence;
    public List<int> group2 = new List<int>();
    bool group2open;
    bool group2Sequence;
    public List<int> group3 = new List<int>();
    bool group3open;
    bool group3Sequence;
    public List<int> group4 = new List<int>();
    bool group4open;
    bool group4Sequence;

    public List<int> group1Face = new List<int>();

    public List<int> group2Face = new List<int>();

    public List<int> group3Face = new List<int>();

    public List<int> group4Face = new List<int>();

    public List<int> pairFace = new List<int>();

    public List<int> flattenedHand = new List<int>();

    public List<int> flattenedConvertedHand = new List<int>();

    public List<List<int>> groups = new List<List<int>>();

    public List<List<int>> convertedGroups = new List<List<int>>();
    public List<string> yakuList = new List<string>();


    //Table Variables
    public bool ron;

    public int roneeID;
    
    public int winnerID;

    List<int> playerID = new List<int>();

    public List<int> payoutValues = new List<int>();

    public int dealerID; 

    public int currentWind;

    public int winnerWind;

    public List<int> riichiStatus = new List<int>();

    public int riichiCount;

    public int riichiCountTemp;

    public List<int> playerScores = new List<int>();

    public List<GameObject> scoreDisplays = new List<GameObject>();

    public int roundCount;

    public int repeatCount;

    //object references

    public GameObject mainScreen;

    public GameObject tileInput;

    public GameObject cleanup;

    //special boys
    public bool haiteRaoyue;
    public bool Chankan;
    public bool rinshanKaihou;
    public bool houteiRaoyui;
    public bool ippatsu;

    //waits
    public bool ryanmen;

    public bool kanchan;

    public bool penchan;

    public bool tanki;

    void Start() {
        playerScores.Add(25000); //allow for user set variables later
        playerScores.Add(25000);
        playerScores.Add(25000);
        playerScores.Add(25000);

        dealerID = 1;

        payoutValues.Add(0);
        payoutValues.Add(0);
        payoutValues.Add(0);
        payoutValues.Add(0);
    
        riichiStatus.Add(0);
        riichiStatus.Add(0);
        riichiStatus.Add(0);
        riichiStatus.Add(0);
    }


    public void updateScores() {
        for(int i = 0; i < scoreDisplays.Count; i++) {
            scoreDisplays[i].GetComponent<TMPro.TextMeshProUGUI>().text = (playerScores[i].ToString());
        }
    }


    bool containsTerminals(){
        for (int i = 0; i < terminals.Length; i++) {
            if (closedWinningHand.Contains(terminals[i]) || winningChii.Contains(terminals[i]) || winningPon.Contains(terminals[i]) || winningClosedKan.Contains(terminals[i]) || winningOpenKan.Contains(terminals[i])) {
                return true;
            }
        }
        return false;
    }

    
    bool groupAllDragons(List<int> group){
        int dragonCount = 0;
        for(int i = 0; i < group.Count; i++) {
            if(group[i] > 31 && group[i] < 35) {
                dragonCount++;
            }
        }
        if(dragonCount >= 2) {
            return true;
        }
        else {
            return false;
        }
    }

    bool groupContainsTerminals(List<int> group){
        for (int i = 0; i < terminals.Length; i++) {
            if (group.Contains(terminals[i])) {
                return true;
            }
        }
        return false;
    }

    bool groupAllTerminals(List<int>group){
        for(int i = 0; i < terminals.Length; i++) {
                if(group[0] == terminals[i] && group[0] == group[1]) {
                    return true;
                }
        }
        return false;
    }
        


    bool isHandOpen() {
        if(winningOpenKan.Count != 0 || winningChii.Count != 0 || winningPon.Count != 0) {
            Debug.Log("Returning true for ishandopen");
            return true;
        }
        else {
            Debug.Log("Returning false for ishandopen");
            return false;
        }

    }

    int sequenceComparator() { 
        int identicalSequenceCount = 0;
        List<List<int>> melds = new List<List<int>>();
        if(group1Sequence) {
            melds.Add(group1);
        }
        if(group2Sequence) {
            melds.Add(group2);

        }
        if(group3Sequence) {
            melds.Add(group3);

        }
        if(group4Sequence) {
            melds.Add(group4);
        }
        
            for(int i = 0; i < melds.Count; i++) {
                for(int j = i; j < melds.Count; j++) { //int j = i prevents the algorithm from checking already compared melds (I.E. i1 = j2, j2 = i1)
                    //Debug.Log("Comparing melds" + i + ", " + j);
                    if(i == j) {                            //skip comparing group with itself
                        continue;
                    }
                    if(melds[i].SequenceEqual(melds[j])) {
                        //Debug.Log("Found identical sequence, adding to count");
                        identicalSequenceCount+=1;
                    }
                }
            }
            return identicalSequenceCount;
    }

    int checkGroupSuit(List<int> group) { //1 = man, 2 = pin, 3 = sou, 4 = wind, 5 = dragon
        Debug.Log("Checking group suit");
        
        for(int i = 0; i < group.Count; i++) {
            Debug.Log("Group[i] is : " + group[i]);
            if(group[i] <= 9) {                         //man
                Debug.Log("Found Man group");
                return 1;
            }
            else if(group[i] >= 10 && group[i] <= 18) { //pin
                Debug.Log("Found pin group");
                return 2;
            }
            else if(group[i] >= 19 && group[i] <= 27) { //sou
                Debug.Log("Found sou group");
                return 3;
            }
            else if(group[i] >= 28 && group [i] <= 31) { //wind
                Debug.Log("Found wind group");
                return 4;
            }
            
        }
        Debug.Log("Found dragon group");
        return 5; //dragon
    } 

    bool groupContainsHonor(List<int> group) {
        for(int i = 6; i < terminals.Length; i++) {
            if(group.Contains(terminals[i])) {
                return true;
            }
        }
        return false;
    }

    bool isGroupTriple(List<int> group) {
        if(group[0] == group[2]) {
            return true;
        }
        return false;
    }

    bool isGroupSequence(List<int> group) {
        if(group[0] < group[1]) {
            return true;
        }
        return false;
    }

    bool groupContainsSameNumbers(List<int> group1, List<int> group2) { //might have unexpected behavior with Kan/Pon comparisons
        int same = 0;
        for(int i = 0; i < group1.Count; i++) {
            if(tileValueToNumber(group1[i]) == tileValueToNumber(group2[i])) {
                same++;
            }
        }
        if(same >= 3) {
            return true;
        }
        return false;
    }

    int tileValueToNumber(int tile) { //converts tile ID to Face number
        return tileNumbers[tile-1];
    }

    void convertAllHandToValues() {
        for(int i = 0; i < group1.Count; i++) {
            group1Face.Add(tileValueToNumber(group1[i]));
        }
        for(int i = 0; i < group2.Count; i++) {
            group2Face.Add(tileValueToNumber(group2[i]));
        }
        for(int i = 0; i < group3.Count; i++) {
            group3Face.Add(tileValueToNumber(group3[i]));
        }
        for(int i = 0; i < group4.Count; i++) {
            group4Face.Add(tileValueToNumber(group4[i]));
        }
        for(int i = 0; i < pair.Count; i++) {
            pairFace.Add(tileValueToNumber(pair[i]));
        }
    }

    void flattenHand() {
        //Debug.Log("Flattening Hand: ");
        for(int i = 0; i < groups.Count; i++) {
            for(int j = 0; j < groups[i].Count; j++) {
                //Debug.Log(groups[i][j]);
                flattenedHand.Add(groups[i][j]);
            }
        }
        for(int i = 0; i < convertedGroups.Count; i ++) {
            for(int j = 0; j < convertedGroups[i].Count; j++) {
                flattenedConvertedHand.Add(convertedGroups[i][j]);
            }
        }
    }

    List<int> flattenList(List<List<int>> inputGroup) { //merge this with above function they're doing the same thing
        List<int> returnList = new List<int>();
        for(int i = 0; i < inputGroup.Count; i++) {
            for(int j = 0; j < inputGroup[i].Count; j++) {
                returnList.Add(groups[i][j]);
            }
        }
        return returnList;
    }

    public int calcFu() {
        List<bool> openValues = new List<bool>();
        openValues.Add(group1open);
        openValues.Add(group2open);
        openValues.Add(group3open);
        openValues.Add(group4open);
        List<bool> sequenceFlags = new List<bool>();
        sequenceFlags.Add(group1Sequence);
        sequenceFlags.Add(group2Sequence);
        sequenceFlags.Add(group3Sequence);
        sequenceFlags.Add(group4Sequence);
        int fuValue = 20;
        if(ron && isHandOpen() == false) { //menzen-kafu
            fuValue += 10;
        }

        if(kanchan == true || penchan == true || tanki == true) {
            fuValue+=2;
        }

        //begin melds

        for(int i = 0; i < groups.Count-1; i++) {
            //Debug.Log("iterator is currently at: " + i);
            //Debug.Log("openValues Count: " + openValues.Count);
            //Debug.Log("sequenceFlags count: " + sequenceFlags.Count);
            if(openValues[i] && sequenceFlags[i]) { //minko
                if(!groupContainsHonor(groups[i])) {
                    fuValue+= 4;
                }
                else {
                    fuValue+= 2;
                }
            }
            if(!openValues[i] && !sequenceFlags[i]) { //anko
                if(!groupContainsHonor(groups[i])) {
                    fuValue+= 4;
                }
                else {
                    fuValue+= 8;
                }
            }
            if(openValues[i] && groups[i].Count == 4) { //minkan
                if(!groupContainsHonor(groups[i])) {
                    fuValue+= 8;
                }
                else {
                    fuValue+= 16;
                }
            }
            if(!openValues[i] && groups[i].Count == 4) { //ankan
                if(!groupContainsHonor(groups[i])) {
                    fuValue+= 16;
                }
                else {
                    fuValue+= 32;
                }
            }
            

        }
        if(pair[0] == currentWind && pair[0] == winnerWind) {
            fuValue+= 4;
        }
        else if(pair[0] == winnerWind || pair[0] == currentWind || pair[0] >= 28 ) {
            fuValue+= 2;
        }

        if(!ron) {
            fuCount+= 2;
        }

        //wait calculation goes here
        Debug.Log("Fuvalue: " + fuValue);
        return fuValue;
    }


    //Currently skipped: pinfu, Chankan, Double Riichi
    public void calcYaku() {
        //closedWinningHand.Sort();
        //openWinningHand.Sort();
        //openKan.Sort();
        //closedKan.Sort();
        //Debug.Log("starting calc");
        //Debug.Log("WinningopenKan count :" + winningOpenKan.Count);
        //Debug.Log(group1.Count);
        handGroup();
        convertAllHandToValues();
        group1Sequence = isGroupSequence(group1);
        group2Sequence = isGroupSequence(group2);
        group3Sequence = isGroupSequence(group3);
        group4Sequence = isGroupSequence(group4);
        groups.Add(group1);
        groups.Add(group2);
        groups.Add(group3);
        groups.Add(group4);
        groups.Add(pair);
        convertedGroups.Add(group1Face);
        convertedGroups.Add(group2Face);
        convertedGroups.Add(group3Face);
        convertedGroups.Add(group4Face);
        convertedGroups.Add(pairFace);
        if(group1open || group2open || group3open || group4open) {
            handOpen = true;
        }
       /*
        for(int i = 0; i < group1.Count; i++){
            Debug.Log(group1[i]);
        }
        for(int i = 0; i < group2.Count; i++){
            Debug.Log(group2[i]);
        }
        for(int i = 0; i < group3.Count; i++){
            Debug.Log(group3[i]);
        }
        for(int i = 0; i < group4.Count; i++){
            Debug.Log(group4[i]);
        }
        for(int i = 0; i < pair.Count; i++) {
            Debug.Log(pair[i]);
        }
    */
        for(int i = 0; i < group1Face.Count; i++){
            Debug.Log(group1Face[i]);
        }
        for(int i = 0; i < group2Face.Count; i++){
            Debug.Log(group2Face[i]);
        }
        for(int i = 0; i < group3Face.Count; i++){
            Debug.Log(group3Face[i]);
        }
        for(int i = 0; i < group4Face.Count; i++){
            Debug.Log(group4Face[i]);
        }
        for(int i = 0; i < pairFace.Count; i++) {
            Debug.Log(pairFace[i]);
        }
        
        flattenHand();
        //Debug.Log("Flat hand");
        for(int i = 0; i < flattenedHand.Count; i++) {
            //Debug.Log(flattenedHand[i]);
        }

        //begin yakuman block
        Debug.Log("Checking Kokushi");
        kokushiMusou(); //Tested and working
        Debug.Log("Checking suuankou");
        suuankou();  //Tested and working
        Debug.Log("Checking daisangen");
        daisangen(); //Tested and working
        Debug.Log("Checking shousuushii");
        shousuushii(); //Tested and working
        Debug.Log("Checking daisuushii");
        daisuushii(); //Tested and working
        Debug.Log("Checking tsuuiisou");
        tsuuiisou(); //Tested and working
        Debug.Log("Checking chinroutou");
        chinroutou(); //Tested and working
        Debug.Log("Checking ryuuiisou");
        ryuuiisou(); //Tested and working
        Debug.Log("Checking chuurenpoutou");
        chuurenPoutou(); //Tested and working (I do not understand but it do be working tho)
        Debug.Log("Checking suukantsu");
        suukantsu(); //Tested and working
        Debug.Log("Checking menzenchin");

        menzenchinTsumohou(); //Tested and working
        Debug.Log("Checking iipeikou");
        iipeikou(); //Tested and working
        Debug.Log("Checking tanyao");
        tanyao(); //Tested and working
        Debug.Log("Checking yakuhai");
        yakuhai(); //Tested and working
        Debug.Log("Checking chantaiyao");
        chantaiyao(); //Tested and working
        Debug.Log("Checking sanshokuDoujun");
        sanshokuDoujun(); //This is breaking Honitsu. Something to do with converting Groups to face values vs tile ID values.
        Debug.Log("Checking iitsu");
        iitsu(); //Tested and working
        Debug.Log("Checking toitoi");
        toitoi(); //Tested and working
        Debug.Log("Checking sanankou");

        sanankou(); //Tested and working
        Debug.Log("Checking sanshoku Doukou");
        sanshokuDoukou(); //Seems to be working(?)
        Debug.Log("Checking sankantsu");
        sankantsu(); //tested and working
        Debug.Log("Checking chiitoitsu");
        chiitoitsu(); //tested and working
        Debug.Log("Checking honroutou");
        //honroutou(); //tested and working
        Debug.Log("Checking shousangen");
        shousangen(); //Tested and working
        Debug.Log("Checking honitsu");
        honitsu();                                  //FIX THIS
        Debug.Log("Checking junchanTaiyao");
        junchanTaiyao(); //Tested and working
        Debug.Log("Checking ranpeikou");

        ryanpeikou();
        Debug.Log("Checking chinitsu");

        chinitsu(); //Tested and working
        Debug.Log("Checking riichi");
        riichi(); //Tested and working

        if(haiteRaoyue) { hanCount += 1; yakuList.Add("Haitei Raoyue");}
        if(Chankan) { hanCount += 1; yakuList.Add("Chankan");}
        if(rinshanKaihou) { hanCount += 1; yakuList.Add("Rinshan Kaihou");}
        if(houteiRaoyui) { hanCount += 1; yakuList.Add("Houtei Raoyue");}
        if(ippatsu) { hanCount += 1; yakuList.Add("Ippatsu");}

        int totalFu = calcFu();
        Debug.Log("han: " + hanCount);
        totalFu = (int)Math.Ceiling((double)totalFu / 10.0d) * 10;

        if(totalFu == 20) {
            hanCount+=1;
            yakuList.Add("Pinfu");
        }

        int basicPoints = totalFu * (int)Mathf.Pow(2, 2+hanCount);
        

        

        

        if(yakumanCount > 0) {
            basicPoints = 8000 * yakumanCount;
        }

        else if (hanCount == 3 && fuCount >= 70) {
            basicPoints = 2000;
        }
        else if (hanCount == 4 && fuCount >= 40) {
            basicPoints = 2000;
        }
        else if (hanCount == 6 || hanCount == 7) {
            basicPoints = 3000;
        }
        else if (hanCount >= 8 && hanCount <= 10) {
            basicPoints = 4000;
        }
        else if (hanCount == 11 || hanCount == 12) {
            basicPoints = 6000;
        }
        else if (hanCount >= 12) {
            basicPoints = 8000;
        }
        if(ron) {                                               // ron block
            if(winnerID == dealerID) {
                int temp = basicPoints * 6;
                temp = (int)Math.Ceiling((double)temp / 100d) * 100;
                //Debug.Log("ronny boy " + roneeID);
                //Debug.Log("payout length" + payoutValues.Count);
                payoutValues[roneeID - 1] = temp + (repeatCount * 300);
            }
            else {
                int temp = basicPoints * 4;
                temp = (int)Math.Ceiling((double)temp / 100d) * 100;
                //Debug.Log("ronny boy " + roneeID);
                //Debug.Log("payout length" + payoutValues.Count);
                payoutValues[roneeID - 1] = basicPoints * 4 + (repeatCount * 300);
            }

        }

        else {                                                  //tsumo block
            if(winnerID == dealerID) {
                for(int i = 0; i < payoutValues.Count; i++) {
                    if(i == winnerID-1) {
                        payoutValues[i] = 0;
                    }
                    else {
                    int temp = basicPoints * 2;
                    temp = (int)Math.Ceiling((double)temp / 100d) * 100;
                    payoutValues[i] = temp + (repeatCount * 100);
                    }
                }
            }
            else {
                for(int i = 0; i < payoutValues.Count; i++) {
                    if(i == winnerID-1) {
                        payoutValues[i] = 0;
                    }
                    else if(i == dealerID-1) {
                        int temp = basicPoints * 2;
                        temp = (int)Math.Ceiling((double)temp / 100d) * 100;
                        payoutValues[i] = temp + (repeatCount * 100);
                        //payoutValues[i] = 2 * basicPoints + (repeatCount * 100);
                    }
                    else {
                        int temp = basicPoints;
                        temp = (int)Math.Ceiling((double)temp / 100d) * 100;
                        payoutValues[i] = temp + (repeatCount * 100);
                        //payoutValues[i] = basicPoints + (repeatCount * 100);
                    }
                }
            }
        }


        int totalPayout = 0;
        for(int i = 0; i < payoutValues.Count; i++) {
            //Debug.Log(playerScores[i]);
            playerScores[i] -= payoutValues[i];
            totalPayout += payoutValues[i];
            //Debug.Log(payoutValues[i]);
        }

        for(int i = 0; i < yakuList.Count; i++){
            Debug.Log(yakuList[i]);
        }

        totalPayout += (riichiCount * 1000) + (repeatCount * 300);
        playerScores[winnerID-1] += totalPayout;

        cleanup.GetComponent<cleanup>().winCleanup();

        updateScores();

        

        tileInput.SetActive(false);
        mainScreen.SetActive(true);






        
        
    }

    public void handGroup() {
        if(winningChii.Count > 0) {
            int chiiMelds = winningChii.Count/3;
            for(int i = 0; i < chiiMelds; i++) {
                if(group1.Count == 0) {
                    group1.Add(winningChii[(i*3)]);
                    group1.Add(winningChii[(i*3)+1]);
                    group1.Add(winningChii[(i*3)+2]);
                    group1open = true;
                }
                else if(group2.Count == 0) {
                    group2.Add(winningChii[(i*3)]);
                    group2.Add(winningChii[(i*3)+1]);
                    group2.Add(winningChii[(i*3)+2]);
                    group2open = true;
                }
                else if(group3.Count == 0) {
                    group3.Add(winningChii[(i*3)]);
                    group3.Add(winningChii[(i*3)+1]);
                    group3.Add(winningChii[(i*3)+2]);
                    group3open = true;
                }
                else if(group4.Count == 0) {
                    group4.Add(winningChii[(i*3)]);
                    group4.Add(winningChii[(i*3)+1]);
                    group4.Add(winningChii[(i*3)+2]);
                    group4open = true;
                }
            }
        }
        if(winningPon.Count > 0) {
            int ponMelds = winningPon.Count/3;
            for(int i = 0; i < ponMelds; i++) {
                if(group1.Count == 0) {
                    group1.Add(winningPon[(i*3)]);
                    group1.Add(winningPon[(i*3)+1]);
                    group1.Add(winningPon[(i*3)+2]);
                    group1open = true;
                }
                else if(group2.Count == 0) {
                    group2.Add(winningPon[(i*3)]);
                    group2.Add(winningPon[(i*3)+1]);
                    group2.Add(winningPon[(i*3)+2]);
                    group2open = true;
                }
                else if(group3.Count == 0) {
                    group3.Add(winningPon[(i*3)]);
                    group3.Add(winningPon[(i*3)+1]);
                    group3.Add(winningPon[(i*3)+2]);
                    group3open = true;
                }
                else if(group4.Count == 0) {
                    group4.Add(winningPon[(i*3)]);
                    group4.Add(winningPon[(i*3)+1]);
                    group4.Add(winningPon[(i*3)+2]);
                    group4open = true;
                }
            }
        }
        if(winningClosedKan.Count > 0) {
            int closedKanMelds = winningClosedKan.Count/3;
            for(int i = 0; i < closedKanMelds; i++) {
                if(group1.Count == 0) {
                    group1.Add(winningClosedKan[(i*3)]);
                    group1.Add(winningClosedKan[(i*3)+1]);
                    group1.Add(winningClosedKan[(i*3)+2]);
                    
                }
                else if(group2.Count == 0) {
                    group2.Add(winningClosedKan[(i*3)]);
                    group2.Add(winningClosedKan[(i*3)+1]);
                    group2.Add(winningClosedKan[(i*3)+2]);
                }
                else if(group3.Count == 0) {
                    group3.Add(winningClosedKan[(i*3)]);
                    group3.Add(winningClosedKan[(i*3)+1]);
                    group3.Add(winningClosedKan[(i*3)+2]);
                }
                else if(group4.Count == 0) {
                    group4.Add(winningClosedKan[(i*3)]);
                    group4.Add(winningClosedKan[(i*3)+1]);
                    group4.Add(winningClosedKan[(i*3)+2]);
                }
            }
        }
        if(winningOpenKan.Count > 0) {
            int openKanMelds = winningOpenKan.Count/3;
            for(int i = 0; i < openKanMelds; i++) {
                if(group1.Count == 0) {
                    group1.Add(winningOpenKan[(i*3)]);
                    group1.Add(winningOpenKan[(i*3)+1]);
                    group1.Add(winningOpenKan[(i*3)+2]);
                    group1open = true;
                }
                else if(group2.Count == 0) {
                    group2.Add(winningOpenKan[(i*3)]);
                    group2.Add(winningOpenKan[(i*3)+1]);
                    group2.Add(winningOpenKan[(i*3)+2]);
                    group2open = true;
                }
                else if(group3.Count == 0) {
                    group3.Add(winningOpenKan[(i*3)]);
                    group3.Add(winningOpenKan[(i*3)+1]);
                    group3.Add(winningOpenKan[(i*3)+2]);
                    group3open = true;
                }
                else if(group4.Count == 0) {
                    group4.Add(winningOpenKan[(i*3)]);
                    group4.Add(winningOpenKan[(i*3)+1]);
                    group4.Add(winningOpenKan[(i*3)+2]);
                    group4open = true;
                }
            }
        }
        List<int> tempList = new List<int>(closedWinningHand);
        pair.Add(closedWinningHand[closedWinningHand.Count-2]);
        //Debug.Log(closedWinningHand[closedWinningHand.Count-1]);
        pair.Add(closedWinningHand[closedWinningHand.Count-1]);
        //Debug.Log(closedWinningHand[closedWinningHand.Count-2]);
       // Debug.Log(pair[0]);
        //Debug.Log(pair[1]);
        tempList.RemoveAt(tempList.Count-1);
        tempList.RemoveAt(tempList.Count-1);
        for(int i = 0; i < tempList.Count/3; i++) {
            if(group1.Count == 0) {
                group1.Add(tempList[(i*3)]);
                group1.Add(tempList[(i*3)+1]);
                group1.Add(tempList[(i*3)+2]);
                group1open = false;
            }
            else if(group2.Count == 0) {
                group2.Add(tempList[(i*3)]);
                group2.Add(tempList[(i*3)+1]);
                group2.Add(tempList[(i*3)+2]);
                group2open = false;
            }
            else if(group3.Count == 0) {
                group3.Add(tempList[(i*3)]);
                group3.Add(tempList[(i*3)+1]);
                group3.Add(tempList[(i*3)+2]);
                group3open = false;
            }
            else if(group4.Count == 0) {
                group4.Add(tempList[(i*3)]);
                group4.Add(tempList[(i*3)+1]);
                group4.Add(tempList[(i*3)+2]);
                group4open = false;
            }
        }


    }

    //Begin one han
    void menzenchinTsumohou() {
        if(isHandOpen() == false && ron == false) {
            hanCount+= 1;
            yakuList.Add("Menzenshin Tsumohou");
        }
    }

    void iipeikou() {
        if(isHandOpen() == false) {
            if(sequenceComparator() == 1) {
                hanCount+= 1;
                yakuList.Add("Iipeikou");
            }
        }
    } 


    void tanyao() {
        if (!containsTerminals()) {
            hanCount+= 1;
            yakuList.Add("Tanyao");
        }
    }

    void yakuhai() {
        int currentWindCounter = 0;
        int seatWindCounter = 0;
        int whiteCounter = 0;
        int greenCounter = 0;
        int redCounter = 0;
        for(int i = 0; i < groups.Count; i++) {
            for(int j = 0; j < groups[i].Count; j++) {
                //Debug.Log(groups[i][j]);
                if (groups[i][j] == currentWind) {
                    currentWindCounter++;
                } 
                if (groups[i][j] == winnerWind) {
                    seatWindCounter++;
                }
                if (groups[i][j] == dragons[0]) { 
                    whiteCounter++;
                }
                if (groups[i][j] == dragons[1]) {
                    greenCounter++;
                }
                if (groups[i][j] == dragons[2]) {
                    redCounter++;
                }
            }
        }
        if (currentWindCounter >= 3) {
            hanCount++;
            yakuList.Add("Yakuhai (Round Wind)");
        }
        if (seatWindCounter >= 3) {
            hanCount++;
            yakuList.Add("Yakuhai (Seat Wind)");
        }
        if (whiteCounter >= 3) {
            hanCount++;
            yakuList.Add("Yakuhai (White Dragon)");
        }
        if (greenCounter >= 3) {
            hanCount++;
            yakuList.Add("Yakuhai (Green Dragon)");
        }
        if (redCounter >= 3) {
            hanCount++;
            yakuList.Add("Yakuhai (Red Dragon)");
        }
    }

    void riichi() {
        if(riichiStatus[winnerID-1] == 1) {
            hanCount+=1;
            yakuList.Add("Riichi");
        }
    }
    //begin two han
    void chantaiyao() {
        if(groupContainsTerminals(group1) == true && groupContainsTerminals(group2) == true && groupContainsTerminals(group3) == true && groupContainsTerminals(group4) == true && groupContainsTerminals(pair) == true) {
            if(group1open == true || group2open == true || group3open == true || group4open == true || pairOpen == true) {
                hanCount+= 1;
                yakuList.Add("Chantaiyao (Open)");
            }
            else {
                hanCount+= 2;
                yakuList.Add("Chantaiyao (Closed)");
            }
        }
    }

    void sanshokuDoujun() {
        //Debug.Log("starting sanshoku doujun");
        List<List<int>> sequenceGroups = new List<List<int>>();
        List<List<int>> groupsCopy = groups.ToList();
        if(group1Sequence) { sequenceGroups.Add(group1Face);}
        if(group2Sequence) { sequenceGroups.Add(group2Face);}
        if(group3Sequence) { sequenceGroups.Add(group3Face);}
        if(group4Sequence) { sequenceGroups.Add(group4Face);}
        
        int sameSequenceCount = 0;
        //Debug.Log(sequenceGroups.Count);
        /*
        for(int i = 0; i < sequenceGroups.Count; i++) {
            for(int j = 0; j < sequenceGroups.Count; j++) {
                sequenceGroups[i][j] = tileValueToNumber(sequenceGroups[i][j]);
                //Debug.Log(sequenceGroups[i][j]);
            }
        }
        */
        for(int i = 0; i < sequenceGroups.Count; i++) {
            for(int j = 0; j < sequenceGroups.Count; j++) {
                if(i == j || j < i ){
                    continue;
                }
                else if(sequenceGroups[i].SequenceEqual(sequenceGroups[j])) {
                    //Debug.Log("adding to samesequencecount");
                    sameSequenceCount++;
                }
            }
        }
        if(sameSequenceCount == 3) {
            if(handOpen) {
                hanCount+=1;
                yakuList.Add("Sanshoku Doujun (Open)");
            }
        hanCount+=2;
        yakuList.Add("Sanshoku Doujun (Closed)");
        }
        //Debug.Log("sanshoku doujun END");
    }

    void iitsu() {
        List<int> manIttsu = new List<int> {
            1, //man
            2,
            3,
            4,
            5,
            6,
            7,
            8,
            9
        };
        List<int> pinIttsu = new List<int> {
            10,//pin
            11,
            12,
            13,
            14,
            15,
            16,
            17,
            18
        };
        List<int> souIttsu = new List<int> {
            19, //sou
            20,
            21,
            22,
            23,
            24,
            25,
            26,
            27,
        };
        List<List<int>> manGroup = new List<List<int>>(); //Could maybe do this with a single 3d list?
        List<List<int>> pinGroup = new List<List<int>>();
        List<List<int>> souGroup = new List<List<int>>();
        for(int i = 0; i < groups.Count-1; i ++) {
            switch(checkGroupSuit(groups[i])) {
                case 1:
                    manGroup.Add(groups[i]);
                    break;
                case 2:
                    pinGroup.Add(groups[i]);
                    break;
                case 3:
                    souGroup.Add(groups[i]);
                    break;
                default:
                    break;
            }
        }
        if(manGroup.Count == 3) {                           //roll these into a single function
            List<int> flatMan = flattenList(manGroup);
            flatMan.Sort();
            if(flatMan.SequenceEqual(manIttsu)) {
                if(isHandOpen() == false) {
                    hanCount += 2;
                    yakuList.Add("Ittsu (Closed)");
                }
                else {
                    hanCount += 1;
                    yakuList.Add("Ittsu (Open)");
                }
            }
            
        }
        if(pinGroup.Count == 3) {
            List<int> flatPin = flattenList(pinGroup);
            flatPin.Sort();
            if(flatPin.SequenceEqual(pinIttsu)) {
                if(isHandOpen() == false) {
                    hanCount += 2;
                    yakuList.Add("Ittsu (Closed)");
                }
                else {
                    hanCount += 1;
                    yakuList.Add("Ittsu (Open)");

                }
            }
            
        }
        if(souGroup.Count == 3) {
            List<int> flatSou = flattenList(souGroup);
            flatSou.Sort();
            if(flatSou.SequenceEqual(souIttsu)) {
                if(isHandOpen() == false) {
                    hanCount += 2;
                    yakuList.Add("Ittsu (Closed)");
                }
                else {
                    hanCount += 1;
                    yakuList.Add("Ittsu (Open)");
                }
            }
            
        }
    }

    void toitoi() {
        if(isGroupTriple(group1) && isGroupTriple(group2) && isGroupTriple(group3) && isGroupTriple(group4)) {
            hanCount+=2;
            yakuList.Add("Toitoi");
        }
    }

    void sanankou() {
        int tripletCount = 0;
        for(int i = 0; i < groups.Count-1; i++) {
            if(isGroupTriple(groups[i])) {
                tripletCount++;
            }
        }
        if(tripletCount == 3) {
            yakuList.Add("Sanankou");
            hanCount+=2;
        }
    }

    void sanshokuDoukou() {
        //Debug.Log("starting sanshoku");
        List<int> tripletCounts = new List<int>();
        tripletCounts.Add(0);
        tripletCounts.Add(0);
        tripletCounts.Add(0);
        tripletCounts.Add(0);
        List<List<int>> tripletGroups = new List<List<int>>();
        for(int i = 0; i < convertedGroups.Count-1; i++) {
            if(isGroupTriple(convertedGroups[i])) {
                tripletGroups.Add(convertedGroups[i]);
            }
        }
        for(int i = 0; i < tripletGroups.Count; i++) {
            for(int j = 0; j < tripletGroups.Count; j++) {
                if(i == j || j < i) {
                    continue;
                }
                else {
                    if(tripletGroups[i].SequenceEqual(tripletGroups[j])) {
                        tripletCounts[i] += 1;
                        //Debug.Log("adding 1 to tripletcounts!");
                    }
                }
                
            }
        }
        for(int i = 0; i < tripletCounts.Count; i++) {
            //Debug.Log("tripletcounts i" + tripletCounts[i]);
            if(tripletCounts[i] == 2) { //2 for counting itself
                if(isHandOpen() == false) {
                    hanCount +=2;
                    yakuList.Add("Sanshoku Doukou (Closed)");
                }
                else{
                    hanCount +=1;
                    yakuList.Add("Sanshoku Doukou (Open)");
                }
                
            }
        }
        

    }

    void sankantsu() {
        if(winningClosedKan.Count/4 + winningOpenKan.Count/4 == 3){
            hanCount+= 2;
            yakuList.Add("sankantsu");
        }
    }

    void chiitoitsu() { //EXCEPTION CALL BEFORE GROUPING
    if(closedWinningHand.Count < 14) {
        return;
    }
    else {
        List<int> sortedFlattenedHand = flattenedHand;
        //Debug.Log("Sorted count" + sortedFlattenedHand.Count);

        //Debug.Log("flattened count" + flattenedHand.Count);

        sortedFlattenedHand.Sort();

        for(int i = 0; i < sortedFlattenedHand.Count-1; i+=2) {
            if(sortedFlattenedHand[i] == sortedFlattenedHand[i+1]) {
                continue;
            }
            else {
                return;
            }
        }
        hanCount+=2;
        yakuList.Add("chiitoitsu");
    }
}
        

    void honroutou() {
        

    }

    void shousangen() {
        int dragonMeldCount = 0;
        for(int i = 0; i < groups.Count-1; i++) { //-1 to skip pair group
            if(groupAllDragons(groups[i])) {
                dragonMeldCount++;
            }
        }
        if(groupAllDragons(pair) && dragonMeldCount == 2) {
            hanCount+=2;
            yakuList.Add("shousangen");
        }
    }


    //Begin three han
    void honitsu() {
        Debug.Log("Starting honitsu");
        
        int suitCount = 0;
        int suitValue = 0;
        int honorCount = 0;
        for(int i = 0; i < groups.Count; i++) {
            int currentSuit = checkGroupSuit(groups[i]);
            if (currentSuit > 3 ) {
                honorCount++;
            } 
            if (suitValue == 0 && currentSuit < 4) {
                suitValue = currentSuit;
                suitCount++;
            }
            else if (currentSuit == suitValue) {
                suitCount++;
            }
        }
        if ((suitCount + honorCount == 5) && suitCount > 1) {
            hanCount+=3;
            yakuList.Add("honitsu");
        }
        
    }   

    void junchanTaiyao() {
        if(groupContainsTerminals(group1) && groupContainsTerminals(group2) && groupContainsTerminals(group3) && groupContainsTerminals(group4) && groupContainsTerminals(pair)) {
            hanCount+=3;
            yakuList.Add("junchan Taiyo");
        }

    }

    void ryanpeikou() {
        
    }

    //Six han??

    void chinitsu() {
        if(checkGroupSuit(group1) == checkGroupSuit(group2) && checkGroupSuit(group2) == checkGroupSuit(group3) && checkGroupSuit(group3) == checkGroupSuit(group4) && checkGroupSuit(group4) == checkGroupSuit(pair)) {
            if(handOpen) {
                hanCount += 5;
                yakuList.Add("Chinitsu (open)");
            }
            else {
                hanCount += 6;
                yakuList.Add("Chinitsu (closed)");
            }
        }
    }


    //YAKUMAN

    void kokushiMusou() {
        List<int> kokushiMusou = new List<int> {
            1,
            9,
            10,
            18,
            19,
            27,
            28,
            29,
            30,
            31,
            32,
            33,
            34,
        };
        //Debug.Log("Starting flattenedhand print");
        for(int i = 0; i < flattenedHand.Count; i++) {
            //Debug.Log(flattenedHand[i]);
        }
        int pairValue = flattenedHand[flattenedHand.Count-1];
        List<int> sortedFlattenedHand = flattenedHand;
        sortedFlattenedHand.Remove(pairValue);
        sortedFlattenedHand.Sort();
        //Debug.Log("starting KM func");
        for(int i = 0; i < sortedFlattenedHand.Count; i++) {
            //Debug.Log(sortedFlattenedHand[i]);
        }
        if(sortedFlattenedHand.SequenceEqual(kokushiMusou)) {
                yakumanCount += 1;
                yakuList.Add("kokushiMusou");
            }
        }

    void suuankou() {
        int concealedTriple = 0;
        if(group1Sequence == false && group1open == false) { //you can do this in a for loop just put the open bools into a list
            concealedTriple++;
        }
        if(group2Sequence == false && group2open == false) {
            concealedTriple++;
        }
        if(group3Sequence == false && group3open == false) {
            concealedTriple++;
        }
        if(group4Sequence == false && group4open == false) {
            concealedTriple++;
        }
        if(concealedTriple == 4) {
            yakumanCount+=1;
            yakuList.Add("suuankou");
        }
    }
    

    void daisangen() {
        int dragonMeldTotal = 0;
        for(int i = 0; i < groups.Count-1; i++) {             //skip pair
            if(checkGroupSuit(groups[i]) == 5) {
                dragonMeldTotal++;
            }
        }
        if(dragonMeldTotal==3) {
            yakumanCount += 1;
            yakuList.Add("daisangen");
        }
    }

    void shousuushii() {
        int windMeldTotal = 0;
        for(int i = 0; i < groups.Count-1; i++) {             //include pair
            if(checkGroupSuit(groups[i]) == 4) {
                windMeldTotal++;
            }
        }
        if(windMeldTotal==3 && checkGroupSuit(pair) == 4) {
            yakumanCount += 1;
            yakuList.Add("shousuushii");
        }
    }

    void daisuushii() {
        int windMeldTotal = 0;
        for(int i = 0; i < groups.Count-1; i++) {             //skip pair
            if(checkGroupSuit(groups[i]) == 4) {
                windMeldTotal++;
            }
        }
        if(windMeldTotal==4) {
            yakumanCount += 1;
            yakuList.Add("daisuushi");
        }
    }

    void tsuuiisou() { 
        for (int i = 0; i < flattenedHand.Count; i++) {
            if(    flattenedHand[i] != 28
                && flattenedHand[i] != 29 
                && flattenedHand[i] != 30 
                && flattenedHand[i] != 31 
                && flattenedHand[i] != 32 
                && flattenedHand[i] != 33 
                && flattenedHand[i] != 34   ) {
                    return;
            }
        }
        yakumanCount++;
        yakuList.Add("tsuuiisou");
    }

    void chinroutou() {
        int terminalGroupCount = 0;
        for(int i = 0; i < groups.Count; i++){
            if(groupAllTerminals(groups[i])) {
            terminalGroupCount++;
            }
        }
        if(terminalGroupCount==5) {
            yakumanCount+=1;
            yakuList.Add("chinroutou");
        }

        
    }

    void ryuuiisou() {
        for (int i = 0; i < flattenedHand.Count; i++) {
            
            if (flattenedHand[i] != 20 && flattenedHand[i] != 21 && flattenedHand[i] != 22 && flattenedHand[i] != 24 && flattenedHand[i] != 26 && flattenedHand[i] != 33) {
                //Debug.Log("Returning at value"+flattenedHand[i]);
                //Debug.Log("returning at index"+i);
                return;
            }   
        }
        yakumanCount++;
        yakuList.Add("ryuuisou");
    }

    void chuurenPoutou() {
        List<int> chuurenPoutou = new List<int> {
            1,
            1,
            1,
            2,
            3,
            4,
            5,
            6,
            7,
            8,
            9,
            9,
            9,
        };
        int pairValue = flattenedConvertedHand[flattenedConvertedHand.Count-1];
        List<int> sortedFlattenedHand = flattenedConvertedHand;

        Debug.Log("Printing sorted flattened hand");
        for(int i = 0; i < sortedFlattenedHand.Count; i++) {
            Debug.Log(sortedFlattenedHand[i]);
        }
        Debug.Log("Printing flattened hand");
        for(int i = 0; i < flattenedHand.Count; i++) {
            Debug.Log(flattenedHand[i]);
        }
        Debug.Log("Before removeal chuuren");
        
        //sortedFlattenedHand.Remove(pair[1]);

        sortedFlattenedHand.Sort();
        
        if(handOpen == false && checkGroupSuit(group1) == checkGroupSuit(group2) && checkGroupSuit(group2) == checkGroupSuit(group3) && checkGroupSuit(group3) == checkGroupSuit(group4) && checkGroupSuit(group4) == checkGroupSuit(pair)) {
            if(flattenedHand.SequenceEqual(chuurenPoutou)) {
                yakumanCount += 1;
                yakuList.Add("chuurenPoutou");
            }
        }
    }

    void suukantsu() {
        if((winningClosedKan.Count/4) + (winningOpenKan.Count/4) == 4) {
            yakumanCount++;
            yakuList.Add("suukantsu");
        }
    }

}




