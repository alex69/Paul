using UnityEngine;
using System.Collections;

public class Coins : MonoBehaviour 
{
	public float speed = 1;
	
	void Update () 
	{
		transform.Rotate(new Vector3(transform.rotation.x + speed * Time.deltaTime,0,0));
	}
}
