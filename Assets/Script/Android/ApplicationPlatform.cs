using UnityEngine;
using System.Collections;

public class ApplicationPlatform : MonoBehaviour
{

	void Start ()
	{
		if(name == "LeftJoystick" || name == "RightJoystick")
		{
			if (Application.platform == RuntimePlatform.Android)
			{
				GetComponent<GUITexture>().enabled = true;
			}
		}
	}
}
