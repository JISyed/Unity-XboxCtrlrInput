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
	private const float MAX_BUL_TME = 0.3f;
	private float bulletTimer = MAX_BUL_TME;
	private const float MAX_TRG_SCL = 1.21f;
	private float dpRightBulletTimer = MAX_BUL_TME * 2;
	private float dpLeftBulletTimer = MAX_BUL_TME * 2;
	private float dpUpBulletTimer = MAX_BUL_TME * 2;
	private float dpDownBulletTimer = MAX_BUL_TME * 2;
	private static bool didQueryNumOfCtrlrs = false;
	
	
	// Start
	void Start () 
	{
		playerNumber = Mathf.Clamp(playerNumber, 1, 4);
		
		switch(playerNumber)
		{
			case 1: GetComponent<Renderer>().material = matRed; break;
			case 2: GetComponent<Renderer>().material = matGreen; break;
			case 3: GetComponent<Renderer>().material = matBlue; break;
			case 4: GetComponent<Renderer>().material = matYellow; break;
		}
		
		newPosition = transform.position;
		
		if(!didQueryNumOfCtrlrs)
		{
			didQueryNumOfCtrlrs = true;
			
			int queriedNumberOfCtrlrs = XCI.GetNumPluggedCtrlrs();
			if(queriedNumberOfCtrlrs == 1)
			{
				Debug.Log("Only " + queriedNumberOfCtrlrs + " Xbox controller plugged in.");
			}
			else if (queriedNumberOfCtrlrs == 0)
			{
				Debug.Log("No Xbox controllers plugged in!");
			}
			else
			{
				Debug.Log(queriedNumberOfCtrlrs + " Xbox controllers plugged in.");
			}
			
			XCI.DEBUG_LogControllerNames();
		}
	}
	
	
	// Update
	void Update () 
	{
		GameObject bulletReference = null;
		
		// Jump (Left Stick)
		if(XCI.GetButtonDown(XboxButton.LeftStick, playerNumber) && canJump)
		{
			canJump = false;
			GetComponent<Rigidbody>().AddRelativeForce(0.0f, jumpImpulse, 0.0f, ForceMode.Impulse);
		}
		
		// Slam (Right Stick)
		if(XCI.GetButtonDown(XboxButton.RightStick, playerNumber) && !canJump)
		{
			GetComponent<Rigidbody>().AddRelativeForce(0.0f, (-jumpImpulse * 1.5f), 0.0f, ForceMode.Impulse);
		}
		
		
		// Shoot colored laser (A,B,X,Y)
		if(bulletTimer > 0.0f)
		{
			bulletTimer -= Time.deltaTime;
		}
		
		if(bulletTimer <= 0.0f)
		{
			if(XCI.GetButton(XboxButton.A, playerNumber))
			{
				Instantiate(laserAPrefab, transform.position, laserAPrefab.transform.rotation);
				bulletTimer = MAX_BUL_TME;
			}
			if(XCI.GetButton(XboxButton.B, playerNumber))
			{
				Instantiate(laserBPrefab, transform.position, laserBPrefab.transform.rotation);
				bulletTimer = MAX_BUL_TME;
			}
			if(XCI.GetButton(XboxButton.X, playerNumber))
			{
				Instantiate(laserXPrefab, transform.position, laserXPrefab.transform.rotation);
				bulletTimer = MAX_BUL_TME;
			}
			if(XCI.GetButton(XboxButton.Y, playerNumber))
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
		
		
		// D-Pad testing
		if(dpUpBulletTimer > 0.0f)
		{
			dpUpBulletTimer -= Time.deltaTime;
		}
		if(dpDownBulletTimer > 0.0f)
		{
			dpDownBulletTimer -= Time.deltaTime;
		}
		if(dpLeftBulletTimer > 0.0f)
		{
			dpLeftBulletTimer -= Time.deltaTime;
		}
		if(dpRightBulletTimer > 0.0f)
		{
			dpRightBulletTimer -= Time.deltaTime;
		}
		if(dpUpBulletTimer <= 0.0f)
		{
			if(XCI.GetDPad(XboxDPad.Up, playerNumber))
			{
				bulletReference = Instantiate(laserBumpPrefab, transform.position, laserYPrefab.transform.rotation) as GameObject;
				bulletReference.transform.localScale = new Vector3(0.1f, 0.1f, 1.0f);
				dpUpBulletTimer = MAX_BUL_TME * 2;
			}
		}
		if(dpDownBulletTimer <= 0.0f)
		{
			if(XCI.GetDPad(XboxDPad.Down, playerNumber))
			{
				bulletReference = Instantiate(laserBumpPrefab, transform.position, laserAPrefab.transform.rotation) as GameObject;
				bulletReference.transform.localScale = new Vector3(0.1f, 0.1f, 1.0f);
				dpDownBulletTimer = MAX_BUL_TME * 2;
			}
		}
		if(dpLeftBulletTimer <= 0.0f)
		{
			if(XCI.GetDPad(XboxDPad.Left, playerNumber))
			{
				bulletReference = Instantiate(laserBumpPrefab, transform.position, laserXPrefab.transform.rotation) as GameObject;
				bulletReference.transform.localScale = new Vector3(0.1f, 0.1f, 1.0f);
				dpLeftBulletTimer = MAX_BUL_TME * 2;
			}
		}
		if(dpRightBulletTimer <= 0.0f)
		{
			if(XCI.GetDPad(XboxDPad.Right, playerNumber))
			{
				bulletReference = Instantiate(laserBumpPrefab, transform.position, laserBPrefab.transform.rotation) as GameObject;
				bulletReference.transform.localScale = new Vector3(0.1f, 0.1f, 1.0f);
				dpRightBulletTimer = MAX_BUL_TME * 2;
			}
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
