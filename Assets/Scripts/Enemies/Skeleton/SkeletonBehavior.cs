using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBehavior : MonoBehaviour
{
	[Header("Movement Borders")]
	[SerializeField] GameObject Edge1;
	[SerializeField] GameObject Edge2;
	
	[SerializeField] GameObject Detection_Edge1;
	[SerializeField] GameObject Detection_Edge2;
	
	[Header("Movement")]
	[SerializeField] GameObject Enemy;
	[SerializeField] [Range(0.5f, 5)] float speed;
	GameObject player;
	PlayerScript p;
	Rigidbody2D rb;
	[SerializeField] bool right = false;
	bool player_inside;
	
	bool right_check = false;
	bool left_check = false;
	
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        p = player.GetComponent<PlayerScript> ();
        rb = Enemy.GetComponent<Rigidbody2D> ();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    	if(player.transform.position.x>Detection_Edge1.transform.position.x && player.transform.position.x < Detection_Edge2.transform.position.x){
	    	player_inside = true;
	    	if(player.transform.position.x > Enemy.transform.position.x){
	    		rb.velocity = new Vector2 ( 1*speed, rb.velocity.y );
	    		right=true;
	    	}else{
	    		rb.velocity = new Vector2 ( -1*speed, rb.velocity.y );
	    		right=false;
	    	}
	    }else{
	    	if(Enemy.transform.position.x<Edge1.transform.position.x){right=true;}
	    	else if(Enemy.transform.position.x>Edge2.transform.position.x){right=false;}
	    	
	    	if(right){rb.velocity = new Vector2 ( 1*speed, rb.velocity.y );}
	    	else{rb.velocity = new Vector2 ( -1*speed, rb.velocity.y );}
	    }
	    	
    	if (!right && !right_check){
	        Enemy.transform.Rotate(0, 180, 0);
	        right_check=true;
	        left_check=false;
	    }
	    else if (right && right_check){
	        Enemy.transform.Rotate(0, 180, 0);
	        right_check=false;
	        left_check=true;
	    }
    }
}
