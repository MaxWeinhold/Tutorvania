using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsUIScript : MonoBehaviour
{
	int Coins;
	public Text CoinsText;
	
    // Update is called once per frame
    void Update()
    {
        Coins = PlayerPrefs.GetInt("Coins");
    	CoinsText.text = Coins.ToString();
    }
}
