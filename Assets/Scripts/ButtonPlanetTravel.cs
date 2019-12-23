using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    //mouseover
    public Text mouseOverTxt;
    Vector3 mousePos;


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
            this.TextRemoval();
        }
    }


    //mouseover text
    void OnMouseOver()
    {
        mouseOverTxt.text = "Travel Here";
        mousePos = Input.mousePosition;
        mouseOverTxt.transform.position = mousePos;
    }

    void TextRemoval(){
        mouseOverTxt.text = "";
    }

    void OnMouseExit()
    {
        this.TextRemoval();
    }
}
