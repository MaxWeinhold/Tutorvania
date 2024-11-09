using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{	
	[Header("Movement")]
	[Tooltip("Speed of horizontaly Movement")]
	[Range(5, 15)]
	[SerializeField] float speed = 1;
	public bool facingRight = true;
	private Vector2 movementInput=Vector2.zero;
	
	[Header("Jumping")]
	[Tooltip("Jumping height")]
	[Range(0, 100)]
	[SerializeField] float thrust = 300;
	[SerializeField] bool grounded = true;
	bool jumped;
	bool attackGP;
	
	[Header("Attacking")]
	[SerializeField] GameObject Attack1_HB;
	bool attack_active;
	bool attack_pressed = false;
	
	[Header("Health")]
	[Range(1, 15)] public int lifes=1;
	[Range(1, 15)] public int actual_lifes=1;
	bool hurted = false;
	[SerializeField] [Range(0, 5)] float hurted_delay=1;
	Enemy_Hurt EH;
	float targetTime;
	bool hurt_start = false;
	public bool dead = false;
	[SerializeField] [Range(10, 50)] float hurt_thrust;
	float short_term_thrust;

	Rigidbody2D rb;
	Animator playerAnimator;
	SpriteRenderer SR;
	
	//OnMove checks if controlls for the movement are pressed
	public void OnMove(InputAction.CallbackContext context){
    	movementInput = context.ReadValue<Vector2>();
    }
    
	//OnJump checks if controlls for the jumping are pressed
    public void OnJump(InputAction.CallbackContext context){
    	jumped = context.action.triggered;
    }
	
    public void OnAttack(InputAction.CallbackContext context){
    	attackGP = context.action.triggered;
    }
	
	//Jump is the class that includes jumping mechanics
	void Jump()
    {
		//If jump is pressed and the player is on the ground, execute jumping
		if(jumped && grounded){
			//This will add a force to the players rigidbody, so the player will jump
			
			//Reset velovity to gain always the same jump height!
			rb.velocity = Vector2.zero;
			
			//add force for jump
			rb.AddForce(transform.up*thrust*100);
			//Player is now in the air, therefore he cant jump anymore. Set this true again, if player hits the floor.
    		grounded=false;
    		
    		//Give playerAnimator Instructions
        	playerAnimator.SetTrigger("Jumping");
		}
		
		
    }
	
	//Move is the class that includes running mechanics
	void Move()
    {
		//Add force to the players rigidbody in the direction, of the input in movementInput.x
		rb.velocity = new Vector2 ( short_term_thrust + movementInput.x*speed, rb.velocity.y );
		//Give playerAnimator Instructions
		if(Mathf.Abs(movementInput.x)>0.01f){
			playerAnimator.SetBool("Running",true);
		}
		else{
			playerAnimator.SetBool("Running",false);
		}
		
		//Flip the sprite so the player runs never backwards
		if(movementInput.x>0.01f  && !facingRight){
			
            transform.Rotate(0, 180, 0);
            facingRight = true;
		}
		if(movementInput.x<-0.01f && facingRight){
			
            transform.Rotate(0, 180, 0);
            facingRight = false;
		}
    }
	
	// Attack1 is the class that includes all code managing our first kind of attack
	void Attack1()
	{
		// If we press the attack button
		if(attackGP){
    		if(!attack_pressed && !attack_active){
    			attack_pressed=true;
    			playerAnimator.SetTrigger("AttackTrigger");
    			attack_active=true;
    		}
    	}
    	else{attack_pressed=false;}
	
	}
	
	//Hurt ist the class managing the amount of lifes until we have zero lifes
	void Hurt(){
   		if(hurted){
	    	if(!hurt_start){
				//Set hurt_start to true, so we decrease our healt only one time per attack (in only one frame)
	    		hurt_start=true;
	    		actual_lifes--;
	    		//If we have zero lifes we will die
	    		if(actual_lifes==0){
	    			dead=true;
	    			SR.color = Color.white;
	    			playerAnimator.SetTrigger("DeathTrigger");
	    		}
	    		
	    		if(facingRight){short_term_thrust=-hurt_thrust;}
	    		else{short_term_thrust=hurt_thrust;}
	    		
	    	}
			// setting the sprite rendering color to red will visible indicate, that the player got hurted
	    	SR.color = Color.red;
	    	//counting time, until the variable hurted will be set to false again, so another attack can apply to the health of our player
	    	targetTime += Time.deltaTime;
	    	if(targetTime>hurted_delay){hurted=false;}
	    }
	    else{
	    	targetTime=0;
	    	//Setting the sprite renderer color to normal again
	    	SR.color = Color.white;
	    	hurt_start=false;
	    }
    }
	
    // Start is called before the first frame update
    void Start()
    {
    	//Get the rigidbody attached to the player
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        SR =  GetComponent<SpriteRenderer>();
        
        actual_lifes = lifes;
        
        PlayerPrefs.SetInt("map_active",0);
    }

    // Update is called permanently
    void FixedUpdate()
    {
    	if(short_term_thrust!=0){short_term_thrust*=0.8f;}
    	//Give playerAnimator Instructions
    	playerAnimator.SetBool("Grounded",grounded);
    	playerAnimator.SetFloat("Velocity",rb.velocity.y);
    	
    	int map_mactive = PlayerPrefs.GetInt("map_active");
    	
    	//Create another class for jumping to organize the code
    	if(map_mactive==0){
    		Jump();
    		Move();
    		Attack1();
	    	Hurt();
    		
	    	//check if the animation "Player_Attack1 is active"
    		if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Player_Attack1")){
	    		// If this is the case, than set the attack object with the trigger collider to active, so enemies can receive an attack.
			    attack_active=true;
			    Attack1_HB.SetActive(true);
    		}else{
    			attack_active=false;
			    Attack1_HB.SetActive(false);
    		}
  
    	}
    }
    
    // OnCollisionEnter2D is called when the object collides with an collider of another objectd
    void OnTriggerStay2D(Collider2D other){
    	if(other.tag=="Floor"){
    		//This bool will make jumping possible if true
    		grounded=true;
		}
    }
    
    void OnTriggerEnter2D(Collider2D other){
    	//check, if the player is attacked by an enemy
    	if(other.tag=="Enemy_Attack" && !dead){
    		hurted=true;
    	}
    	//check if the player enters an instant death zones e.g. Lava, Water or Void
    	if(other.tag=="InstantDeath"){
    		actual_lifes=0;
	    	dead=true;
	    	SR.color = Color.white;
	    	playerAnimator.SetTrigger("DeathTrigger");
    	}
    }
}
