using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPlanetTravel : MonoBehaviour {

    public Planets PlanetsInfo;
    public SinglePlanet selectedPlanet;
    Shuttle[] allShuttles;
    Shuttle selectedShuttle;

    bool clickable = false;
    bool foundOne = false;

    int[] locationOfPlanet;
    int[] locationOfShuttle;

    public Sprite[] ButtonImages;


    // Use this for initialization
    void Start () {
        
    }
    
    // Update is called once per frame
    void Update () {
        if(null != selectedPlanet){
            allShuttles = GameObject.FindObjectsOfType<Shuttle>();
            for (var i = 0 ; i < allShuttles.Length ; i ++){
                locationOfShuttle = allShuttles[i].GetLocationOfShuttle();
                if(allShuttles[i].IsThisShuttleSelected() && allShuttles[i].IsThisShuttleMoving()){
                    if (PlanetsInfo.CanITravelFromTo(locationOfShuttle, locationOfPlanet)){
                        //if(!(locationOfShuttle[0] == locationOfPlanet[0] && locationOfShuttle[1] == locationOfPlanet[1])){
                        selectedShuttle = allShuttles[i];
                        foundOne = true;
                        break;
                        //}
                    }
                }
            }
        }

        if (foundOne){
            clickable = true;
            this.GetComponent<SpriteRenderer>().sprite = ButtonImages[0];
        } else{
            clickable = false;
            this.GetComponent<SpriteRenderer>().sprite = ButtonImages[1];
        }
        foundOne = false;
    }

    public void SelectedPlanetIs(SinglePlanet tmpPlanet){
        selectedPlanet = tmpPlanet;
        locationOfPlanet = selectedPlanet.GetMyID();
    }

    void OnMouseDown(){
        if (clickable){
            selectedPlanet.MoveShipHere(selectedShuttle);
        }
    }
}
