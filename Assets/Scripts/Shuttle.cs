using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuttle : MonoBehaviour {

    // About Player
    int PlayerID = 0;
    int ShipID = 0;
    SinglePlayer ThisPlayer;
    SinglePlayer[] ListOfPlayers;
    Shuttle[] SelectedShuttles;
    
    //MOVEMENT
    Vector3 targetPosition;
    float ZpositionOfShuttle = -3f;
    float speed = 5.0f;
    float step = 0;
    Vector3 sunPosition = new Vector3(-0.4f, -0.1f, 0);

    //LOCATION 0 = orbitLocation, 1 = planetLocation
    int[] ShuttleLocation = new int[2];
    Vector3 shuttlePosition;

    // SELECTING SHUTTLE:
    bool flagThisOneIsSelected = false;
    public Sprite[] ShuttleImages;
    int spriteShuttleIsSelected = 1;
    int spriteShuttleIsMoving = 2;
    //int spriteShuttleIsDiscovering = 3;
    //function is:      this.sprite = ShuttleImages[number]; // 0 is default
    public ButtonShuttleTravel btnMove;
    public GameObject btnMoveGO;
    public ButtonShuttleAction1 btnAction1;
    public GameObject btnAction1GO;
    public ButtonShuttleAction2 btnAction2;
    public GameObject btnAction2GO;
    float ZpositionOfButtons = -3.5f;

    // ACTIONS
    bool flagIAmMoving = false;
    SinglePlanet singlePlanetPlaceholder;

    //Scripts
    public Planets PlanetsInfo;
    public HUDControler HUDMaster;

    void Start () {

        if (PlanetsInfo == null) PlanetsInfo = GameObject.FindObjectOfType<Planets>();
        if (HUDMaster == null) HUDMaster = GameObject.FindObjectOfType<HUDControler>();

        btnMove = HUDMaster.GetBtnMove();
        btnMoveGO = HUDMaster.GetBtnMoveGO();
        btnAction1 = HUDMaster.GetBtnAction1();
        btnAction1GO = HUDMaster.GetBtnAction1GO();
        btnAction2 = HUDMaster.GetBtnAction2();
        btnAction2GO = HUDMaster.GetBtnAction2GO();
    }

    public void SetupThisShuttle(int homeOrbit, int homePlanet, SinglePlayer plID){
        //Function goes faster than Start...
        if (PlanetsInfo == null) PlanetsInfo = GameObject.FindObjectOfType<Planets>();
        if (HUDMaster == null) HUDMaster = GameObject.FindObjectOfType<HUDControler>();

        ShuttleLocation[0] = homeOrbit;
        ShuttleLocation[1] = homePlanet;
        //PlayerID = plID;
        ThisPlayer = plID;
        PlayerID = ThisPlayer.GetPlayerID();

        /*ListOfPlayers = GameObject.FindObjectsOfType<SinglePlayer>();
        for(var i = 0 ; i < ListOfPlayers.Length ; i ++){
            if(ListOfPlayers[i].GetPlayerID() == PlayerID){
                ThisPlayer = ListOfPlayers[i];
            }
        }*/

        Debug.Log("ThisPlayer set: " + ThisPlayer.name);

        this.PortShuttleOnThisPosition(PlanetsInfo.GetPositionOfPlanet(ShuttleLocation[0], ShuttleLocation[1]));

        Debug.Log("New location of Shuttle: " + shuttlePosition);

        ShipID = ThisPlayer.GetShipsNum();
    }
    
    // Update is called once per frame
    void Update () {
        if(flagThisOneIsSelected){
            //Debug.Log("Shuttle is selected");
            HUDMaster.PlayerIsSelected(ThisPlayer);
            if(flagIAmMoving){
                this.GetComponent<SpriteRenderer>().sprite = ShuttleImages[spriteShuttleIsMoving];
            } else {
                this.GetComponent<SpriteRenderer>().sprite = ShuttleImages[spriteShuttleIsSelected];
            }
        } else {
            this.GetComponent<SpriteRenderer>().sprite = ShuttleImages[0];
        }

        // Movement
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
            shuttlePosition = this.transform.position;
        }
    }
    //
    // SELECTING THE SHIP *************************************************************************************************
    //
    void OnMouseDown(){
        //Selecting Ship
        this.ChangeflagThisOneIsSelectedAndChangeButtons();
    }
    public void ChangeflagThisOneIsSelectedAndChangeButtons()
    {
        //Change flag of selection of ship
        if (flagThisOneIsSelected) {
            flagThisOneIsSelected = false;
            flagIAmMoving = false;
            TurnOffButtonsOfShuttle();
            HUDMaster.PlayerIsDeselected();
        } else {
            // First unselect all other Shuttles
            SelectedShuttles = GameObject.FindObjectsOfType<Shuttle>();
            for(var i = 0 ; i < SelectedShuttles.Length ; i ++)
            {
                SelectedShuttles[i].SetflagThisOneIsSelectedtoFALSE();
            }
            flagThisOneIsSelected = true;
            TurnOnButtonsOfShuttle();
        }
    }
    public void SetflagThisOneIsSelectedtoFALSE()
    {
        //Change flag of selection of ship to false
        flagThisOneIsSelected = false;
        flagIAmMoving = false;
    }
    public void SetflagThisOneIsSelectedtoTRUE()
    {
        //Change flag of selection of ship to true
        flagThisOneIsSelected = true;
    }
    public bool IsThisShuttleSelected(){
        return flagThisOneIsSelected;
    }
    public void TurnOffButtonsOfShuttle(){
        btnMoveGO.SetActive(false);
        btnAction1GO.SetActive(false);
        btnAction2GO.SetActive(false);
    }
    public void TurnOnButtonsOfShuttle(){
        btnMoveGO.SetActive(true);
        btnMove.transform.position = new Vector3(targetPosition.x, (targetPosition.y - 0.6f), ZpositionOfButtons);
        btnMove.IWantToMove(this);
        btnAction1GO.SetActive(true);
        btnAction1.transform.position = new Vector3((targetPosition.x + 0.4f), (targetPosition.y - 0.4f), ZpositionOfButtons);
        btnAction1.IWantToUseActions(this);
        btnAction2GO.SetActive(true);
        btnAction2.transform.position = new Vector3((targetPosition.x + 0.7f), (targetPosition.y - 0.1f), ZpositionOfButtons);
        btnAction2.IWantToUseActions(this);
    }

    //
    // Actions *********************************************************************************************************
    //

    public void SetMeToMovingAction(){
        flagIAmMoving = true;
        this.GetComponent<SpriteRenderer>().sprite = ShuttleImages[spriteShuttleIsMoving];
    }
    public bool IsThisShuttleMoving(){
        return flagIAmMoving;
    }
    public void ThisShuttlesActionIsDone(){
        ThisPlayer.ActionDone();
    }

    //public void IAmDiscoveringPlanet(){
    //    singlePlanetPlaceholder = PlanetsInfo.GetPlanetOnThisLocation(ShuttleLocation[0], ShuttleLocation[1]);
    //    singlePlanetPlaceholder.DiscoverOnThisPlanet(this);
    //    ThisShuttlesActionIsDone();
    //}

    //
    // 
    //

    //
    // CHANGE LOCATION OF SHIP ********************************************************************************************
    //
    public void SetPositionOfShuttle(Vector3 newPosition){
        targetPosition = new Vector3((newPosition.x + 0.3f + ((float)ShipID*2/10)), (newPosition.y - 0.3f - ((float)ShipID*2/10)), ZpositionOfShuttle);
    }
    public void PortShuttleOnThisPosition(Vector3 newPosition){
        targetPosition = new Vector3((newPosition.x + 0.3f + ((float)ShipID*2/10)), (newPosition.y - 0.3f - ((float)ShipID*2/10)), ZpositionOfShuttle);
        transform.position = new Vector3((newPosition.x + 0.3f + ((float)ShipID*2/10)), (newPosition.y - 0.3f - ((float)ShipID*2/10)), ZpositionOfShuttle);
        shuttlePosition = this.transform.position;
    }
    public Vector3 GetPositionOfShuttle(){   
        return shuttlePosition;
    }
    public int[] GetLocationOfShuttle(){
        int[] tmpArrayOfLocation = new int[2]; // SO IT DOES NOT SEND POINTER TO LOCATION
        tmpArrayOfLocation[0] = ShuttleLocation[0];
        tmpArrayOfLocation[1] = ShuttleLocation[1];
        return tmpArrayOfLocation;
    }
    public void SetLocationOfShuttle(int[] newLocation){
        Debug.Log("LOCATION WAS JUST SET: " + newLocation[0] + newLocation[1]);
        ShuttleLocation[0] = newLocation[0];
        ShuttleLocation[1] = newLocation[1];
    }

    public SinglePlayer GetMyPlayer(){
        return ThisPlayer;
    }
    public void SetMyPlayer(SinglePlayer thisGuy){
        ThisPlayer = thisGuy;
    }
}
