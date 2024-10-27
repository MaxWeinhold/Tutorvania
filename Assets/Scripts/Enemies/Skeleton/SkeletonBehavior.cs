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
	
	[Header("Health")]
	[SerializeField] [Range(1, 5)] int lifes=1;
	int actual_lifes;
	bool hurted = false;
	[SerializeField] [Range(0, 5)] float hurted_delay=1;
	Enemy_Hurt EH;
	float targetTime;
	SpriteRenderer sr;
	bool hurt_start = false;
	bool dead = false;
	[SerializeField] [Range(5, 25)] float hurt_thrust;
	float short_term_thrust;
	
	
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        sr = Enemy.GetComponent<SpriteRenderer> ();
        EH = Enemy.GetComponent<Enemy_Hurt> ();
        p = player.GetComponent<PlayerScript> ();
        rb = Enemy.GetComponent<Rigidbody2D> ();
        actual_lifes = lifes;
    }

    
    void Hurt(){
    	
   		if(EH.hurted){
	    	if(!hurt_start){
    			GetComponent<AudioSource>().Play();
	    		hurt_start=true;
	    		actual_lifes--;
	    		if(actual_lifes==0){
	    			dead=true;
        			Enemy.GetComponent<Animator>().SetBool("Dead",true);
	    		}
	    		
	    		if(p.facingRight){short_term_thrust=hurt_thrust;}
	    		else{short_term_thrust=-hurt_thrust;}
	    		
	    	}
	    	sr.color = Color.red;
	    	targetTime += Time.deltaTime;
	    	if(targetTime>hurted_delay){EH.hurted=false;}
	    }
	    else{
	    	targetTime=0;
	    	sr.color = Color.white;
	    	hurt_start=false;
	    }
    	
    	if(dead){short_term_thrust=0;}//else{Enemy.GetComponent<Animator>().SetBool("Dead",false);}// Need this when enemies will respawn later in game development
    }
    
    
    // Update is called once per frame
    void FixedUpdate()
    {

    	if(!dead){
    		Hurt();
    		
    		if(player.transform.position.x>Detection_Edge1.transform.position.x && player.transform.position.x < Detection_Edge2.transform.position.x){
		    	player_inside = true;
		    	if(player.transform.position.x > Enemy.transform.position.x){
		    		rb.velocity = new Vector2 ( 1*speed + short_term_thrust*4, rb.velocity.y );
		    		right=true;
		    	}else{
		    		rb.velocity = new Vector2 ( -1*speed + short_term_thrust*4, rb.velocity.y );
		    		right=false;
		    	}
		    }else{
		    	if(Enemy.transform.position.x<Edge1.transform.position.x){right=true;}
		    	else if(Enemy.transform.position.x>Edge2.transform.position.x){right=false;}
		    	
		    	if(right){rb.velocity = new Vector2 ( 1*speed + short_term_thrust*4, rb.velocity.y );}
		    	else{rb.velocity = new Vector2 ( -1*speed + short_term_thrust*4, rb.velocity.y );}
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
	    	
	    	if(short_term_thrust!=0){short_term_thrust*=0.8f;}
	    	
    	}else{
    		rb.velocity = new Vector2 (0, rb.velocity.y );
    		sr.color = Color.white;
    	}
    }
}
