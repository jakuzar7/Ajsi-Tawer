using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	Animator anim;
	Rigidbody2D rb;
	public float jumpForce;
	public float moveForce;
	//public Vector2 wallAddForce;
	public bool canJump;
	//[SerializeField]
	Vector2 newVelocity, changedVelocity/*, actualVelocity*/;


	void Start () {
		rb = gameObject.GetComponent<Rigidbody2D>();
		anim = gameObject.GetComponent<Animator>();
		canJump = true;
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
		}else if(collision.gameObject.CompareTag("Wall")){
			
			anim.SetTrigger("GravityDown");  //scaling gravity over time

			if (rb.velocity.y > 0)  //multiplying Y VELOCITY
			{
				// WALL AS A TRIGGER WITH CALCULATING OPPOSITE VELOCITY

				// blocking/adding force opposite wall

				Vector2 velo = rb.velocity;
				velo.y *= 1.3f;
				//rb.AddForce(wallAddForce, ForceMode2D.Force);
				rb.velocity = velo;
			}
		}
	}

	void OnCollisionExit2D(Collision2D collision){
		//Debug.Log("collisionExit with: " + collision.gameObject.name);
		if(collision.gameObject.CompareTag("Platform")){
			canJump = false;
		}
	}

}
