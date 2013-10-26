using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour 
{
	public enum XYZ { X, Y, Z};
	public XYZ Direction;
	
	public GameObject[] collider;
	public float WaitTime = 2.0f;
	
	public float SpeedX = 2.0f;	
    public float MaxX = 5.0f;
    public float MinX = 0.0f;
	
	public float SpeedY = 2.0f;	
    public float MaxY = 5.0f;
    public float MinY = 0.0f;
	
	public float SpeedZ = 2.0f;	
    public float MaxZ = 5.0f;
    public float MinZ = 0.0f;

    private float DirectionX = 1.0f;
	private float DirectionY = 1.0f;
	private float DirectionZ = 1.0f;
	public bool Wait = true;
	public bool b = true;

	void Update () 
	{		
		if (Direction.ToString() == "X") 
		{
			if (transform.position.x >= MaxX) 
			{				
				if (Wait && b) 
				{
					b = false;
					StartCoroutine("WaitX");
					transform.position = new Vector3(MaxX,transform.position.y,transform.position.z);
				}
				
				if (!Wait) 
				{
					DirectionX = -1;
					Wait = true;
					b = true;
				}
			}
			else if (transform.position.x <= MinX) 
			{
				if (Wait && b) 
				{
					b = false;
					StartCoroutine("WaitX");
					transform.position = new Vector3(MinX,transform.position.y,transform.position.z);
				}
				
				if (!Wait) 
				{
					DirectionX = 1;
					Wait = true;
					b = true;
				}
			}
			
			transform.Translate(new Vector3(DirectionX * SpeedX * Time.deltaTime,0,0));
		}
		
		else if (Direction.ToString() == "Y") 
		{
			if (transform.position.y >= MaxY) 
			{				
				if (Wait && b) 
				{
					b = false;
					StartCoroutine("WaitY");
					transform.position = new Vector3(transform.position.x,MaxY,transform.position.z);
				}
				
				if (!Wait) 
				{
					DirectionY = -1;
					Wait = true;
					b = true;
				}
			}
			else if (transform.position.y <= MinY) 
			{
				if (Wait && b) 
				{
					b = false;
					StartCoroutine("WaitY");
					transform.position = new Vector3(transform.position.x,MinY,transform.position.z);
				}
				
				if (!Wait) 
				{
					DirectionY = 1;
					Wait = true;
					b = true;
				}
			}
			
			transform.Translate(new Vector3(0,DirectionY * SpeedY * Time.deltaTime,0));
		}
		
		else if (Direction.ToString() == "Z")  
		{
			if (transform.position.z >= MaxZ) 
			{
				if (Wait && b) 
				{
					b = false;
					StartCoroutine("WaitZ");
					transform.position = new Vector3(transform.position.x,transform.position.y,MaxZ);
				}
				
				if (!Wait) 
				{
					DirectionZ = -1;
					Wait = true;
					b = true;
				}
			}
			else if (transform.position.z <= MinZ) 
			{
				if (Wait && b) 
				{
					b = false;
					StartCoroutine("WaitZ");
					transform.position = new Vector3(transform.position.x,transform.position.y,MinZ);
				}
				
				if (!Wait) 
				{
					DirectionZ = 1;
					Wait = true;
					b = true;
				}
			}
			
			transform.Translate(new Vector3(0,0,DirectionZ * SpeedZ * Time.deltaTime));
		}
	}
	
	IEnumerator WaitX() 
	{
		DirectionX = 0;
		for (int i = 0; i < collider.Length; i++) 
		{
			collider[i].GetComponent<BoxCollider>().isTrigger = true;
		}
		yield return new WaitForSeconds(WaitTime);
		for (int i = 0; i < collider.Length; i++) 
		{
			collider[i].GetComponent<BoxCollider>().isTrigger = false;
		}
		Wait = false;
	}
	IEnumerator WaitY()
	{
		DirectionY = 0;
		for (int i = 0; i < collider.Length; i++) 
		{
			collider[i].GetComponent<BoxCollider>().isTrigger = true;
		}
		yield return new WaitForSeconds(WaitTime);
		for (int i = 0; i < collider.Length; i++) 
		{
			collider[i].GetComponent<BoxCollider>().isTrigger = false;
		}
		Wait = false;
	}
	IEnumerator WaitZ()
	{
		DirectionZ = 0;
		for (int i = 0; i < collider.Length; i++) 
		{
			collider[i].GetComponent<BoxCollider>().isTrigger = true;
		}
		yield return new WaitForSeconds(WaitTime);
		for (int i = 0; i < collider.Length; i++) 
		{
			collider[i].GetComponent<BoxCollider>().isTrigger = false;
		}
		
		Wait = false;
	}
}
