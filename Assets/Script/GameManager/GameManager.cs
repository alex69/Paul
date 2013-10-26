using UnityEngine;
using System.Collections;

public class GameManager: MonoBehaviour 
{
	private AudioSource sound;
	private GameObject Door;
	private bool won;
	private bool death;
	public GUIStyle guiStyle;
	public string NextLevel;
	
	public int totalCoin;
	public int foundCoin;
	
	public AudioClip DestroySound;
	public AudioClip WonSound;
	public AudioClip CoinSound;
	public AudioClip SpeedBoosterSound;
	public AudioClip JumpBoosterSound;
	public AudioClip TeleporterSound;
	public AudioClip BallJumpSound;
	public AudioClip BallHitGroundSound;
	
	void Start () 
	{
		Time.timeScale = 1.0f;
		totalCoin = GameObject.FindGameObjectsWithTag("Coin").Length;
		Door = GameObject.Find("Door");
		sound = GetComponent<AudioSource>();
	}
	
	void Updata()
	{
		if (Input.GetKey (KeyCode.X))
		{
			Application.LoadLevel("Main_Menu");
		}
	}
	
	void OnGUI () 
	{
		if (GUI.Button(new Rect(10, Screen.height - 25, 50, 20), "Menu"))
		{            
			Application.LoadLevel("Main_Menu");
		}
		
		if (won) 
		{
			GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 150, 100, 20), "You Won", guiStyle);
			if(NextLevel != "Main_Menu")
			{
				if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 130, 100, 20), "Next Level"))
				{            
					Application.LoadLevel(NextLevel);
				}
			}
			if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 105, 100, 20), "Menu"))
			{            
				Time.timeScale = 1f;
				Application.LoadLevel("Main_Menu");
			}
		}
		
		else if (death) 
		{
			GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 150, 100, 20), "You're Dead", guiStyle);
			if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 130, 100, 20), "Try Again"))
			{            
				Application.LoadLevel(Application.loadedLevelName);
			}
			if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 105, 100, 20), "Menu"))
			{            
				Application.LoadLevel("Main_Menu");
			}
		}
		GUI.Label(new Rect(10, 10, 500, 20), "Found Coins: "+foundCoin+"/"+totalCoin, guiStyle);
	}
	
	public void FoundCoin()
    {
		audio.clip = CoinSound;
		audio.Play();
        foundCoin++;
		
        if (foundCoin >= totalCoin)
        {
            Door.GetComponent<Door>().FindAllCoin = true;
        }
    }
	
	public void SpeedBooster()
    {
		audio.clip = SpeedBoosterSound;
		audio.Play();
	}
	
	public void JumpBooster()
    {
		audio.clip = JumpBoosterSound;
		audio.Play();
	}
	
	public void BallJump()
    {
		audio.clip = BallJumpSound;
		audio.Play();
	}
	
	public void BallHitGround()
    {
		audio.clip = BallHitGroundSound;
		audio.Play();
	}
	
	public void Teleporter()
    {
		audio.clip = TeleporterSound;
		audio.Play();
	}
	
	public void Won()
	{
		audio.clip = WonSound;
		audio.Play();
		Time.timeScale = 0f;
		won = true;
	}
	
	public void Death()
	{
		audio.clip = DestroySound;
		audio.Play();
		death = true;
	}
}
