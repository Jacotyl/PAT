using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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

    public int[] terminals = {
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

    public int currentWind;

    public int playerWind;

    //scoring

    int hanCount;
    int yakumanCount;

    int fuCount;


    //grouping

    bool handOpen;
    List<int> pair = new List<int>();
    bool pairOpen;
    List<int> group1 = new List<int>();
    bool group1open;
    bool group1Sequence;
    List<int> group2 = new List<int>();
    bool group2open;
    bool group2Sequence;
    List<int> group3 = new List<int>();
    bool group3open;
    bool group3Sequence;
    List<int> group4 = new List<int>();
    bool group4open;
    bool group4Sequence;

    List<int> flattenedHand = new List<int>();

    List<List<int>> groups = new List<List<int>>();
    
    List<string> yakuList = new List<string>();


    //Table Variables
    public bool ron;




    bool containsTerminals(){
        for (int i = 0; i < terminals.Length; i++) {
            if (closedWinningHand.Contains(i) || winningChii.Contains(i) || winningPon.Contains(i) || winningClosedKan.Contains(i) || winningOpenKan.Contains(i)) {
                return true;
            }
        }
        return false;
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
            return false;
        }
        else {
            return true;
        }

    }

    int sequenceComparator() {
        int identicalSequenceCount = 0;
        List<List<int>> melds = new List<List<int>> {group1, group2, group3, group4};
            for(int i = 0; i < melds.Count; i++) {
                for(int j = 0; j < melds.Count; j++) {
                    if(i == j) {                            //skip comparing group with itself
                        continue;
                    }
                    else if(melds[i].SequenceEqual(melds[j])) {
                        identicalSequenceCount+=1;
                    }
                }
            }
            return identicalSequenceCount;
    }

    int checkGroupSuit(List<int> group) { //1 = man, 2 = pin, 3 = sou, 4 = wind, 5 = dragon
        for(int i = 0; i < group.Count; i++) {
            if(group[i] <= 9) { //man
                return 1;
            }
            else if(group[i] >= 10 && group[i] <= 18) { //pin
                return 2;
            }
            else if(group[i] >= 19 && group[i] <= 27) { //sou
                return 3;
            }
            else if(group[i] >= 28 && group [i] <= 31) { //wind
                return 4;
            }
            
        }
        return 5; //dwagon uwu
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
        if(group[0] == group[2] && group.Count < 4) { //Currently makes sure it is not a Kan.
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
        return tileNumbers[tile+1];
    }

    void flattenHand() {
        for(int i = 0; i < groups.Count; i++) {
            for(int j = 0; j < groups[i].Count; j++) {
                flattenedHand.Add(groups[i][j]);
            }
        }
    }


    //Currently skipped: Riichi, Ippatsu, pinfu, Haitei Raoyue, Houtei Raoyui, Rinshan Kaihou, Chankan, Double Riichi, Kazoe Yakuman
    public void calcYaku() {
        //closedWinningHand.Sort();
        //openWinningHand.Sort();
        //openKan.Sort();
        //closedKan.Sort();
        Debug.Log("starting calc");
        Debug.Log(group1.Count);
        handGroup();
        group1Sequence = isGroupSequence(group1);
        group2Sequence = isGroupSequence(group2);
        group3Sequence = isGroupSequence(group3);
        group4Sequence = isGroupSequence(group4);
        groups.Add(group1);
        groups.Add(group2);
        groups.Add(group3);
        groups.Add(group4);
        groups.Add(pair);
        if(group1open || group2open || group3open || group4open) {
            handOpen = true;
        }

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
        flattenHand();
        Debug.Log("Flat hand");
        for(int i = 0; i < flattenedHand.Count; i++) {
            Debug.Log(flattenedHand[i]);
        }

        kokushiMusou();
        suuankou();
        daisangen();
        shousuushii();
        daisuushii();
        tsuuiisou();
        chinroutou();
        ryuuiisou();
        chuurenPoutou();
        suukantsu();

        Debug.Log(yakumanCount);
        for(int i = 0; i < yakuList.Count; i++){
            Debug.Log(yakuList[i]);
        }
        

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
        pair.Add(closedWinningHand[closedWinningHand.Count-1]);
        //Debug.Log(closedWinningHand[closedWinningHand.Count-1]);
        pair.Add(closedWinningHand[closedWinningHand.Count-2]);
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
        if(isHandOpen() && ron == false) {
            hanCount+= 1;
        }
    }

    void iipeikou() {
        if(isHandOpen() == false) {
            if(sequenceComparator() == 1) {
                hanCount+= 1;
            }
        }
    } 


    void tanyao() {
        if (containsTerminals() == false) {
            hanCount+= 1;
        }
    }

    void yakuhai(List<int> open, List<int> closed, List<int> openKan, List<int> closedKan) {
        int currentWindCounter = 0;
        int seatWindCounter = 0;
        int whiteCounter = 0;
        int greenCounter = 0;
        int redCounter = 0;
        for(int i = 0; i < open.Count; i++) { //roll this into a re-usable function later this is messy
            if (open[i] == currentWind) {
                currentWindCounter++;
            } 
            if (open[i] == playerWind) {
                seatWindCounter++;
            }
            if (open[i] == dragons[0]) {
                whiteCounter++;
            }
            if (open[i] == dragons[1]) {
                greenCounter++;
            }
            if (open[i] == dragons[2]) {
                redCounter++;
            }
        }
        for(int i = 0; i < closed.Count; i++) {
            if (closed[i] == currentWind) {
                currentWindCounter++;
            } 
            if (closed[i] == playerWind) {
                seatWindCounter++;
            }
            if (closed[i] == dragons[0]) {
                whiteCounter++;
            } 
            if (closed[i] == dragons[1]) {
                greenCounter++;
            }
            if (closed[i] == dragons[2]) {
                redCounter++;
            }            
        }
        for(int i = 0; i < closedKan.Count; i++) {
            if (closedKan[i] == currentWind) {
                currentWindCounter++;
            } 
            if (closedKan[i] == playerWind) {
                seatWindCounter++;
            }
            if (closedKan[i] == dragons[0]) {
                whiteCounter++;
            }
            if (closedKan[i] == dragons[1]) {
                greenCounter++;
            }
            if (closedKan[i] == dragons[2]) {
                redCounter++;
            }            
        }
        for(int i = 0; i < openKan.Count; i++) {
            if (openKan[i] == playerWind) {
                currentWindCounter++;
            } 
            if (openKan[i] == seatWindCounter) {
                seatWindCounter++;
            }
            if (openKan[i] == dragons[0]) {
                whiteCounter++;
            }
            if (openKan[i] == dragons[1]) {
                greenCounter++;
            }
            if (openKan[i] == dragons[2]) {
                redCounter++;
            }            
        }
        if (currentWindCounter >= 3) {
            hanCount++;
        }
        if (seatWindCounter >= 3) {
            hanCount++;
        }
        if (whiteCounter >= 3) {
            hanCount++;
        }
        if (greenCounter >= 3) {
            hanCount++;
        }
        if (redCounter >= 3) {
            hanCount++;
        }
    }


    //begin two han
    void chantaiyao() {
        if(groupContainsTerminals(group1) == true && groupContainsTerminals(group1) == true && groupContainsTerminals(group1) == true && groupContainsTerminals(group1) == true && groupContainsTerminals(pair) == true) {
            if(group1open == true || group2open == true || group3open == true || group4open == true || pairOpen == true) {
                hanCount+= 1;
            }
            else {
                hanCount+= 2;
            }
        }
    }

    void sanshokuDoujun() {
        List<List<int>> sequenceGroups = new List<List<int>>();
        if(group1Sequence) { sequenceGroups.Add(group1);}
        if(group2Sequence) { sequenceGroups.Add(group2);}
        if(group3Sequence) { sequenceGroups.Add(group3);}
        if(group4Sequence) { sequenceGroups.Add(group4);}
        int sameSequenceCount = 0;
        for(int i = 0; i < sequenceGroups.Count; i++) {
            for(int j = 0; j < sequenceGroups.Count; i++) {
                if(i == j) {
                    continue;
                }
                else if(groupContainsSameNumbers(sequenceGroups[i], sequenceGroups[j])) {
                    sameSequenceCount++;
                }
            }
        }
        if(sameSequenceCount == 3) {
            if(handOpen) {
                hanCount+=1;
            }
        hanCount+=2;
        }
    }

    void iitsu(List<int> open, List<int> closed) {

    }

    void toitoi() {
        if(isGroupTriple(group1) && isGroupTriple(group2) && isGroupTriple(group3) && isGroupTriple(group4)) {
            hanCount+=2;
        }
    }

    void sanankou(List<int> open, List<int> closed) {

    }

    void sanshokuDoukou() {

    }

    void sankantsu(List<int> open, List<int> closed) {
        if(winningClosedKan.Count/4 + winningOpenKan.Count/4 == 3){
            hanCount+= 2;
        }
    }

    void chiitoitsu(List<int> open, List<int> closed) { //EXCEPTION CALL BEFORE GROUPING

    }

    void honroutou(List<int> open, List<int> closed) {

    }

    void shousangen(List<int> open, List<int> closed) {

    }


    //Begin three han
    void honitsu(List<int> open, List<int> closed) {

    }

    void junchanTaiyao(List<int> open, List<int> closed) {

    }

    void ryanpeikou(List<int> open, List<int> closed) {

    }

    //Six han??

    void chinitsu() {
        if(checkGroupSuit(group1) == checkGroupSuit(group2) && checkGroupSuit(group2) == checkGroupSuit(group3) && checkGroupSuit(group3) == checkGroupSuit(group4) && checkGroupSuit(group4) == checkGroupSuit(pair)) {
            if(handOpen) {
                hanCount += 5;
            }
            else {
                hanCount += 6;
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
        int pairValue = flattenedHand[flattenedHand.Count-1];
        List<int> sortedFlattenedHand = flattenedHand;
        sortedFlattenedHand.Remove(pairValue);
        sortedFlattenedHand.Sort();
        if(handOpen == false && checkGroupSuit(group1) == checkGroupSuit(group2) && checkGroupSuit(group2) == checkGroupSuit(group3) && checkGroupSuit(group3) == checkGroupSuit(group4) && checkGroupSuit(group4) == checkGroupSuit(pair)) {
            if(sortedFlattenedHand.SequenceEqual(kokushiMusou)) {
                yakumanCount += 1;
                yakuList.Add("kokushiMusou");
            }
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
                Debug.Log("Returning at value"+flattenedHand[i]);
                Debug.Log("returning at index"+i);
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
        int pairValue = flattenedHand[flattenedHand.Count-1];
        List<int> sortedFlattenedHand = flattenedHand;
        sortedFlattenedHand.Remove(pair[0]);
        sortedFlattenedHand.Sort();
        for(int i = 0; i < sortedFlattenedHand.Count; i++){
            sortedFlattenedHand[i] = tileValueToNumber(sortedFlattenedHand[i]); //convert to tile values
        }
        if(handOpen == false && checkGroupSuit(group1) == checkGroupSuit(group2) && checkGroupSuit(group2) == checkGroupSuit(group3) && checkGroupSuit(group3) == checkGroupSuit(group4) && checkGroupSuit(group4) == checkGroupSuit(pair)) {
            if(sortedFlattenedHand.SequenceEqual(chuurenPoutou)) {
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




