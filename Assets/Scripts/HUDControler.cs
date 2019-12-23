using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDControler : MonoBehaviour {

    //Other Objects
    //Shuttle[] AllShuttles;
    //Shuttle SelectedShuttle;
    //SinglePlayer[] AllPlayers;
    SinglePlayer SelectedPlayer;
    SinglePlanet SelectedPlanet;

    //Resources
    string cheese, coffee, scrapMetal, energyDrink, gold;
    string A1Gain, A2Gain, A2Cost;

    //Botom Left HUD
    public Text txtBotLeftCheese;
    public Text txtBotLeftCoffee;
    public Text txtBotLeftEnergyDrink;
    public Text txtBotLeftScrapMetal;
    public Text txtBotLeftGold;
    public Image hudBotLeft;
    public Image hudBotLeftResources;

    //Botom Right HUD
    public Sprite[] PlanetImages;
    public Image hudBRPlanetImage;
    public Image hudBRPlanetImageOuterLayer;
    //RectTransform hudBRPlanetImageRT;
    Vector3 rotationEuler;
    Vector3 rotationEulerOutLay;

    public Text txtBRAction1;
    public Text txtBRAction2;
    //public Text txtBRCost1;
    public Text txtBRCost2;
    public Image hudBRAction1;
    public Image hudBRAction2;
    public Image hudBRCost2;
    public Sprite[] ResourcesImages;
    int[] arrTypeOfResourceGain = new int[3];
    int[] arrAmountOfResourceGain = new int[3];
    int[] arrTypeOfResourceCost = new int[3];
    int[] arrAmountOfResourceCost = new int[3];


    bool flagPlayerIsSelected = false;

    //References
    public ButtonShuttleTravel btnMove;
    public GameObject btnMoveGO;
    public ButtonShuttleAction1 btnAction1;
    public GameObject btnAction1GO;
    public ButtonShuttleAction2 btnAction2;
    public GameObject btnAction2GO;



    // Use this for initialization
    void Start () {
        //Save References
        //btnMove = GameObject.FindObjectOfType<ButtonShuttleTravel>();
        //btnMoveGO = GameObject.Find("ButtonShuttleTravel");
        //btnAction1 = GameObject.FindObjectOfType<ButtonShuttleAction1>();
        //btnAction1GO = GameObject.Find("ButtonShuttleAction1");
        //btnAction2 = GameObject.FindObjectOfType<ButtonShuttleAction2>();
        //btnAction2GO = GameObject.Find("ButtonShuttleAction2");

        //Botom Left HUD
        hudBotLeft.enabled = true;
        hudBotLeftResources.enabled = false;
        txtBotLeftCheese.text = "";
        txtBotLeftCoffee.text = "";
        txtBotLeftEnergyDrink.text = "";
        txtBotLeftScrapMetal.text = "";

        //Bottom Left HUD
        hudBRPlanetImageOuterLayer.enabled = false;
        hudBRAction1.sprite = ResourcesImages[0];
        hudBRAction2.sprite = ResourcesImages[0];
        hudBRCost2.sprite = ResourcesImages[0];

        //Bottom Right HUD
        txtBRAction1.text = "";
        txtBRAction2.text = "";
        txtBRCost2.text = "";
    }
    
    // Update is called once per frame
    void Update () {

        if(flagPlayerIsSelected){
            this.PlayerResourceUpdate(SelectedPlayer);
        }

        txtBotLeftCheese.text = cheese;
        txtBotLeftCoffee.text = coffee;
        txtBotLeftEnergyDrink.text = energyDrink;
        txtBotLeftScrapMetal.text = scrapMetal;
        txtBotLeftGold.text = gold;

        rotationEuler+= Vector3.forward*30*Time.deltaTime; //increment 30 degrees every second
        hudBRPlanetImage.transform.rotation = Quaternion.Euler(rotationEuler);
        rotationEulerOutLay+= Vector3.forward*10*Time.deltaTime;
        hudBRPlanetImageOuterLayer.transform.rotation = Quaternion.Euler(rotationEulerOutLay);

        txtBRAction1.text = A1Gain;
        txtBRAction2.text = A2Gain;
        txtBRCost2.text = A2Cost;
    }

    public void PlayerIsSelected(SinglePlayer thisPlayer){
        SelectedPlayer = thisPlayer;
        cheese = "" + thisPlayer.HowMuchResourcesIHave(1);
        coffee = "" + thisPlayer.HowMuchResourcesIHave(2);
        energyDrink = "" + thisPlayer.HowMuchResourcesIHave(3);
        scrapMetal = "" + thisPlayer.HowMuchResourcesIHave(4);
        gold = "" + thisPlayer.HowMuchResourcesIHave(5);
        hudBotLeftResources.enabled = true;
        flagPlayerIsSelected = true;
    }
    void PlayerResourceUpdate(SinglePlayer thisPlayer){
        cheese = "" + thisPlayer.HowMuchResourcesIHave(1);
        coffee = "" + thisPlayer.HowMuchResourcesIHave(2);
        energyDrink = "" + thisPlayer.HowMuchResourcesIHave(3);
        scrapMetal = "" + thisPlayer.HowMuchResourcesIHave(4);
        gold = "" + thisPlayer.HowMuchResourcesIHave(5);
    }
    public void PlayerIsDeselected(){
        cheese = "";
        coffee = "";
        energyDrink = "";
        scrapMetal = "";
        gold = "";
        hudBotLeftResources.enabled = false;
        flagPlayerIsSelected = false;
    }
    public void PlanetIsSelected(SinglePlanet thisPlanet, int planetSpriteNum){
        PlanetIsDeselected(); // Cleanup
        hudBRPlanetImage.sprite = PlanetImages[planetSpriteNum];
        SelectedPlanet = thisPlanet;
        if (planetSpriteNum != 0 && planetSpriteNum != 31){
            // There is planet!!
            hudBRPlanetImageOuterLayer.enabled = true;
            SelectedPlanet.GETarrTypeOfResourceGain();
            arrTypeOfResourceGain = SelectedPlanet.GETarrTypeOfResourceGain();
            arrAmountOfResourceGain = SelectedPlanet.GETarrAmountOfResourceGain();
            arrTypeOfResourceCost = SelectedPlanet.GETarrTypeOfResourceCost();
            arrAmountOfResourceCost = SelectedPlanet.GETarrAmountOfResourceCost();
            SetPlanetInfoUI();
        } else {
            //There is no planet!!
            hudBRPlanetImageOuterLayer.enabled = false;
        }

    }
    public void PlanetIsDeselected(){
        // Used site to crop photos into circles
        // https://www.imgonline.com.ua/eng/crop-photo-into-various-shapes.php
        hudBRPlanetImage.sprite = PlanetImages[0];
        hudBRPlanetImageOuterLayer.enabled = false;

        A1Gain = "";
        A2Gain = "";
        A2Cost = "";

        hudBRAction1.sprite = ResourcesImages[0];
        hudBRAction2.sprite = ResourcesImages[0];
        hudBRCost2.sprite = ResourcesImages[0];
    }

    void SetPlanetInfoUI(){
        A1Gain = "" + arrAmountOfResourceGain[0];
        A2Gain = "" + arrAmountOfResourceGain[1];
        A2Cost = "/" + arrAmountOfResourceCost[1];

        hudBRAction1.sprite = ResourcesImages[arrTypeOfResourceGain[0]];
        hudBRAction2.sprite = ResourcesImages[arrTypeOfResourceGain[1]];
        hudBRCost2.sprite = ResourcesImages[arrTypeOfResourceCost[1]];
    }

    //Return References
    public ButtonShuttleTravel GetBtnMove(){
        return btnMove;
    }
    public GameObject GetBtnMoveGO(){
        return btnMoveGO;
    }
    public ButtonShuttleAction1 GetBtnAction1(){
        return btnAction1;
    }
    public GameObject GetBtnAction1GO(){
        return btnAction1GO;
    }
    public ButtonShuttleAction2 GetBtnAction2(){
        return btnAction2;
    }
    public GameObject GetBtnAction2GO(){
        return btnAction2GO;
    }
}
