using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour {

    public float panSpeed = 1f;
    public float panBoarderThickness = 25;
    float howIsZoomDoing = 0f;
    Vector3 pos;
    float panLimitX = 5;
    float panLimitY = 5;

    float maxZoom = 11f;
    float minZoom = 1.5f;

    void Start () {
        
    }
    
    // Update is called once per frame
    void Update () {

        howIsZoomDoing = Camera.main.orthographicSize;
        howIsZoomDoing = howIsZoomDoing/maxZoom;
        pos = transform.position;

        if(Input.mousePosition.y >= Screen.height - panBoarderThickness){
            pos.y += panSpeed * howIsZoomDoing * Time.deltaTime;
        }
        if(Input.mousePosition.y <= panBoarderThickness){
            pos.y -= panSpeed * howIsZoomDoing * Time.deltaTime;
        }
        if(Input.mousePosition.x >= Screen.width - panBoarderThickness){
            pos.x += panSpeed * howIsZoomDoing * Time.deltaTime;
        }
        if(Input.mousePosition.x <= panBoarderThickness){
            pos.x -= panSpeed * howIsZoomDoing * Time.deltaTime;
        }

        pos.x = Mathf.Clamp(pos.x, -panLimitX, panLimitX);
        //pos.x = Mathf.Clamp(pos.x, -panLimit.x, panLimit.x);
        pos.y = Mathf.Clamp(pos.y, -panLimitY, panLimitY);

        transform.position = pos;


        // -------------------Code for Zooming Out------------
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            //if (Camera.main.fieldOfView<=125){
            //    Camera.main.fieldOfView +=2;
            //}
            if (Camera.main.orthographicSize<=maxZoom){
                Camera.main.orthographicSize +=0.5f;
            }
        }
        // ---------------Code for Zooming In------------------------
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            //if (Camera.main.fieldOfView>2){
            //    Camera.main.fieldOfView -=2;
            //}
            if (Camera.main.orthographicSize>=minZoom){
                Camera.main.orthographicSize -=0.5f;
            }
        }
    }
}
