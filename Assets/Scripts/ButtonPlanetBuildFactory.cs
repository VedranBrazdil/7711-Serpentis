using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPlanetBuildFactory : MonoBehaviour {

    Shuttle buildFromThisShuttle;
    Shuttle[] allShuttles;

    SinglePlanet selectedPlanet;
    bool clickable = false;
    bool foundOne = false;

    int[] locationOfPlanet;
    int[] locationOfShuttle;

    public Sprite[] ButtonImages;

    //mouseover text
    public Text mouseOverTxt;
    Vector3 mousePos;

    // Use this for initialization
    void Start () {
    }
    
    // Update is called once per frame
    void Update () {
        if(null != selectedPlanet && !selectedPlanet.isFactoryBuilt() && !selectedPlanet.DiscoverFailedOrFalse()){
            allShuttles = GameObject.FindObjectsOfType<Shuttle>();
            for (var i = 0 ; i < allShuttles.Length ; i ++){
                locationOfShuttle = allShuttles[i].GetLocationOfShuttle();
                if(locationOfShuttle[0] == locationOfPlanet[0] && locationOfShuttle[1] == locationOfPlanet[1]){
                    buildFromThisShuttle = allShuttles[i];
                    if(buildFromThisShuttle.GetMyPlayer().CanIBuildFactoryCheck()){
                        foundOne = true;
                    }
                    break;
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

    public void SelectedPlanetIs(SinglePlanet thisPlanet){
        selectedPlanet = thisPlanet;
        locationOfPlanet = selectedPlanet.GetMyID();
    }

    void OnMouseDown(){
        if(clickable){
            buildFromThisShuttle.ThisShuttlesActionIsDone();
            selectedPlanet.BuildFactoryHere(buildFromThisShuttle.GetMyPlayer());
        }
        Debug.Log("ButtonClicked");
    }

    //mouseover text
    void OnMouseOver()
    {
        mouseOverTxt.text = "Build Factory       ";
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
