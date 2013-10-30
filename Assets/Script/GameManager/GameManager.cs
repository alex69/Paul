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
	//PBO - 27/10/2013 - Score en point
	public int score = 0;
	//PBO - 27/10/2013 - Energie comprise entre 0 et 100. La partie est perdue lorsque l'énergie atteint l'une de ses valeurs limite, elle diminue au cours du temps et augmente quand on mange
	public float energy = 50.0f;
	public Texture2D EnergyBar;
	
	
	public AudioClip DestroySound;
	public AudioClip WonSound;
	public AudioClip EatSound;
	public AudioClip SpeedBoosterSound;
	public AudioClip JumpBoosterSound;
	public AudioClip TeleporterSound;
	public AudioClip BallJumpSound;
	public AudioClip BallHitGroundSound;
	
	void Start () 
	{
	}
	
	void Update()
	{
		//PBO - 27/10/2013 - L'énergie diminue au cours du temps
		energy -= 0.015f;
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
		
		//PBO - 27/10/2013 - On stocke l'énergie avec une valeur entre 0 et 100 pour éviter que la barre ne sorte du cadre
		float tempEnergy = Mathf.Max (0.0f,Mathf.Min(100.0f,energy));
		//PBO - 27/10/2013 - Affichage score
		((GUIText)GameObject.Find("Score").GetComponent("GUIText")).text = "Score : " + score;
		//PBO - 27/10/2013 - Affichage énergie (Taille barre + couleur)
		((GUITexture)GameObject.Find("Energy").GetComponent("GUITexture")).transform.localScale = new Vector3(0.3f * (float)tempEnergy / 100,0.059f,0);
		((GUITexture)GameObject.Find("Energy").GetComponent("GUITexture")).transform.localPosition = new Vector3(0.501f - 0.3f * ( 100 - (float)tempEnergy ) / 200 ,0.96f,0.0f);
		
		
		if(Mathf.Abs((int)energy - 50) > 40){
			((GUITexture)GameObject.Find("Energy").GetComponent("GUITexture")).color = Color.red;
		}
		else if(Mathf.Abs((int)energy - 50) > 10){
			((GUITexture)GameObject.Find("Energy").GetComponent("GUITexture")).color = Color.yellow;
		}
		else{
			((GUITexture)GameObject.Find("Energy").GetComponent("GUITexture")).color = Color.green;
		}
	}
	
	//PBO - 27/10/2013 - A appeler lorsqu'on mange de la nourriture, augmente le score et l'énergie
	public void Eat(float energy,int score){
		this.score += score;
		this.energy += energy;
		audio.clip = EatSound;
		audio.Play();
		
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
