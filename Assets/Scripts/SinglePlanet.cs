using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinglePlanet : MonoBehaviour {

        // Objects
    //Ship
    Shuttle[] SelectedShuttles;
    Shuttle ThisShuttle;
    Shuttle CreatedShuttle;
    public Shuttle prefabShuttle;
    Vector3 positionOfThisPlanet;
    SinglePlayer useThisPlayer;
    SinglePlayer ThisPlayerIsOwner;
    //Scripts
    public Planets PlanetsInfo;
    public HUDControler HUDMaster;
    //System.Random randomNumber = new System.Random();

    // Planet desc
    SinglePlanet[] SelectedSinglePlanets;
    int[] PlanetID = new int[2];
    bool flagThisIsHomePlanet = false;
    int HomePlanetForPlayer = -1;
    bool flagThisPlanetIsSelected = false;
    bool flagFactoryBuilt = false;


    // Buttons
    public ButtonPlanetTravel btnMove;
    public GameObject btnMoveGO;
    public ButtonShuttleDiscover btnDiscover;
    public GameObject btnDiscoverGO;
    public ButtonPlanetBuildFactory btnBuildFactory;
    public GameObject btnBuildFactoryGO;
    public ButtonPlanetBuildShip btnBuildShip;
    public GameObject btnBuildShipGO;
    //public ButtonShuttleDiscover btnDiscover;
    //public GameObject btnDiscoverGO;
    float ZpositionOfButtons = -3.5f;

    // Planet Discovering
    bool flagPlanetDiscoveredHere = false;
    bool discoverDone = false;
    public Sprite[] PlanetImages;
    int planetSpriteNum;
    //Resources
    int[] arrTypeOfResourceGain = new int[3];
    int[] arrAmountOfResourceGain = new int[3];
    int[] arrTypeOfResourceCost = new int[3];
    int[] arrAmountOfResourceCost = new int[3];



    //MOVEMENT
    Vector3 targetPosition;
    float speed = 5.0f;
    float step = 0;
    Vector3 sunPosition = new Vector3(-0.4f, -0.1f, 0);


    // Rotation
    Vector3 rotationEuler;

    // Use this for initialization
    void Start () {
        targetPosition = this.transform.position;
        //positionOfThisPlanet = transform.position;
        //HUDMaster = GameObject.FindObjectOfType<HUDControler>();
    }
    
    // Update is called once per frame
    void Update () {
        //Movement
        if(Vector3.Distance(transform.position, targetPosition) >= 0.1f){
        //if(transform.position != targetPosition){
            step = speed * Time.deltaTime; // calculate distance to move
            //transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
            Vector3 riseRelCenter = transform.position - sunPosition;
            Vector3 setRelCenter = targetPosition - sunPosition;

            transform.position = Vector3.Slerp(riseRelCenter, setRelCenter, step);
            transform.position += sunPosition;
        } else {
            transform.position = targetPosition;
            positionOfThisPlanet = this.transform.position;
        }

        rotationEuler+= Vector3.forward*30*Time.deltaTime; //increment 30 degrees every second
        this.transform.rotation = Quaternion.Euler(rotationEuler);
    }

    public void SetMyID (int newO, int newP){
        PlanetID[0] = newO;
        PlanetID[1] = newP;
        //this.tag = "Planet" + PlanetID[0] + PlanetID[1];
    }
    public int[] GetMyID (){
        return PlanetID;
    }

    public void SetPositionOfThisPlanet(Vector3 newPosition){
        targetPosition = newPosition;
         //its pointer to its position, not only value
    }
    public void TeleportThisPlanetToThisLocation(Vector3 newTPPosition){
        targetPosition = newTPPosition;
        transform.position = newTPPosition;
    }

    // HOME PLANET FUNCTIONS
    public void SetThisPlanetToBeHomePlanetFor(int playerNum){
        flagThisIsHomePlanet = true;
        HomePlanetForPlayer = playerNum;
        CreatePlanetHere();
        discoverDone = true;
        Debug.Log("SetThisPlanetToBeHomePlanetFor: " + playerNum + " Planet:" + PlanetID[0] + " " + PlanetID[1]);
    }
    public bool IsThisHomePlanet(){
        return flagThisIsHomePlanet;
    }
    public int ForWhatPlayerIsThisHomePlanet(){
        if (HomePlanetForPlayer > -1){
            return HomePlanetForPlayer;
        }
        return -1;
    }

    //DISCOVER A PLANET ********************************************************************************************************
    public void DiscoverOnThisPlanet(Shuttle thisOne){
        // https://pinetools.com/image-gradient-generator for randomizeing PLANETS
        if(!flagPlanetDiscoveredHere){
            ThisShuttle = thisOne;
            //int randNum = randomNumber.Next(101);
            int randNum = (int)Random.Range(0, 101);
            int bonus = PlanetsInfo.DiscoveringBonusIs();
            if((randNum+bonus) > 60){
                PlanetsInfo.DiscoverHappened(true);
                CreatePlanetHere();
            } else{
                PlanetsInfo.DiscoverHappened(false);
                this.GetComponent<SpriteRenderer>().sprite = PlanetImages[31];
                planetSpriteNum = 31;
            }
            discoverDone = true;
            ThisShuttle.SetflagThisOneIsSelectedtoFALSE();
            ThisShuttle.TurnOffButtonsOfShuttle();
            //Debug.Log("Random: " + randNum + " and bonus: " + bonus + "!");
        }
        HUDMaster.PlanetIsSelected(this, planetSpriteNum);
    }
    void CreatePlanetHere(){
        flagPlanetDiscoveredHere = true;
        GenerateThisPlanet();
        SetMyResources();
    }
    public bool isPlanetHere(){
        return flagPlanetDiscoveredHere;
    }
    void GenerateThisPlanet(){
        int randNum = (int)Random.Range(1, 31); // exclude 0 (default) + it goes to less than maxNum (so put +1)
        this.GetComponent<SpriteRenderer>().sprite = PlanetImages[randNum];
        planetSpriteNum = randNum;
    }
    public bool DiscoverFailedOrFalse(){
        if (planetSpriteNum == 31 || planetSpriteNum == 0){
            return true;
        }
        return false;
    }

    // BUILD FACTORY ************************************************************************************************************

    public bool isFactoryBuilt(){
        return flagFactoryBuilt;
    }

    public void BuildFactoryHere(SinglePlayer thisPlayer){
        flagFactoryBuilt = true;
        ThisPlayerIsOwner = thisPlayer;
        thisPlayer.IBuiltFactory();
    }

    public void ActivateFactory(){
        if(flagFactoryBuilt){
            this.ActionOne(ThisPlayerIsOwner);
        }
    }

    public SinglePlayer GetMyOwner(){
        if(flagFactoryBuilt){
            return ThisPlayerIsOwner;
        }
        return null;
    }


    // BUILD SHIP ************************************************************************************************************

    public void BuildShipHere(){
        //CreatedShuttle = Instantiate(prefabShuttle, new Vector3(i * 2.0F, 0, 0), Quaternion.identity);
        ThisPlayerIsOwner.PayForShip();
        ThisPlayerIsOwner.ActionDone();
        ThisPlayerIsOwner.IBuiltShip();

        CreatedShuttle = Instantiate<Shuttle>(prefabShuttle);
        CreatedShuttle.SetupThisShuttle(PlanetID[0], PlanetID[1], ThisPlayerIsOwner);

        //CreatedShuttle.SetMyPlayer(ThisPlayerIsOwner); 
        //CreatedShuttle.SetPositionOfShuttle(positionOfThisPlanet);
        //CreatedShuttle.SetLocationOfShuttle(PlanetID);
    }

    // SET RESOURCES OF PLANET ****************************************************************************************************

    void SetMyResources(){
        // FirstAction
        int randNum = (int)Random.Range(1, 6);
        arrTypeOfResourceGain[0] = randNum;
        if(randNum == 3 || randNum == 4 || randNum == 5){
            // Energy, Scrap, Gold
            arrAmountOfResourceGain[0] = 1;
        } else{
            // Coffe, Cheese
            randNum = (int)Random.Range(1, 2);
            arrAmountOfResourceGain[0] = randNum;
        }

        // Second Action
        randNum = (int)Random.Range(1, 6);
        arrTypeOfResourceGain[1] = randNum;
        if(randNum == 3 || randNum == 4 || randNum == 5){
            // Energy, Scrap, Gold
            randNum = (int)Random.Range(2, 3);
            arrAmountOfResourceGain[1] = randNum;
        } else{
            // Coffe, Cheese
            randNum = (int)Random.Range(2, 4);
            arrAmountOfResourceGain[1] = randNum;
        }

        // Cost
        arrTypeOfResourceCost[0] = 0;
        do {
            randNum = (int)Random.Range(1, 6);
        } while (randNum == arrTypeOfResourceGain[1]);
        arrTypeOfResourceCost[1] = randNum;
        if(randNum == 3 || randNum == 4 || randNum == 5){
            // Energy, Scrap, Gold
            arrAmountOfResourceCost[1] = 1;
        } else{
            // Coffe, Cheese
            arrAmountOfResourceCost[1] = 2;
        }
    }

    public int[] GETarrTypeOfResourceGain(){
        return arrTypeOfResourceGain;
    }
    public int[] GETarrAmountOfResourceGain(){
        return arrAmountOfResourceGain;
    }
    public int[] GETarrTypeOfResourceCost(){
        return arrTypeOfResourceCost;
    }
    public int[] GETarrAmountOfResourceCost(){
        return arrAmountOfResourceCost;
    }

    //
    // RESOURCES ACTION *********************************************************************************************************
    //
    
    public bool ActionOne(Shuttle thisShuttle){
        useThisPlayer = thisShuttle.GetMyPlayer();
        //if (useThisPlayer.CanIPayCheck()) no need for check, first action is free
        useThisPlayer.GainResources(arrTypeOfResourceGain[0], arrAmountOfResourceGain[0]);
        return true;
    }
    public bool ActionOne(SinglePlayer thisPlayer){
        //useThisPlayer = thisShuttle.GetMyPlayer();
        //if (useThisPlayer.CanIPayCheck()) no need for check, first action is free
        thisPlayer.GainResources(arrTypeOfResourceGain[0], arrAmountOfResourceGain[0]);
        return true;
    }

    public bool ActionTwo(Shuttle thisShuttle){
        useThisPlayer = thisShuttle.GetMyPlayer();
        useThisPlayer.PayResources(arrTypeOfResourceCost[1], arrAmountOfResourceCost[1]);
        useThisPlayer.GainResources(arrTypeOfResourceGain[1], arrAmountOfResourceGain[1]);
        return true;
    }

    //
    // SELECTING THE PLANET *************************************************************************************************
    //
    void OnMouseDown(){
        //Selecting Planet
        ChangeflagThisPlanetIsSelectedAndChangeButtons();
    }

    public void MoveShipHere(Shuttle mvThisShuttle){
        //Teleport Ship to selected Planet
        //SelectedShuttles = GameObject.FindObjectsOfType<Shuttle>();
        //for(var i = 0 ; i < SelectedShuttles.Length ; i ++)
        //{
        //    if(SelectedShuttles[i].IsThisShuttleSelected() && SelectedShuttles[i].IsThisShuttleMoving()){
        //        if (PlanetsInfo.CanITravelFromTo(SelectedShuttles[i].GetLocationOfShuttle(), PlanetID)){
                    // For now it will use its own location(position)
                    // maybe it should have orbit/planet getter from Planets
                    mvThisShuttle.SetPositionOfShuttle(positionOfThisPlanet);
                    mvThisShuttle.SetLocationOfShuttle(PlanetID);
                    mvThisShuttle.ThisShuttlesActionIsDone();
        //        }
        //    }
             // Reset the selection
            mvThisShuttle.SetflagThisOneIsSelectedtoFALSE();
            // TurnOffButtons anyway
            mvThisShuttle.TurnOffButtonsOfShuttle();
        //}
        this.TurnOffButtonsOfPlanet();
    }

    public void ChangeflagThisPlanetIsSelectedAndChangeButtons()
    {
        //Change flag of selection of ship
        if (flagThisPlanetIsSelected) {
            flagThisPlanetIsSelected = false;
            TurnOffButtonsOfPlanet();
            HUDMaster.PlanetIsDeselected();
        } else {
            // First unselect all other Planets
            SelectedSinglePlanets = GameObject.FindObjectsOfType<SinglePlanet>();
            for(var i = 0 ; i < SelectedSinglePlanets.Length ; i ++){
                SelectedSinglePlanets[i].SetflagThisPlanetIsSelectedtoFALSE();
            }
            flagThisPlanetIsSelected = true;
            TurnOnButtonsOfPlanet();
            HUDMaster.PlanetIsSelected(this, planetSpriteNum);
        }
    }
    public void SetflagThisPlanetIsSelectedtoFALSE()
    {
        //Change flag of selection of planet to false
        flagThisPlanetIsSelected = false;
    }
    public void SetflagThisPlanetIsSelectedtoTRUE()
    {
        //Change flag of selection of planet to true
        flagThisPlanetIsSelected = true;
    }
    public bool IsThisPlanetSelected(){
        return flagThisPlanetIsSelected;
    }
    public void TurnOffButtonsOfPlanet(){
        btnMoveGO.SetActive(false);
        btnDiscoverGO.SetActive(false);
        btnBuildFactoryGO.SetActive(false);
        btnBuildShipGO.SetActive(false);
    }
    public void TurnOnButtonsOfPlanet(){
        btnMoveGO.SetActive(true);
        btnMove.transform.position = new Vector3((targetPosition.x - 0.6f), (targetPosition.y + 0.6f), ZpositionOfButtons);
        btnMove.SelectedPlanetIs(this);
        btnDiscoverGO.SetActive(true);
        btnDiscover.transform.position = new Vector3((targetPosition.x - 0.2f), (targetPosition.y + 0.8f), ZpositionOfButtons);
        btnDiscover.SelectedPlanetIs(this);
        btnBuildFactoryGO.SetActive(true);
        btnBuildFactory.transform.position = new Vector3((targetPosition.x + 0.2f), (targetPosition.y + 0.8f), ZpositionOfButtons);
        btnBuildFactory.SelectedPlanetIs(this);
        btnBuildShipGO.SetActive(true);
        btnBuildShip.transform.position = new Vector3((targetPosition.x + 0.6f), (targetPosition.y + 0.6f), ZpositionOfButtons);
        btnBuildShip.SelectedPlanetIs(this);
        
    }
    public bool IsDiscoverDoneTrue(){
        return discoverDone;
    }

}
