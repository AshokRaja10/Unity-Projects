using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent (typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
	CharacterController characterController;
	public float movementSpeed;
	public float mouseSensitivity;
	private float verticalVisionRage = 30.0f;
	float verticalRotation = 0.0f, horizontalRotation = 0.0f;
	float verticalVelocity = 0.0f;
	public float jumpHeight;
	private float sprintEndTime;

	void Start ()
	{
		characterController = GetComponent<CharacterController> ();
		Screen.lockCursor = true;
	}


	void Update ()
	{
		//player rotation using mouse
		horizontalRotation = Input.GetAxis ("Mouse X") * mouseSensitivity;
		transform.Rotate (0.0f, horizontalRotation, 0.0f); //for horizontal roataion we rotate the player about the Y-axis

		verticalRotation -= Input.GetAxis ("Mouse Y") * mouseSensitivity;//for vertical rotation we tilt the main camera along the X-axis
		verticalRotation = Mathf.Clamp (verticalRotation, -verticalVisionRage, verticalVisionRage);
		Camera.main.transform.localRotation = Quaternion.Euler (verticalRotation, 0.0f, 0.0f);

		//player movement
		float moveVertical = Input.GetAxis ("Vertical") * movementSpeed;
		float moveHorizontal = Input.GetAxis ("Horizontal") * movementSpeed;
		verticalVelocity += Physics.gravity.y * Time.deltaTime;

		if (characterController.isGrounded && Input.GetButtonDown ("Jump")) {
			verticalVelocity = jumpHeight;
		}

		Vector3 movement = new Vector3 (moveHorizontal, verticalVelocity, moveVertical);
		movement = transform.rotation * movement;
		characterController.Move (movement * Time.deltaTime);
		//Debug.Log ("player rotation" + transform.rotation);
	}
}
