using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPlanetBuildShip : MonoBehaviour {

    SinglePlanet selectedPlanet;
    SinglePlayer ownerPlayer;
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
        if(null != selectedPlanet && selectedPlanet.isFactoryBuilt()){
            ownerPlayer = selectedPlanet.GetMyOwner();
            if(ownerPlayer.CanIBuildShipCheck()){
                foundOne = true;
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
            selectedPlanet.BuildShipHere();
        }
        Debug.Log("ButtonClicked");
    }

    //mouseover text
    void OnMouseOver()
    {
        mouseOverTxt.text = "Build Ship              ";
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
