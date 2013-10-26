using UnityEngine;
using System.Collections;

public class LevelName : MonoBehaviour 
{
	void Start () 
	{
		GetComponent<TextMesh>().text = Application.loadedLevelName;
	}
}
