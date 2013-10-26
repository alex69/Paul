using UnityEngine;
using System.Collections;

public class SpeedBooster : MonoBehaviour 
{
	public float BoosterSpeed = 700f;
	public Vector3 Way;
	
	void OnTriggerEnter  (Collider other  ) 
	{
		if (other.tag == "Player")
		{
			other.rigidbody.AddForce(Way * BoosterSpeed);
		}
    }
}
