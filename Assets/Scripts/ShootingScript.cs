using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootingScript : MonoBehaviour
{
	public GameObject bullet;
	public Text textMsg;
	Camera mainCamera;
	// Use this for initialization
	void Start ()
	{
		mainCamera = Camera.main;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetButtonDown ("Fire1")) {
			shoot ();
		}

	}

	void shoot ()
	{
		GameObject bullet = RaycastController.getBulletGO ();
		if (bullet == null) {
			Debug.Log ("Bullet Empty");
		} else {
			Quaternion bulletRotation = new Quaternion (mainCamera.transform.rotation.x, transform.rotation.y, 0.0f, 0.0f);
			GameObject bulletGO = (GameObject)Instantiate (bullet, mainCamera.transform.position, bulletRotation);
			Rigidbody bullectRB = bulletGO.GetComponent <Rigidbody> ();
			bullectRB.AddForce (mainCamera.transform.forward * 25.0f, ForceMode.Impulse);
			Debug.Log (mainCamera.transform.forward);
			Destroy (bulletGO, 3.0f);
		}
	}


}
