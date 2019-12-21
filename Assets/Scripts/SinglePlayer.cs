using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinglePlayer : MonoBehaviour {

    //Player INFO
    int PlayerID = 1;
    string Race;
    int homeOrbit, homePlanet;
    int[] arrResources = new int[8];
    //1. Coffe        4. ScrapMetal    7. 
    //2. Cheese       5. Gold
    //3. EnergyDrink  6. 

    int peopleNum, resFood;
    int shipNum, factoryNum;
    int maxShips, maxFactorys;

    //Mechanic stuff:
    int actionCount = 0;
    int actionsDone = 4;
    public WorldCreatorScript GameMaster;
    public Planets PlanetsInfo;

    // Prefabs:
    public Shuttle prefabShuttle;
    Shuttle thisShuttle;

    // Use this for initialization
    void Start () {
        PlayerID = 1;

        //Settup resources:
        arrResources[0] = 0;
        arrResources[1] = 10; // Cheese
        arrResources[2] = 4; // Coffe
        arrResources[3] = 6; // Energy
        arrResources[4] = 0; // Scrap Metal
        arrResources[5] = 10; // Gold
        arrResources[6] = 0;
        arrResources[7] = 0;


        maxShips = 3;
        maxFactorys = 3;

        resFood = 6;
        peopleNum = 3;

        // Home Planet (hardcoded atm)
        homeOrbit = 1;
        homePlanet = 2;
        Invoke("SetHomePlanet", 2);
        Invoke("CreateFirstShuttle", 2);
        
    }

    void SetHomePlanet()
    {
        PlanetsInfo.SetThisPlanetToBeHomePlanet(homeOrbit, homePlanet, PlayerID);        
    }
    void CreateFirstShuttle()
    {
        // Get one Shuttle at start
        thisShuttle = Instantiate<Shuttle>(prefabShuttle); // (Shuttle)
        shipNum ++;
        thisShuttle.SetupThisShuttle(homeOrbit, homePlanet, this);    
    }

    public int GetPlayerID (){
        return PlayerID;
    }
    
    // Update is called once per frame
    void Update () {
        if (actionCount == actionsDone){
            actionCount = 0;
            GameMaster.PlayerDone();
        }
    }

    public void ActionDone(){
        actionCount ++;
        Debug.Log("Player " + PlayerID + " action done: " + actionCount); 
    }

    // RESOURCES *********************************************************************************
    public bool CanIPayCheck(int type, int cost){
        if((arrResources[type] - cost) < 0){
            return false;
        } else {
            return true;
        }
    }

    public void PayResources(int type, int cost){
        arrResources[type] = arrResources[type] - cost;
    }
    public void GainResources(int type, int gain){
        arrResources[type] = arrResources[type] + gain;
    }

    public int HowMuchResourcesIHave(int type){
        return arrResources[type];
    }


    // BUILDINGS *************************************************************************
    public bool CanIBuildFactoryCheck(){
        if(factoryNum == maxFactorys){
            return false;
        }
        return true;
    }
    public void IBuiltFactory(){
        factoryNum ++;
    }

    // SHIPS *********************************************************************************
    public bool CanIBuildShipCheck(){
        if (shipNum == maxShips){
            return false;
        }
        /*if ((arrResources[4] - 4) < 0 || (arrResources[3] - 2) < 0 || (arrResources[5] - 1) < 0) {
            // cost atm: 4 scrap metal, 2 energy, 1 gold
            return false;
        }*/
        return true;
    }
    public void PayForShip(){
        /*arrResources[4] = arrResources[4] - 4;
        arrResources[3] = arrResources[3] - 2;
        arrResources[5] = arrResources[5] - 1;
        */
        // cost atm: 4 scrap metal, 2 energy, 1 gold
    }
    public void IBuiltShip(){
        shipNum ++;
    }
    public int GetShipsNum(){
        return shipNum;
    }
}
