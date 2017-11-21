using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowOnlyY : MonoBehaviour {

	public GameObject target;
	private Vector3 changedPosition;

	void Start () {

	}

	// Update is called once per frame
	void Update () {
		changedPosition = target.transform.position;
		changedPosition.y = Mathf.Clamp(changedPosition.y, gameObject.transform.position.y,gameObject.transform.position.y + 300f);
		changedPosition.x = gameObject.transform.position.x;
		changedPosition.z = gameObject.transform.position.z;
		gameObject.transform.position = changedPosition;
	}
}
