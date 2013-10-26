using UnityEngine;
using System.Collections;

public class Teleporter : MonoBehaviour 
{
	public Transform Teleporter_Out;
	
	void Start () 
	{
	
	}

	void Update () 
	{
	
	}
	
	void OnTriggerEnter  (Collider other  ) 
	{
        if (other.tag == "Player")
        {		
			other.transform.position = Teleporter_Out.position;
			other.transform.rotation = Teleporter_Out.rotation;
        }
    }
}
