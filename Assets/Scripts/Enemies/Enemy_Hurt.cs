using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Hurt : MonoBehaviour
{
	public bool hurted = false;
	
    void OnTriggerEnter2D(Collider2D other){
    	if(other.tag=="Player_Attack"){
    		hurted=true;
    	}
    }
}
