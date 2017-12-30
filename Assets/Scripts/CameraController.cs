using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public GameObject target;
	private Vector3 changedPosition;
	//Rigidbody2D rb;
	public float speed = 2f, speedMultiply = 1.5f;
	public int raiseSpeedPerPlatforms = 20;
	public static CameraController Cameracontroller;

    void Awake()
    {
        if (Cameracontroller == null)
        {
            Cameracontroller = this;
        }
        if (Cameracontroller != this)
        {
            Destroy(Cameracontroller.gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (!GameManager.gameManager.paused)
        {
            changedPosition = transform.position;
            changedPosition.y += speed * Time.deltaTime;
            transform.position = changedPosition;
            if (target.transform.position.y > transform.position.y)
            {
                changedPosition = target.transform.position;
                changedPosition.z -= 10;
                changedPosition.x = 0f; //Mathf.Clamp(changedPosition.x, -5f, 5f);
                changedPosition.y = Mathf.Clamp(changedPosition.y, gameObject.transform.position.y - 5f, gameObject.transform.position.y + 300f);
                gameObject.transform.position = changedPosition;
            }
        }
    }
    public void speedUp(int platformlevel)
    {
        if (platformlevel % raiseSpeedPerPlatforms == 0){
			speed *= speedMultiply;

		}
	}

}
