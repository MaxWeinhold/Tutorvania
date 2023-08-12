using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
	
	int coins = 0;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnTriggerEnter2D(Collider2D other){
	
		if(other.tag=="Player"){
    		
    		coins = PlayerPrefs.GetInt("Coins");
    		coins++;
    		PlayerPrefs.SetInt("Coins",coins);
    		
    		GetComponent<Renderer>().enabled = false;
    		GetComponent<BoxCollider2D>().enabled = false;
    		
    		print(coins);
		}
	}
}
