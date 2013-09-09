using UnityEngine;
using System.Collections;
using XboxCtrlrInput;		// Be sure to include this if you want an object to have Xbox input

public class MovePlayer : MonoBehaviour 
{
	
	public float jumpImpulse;
	public float maxMoveSpeed;
	public int playerNumber = 0;
	
	public Material matRed;
	public Material matGreen;
	public Material matBlue;
	public Material matYellow;
	
	public GameObject laserAPrefab;
	public GameObject laserBPrefab;
	public GameObject laserXPrefab;
	public GameObject laserYPrefab;
	public GameObject laserBumpPrefab;
	
	public GameObject triggerLeftPrefab;
	public GameObject triggerRightPrefab;
	
	private Vector3 newPosition;
	private bool canJump = false;
	private const float MAX_BUL_TME = 0.1f;
	private float bulletTimer = MAX_BUL_TME;
	private const float MAX_TRG_SCL = 1.21f;
	
	
	// Start
	void Start () 
	{
		playerNumber = Mathf.Clamp(playerNumber, 1, 4);
		
		switch(playerNumber)
		{
			case 1: renderer.material = matRed; break;
			case 2: renderer.material = matGreen; break;
			case 3: renderer.material = matBlue; break;
			case 4: renderer.material = matYellow; break;
		}
		
		newPosition = transform.position;
		
		//XCI.DEBUGLogControllerNames();
	}
	
	
	// Update
	void Update () 
	{
		GameObject bulletReference = null;
		
		// Jump
		if(XCI.GetButtonDown(XboxButton.LeftStick, playerNumber) && canJump)
		{
			canJump = false;
			rigidbody.AddRelativeForce(0.0f, jumpImpulse, 0.0f, ForceMode.Impulse);
		}
		
		// Slam
		if(XCI.GetButtonDown(XboxButton.RightStick, playerNumber) && !canJump)
		{
			rigidbody.AddRelativeForce(0.0f, -jumpImpulse * 1.5f, 0.0f, ForceMode.Impulse);
		}
		
		
		// Shoot colored laser
		if(bulletTimer > 0.0f)
		{
			bulletTimer -= Time.deltaTime;
		}
		
		if(bulletTimer <= 0.0f)
		{
			if(XCI.GetButtonDown(XboxButton.A, playerNumber))
			{
				Instantiate(laserAPrefab, transform.position, laserAPrefab.transform.rotation);
				bulletTimer = MAX_BUL_TME;
			}
			if(XCI.GetButtonDown(XboxButton.B, playerNumber))
			{
				Instantiate(laserBPrefab, transform.position, laserBPrefab.transform.rotation);
				bulletTimer = MAX_BUL_TME;
			}
			if(XCI.GetButtonDown(XboxButton.X, playerNumber))
			{
				Instantiate(laserXPrefab, transform.position, laserXPrefab.transform.rotation);
				bulletTimer = MAX_BUL_TME;
			}
			if(XCI.GetButtonDown(XboxButton.Y, playerNumber))
			{
				Instantiate(laserYPrefab, transform.position, laserYPrefab.transform.rotation);
				bulletTimer = MAX_BUL_TME;
			}
		}
		
		
		// Left stick movement
		newPosition = transform.position;
		float axisX = XCI.GetAxis(XboxAxis.LeftStickX, playerNumber);
		float axisY = XCI.GetAxis(XboxAxis.LeftStickY, playerNumber);
		float newPosX = newPosition.x + (axisX * maxMoveSpeed * Time.deltaTime);
		float newPosZ = newPosition.z + (axisY * maxMoveSpeed * Time.deltaTime);
		newPosition = new Vector3(newPosX, transform.position.y, newPosZ);
		transform.position = newPosition;
		
		
		// Right stick movement
		newPosition = transform.position;
		axisX = XCI.GetAxis(XboxAxis.RightStickX, playerNumber);
		axisY = XCI.GetAxis(XboxAxis.RightStickY, playerNumber);
		newPosX = newPosition.x + (axisX * maxMoveSpeed * 0.3f * Time.deltaTime);
		newPosZ = newPosition.z + (axisY * maxMoveSpeed * 0.3f * Time.deltaTime);
		newPosition = new Vector3(newPosX, transform.position.y, newPosZ);
		transform.position = newPosition;
		
		
		// D-Pad movement
		float newPos = 0.0f;
		
		if(XCI.GetDPad(XboxDPad.Up, playerNumber))
		{
			newPosition = transform.position;
			newPos = newPosition.z + (maxMoveSpeed * Time.deltaTime);
			newPosition = new Vector3(transform.position.x, transform.position.y, newPos);
			transform.position = newPosition;
		}
		if(XCI.GetDPad(XboxDPad.Down, playerNumber))
		{
			newPosition = transform.position;
			newPos = newPosition.z - (maxMoveSpeed * Time.deltaTime);
			newPosition = new Vector3(transform.position.x, transform.position.y, newPos);
			transform.position = newPosition;
		}
		if(XCI.GetDPad(XboxDPad.Left, playerNumber))
		{
			newPosition = transform.position;
			newPos = newPosition.x - (maxMoveSpeed * Time.deltaTime);
			newPosition = new Vector3(newPos, transform.position.y, transform.position.z);
			transform.position = newPosition;
		}
		if(XCI.GetDPad(XboxDPad.Right, playerNumber))
		{
			newPosition = transform.position;
			newPos = newPosition.x + (maxMoveSpeed * Time.deltaTime);
			newPosition = new Vector3(newPos, transform.position.y, transform.position.z);
			transform.position = newPosition;
		}
		
		
		// Trigger input
		float trigSclX = triggerLeftPrefab.transform.localScale.x;
		float trigSclZ = triggerLeftPrefab.transform.localScale.z;
		float leftTrigHeight = MAX_TRG_SCL * (1.0f - XCI.GetAxis(XboxAxis.LeftTrigger, playerNumber));
		float rightTrigHeight = MAX_TRG_SCL * (1.0f - XCI.GetAxis(XboxAxis.RightTrigger, playerNumber));
		triggerLeftPrefab.transform.localScale = new Vector3(trigSclX, leftTrigHeight, trigSclZ);
		triggerRightPrefab.transform.localScale = new Vector3(trigSclX, rightTrigHeight, trigSclZ);
		
		
		// Bumper input
		if(XCI.GetButtonDown(XboxButton.LeftBumper, playerNumber))
		{
			Instantiate(laserBumpPrefab, triggerLeftPrefab.transform.position, laserBumpPrefab.transform.rotation);
		}
		if(XCI.GetButtonDown(XboxButton.RightBumper, playerNumber))
		{
			Instantiate(laserBumpPrefab, triggerRightPrefab.transform.position, laserBumpPrefab.transform.rotation);
		}
		
		
		// Start and back, same as bumpers but smaller bullets
		if(XCI.GetButtonUp(XboxButton.Back, playerNumber))
		{
			bulletReference = Instantiate(laserBumpPrefab, triggerLeftPrefab.transform.position, laserBumpPrefab.transform.rotation) as GameObject;
			bulletReference.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
		}
		if(XCI.GetButtonUp(XboxButton.Start, playerNumber))
		{
			bulletReference = Instantiate(laserBumpPrefab, triggerRightPrefab.transform.position, laserBumpPrefab.transform.rotation) as GameObject;
			bulletReference.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
		}
		
		
		// To quit the program (with keyboard)
		if(Input.GetKeyUp(KeyCode.Escape))
		{
			Application.Quit();
		}
		
	}
	
	
	// Trigger Collision
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.CompareTag("KillField"))
		{
			Application.LoadLevel(Application.loadedLevel);
		}
	}
	
	// Rigidbody Collision
	void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.CompareTag("Ground"))
		{
			canJump = true;
		}
	}
	
	// Gizmo Drawing
	void OnDrawGizmos()
	{
		switch (playerNumber)
		{
			case 1:		Gizmos.color = Color.red; break;
			case 2:		Gizmos.color = Color.green; break;
			case 3:		Gizmos.color = Color.blue; break;
			case 4:		Gizmos.color = Color.yellow; break;
			default:	Gizmos.color = Color.white; break;
		}
		
		Gizmos.color = new Color(Gizmos.color.r, Gizmos.color.g, Gizmos.color.b, 0.5f);
		
		Gizmos.DrawCube(transform.position, new Vector3(1.2f, 1.2f, 1.2f));
	}
	
}
