using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaycastController : MonoBehaviour
{
	private Camera firstPersonCamera;
	private float maxDistance;
	public Text textMsg;
	public static GameObject bullet;

	void Start ()
	{
		firstPersonCamera = GetComponent <Camera> ();
		maxDistance = 5.0f;
		bullet = null;
	}


	void Update ()
	{
		RaycastHit hitInfo; // this holds the information of the gameObject that our ray hits.
		Vector3 rayCastDirection = firstPersonCamera.transform.forward * maxDistance; 
		//Note: forward direction can also be obtained as 'transform.TransformDirection (Vector3.forward)'
		RaycastHit[] hits = Physics.RaycastAll (firstPersonCamera.transform.position, rayCastDirection, maxDistance);
		/*
		 * the first parameter - Denotes the position at which our ray originates
		 * the second parameter - Denotes in which direction the ray must be casted
		 * the third paramter - Denotes in which variable the info of the gameObject that the ray hits must be stored
		 * the fourth parameter - Denotes what will be the min distance between the player and the object for raycast to work.
		 */
		foreach (RaycastHit hit in hits) {
			Debug.DrawLine (hit.point, hit.point + Vector3.up * maxDistance, Color.yellow);
		}

		Debug.DrawRay (firstPersonCamera.transform.position, rayCastDirection, Color.magenta);

		if (Physics.Raycast (transform.position, rayCastDirection, out hitInfo, maxDistance)) {
			string displayMsg = checkObject (hitInfo);
			textMsg.text = displayMsg;
		} else {
			textMsg.text = "";
		}
	}

	private string checkObject (RaycastHit hitInfo)
	{
		if (hitInfo.transform.tag.Equals ("bullet")) {
			if (Input.GetKeyDown (KeyCode.P)) {
				bullet = hitInfo.collider.gameObject;
				return "Congrats You have aquired the " + hitInfo.collider.gameObject.name;
			}
			return "Wow, you have found something new.. \n Press P to pick it up";
		} else {
			float distanceFromGO = hitInfo.distance;
			return "This is a " + hitInfo.collider.gameObject.name + " at a distance " + distanceFromGO;
		}
	}

	public static GameObject getBulletGO ()
	{
		return bullet;
	}
}
