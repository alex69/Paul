using UnityEngine;
using System.Collections;

public class GameManager: MonoBehaviour 
{	
	enum GameState
	{
		TutoPhase1,
		TutoPhase2,
		TutoPhase3,
		TutoPhase4,
		TutoPhase5,
		TutoPhase6,
		Game,
		Pause,
		End
	}
	private GameState state  = GameState.TutoPhase1;
	public GameObject Car;
	private bool won;
	private bool death;
	public GUIStyle guiStyle;
	public string NextLevel;
	//PBO - 27/10/2013 - Score en point
	public int score = 0;
	//PBO - 27/10/2013 - Energie comprise entre 0 et 100. La partie est perdue lorsque l'énergie atteint l'une de ses valeurs limite, elle diminue au cours du temps et augmente quand on mange
	public float energy = 50.0f;
	public Texture2D EnergyBar;
	public ArrayList tutorial;
	
	//PBO - 30/10/2013 - Timer
	private float Timer = 0;
	public GUIText TimerText;	
	
	//PBO - 30/10/2013 - Gestion de l'interface de tutorial
	public GUIText tutorialText;
	public float lineLength;
	public GUITexture tutorialWindow;	
	private bool isFading;
	private int increment;
	private int MAXHEIGHT = 300;
	public int indiceMessage = 0;
	private int moveNumber = 150;
	
	
	public void animateWindow(){
		tutorialWindow.guiTexture.enabled = true;
		tutorialText.guiText.enabled = false;
		isFading = true;
		increment = -50;
	}
	
	public void ShowMessage (GUIText guiText,string text) {

 
    int numberOfLines = 0;
    var words = text.Split(" "[0]); //Split the string into seperate words
    string result = "";

 

    for( var index = 0; index < words.Length; index++) {

 

       string word = words[index].Trim();

   

       if (index == 0) {

          result = words[0];

          guiText.text = result;

       }

 

       if (index > 0 ) {

 

         result += " " + word;

 

          guiText.text = result;

   }

 

     var TextSize = guiText.GetScreenRect();

   

      if (TextSize.width > lineLength)

      {

          //remover

          result = result.Substring(0,result.Length-(word.Length));

       

          result += "\n" + word;


          guiText.text = result;

      }

    }

}
	
	//PBO - 30/10/2013 - Va permettre de gérer l'affichage des différents messages du tutorial et le franchissement des étapes de ces tutorial
	public void nextMessage(){
		if(indiceMessage < tutorial.Count){
			animateWindow();	
			ShowMessage(tutorialText,tutorial[indiceMessage].ToString());

   

		      

    		
			indiceMessage++;
		}
		else {
			isFading = false;
			tutorialText.guiText.enabled = false;
			tutorialWindow.guiTexture.enabled = false;
			tutorialWindow.pixelInset = new Rect(0,0,0,0);
			switch(state){
				case GameState.TutoPhase1:
					state = GameState.TutoPhase2;
				break;
				case GameState.TutoPhase3:
					state = GameState.TutoPhase4;
					Car.animation.Play();
				break;
				case GameState.TutoPhase5:
					state = GameState.TutoPhase6;
					indiceMessage = 0;
					tutorial = new ArrayList();
					TimerText.guiText.enabled = true;
					tutorial.Add("Ah oui... parce que j'ai omis de te dire que ton objectif, c'etait d'atteindre l'arrivee en un minimum de temps.");
					tutorial.Add("Une derniere chose, evidemment, tout le monde, des grand-meres jusqu'aux pigeons prendront un malin plaisir a t'empecher d'atteindre cet objectif, donc soit prudent et bonne chance! C'est partiiiiiiiiiiiiit!");
					nextMessage();
				break;
				case GameState.TutoPhase6:
					state = GameState.Game;
					
				break;
			}
				
		}
		
	}
	
	public AudioClip DestroySound;
	public AudioClip WonSound;
	public AudioClip EatSound;
	public AudioClip SpeedBoosterSound;
	public AudioClip JumpBoosterSound;
	public AudioClip TeleporterSound;
	public AudioClip BallJumpSound;
	public AudioClip BallHitGroundSound;
	
	
	public void ToEtape5(){
		state = GameState.TutoPhase5;	
		indiceMessage = 0;
		tutorial = new ArrayList();
		tutorial.Add("HUM... Quelle tuerie! Comme tu peux le voir, non seulement manger me rend de l'energie. Mais en plus il augmente ton score que tu peux observer dans le coin superieur droit de ton ecran.");
		tutorial.Add("Bon soyons raisonnable un moment... Les hamburgers, c'est le must, mais c'est un peu gras... Fais donc bien attention parce que la barre d'energie en prend un coup! Mais ne t'en fais pas, il parait qu'il existe d'autres aliments qui ont un impact un peu moins fort sur mon taux de cholesterol...");
		tutorial.Add("Par contre, garde en tete que mon niveau d'energie influe sur ma vitesse de deplacement, plus ce niveau sera proche du centre de la barre et plus je me deplacerais vite.");
		nextMessage();
	}
	
	public bool canMove(){
		return state == GameState.Game || state == GameState.TutoPhase2 || state == GameState.TutoPhase4;	
	}
	
	
	void Start () 
	{
		tutorial = new ArrayList();
		tutorial.Add("Hey! Bienvenue! On m'avait prevenu que tu allais venir m'epauler! La premiere chose a savoir, c'est que quand tu as finis de lire ce que je te dis, il te suffit d'appuyer sur la  touche \"Espace\" pour passer au message suivant.");
		tutorial.Add("Bien! On dirait que tu es doue pour suivre les instructions! Bon ok... je te tutoies, mais vu que tu vas controler mes moindres faits et gestes on est deja des intimes non? Allez on va faire encore plus dur! Tu vas essayer de me deplacer avec les fleches de ton clavier!");
		MAXHEIGHT = (int)(Screen.height / 3);
		lineLength = 1.6f * MAXHEIGHT;
		tutorialText.fontSize = MAXHEIGHT / 18;
		nextMessage();
	}
	
	void Update()
	{
		if(isFading){
			if( tutorialWindow.pixelInset.height < MAXHEIGHT || tutorialWindow.pixelInset.height > 0){
				tutorialWindow.pixelInset = new Rect(0,0,tutorialWindow.pixelInset.width +  2 * increment,tutorialWindow.pixelInset.height +  increment);

			}
			if( tutorialWindow.pixelInset.height <= 0){
				increment = 20;
				
			}
			else if( tutorialWindow.pixelInset.height >= MAXHEIGHT && isFading){
				isFading = false;
				tutorialText.guiText.enabled = true;				
			}
			
			
			
		}
	    //PBO - 30/10/2013 - Actions à mener par le manager en fonction du statut du jeu 
		switch(state){
			case GameState.TutoPhase1:
			case GameState.TutoPhase3:
			case GameState.TutoPhase5:
			case GameState.TutoPhase6:
				if (Input.GetKeyDown(KeyCode.Space))
				{
					nextMessage();
				}
			break;
			case GameState.TutoPhase2:
				if(Input.GetKey("left") || Input.GetKey("right") || Input.GetKey("up") || Input.GetKey("down"))
				{
					moveNumber--;
				}
				if(moveNumber <= 0){
					state = GameState.TutoPhase3;
					indiceMessage = 0;
					tutorial = new ArrayList();
					tutorial.Add("C'est pas mal... mais j'espere que tu en as encore  sous le pied parce que ca va se corser... la prochaine chose que je vais te demander et qui est primordiale... C'EST QUE JE T'INDERDIS DE ME TUER OK?!");
					tutorial.Add("Tu vois cette barre en haut de ton ecran? C'est mon energie!Si elle se vide completement, je meurs de faim et si tu la rempli completement, mon estomac eclate!");
					tutorial.Add("Oui, je sais, je suis un grand sensible... Mais si je ne l'etais pas, ce jeu n'aurait aucun interet! Maintenant qu'on est d'accord, peut passer a la partie interessante : la bouffe!");
					tutorial.Add("Au fil du temps mon energie diminue, tu vas donc devoir me procurer de la nourriture pour que je puisse survivre.");
					tutorial.Add("Tu vois le truc qui tourne en haut de la route la bas? C'est un burger! Et les burger c'est mon kiffe! Donc file vite me le chercher avant que je clamse.");
					nextMessage();
				}
				
			break;
			case GameState.Game :	
				int minute = ((int)Timer / 60);
				int second = (int)(Timer % 60);
				int milisecond = (int)(Timer * 100 % 100);
				
				Timer += Time.deltaTime;
				TimerText.text = minute + ":" + ((second < 10)?"0":"") + second + ":" + ((milisecond < 10)?"0":"") + milisecond;
				energy -= 0.015f;
			break;
			case GameState.TutoPhase4:
				//PBO - 27/10/2013 - L'énergie diminue au cours du temps
				energy -= 0.015f;
			break;
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
		
		//PBO - 27/10/2013 - On stocke l'énergie avec un maximum de 100 pour éviter que la barre ne sorte du cadre
		float tempEnergy = Mathf.Min(100.0f,energy);
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
