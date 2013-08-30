using UnityEngine;
using System.Collections;

public class MoveLaser : MonoBehaviour 
{
	public float speed = 15.0f;
	private Vector3 newPosition;
	
	// Use this for initialization
	void Start () 
	{
		Destroy(gameObject, 1.0f);
	}
	
	// Update is called once per frame
	void Update () 
	{
		newPosition = transform.position;
		newPosition = transform.position + transform.forward * speed * Time.deltaTime;
		transform.position = newPosition;
	}
}
