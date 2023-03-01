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
	
	//Jump is the class that includes jumping mechanics
	void Jump()
    {
		//If jump is pressed and the player is on the ground, execute jumping
		if(jumped && grounded){
			//This will add a force to the players rigidbody, so the player will jump
			rb.AddForce(transform.up*thrust*100);
			//Player is now in the air, therefore he cant jump anymore. Set this true again, if player hits the floor.
    		grounded=false;
    		
        	playerAnimator.SetTrigger("Jumping");
		}
		
		
    }
	
	//Move is the class that includes running mechanics
	void Move()
    {
		//Add force to the players rigidbody in the direction, of the input in movementInput.x
		rb.velocity = new Vector2 ( movementInput.x*speed, rb.velocity.y );
		if(Mathf.Abs(movementInput.x)>0.01f){
			playerAnimator.SetBool("Running",true);
		}
		else{
			playerAnimator.SetBool("Running",false);
		}
		
		if(movementInput.x>0.01f){SR.flipX = false;}
		if(movementInput.x<-0.01f){SR.flipX = true;}
    }
	
    // Start is called before the first frame update
    void Start()
    {
    	//Get the rigidbody attached to the player
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        SR =  GetComponent<SpriteRenderer>();
    }

    // Update is called permanently
    void FixedUpdate()
    {
    	playerAnimator.SetBool("Grounded",grounded);
    	playerAnimator.SetFloat("Velocity",rb.velocity.y);
    	
    	//Create another class for jumping to organize the code
    	Jump();
    	Move();
    }
    
    // OnCollisionEnter2D is called when the object collides with an collider of another object
    void OnCollisionEnter2D(Collision2D col){
    	
    	//checks wethere the collision is made with the floor
    	if(col.gameObject.tag=="Floor"){
    		
    		//This bool will make jumping possible if true
    		grounded=true;
    	}
    }
}
