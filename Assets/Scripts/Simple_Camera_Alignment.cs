using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Simple_Camera_Alignment : MonoBehaviour
{
	[Header("Camera Alignment Components")]
	[SerializeField] Grid grid;
	[SerializeField] GameObject MainCamera;
	[SerializeField] GameObject player;
	[SerializeField] Tile [] Tiles;
	
	[Header("Room Indication Components")]
	[SerializeField] Tilemap map_tilemap;
	[SerializeField] Tilemap foreground_map_tilemap;
	[SerializeField] GameObject playerDot;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    	//translate the position of the player to the cell position within the large grid, which is indicating the rooms
    	Vector3Int cellPosition = grid.WorldToCell(player.transform.position);
    	foreground_map_tilemap.SetTile(cellPosition, null);
    	
    	//Ask if the map is inactive, since we only want to change the camera position, while we are playing
    	if(PlayerPrefs.GetInt("map_active")==0){
    		
    		if(map_tilemap.GetTile(cellPosition)==Tiles[0]){
	//        	print("Single Size Room");
	        	MainCamera.transform.position = grid.GetCellCenterWorld(cellPosition);
	        }
	        else if(map_tilemap.GetTile(cellPosition)==Tiles[1]){
	//        	print("Top left Corner");
	        	Vector3 Pos = player.transform.position;
	        	if(Pos.x<grid.GetCellCenterWorld(cellPosition).x){Pos.x=grid.GetCellCenterWorld(cellPosition).x;}
	        	if(Pos.y>grid.GetCellCenterWorld(cellPosition).y){Pos.y=grid.GetCellCenterWorld(cellPosition).y;}
	        	Pos.z=grid.GetCellCenterWorld(cellPosition).z;
	        	MainCamera.transform.position = Pos;
	        }
	        else if(map_tilemap.GetTile(cellPosition)==Tiles[2]){
	//        	print("Large Room top center");
	        	Vector3 Pos = player.transform.position;
	        	if(Pos.y>grid.GetCellCenterWorld(cellPosition).y){Pos.y=grid.GetCellCenterWorld(cellPosition).y;}
	        	Pos.z=grid.GetCellCenterWorld(cellPosition).z;
	        	MainCamera.transform.position = Pos;
	        }
	        else if(map_tilemap.GetTile(cellPosition)==Tiles[3]){
	//        	print("Top right Corner");
	        	Vector3 Pos = player.transform.position;
	        	if(Pos.x>grid.GetCellCenterWorld(cellPosition).x){Pos.x=grid.GetCellCenterWorld(cellPosition).x;}
	        	if(Pos.y>grid.GetCellCenterWorld(cellPosition).y){Pos.y=grid.GetCellCenterWorld(cellPosition).y;}
	        	Pos.z=grid.GetCellCenterWorld(cellPosition).z;
	        	MainCamera.transform.position = Pos;
	        }
	        else if(map_tilemap.GetTile(cellPosition)==Tiles[4]){
	//        	print("Large Room left");
	        	Vector3 Pos = player.transform.position;
	        	if(Pos.x<grid.GetCellCenterWorld(cellPosition).x){Pos.x=grid.GetCellCenterWorld(cellPosition).x;}
	        	Pos.z=grid.GetCellCenterWorld(cellPosition).z;
	        	MainCamera.transform.position = Pos;
	        }
	        else if(map_tilemap.GetTile(cellPosition)==Tiles[5]){
	//        	print("Large Room Center");
	        	Vector3 Pos = player.transform.position;
	        	Pos.z=grid.GetCellCenterWorld(cellPosition).z;
	        	MainCamera.transform.position = Pos;
	        }
	        else if(map_tilemap.GetTile(cellPosition)==Tiles[6]){
	//        	print("Large Room Right");
	        	Vector3 Pos = player.transform.position;
	        	if(Pos.x>grid.GetCellCenterWorld(cellPosition).x){Pos.x=grid.GetCellCenterWorld(cellPosition).x;}
	        	Pos.z=grid.GetCellCenterWorld(cellPosition).z;
	        	MainCamera.transform.position = Pos;
	        }
	        else if(map_tilemap.GetTile(cellPosition)==Tiles[7]){
	//        	print("Bottom left Corner");
	        	Vector3 Pos = player.transform.position;
	        	if(Pos.x<grid.GetCellCenterWorld(cellPosition).x){Pos.x=grid.GetCellCenterWorld(cellPosition).x;}
	        	if(Pos.y<grid.GetCellCenterWorld(cellPosition).y){Pos.y=grid.GetCellCenterWorld(cellPosition).y;}
	        	Pos.z=grid.GetCellCenterWorld(cellPosition).z;
	        	MainCamera.transform.position = Pos;
	        }
	        else if(map_tilemap.GetTile(cellPosition)==Tiles[8]){
	//        	print("Large Room Bottom Center");
	        	Vector3 Pos = player.transform.position;
	        	if(Pos.y<grid.GetCellCenterWorld(cellPosition).y){Pos.y=grid.GetCellCenterWorld(cellPosition).y;}
	        	Pos.z=grid.GetCellCenterWorld(cellPosition).z;
	        	MainCamera.transform.position = Pos;
	        }
	        else if(map_tilemap.GetTile(cellPosition)==Tiles[9]){
	//        	print("Bottom right Corner");
	        	Vector3 Pos = player.transform.position;
	        	if(Pos.x>grid.GetCellCenterWorld(cellPosition).x){Pos.x=grid.GetCellCenterWorld(cellPosition).x;}
	        	if(Pos.y<grid.GetCellCenterWorld(cellPosition).y){Pos.y=grid.GetCellCenterWorld(cellPosition).y;}
	        	Pos.z=grid.GetCellCenterWorld(cellPosition).z;
	        	MainCamera.transform.position = Pos;
	        }
	        else if(map_tilemap.GetTile(cellPosition)==Tiles[10]){
	//        	print("vertival corridor top");
	        	Vector3 Pos = player.transform.position;
	        	if(Pos.y>grid.GetCellCenterWorld(cellPosition).y){Pos.y=grid.GetCellCenterWorld(cellPosition).y;}
	        	Pos.x=grid.GetCellCenterWorld(cellPosition).x;
	        	Pos.z=grid.GetCellCenterWorld(cellPosition).z;
	        	MainCamera.transform.position = Pos;
	        }
	        else if(map_tilemap.GetTile(cellPosition)==Tiles[11]){
	//        	print("vertival corridor center");
	        	Vector3 Pos = player.transform.position;
	        	Pos.x=grid.GetCellCenterWorld(cellPosition).x;
	        	Pos.z=grid.GetCellCenterWorld(cellPosition).z;
	        	MainCamera.transform.position = Pos;
	        }
	        else if(map_tilemap.GetTile(cellPosition)==Tiles[12]){
	//        	print("vertival corridor bottom");
	        	Vector3 Pos = player.transform.position;
	        	if(Pos.y<grid.GetCellCenterWorld(cellPosition).y){Pos.y=grid.GetCellCenterWorld(cellPosition).y;}
	        	Pos.x=grid.GetCellCenterWorld(cellPosition).x;
	        	Pos.z=grid.GetCellCenterWorld(cellPosition).z;
	        	MainCamera.transform.position = Pos;
	        }
	        else if(map_tilemap.GetTile(cellPosition)==Tiles[13]){
	//        	print("horizontal corridor left");
	        	Vector3 Pos = player.transform.position;
	        	if(Pos.x<grid.GetCellCenterWorld(cellPosition).x){Pos.x=grid.GetCellCenterWorld(cellPosition).x;}
	        	Pos.y=grid.GetCellCenterWorld(cellPosition).y;
	        	Pos.z=grid.GetCellCenterWorld(cellPosition).z;
	        	MainCamera.transform.position = Pos;
	        }
	        else if(map_tilemap.GetTile(cellPosition)==Tiles[14]){
	//        	print("horizontal corridor center");
	        	Vector3 Pos = player.transform.position;
	        	Pos.y=grid.GetCellCenterWorld(cellPosition).y;
	        	Pos.z=grid.GetCellCenterWorld(cellPosition).z;
	        	MainCamera.transform.position = Pos;
	        }
	        else if(map_tilemap.GetTile(cellPosition)==Tiles[15]){
	//        	print("horizontal corridor right");
	        	Vector3 Pos = player.transform.position;
	        	if(Pos.x>grid.GetCellCenterWorld(cellPosition).x){Pos.x=grid.GetCellCenterWorld(cellPosition).x;}
	        	Pos.y=grid.GetCellCenterWorld(cellPosition).y;
	        	Pos.z=grid.GetCellCenterWorld(cellPosition).z;
	        	MainCamera.transform.position = Pos;
	        }
    		
    		foreground_map_tilemap.gameObject.SetActive(false);
    		
    	}
    	else{
    		
    		playerDot.transform.position = map_tilemap.GetCellCenterWorld(cellPosition);
    		foreground_map_tilemap.gameObject.SetActive(true);
    	
    	}
    }
}
