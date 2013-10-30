using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour 
{
	private GameObject moveJoy;
	private GameManager _GameManager;
	public Vector3 movement;
	public float moveSpeed = 6.0f;
	public float jumpSpeed = 5.0f;
	public float drag = 2;
	private bool canJump = true;
	
	void Start()
	{
		// Here start the player control
		moveJoy = GameObject.Find("LeftJoystick");
		_GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}
	
	void Update () 
	{	
		if(_GameManager.canMove()){
			rigidbody.isKinematic = false;
			//PBO - 27/10/2013 - L'energie va influer sur la vites
			moveSpeed = 24.0f * (90 - (Mathf.Abs((int)(_GameManager.energy) - 50) - 10)) / 100;
			if(_GameManager.energy > 100 || _GameManager.energy < 0){		
				_GameManager.Death();	
				Destroy(gameObject);
			}
			Vector3 forward = Camera.main.transform.TransformDirection(Vector3.forward);
			forward.y = 0;
			forward = forward.normalized;
			
			Vector3 forwardForce = new Vector3();
			if (Application.platform == RuntimePlatform.Android) 
			{
				float tmpSpeed = moveJoy.GetComponent<Joystick>().position.y;
				forwardForce = forward * tmpSpeed * 1f * moveSpeed;
			}
			else
			{
				forwardForce = forward * Input.GetAxis("Vertical") * moveSpeed;
			}
			rigidbody.AddForce(forwardForce);
			
			Vector3 right= Camera.main.transform.TransformDirection(Vector3.right);
			right.y = 0;
			right = right.normalized;
			
			Vector3 rightForce = new Vector3();
			if (Application.platform == RuntimePlatform.Android) 
			{
				float tmpSpeed = moveJoy.GetComponent<Joystick>().position.x;
				rightForce = right * tmpSpeed * 0.8f * moveSpeed;
			}
			else
			{
				rightForce= right * Input.GetAxis("Horizontal") * moveSpeed;
			}		
			rigidbody.AddForce(rightForce);
			
		}
		else{
			
			rigidbody.isKinematic = true;	
		}
	}
	
	void OnTriggerEnter(Collider other) 
	{
		if (other.tag == "Hamburger")
		{
				Destroy(other.gameObject);
				_GameManager.Eat(20,200);
		
		}
		else if (other.tag == "PremierBurger")
		{
				Destroy(other.gameObject);				
				_GameManager.Eat(20,200);
				_GameManager.ToEtape5();
		
		}
		else if (other.tag == "Car")
		{
				rigidbody.angularVelocity = new Vector3(-rigidbody.angularVelocity.x,-rigidbody.angularVelocity.y,-rigidbody.angularVelocity.z);
				rigidbody.velocity = new Vector3(-rigidbody.velocity.x,-rigidbody.velocity.y,-rigidbody.velocity.z);
		
		}
    }
	
	
	
	void OnGUI()
	{
		//GUI.Label(new Rect(300,10,100,100),"X: " + moveJoy.GetComponent<Joystick>().position.x.ToString());
		//GUI.Label(new Rect(300,30,100,100),"Y: " + moveJoy.GetComponent<Joystick>().position.y.ToString());
	}
}
