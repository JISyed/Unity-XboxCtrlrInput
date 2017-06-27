using UnityEngine;
using UnityEngine.SceneManagement;
using XboxCtrlrInput;		// Be sure to include this if you want an object to have Xbox input

public class MovePlayer : MonoBehaviour 
{
	
	public float jumpImpulse;
	public float maxMoveSpeed;
	public XboxController controller;
	
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
		switch(controller)
		{
			case XboxController.First: GetComponent<Renderer>().material = matRed; break;
			case XboxController.Second: GetComponent<Renderer>().material = matGreen; break;
			case XboxController.Third: GetComponent<Renderer>().material = matBlue; break;
			case XboxController.Fourth: GetComponent<Renderer>().material = matYellow; break;
		}


		newPosition = transform.position;
		

        // Call for the number of connected controllers once
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

            // This code only works on Windows
            if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
            {
                Debug.Log("Windows Only:: Any Controller Plugged in: " + XCI.IsPluggedIn(XboxController.Any).ToString());

                Debug.Log("Windows Only:: Controller 1 Plugged in: " + XCI.IsPluggedIn(XboxController.First).ToString());
                Debug.Log("Windows Only:: Controller 2 Plugged in: " + XCI.IsPluggedIn(XboxController.Second).ToString());
                Debug.Log("Windows Only:: Controller 3 Plugged in: " + XCI.IsPluggedIn(XboxController.Third).ToString());
                Debug.Log("Windows Only:: Controller 4 Plugged in: " + XCI.IsPluggedIn(XboxController.Fourth).ToString());
            }
        }

	}
	
	
	// Update
	void Update () 
	{
		GameObject bulletReference = null;
		
		// Jump (Left Stick)
		if(XCI.GetButtonDown(XboxButton.LeftStick, controller) && canJump)
		{
			canJump = false;
			GetComponent<Rigidbody>().AddRelativeForce(0.0f, jumpImpulse, 0.0f, ForceMode.Impulse);
		}
		
		// Slam (Right Stick)
		if(XCI.GetButtonDown(XboxButton.RightStick, controller) && !canJump)
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
			if(XCI.GetButton(XboxButton.A, controller))
			{
				Instantiate(laserAPrefab, transform.position, laserAPrefab.transform.rotation);
				bulletTimer = MAX_BUL_TME;
			}
			if(XCI.GetButton(XboxButton.B, controller))
			{
				Instantiate(laserBPrefab, transform.position, laserBPrefab.transform.rotation);
				bulletTimer = MAX_BUL_TME;
			}
			if(XCI.GetButton(XboxButton.X, controller))
			{
				Instantiate(laserXPrefab, transform.position, laserXPrefab.transform.rotation);
				bulletTimer = MAX_BUL_TME;
			}
			if(XCI.GetButton(XboxButton.Y, controller))
			{
				Instantiate(laserYPrefab, transform.position, laserYPrefab.transform.rotation);
				bulletTimer = MAX_BUL_TME;
			}
		}
		
		// Left stick movement
		newPosition = transform.position;
		float axisX = XCI.GetAxis(XboxAxis.LeftStickX, controller);
		float axisY = XCI.GetAxis(XboxAxis.LeftStickY, controller);
		float newPosX = newPosition.x + (axisX * maxMoveSpeed * Time.deltaTime);
		float newPosZ = newPosition.z + (axisY * maxMoveSpeed * Time.deltaTime);
		newPosition = new Vector3(newPosX, transform.position.y, newPosZ);
		transform.position = newPosition;
		
		
		// Right stick movement
		newPosition = transform.position;
		axisX = XCI.GetAxis(XboxAxis.RightStickX, controller);
		axisY = XCI.GetAxis(XboxAxis.RightStickY, controller);
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
			if(XCI.GetDPad(XboxDPad.Up, controller))
			{
				bulletReference = Instantiate(laserBumpPrefab, transform.position, laserYPrefab.transform.rotation) as GameObject;
				bulletReference.transform.localScale = new Vector3(0.1f, 0.1f, 1.0f);
				dpUpBulletTimer = MAX_BUL_TME * 2;
			}
		}
		if(dpDownBulletTimer <= 0.0f)
		{
			if(XCI.GetDPad(XboxDPad.Down, controller))
			{
				bulletReference = Instantiate(laserBumpPrefab, transform.position, laserAPrefab.transform.rotation) as GameObject;
				bulletReference.transform.localScale = new Vector3(0.1f, 0.1f, 1.0f);
				dpDownBulletTimer = MAX_BUL_TME * 2;
			}
		}
		if(dpLeftBulletTimer <= 0.0f)
		{
			if(XCI.GetDPad(XboxDPad.Left, controller))
			{
				bulletReference = Instantiate(laserBumpPrefab, transform.position, laserXPrefab.transform.rotation) as GameObject;
				bulletReference.transform.localScale = new Vector3(0.1f, 0.1f, 1.0f);
				dpLeftBulletTimer = MAX_BUL_TME * 2;
			}
		}
		if(dpRightBulletTimer <= 0.0f)
		{
			if(XCI.GetDPad(XboxDPad.Right, controller))
			{
				bulletReference = Instantiate(laserBumpPrefab, transform.position, laserBPrefab.transform.rotation) as GameObject;
				bulletReference.transform.localScale = new Vector3(0.1f, 0.1f, 1.0f);
				dpRightBulletTimer = MAX_BUL_TME * 2;
			}
		}
		
		
		// Trigger input
		float trigSclX = triggerLeftPrefab.transform.localScale.x;
		float trigSclZ = triggerLeftPrefab.transform.localScale.z;
		float leftTrigHeight = MAX_TRG_SCL * (1.0f - XCI.GetAxis(XboxAxis.LeftTrigger, controller));
		float rightTrigHeight = MAX_TRG_SCL * (1.0f - XCI.GetAxis(XboxAxis.RightTrigger, controller));
		triggerLeftPrefab.transform.localScale = new Vector3(trigSclX, leftTrigHeight, trigSclZ);
		triggerRightPrefab.transform.localScale = new Vector3(trigSclX, rightTrigHeight, trigSclZ);
		
		
		// Bumper input
		if(XCI.GetButtonDown(XboxButton.LeftBumper, controller))
		{
			Instantiate(laserBumpPrefab, triggerLeftPrefab.transform.position, laserBumpPrefab.transform.rotation);
		}
		if(XCI.GetButtonDown(XboxButton.RightBumper, controller))
		{
			Instantiate(laserBumpPrefab, triggerRightPrefab.transform.position, laserBumpPrefab.transform.rotation);
		}
		
		
		// Start and back, same as bumpers but smaller bullets
		if(XCI.GetButtonUp(XboxButton.Back, controller))
		{
			bulletReference = Instantiate(laserBumpPrefab, triggerLeftPrefab.transform.position, laserBumpPrefab.transform.rotation) as GameObject;
			bulletReference.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
		}
		if(XCI.GetButtonUp(XboxButton.Start, controller))
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
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
		switch (controller)
		{
			case XboxController.First:  Gizmos.color = Color.red; break;
			case XboxController.Second: Gizmos.color = Color.green; break;
			case XboxController.Third:  Gizmos.color = Color.blue; break;
			case XboxController.Fourth: Gizmos.color = Color.yellow; break;
			default:                    Gizmos.color = Color.white; break;
		}
		
		Gizmos.color = new Color(Gizmos.color.r, Gizmos.color.g, Gizmos.color.b, 0.5f);
		
		Gizmos.DrawCube(transform.position, new Vector3(1.2f, 1.2f, 1.2f));
	}
	
}
