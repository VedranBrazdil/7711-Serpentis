using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonShuttleAction1 : MonoBehaviour {

    Shuttle selectedShuttle;
    SinglePlanet selectedPlanet;

    int[] locationOfPlanet;
    int[] locationOfShuttle;

    public Planets PlanetsInfo;

    bool clickable;
    bool setToClickable;
    bool actionDone = false;

    public Sprite[] ButtonImages;

    // Use this for initialization
    void Start () {
        clickable = false;
        setToClickable = false;
    }
    
    // Update is called once per frame
    void Update () {
        if(null != selectedShuttle){
            locationOfShuttle = selectedShuttle.GetLocationOfShuttle();
            selectedPlanet = PlanetsInfo.GetPlanetOnThisLocation(locationOfShuttle[0], locationOfShuttle[1]);
        }

        if(null != selectedPlanet && selectedPlanet.isPlanetHere()){
            setToClickable = true;
        }


        if(setToClickable){
            clickable = true;
            this.GetComponent<SpriteRenderer>().sprite = ButtonImages[0];
        } else {
            clickable = false;
            this.GetComponent<SpriteRenderer>().sprite = ButtonImages[1];
        }
        setToClickable = false;
    }

    public void IWantToUseActions(Shuttle thisShuttle){
        selectedShuttle = thisShuttle;
        locationOfShuttle = selectedShuttle.GetLocationOfShuttle();
    }

    void OnMouseDown(){
        //Debug.Log("A1 clickable: " + clickable + " selectedPlanet: " + selectedPlanet);
        if(clickable){
            actionDone = selectedPlanet.ActionOne(selectedShuttle);
            if(actionDone){
                selectedShuttle.ThisShuttlesActionIsDone();
            }
        }
        //Debug.Log("ButtonClicked");
    }
}
