using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldCreatorScript : MonoBehaviour
{
    
    //ALL VARIABLES HERE
    int roundNum = 0;
    //Background Images
    //public Image ImgStatusBar;

    //Player Input
    int playerNum = 1;
    int playersDone = 0;

    //Planets
    public Planets PlanetsList;


    void Start()
    {
        //Setup mechanic:
        //for each player::::

        // SETUP Visuals
        //ImgStatusBar.enabled = true;

        //Create world    

    }

    // Update is called once per frame
    void Update()
    {
        if (playersDone == playerNum){
            playersDone = 0;
            roundNum ++;
            Debug.Log("Round DONE: " + roundNum);
            PlanetsList.RoundDone();
            PlanetsList.FactoryCheckup();
        }
    }
    public void PlayerDone(){
        playersDone ++;
    }

    // Planet movement
    void MovePlanets() {

    }

}
