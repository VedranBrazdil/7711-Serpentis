using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonShuttleTravel : MonoBehaviour {

    Shuttle moveThisShuttle;
    
    // Use this for initialization
    void Start () {
        moveThisShuttle = GameObject.FindObjectOfType<Shuttle>();
    }
    
    // Update is called once per frame
    void Update () {
        
    }

    public void IWantToMove(Shuttle thisShuttle){
        moveThisShuttle = thisShuttle;
    }

    void OnMouseDown(){
        moveThisShuttle.SetMeToMovingAction();
    }
}
