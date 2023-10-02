using UnityEngine;
using System.Collections;
 
public class ExtendedFlycam : MonoBehaviour
{
 
	/*

	FEATURES
		WASD/Arrows:    Movement
		          Q:    Climb
		          E:    Drop
              Shift:    Move faster
            Control:    Move slower
                Esc:    Toggle cursor locking to screen (you can also press Ctrl+P to toggle play mode on and off).
	*/
 
	public float cameraSensitivity = 90;
	public float climbSpeed = 4;
	public float normalMoveSpeed = 10;
	public float slowMoveFactor = 0.25f;
	public float fastMoveFactor = 3;
 
	private float rotationX = 0.0f;
	private float rotationY = 0.0f;

    [System.Obsolete]
    void Start ()
	{
		Screen.lockCursor = true;
	}

    [System.Obsolete]
    void Update ()
	{
		if (Input.GetMouseButtonDown(1))
		{
			Screen.lockCursor = !Screen.lockCursor;
		}
		
		if(Screen.lockCursor)
		{
			rotationX += Input.GetAxis("Mouse X") * cameraSensitivity * Time.deltaTime;
			rotationY += Input.GetAxis("Mouse Y") * cameraSensitivity * Time.deltaTime;
			rotationY = Mathf.Clamp (rotationY, -90, 90);
	 
			transform.localRotation = Quaternion.AngleAxis(rotationX, Vector3.up);
			transform.localRotation *= Quaternion.AngleAxis(rotationY, Vector3.left);
	 
		 	if (Input.GetKey (KeyCode.LeftShift) || Input.GetKey (KeyCode.RightShift))
		 	{
				transform.position += transform.forward * (normalMoveSpeed * fastMoveFactor) * Input.GetAxis("Vertical") * Time.deltaTime;
				transform.position += transform.right * (normalMoveSpeed * fastMoveFactor) * Input.GetAxis("Horizontal") * Time.deltaTime;
		 	}
		 	else if (Input.GetKey (KeyCode.LeftControl) || Input.GetKey (KeyCode.RightControl))
		 	{
				transform.position += transform.forward * (normalMoveSpeed * slowMoveFactor) * Input.GetAxis("Vertical") * Time.deltaTime;
				transform.position += transform.right * (normalMoveSpeed * slowMoveFactor) * Input.GetAxis("Horizontal") * Time.deltaTime;
		 	}
		 	else
		 	{
		 		transform.position += transform.forward * normalMoveSpeed * Input.GetAxis("Vertical") * Time.deltaTime;
				transform.position += transform.right * normalMoveSpeed * Input.GetAxis("Horizontal") * Time.deltaTime;
		 	}
	 
	 
			if (Input.GetKey (KeyCode.Q)) {transform.position += transform.up * climbSpeed * Time.deltaTime;}
			if (Input.GetKey (KeyCode.E)) {transform.position -= transform.up * climbSpeed * Time.deltaTime;}
		}
		
		
	}
	void OnGUI()
	{
		GUI.Box(new Rect(10, 32, 400, 90),"Move arround with: W-A-S-D \n" + 
										"Use mouse to look \n" + 
										"Climb up and down with: Q-E \n" + 
										"Speed up and down: L_SHIFT-L_CTRL \n" + 
										"Press L_mouse button to Sever, prees again to move");
	}
}