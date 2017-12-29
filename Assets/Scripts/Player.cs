using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public float jumpForce, moveForce, timeBetweenBang;
	//public Vector2 wallAddForce;
	public bool canJump;
	public Vector2 OriginalwallMultiply;
	[SerializeField]
	Vector2 newVelocity, changedVelocity,wallMultiply/*, actualVelocity*/;
	Animator anim;
	Rigidbody2D rb;
	[SerializeField]
	float rightBangTime, leftBangTime;

	void Start () {
		rb = gameObject.GetComponent<Rigidbody2D>();
		anim = gameObject.GetComponent<Animator>();
		canJump = true;
		wallMultiply = OriginalwallMultiply;
		newVelocity.y = 0f;
	}

	void Update () {
		//actualVelocity = rb.velocity;
		if(Input.GetKeyDown(KeyCode.Space)){
			//Debug.Log("pressed space");
			if (canJump){
				newVelocity.x = rb.velocity.x;
				rb.velocity = newVelocity;
				rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Force);
				//canJump = false;
			}
		} 
		if(Input.GetKey(KeyCode.A)){ //left
			if(rb.velocity.x>0){
				Vector2 velo = rb.velocity;
				velo.x *= .95f;
				rb.velocity = velo;
			}
			rb.AddForce(Vector2.left * moveForce * Time.deltaTime, ForceMode2D.Force);

		}
		if(Input.GetKey(KeyCode.D)){ //right
			if(rb.velocity.x < 0){
				Vector2 velo = rb.velocity;
				velo.x *= .95f;
				rb.velocity = velo;
			}
			rb.AddForce(Vector2.right * moveForce * Time.deltaTime, ForceMode2D.Force);
		}


	}

	void OnCollisionEnter2D(Collision2D collision){
		//Debug.Log("collisionEnter with: " + collision.gameObject.name);
		if(collision.gameObject.CompareTag("Platform")){
			canJump = true;
		}else if(collision.gameObject.CompareTag("RightWall")||collision.gameObject.CompareTag("LeftWall")){
			
			//anim.SetTrigger("GravityDown");  //scaling gravity over time

			if (rb.velocity.y > 0)  //multiplying Y VELOCITY
			{
				// WALL AS A TRIGGER WITH CALCULATING OPPOSITE VELOCITY

				// blocking/adding force opposite wall

				/*Vector2 velo = rb.velocity;
				velo.y *= 1.3f;
				//rb.AddForce(wallAddForce, ForceMode2D.Force);
				rb.velocity = velo;*/// multiplying y velo is in ontrigger\|/
			}
		}
	}

	void OnTriggerEnter2D(Collider2D collision){
		if (collision.gameObject.CompareTag("RightWall")){
			//Debug.Log("triggerEnter with: " + collision.gameObject.name);

			if(CanWallBang(rightBangTime)){
				rightBangTime = (float)Time.time;
				wallMultiply = OriginalwallMultiply;
			}else{
				rightBangTime = (float)Time.time;
				wallMultiply.y = 1.1f;
			}

			if(rb.velocity.x > 0f){
				rb.velocity = new Vector2(-rb.velocity.x * wallMultiply.x , (rb.velocity.y > 0) ? rb.velocity.y * wallMultiply.y : rb.velocity.y);
			}else{
				rb.velocity = new Vector2(rb.velocity.x * wallMultiply.x, (rb.velocity.y > 0) ? rb.velocity.y * wallMultiply.y : rb.velocity.y);
			}

		}
		if (collision.gameObject.CompareTag("LeftWall")){
			//Debug.Log("triggerEnter with: " + collision.gameObject.name);

			if(CanWallBang(leftBangTime)){
				leftBangTime = (float)Time.time;
				wallMultiply = OriginalwallMultiply;
			}else{
				leftBangTime = (float)Time.time;
				wallMultiply.y = 1.1f;
			}

			if(rb.velocity.x < 0f){
				rb.velocity = new Vector2(-rb.velocity.x * wallMultiply.x, (rb.velocity.y > 0) ? rb.velocity.y * wallMultiply.y : rb.velocity.y);
			}else{
				rb.velocity = new Vector2(rb.velocity.x * wallMultiply.x, (rb.velocity.y > 0) ? rb.velocity.y * wallMultiply.y : rb.velocity.y);
			}
		}
	}

	bool CanWallBang(float bangTime){
		//Debug.Log("resetingwallbang" + Time.time);
		if((float)Time.time - bangTime > timeBetweenBang){
			Debug.Log("resetingwallbang Time.time: " + (float)Time.time +" bangtime: " + bangTime + " returned TRUE");
			return true;
		}else{
			Debug.Log("resetingwallbang Time.time: " + (float)Time.time +" bangtime: " + bangTime + " returned FALSE");
			return false;
		}

	}

	void OnCollisionExit2D(Collision2D collision){
		//Debug.Log("collisionExit with: " + collision.gameObject.name);
		if(collision.gameObject.CompareTag("Platform")){
			canJump = false;
		}
	}

}
