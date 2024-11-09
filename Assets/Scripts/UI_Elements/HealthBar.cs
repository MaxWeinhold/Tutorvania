using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
	//Variables for accesing the variable actual_lifes within the Player Scripts
	GameObject Player;
	PlayerScript PM;
	
	//The Image within our Health UI Element
	Image m_Image;
	
	//6 Images indicating 6 different amounts of lifes from 0 Hearts to 6 Hearts
    [SerializeField] Sprite m_0_5;
    [SerializeField] Sprite m_1_5;
    [SerializeField] Sprite m_2_5;
    [SerializeField] Sprite m_3_5;
    [SerializeField] Sprite m_4_5;
	[SerializeField] Sprite m_5_5;
	
    // Start is called before the first frame update
    void Start()
    {
    	//Access the Player Script by finding the Player GameObject
        Player = GameObject.Find("Player");
    	PM = Player.GetComponent<PlayerScript>();
    	
    	//Initialize the UI Image
    	m_Image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
    	// Set the correct Sprite for the correct amount of Lifes/Hearts
        if(PM.actual_lifes==0){m_Image.sprite = m_0_5;}
        if(PM.actual_lifes==1){m_Image.sprite = m_1_5;}
        if(PM.actual_lifes==2){m_Image.sprite = m_2_5;}
        if(PM.actual_lifes==3){m_Image.sprite = m_3_5;}
        if(PM.actual_lifes==4){m_Image.sprite = m_4_5;}
        if(PM.actual_lifes==5){m_Image.sprite = m_5_5;}
    }
}
