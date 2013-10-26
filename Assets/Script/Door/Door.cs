using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour 
{
	private GameObject _GameManager;
	public GUIStyle guiStyle;
	public bool FindAllCoin = false;
	public bool NeedAllCoin = false;
	
	void Start()
	{
		_GameManager = GameObject.Find("_GameManager");
	}

	void OnGUI () 
	{
		//GUI.contentColor = Color.black;
		
		if (NeedAllCoin) 
		{
			GUI.Label(new Rect(5, 30, 600, 20), "You Need To Find All The Coins, Before You Can Go To the Next Level", guiStyle);
		}
	}
	
	void OnTriggerEnter  (Collider other  ) 
	{
        if (other.tag == "Player")
        {		
			if (FindAllCoin) 
			{	
				_GameManager.GetComponent<GameManager>().Won();
			}
			else 
			{
				NeedAllCoin = true;
				StartCoroutine("HideGUI");
			}
        }
    }
	IEnumerator HideGUI()
	{
		yield return new WaitForSeconds(3);
		NeedAllCoin = false;
	}
}
