using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planets : MonoBehaviour {

    // First num is ORBIT - goes from sun to out
    // Second num is PLANET in ORBIT - goes like on clock (from 1 to 12), clockwise
    Vector3[,] arrPlanetPosition = new Vector3[4, 6];

    //PlanetLocators
    public SinglePlanet Planet00;
    public SinglePlanet Planet01;
    public SinglePlanet Planet02;
    public SinglePlanet Planet10;
    public SinglePlanet Planet11;
    public SinglePlanet Planet12;
    public SinglePlanet Planet13;
    public SinglePlanet Planet20;
    public SinglePlanet Planet21;
    public SinglePlanet Planet22;
    public SinglePlanet Planet23;
    public SinglePlanet Planet24;
    public SinglePlanet Planet30;
    public SinglePlanet Planet31;
    public SinglePlanet Planet32;
    public SinglePlanet Planet33;
    public SinglePlanet Planet34;
    public SinglePlanet Planet35;

    private SinglePlanet[,] arrListOfPlanets = new SinglePlanet[4, 6]; 

    //Planet rules:
    string[,] arrCanTravelTo = new string[4, 6];

    //Shuttle gameobject
    Shuttle[] listOfShuttles;

    //Discovering
    int discoverNum;
    int discoverFail;

    // Use this for initialization
    void Start () {
        //Take Location of Planet placeholder Locators:
        //Orbit 0
        arrPlanetPosition[0,0] = Planet00.transform.position;
        Planet00.SetMyID(0,0);
        arrCanTravelTo[0,0] = "1,1 0,1 0,2";
        arrPlanetPosition[0,1] = Planet01.transform.position;
        Planet01.SetMyID(0,1);
        arrCanTravelTo[0,1] = "1,2 0,2 0,0";
        arrPlanetPosition[0,2] = Planet02.transform.position;
        Planet02.SetMyID(0,2);
        arrCanTravelTo[0,2] = "1,0 1,3 0,1 0,0";
        //Orbit 1
        arrPlanetPosition[1,0] = Planet10.transform.position;
        Planet10.SetMyID(1,0);
        arrCanTravelTo[1,0] = "0,2 2,0 1,1 1,3";
        arrPlanetPosition[1,1] = Planet11.transform.position;
        Planet11.SetMyID(1,1);
        arrCanTravelTo[1,1] = "0,0 2,1 2,2 1,0 1,2";
        arrPlanetPosition[1,2] = Planet12.transform.position;
        Planet12.SetMyID(1,2);
        arrCanTravelTo[1,2] = "0,1 2,3 1,1 1,3";
        arrPlanetPosition[1,3] = Planet13.transform.position;
        Planet13.SetMyID(1,3);
        arrCanTravelTo[1,3] = "0,2 2,4 1,0 1,2";
        //Orbit 2
        arrPlanetPosition[2,0] = Planet20.transform.position;
        Planet20.SetMyID(2,0);
        arrCanTravelTo[2,0] = "1,0 3,5 2,1 2,4";
        arrPlanetPosition[2,1] = Planet21.transform.position;
        Planet21.SetMyID(2,1);
        arrCanTravelTo[2,1] = "1,1 3,0 3,1 2,0 2,2";
        arrPlanetPosition[2,2] = Planet22.transform.position;
        Planet22.SetMyID(2,2);
        arrCanTravelTo[2,2] = "1,1 3,2 2,1 2,3";
        arrPlanetPosition[2,3] = Planet23.transform.position;
        Planet23.SetMyID(2,3);
        arrCanTravelTo[2,3] = "1,2 3,3 2,2 2,4";
        arrPlanetPosition[2,4] = Planet24.transform.position;
        Planet24.SetMyID(2,4);
        arrCanTravelTo[2,4] = "1,3 3,4 2,0 2,3";
        //Orbit 3
        arrPlanetPosition[3,0] = Planet30.transform.position;
        Planet30.SetMyID(3,0);
        arrCanTravelTo[3,0] = "2,1 3,1 3,5";
        arrPlanetPosition[3,1] = Planet31.transform.position;
        Planet31.SetMyID(3,1);
        arrCanTravelTo[3,1] = "2,1 3,0 3,2";
        arrPlanetPosition[3,2] = Planet32.transform.position;
        Planet32.SetMyID(3,2);
        arrCanTravelTo[3,2] = "2,2 3,1 3,3";
        arrPlanetPosition[3,3] = Planet33.transform.position;
        Planet33.SetMyID(3,3);
        arrCanTravelTo[3,3] = "2,3 3,2 3,4";
        arrPlanetPosition[3,4] = Planet34.transform.position;
        Planet34.SetMyID(3,4);
        arrCanTravelTo[3,4] = "2,4 3,3 3,5";
        arrPlanetPosition[3,5] = Planet35.transform.position;
        Planet35.SetMyID(3,5);
        arrCanTravelTo[3,5] = "2,0 3,0 3,4";


        // Set planets to array for easy lookup:
        arrListOfPlanets[0,0] = Planet00;
        arrListOfPlanets[0,1] = Planet01;
        arrListOfPlanets[0,2] = Planet02;

        arrListOfPlanets[1,0] = Planet10;
        arrListOfPlanets[1,1] = Planet11;
        arrListOfPlanets[1,2] = Planet12;
        arrListOfPlanets[1,3] = Planet13;

        arrListOfPlanets[2,0] = Planet20;
        arrListOfPlanets[2,1] = Planet21;
        arrListOfPlanets[2,2] = Planet22;
        arrListOfPlanets[2,3] = Planet23;
        arrListOfPlanets[2,4] = Planet24;

        arrListOfPlanets[3,0] = Planet30;
        arrListOfPlanets[3,1] = Planet31;
        arrListOfPlanets[3,2] = Planet32;
        arrListOfPlanets[3,3] = Planet33;
        arrListOfPlanets[3,4] = Planet34;
        arrListOfPlanets[3,5] = Planet35;
    }
    
    // Update is called once per frame
    void Update () {
        
    }

    public Vector3 GetPositionOfPlanet(int orbitNum, int planetNum) //, int xory
    {
        // Returns Position of selected Planet
        return arrPlanetPosition[orbitNum, planetNum];
    }
    public SinglePlanet GetPlanetOnThisLocation(int orbitNum, int planetNum) //, int xory
    {
        // Returns Planet on selected location
        return arrListOfPlanets[orbitNum, planetNum];
    }
    public void SetThisPlanetToBeHomePlanet(int orb, int planet, int player){
        //Each player needs Home Planet!
        Debug.Log("Home Planet is created: " + arrListOfPlanets[orb, planet].name);
        arrListOfPlanets[orb, planet].SetThisPlanetToBeHomePlanetFor(player);
    }

    public bool CanITravelFromTo(int[] ShuttlePosition, int[] PlanetPosition) //, int xory
    {
        string tmpString = PlanetPosition[0] + "," + PlanetPosition[1];
        Debug.Log("CanITravelFrom: " + ShuttlePosition[0] + "," + ShuttlePosition[1] + " To- Input: " + tmpString + " check in: " + arrCanTravelTo[ShuttlePosition[0],ShuttlePosition[1]]);
        if (arrCanTravelTo[ShuttlePosition[0],ShuttlePosition[1]].Contains(tmpString)){
            return true;
        }
        return false;
    }

    public void RoundDone() {
        //arrListOfPlanets update
        //arrPlanetPosition NOT UPDATED but location of each planet is
        // each arrListOfPlanets[].SetMyID(1,0) NEW ID
        SinglePlanet holderOfNext = Planet00; // just for syntax reasons, accly its placeholder
        SinglePlanet tmpHolderOfNext;
        int lastJ = 0;
        for (var i = 0 ; i < arrListOfPlanets.Length ; i ++){
            for (var j = 0 ; j < arrListOfPlanets.Length ; j ++){
                //Limits to not enter null fields
                if (i == 0){
                    if (j > 2) break;
                    lastJ = 2;
                }
                else if (i == 1){
                    if(j > 3) break;
                    lastJ = 3;
                }
                else if (i == 2){
                    if(j > 4) break;
                    lastJ = 4;
                }
                else if (i == 3){
                    if(j > 5) break;
                    lastJ = 5;
                }
                else {
                    break;
                }

                if (j == 0){
                    holderOfNext = arrListOfPlanets[i,(j+1)];
                    arrListOfPlanets[i,(j+1)] = arrListOfPlanets[i,j];
                }
                else if (j != lastJ){
                    tmpHolderOfNext = arrListOfPlanets[i,(j+1)];
                    arrListOfPlanets[i,(j+1)] = holderOfNext;
                    holderOfNext = tmpHolderOfNext;
                }
                else {
                    // last one goes to first place
                    arrListOfPlanets[i,0] = holderOfNext;
                }
            }
        }

        // Set position and location of each planet:
        for (var i = 0 ; i < arrListOfPlanets.Length ; i ++){
            for (var j = 0 ; j < arrListOfPlanets.Length ; j ++){
                //Limits to not enter null fields
                if (i == 0){
                    if (j > 2) break;
                }
                else if (i == 1){
                    if(j > 3) break;
                }
                else if (i == 2){
                    if(j > 4) break;
                }
                else if (i == 3){
                    if(j > 5) break;
                }
                else {
                    break;
                }

                //arrListOfPlanets[i,j].transform.position = arrPlanetPosition[i,j];
                arrListOfPlanets[i,j].SetMyID(i,j);
                arrListOfPlanets[i,j].SetPositionOfThisPlanet(arrPlanetPosition[i,j]);
            }
        }

        // Also Change Shuttle location if its on planet!
        int[] possiblyNewLocation = new int[2];
        listOfShuttles = GameObject.FindObjectsOfType<Shuttle>();
        for (var i = 0 ; i < listOfShuttles.Length ; i ++){
            listOfShuttles[i].SetflagThisOneIsSelectedtoFALSE();
            possiblyNewLocation = listOfShuttles[i].GetLocationOfShuttle();

            if (possiblyNewLocation[0] == 0){
                if (possiblyNewLocation[1] == 2){
                    possiblyNewLocation[1] = 0;
                } else {
                    possiblyNewLocation[1] = possiblyNewLocation[1] + 1;
                }
            }
            else if (possiblyNewLocation[0] == 1){
                if(possiblyNewLocation[1] == 3){
                    possiblyNewLocation[1] = 0;
                } else {
                    possiblyNewLocation[1] = possiblyNewLocation[1] + 1;
                }
            }
            else if (possiblyNewLocation[0] == 2){
                if(possiblyNewLocation[1] == 4){
                    possiblyNewLocation[1] = 0;
                } else {
                    possiblyNewLocation[1] = possiblyNewLocation[1] + 1;
                }
            }
            else if (possiblyNewLocation[0] == 3){
                if(possiblyNewLocation[1] == 5){
                    possiblyNewLocation[1] = 0;
                } else {
                    possiblyNewLocation[1] = possiblyNewLocation[1] + 1;
                }
            }

            // Move only if shuttle was on planet!
            if(arrListOfPlanets[possiblyNewLocation[0],possiblyNewLocation[1]].isPlanetHere()){
                listOfShuttles[i].SetLocationOfShuttle(possiblyNewLocation);
                listOfShuttles[i].SetPositionOfShuttle(arrPlanetPosition[possiblyNewLocation[0],possiblyNewLocation[1]]);
            }

        }
        // only one is enaugh to turn off Buttons
        listOfShuttles[0].TurnOffButtonsOfShuttle();
        arrListOfPlanets[0,0].TurnOffButtonsOfPlanet();
    }

    // DISCOVERING
    public void DiscoverHappened(bool isOrisNot){
        if(!isOrisNot){
            discoverFail ++;
        }
        discoverNum ++;
    }
    public int DiscoveringBonusIs(){
        return (int)((discoverNum/3) + discoverFail - ((discoverNum - discoverFail)/2));
    }


    // FACTORY
    public void FactoryCheckup(){
        int lastJ = 0;
        for (var i = 0 ; i < arrListOfPlanets.Length ; i ++){
            for (var j = 0 ; j < arrListOfPlanets.Length ; j ++){
                if (i == 0){
                    if (j > 2) break;
                    lastJ = 2;
                }
                else if (i == 1){
                    if(j > 3) break;
                    lastJ = 3;
                }
                else if (i == 2){
                    if(j > 4) break;
                    lastJ = 4;
                }
                else if (i == 3){
                    if(j > 5) break;
                    lastJ = 5;
                }
                else {
                    break;
                }
                if(null != arrListOfPlanets[i,j]){
                    arrListOfPlanets[i,j].ActivateFactory();
                }
            }
        }
    }

}
