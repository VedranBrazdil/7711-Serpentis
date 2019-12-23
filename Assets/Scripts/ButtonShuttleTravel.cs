using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonShuttleTravel : MonoBehaviour {

    Shuttle moveThisShuttle;

    //mouseover text
    public Text mouseOverTxt;
    Vector3 mousePos;
    
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
        this.TextRemoval();
    }

    //mouseover text
    void OnMouseOver()
    {
        mouseOverTxt.text = "Travel                     ";
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
