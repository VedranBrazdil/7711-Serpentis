using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonShuttleDiscover : MonoBehaviour {

    Shuttle discoverFromThisShuttle;
    Shuttle[] allShuttles;

    SinglePlanet selectedPlanet;
    bool clickable = false;
    bool foundOne = false;

    int[] locationOfPlanet;
    int[] locationOfShuttle;

    //public Transform btnDiscoverGO;

    public Sprite[] ButtonImages;

    //mouseover text
    public Text mouseOverTxt;
    Vector3 mousePos;

    // Use this for initialization
    void Start () {
    }
    
    // Update is called once per frame
    void Update () {
        if(null != selectedPlanet && !selectedPlanet.IsDiscoverDoneTrue()){
            allShuttles = GameObject.FindObjectsOfType<Shuttle>();
            for (var i = 0 ; i < allShuttles.Length ; i ++){
                locationOfShuttle = allShuttles[i].GetLocationOfShuttle();
                if(locationOfShuttle[0] == locationOfPlanet[0] && locationOfShuttle[1] == locationOfPlanet[1]){
                    foundOne = true;
                    discoverFromThisShuttle = allShuttles[i];
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

            /*if(!clickable || selectedPlanet.IsDiscoverDoneTrue()){
                btnDiscoverGO.GetComponent<Button>().interactable = false;
            } else {
                btnDiscoverGO.GetComponent<Button>().interactable = true;
            }*/
    }

    public void SelectedPlanetIs(SinglePlanet thisPlanet){
        selectedPlanet = thisPlanet;
        locationOfPlanet = selectedPlanet.GetMyID();
    }

    void OnMouseDown(){
        //Debug.Log("clickable: " + clickable + " selectedPlanetwasted: " + selectedPlanet.IsDiscoverDoneTrue());
        if(clickable){
            discoverFromThisShuttle.ThisShuttlesActionIsDone();
            selectedPlanet.DiscoverOnThisPlanet(discoverFromThisShuttle);
        }
    }


    //mouseover text
    void OnMouseOver()
    {
        mouseOverTxt.text = "    Discover";
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
