using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class environmentController : MonoBehaviour {

	//public float destroyTime;
	Vector3 changedScale;
	public Vector2 minAndMaxScale;
	bool pointed = false;

	void Start () {
		gameObject.name =gameObject.name +" "+ GameManager.gameManager.platformCount;

		changedScale = transform.localScale;
		changedScale.x = Random.Range(minAndMaxScale.x, minAndMaxScale.y);
		transform.localScale = changedScale;
		//Destroy(gameObject, destroyTime);
	}

	void OnTriggerExit2D(Collider2D collider){
		if (collider.gameObject.CompareTag("Player")&& !pointed)
		{
			pointed = true;
			GameManager.gameManager.AddPlatformLevel(1);	
		}
	}

}
