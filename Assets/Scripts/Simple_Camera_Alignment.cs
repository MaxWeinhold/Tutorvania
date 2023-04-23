using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Simple_Camera_Alignment : MonoBehaviour
{
	[SerializeField] Grid grid;
	[SerializeField] GameObject MainCamera;
	[SerializeField] GameObject player;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    	//Ask if the map is inactive, since we only want to change the camera position, while we are playing
    	if(PlayerPrefs.GetInt("map_active")==0){
    		
    		//translate the position of the player to the cell position within the large grid, which is indicating the rooms
    		Vector3Int cellPosition = grid.WorldToCell(player.transform.position);
    		
    		//Adjust the camera position to the cell position and select the center of the cell
    		MainCamera.transform.position = grid.GetCellCenterWorld(cellPosition);
    		
    	}
    }
}
