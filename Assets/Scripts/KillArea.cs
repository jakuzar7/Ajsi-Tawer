using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillArea : MonoBehaviour {

	public GameObject target;
	private Vector3 changedPosition;

	void Start () {

	}

	// Update is called once per frame
	void Update () {
		changedPosition = gameObject.transform.position;


		if(target.transform.position.y -22f > gameObject.transform.position.y){
			changedPosition.y = target.transform.position.y - 22f;

		}else if(target.transform.position.y -22f < gameObject.transform.position.y){
			//changedPosition.y = gameObject.transform.position.y;
		}
			
		gameObject.transform.position = changedPosition;
	}


	void OnTriggerExit2D(Collider2D collider){
		
			//Debug.Log("destroyed " + collider.name);
			if (!collider.gameObject.CompareTag("Player"))
			{
				Destroy(collider.gameObject, 0.5f);
			}else{
				Debug.Log("Game Over");
            GameManager.gameManager.PlayerKilled();
			}
			
	}
}
