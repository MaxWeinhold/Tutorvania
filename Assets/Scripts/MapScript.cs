using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MapScript : MonoBehaviour
{
	bool map_active;
	bool map_pressed;
	[SerializeField] GameObject map;
	[SerializeField] GameObject MapCamera;
	[SerializeField] float speed;
	
	bool ButtonMap;
	private Vector2 movementInput=Vector2.zero;
	
	//OnMove checks if controlls for the movement are pressed
	public void OnMove(InputAction.CallbackContext context){
    	movementInput = context.ReadValue<Vector2>();
    }
	
	
	//OnMove checks if controlls for the map is pressed
	public void OnMap(InputAction.CallbackContext context){
    	ButtonMap = context.action.triggered;
    }

	void Start()
    {
		//make the map invisible
		map.SetActive(false);
    	map_active = false;
    }
	
    // Update is called once per frame
    void Update()
    {
    	//Activate the Map or deactivate the Map
    	//Second condition is needed so we dont switch between visible and invisible while pressed button
    	if(ButtonMap && !map_pressed){
    		if(map_active){
    			//make the map invisible
    			map_active=false;
    			map.SetActive(false);
    			PlayerPrefs.SetInt("map_active",0);
    		}
    		else{
    			//make the map visible
    			map_active=true;
    			map.SetActive(true);
    			PlayerPrefs.SetInt("map_active",1);
    		}
    		map_pressed = true;
    	}
    	//condition set we can press again
    	if(!ButtonMap){map_pressed = false;}
    	
    	//Create Map_Camera Movement
    	if(map_active){
	    	
    		//Get MapCameraPosition
	    	Vector3 pos1 = MapCamera.transform.position;
	    	//Alterate MapCameraPosition
        	pos1.y+=speed*movementInput.y;
        	pos1.x+=speed*movementInput.x;
        	print(movementInput.y);
        	//Save new Position and create Camera Movement on the Map
        	MapCamera.transform.position=pos1;
    	}
    }
}
