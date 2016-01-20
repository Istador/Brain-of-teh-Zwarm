using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Reflection;

public class MainMenu: MonoBehaviour {

	double sHeight;
	double sWidth;
	double s;

	readonly double btn_padding = 10.0;
	readonly double btn_margin = 10.0;
	readonly double btn_border = 4.0;

	GString g_title;
	Vector2 pos_title;
	double size_title = 0.65;

	double size_img = 0.18;
	GImage g_left;
	Vector2 pos_left;
	GImage g_right;
	Vector2 pos_right;

	double size_button = 1.5;
	GButton g_start;
	Vector2 pos_start;
	GButton g_highscores;
	Vector2 pos_highscores;
	GButton g_credits;
	Vector2 pos_credits;
	GButton g_quit = null;
	Vector2 pos_quit;


	Action<GButton> a_start = (b) => {
		MessageDispatcher.I.EmptyQueue();
		SceneManager.LoadScene("Levels/TestLevel");
	};

	Action<GButton> a_highscores = (b) => {
		MessageDispatcher.I.EmptyQueue();
		SceneManager.LoadScene("Levels/Highscores");
	};

	Action<GButton> a_credits = (b) => {
		MessageDispatcher.I.EmptyQueue();
		SceneManager.LoadScene("Levels/Credits");
	};

	Action<GButton> a_quit = (b) => {
		//Beendet das Spiel
		#if UNITY_EDITOR
		//Unity Editor
		UnityEditor.EditorApplication.isPlaying = false;
		#elif UNITY_WEBPLAYER
		//Webplayer
		Application.OpenURL("https://games.blackpinguin.de/BrainOfTehZwarm/play.html");
		#else
		//Standalone Build
		Application.Quit();
		#endif

	};



	void Start(){

		g_title = GString.GetString("Brain of teh Zwarm");

		g_left = new GImage(Resource.Texture["love_left"]);
		g_right = new GImage(Resource.Texture["love_right"]);

		g_start = new GButton(250, 40, GString.GetString("Spiel starten"), a_start);
		g_start.Padding.all = btn_padding;
		g_start.Border.all = btn_border;
		g_start.Margin.vertical = btn_margin;

		g_highscores = new GButton(250, 40, GString.GetString("Highscores"), a_highscores);
		g_highscores.Padding.all = btn_padding;
		g_highscores.Border.all = btn_border;
		g_highscores.Margin.vertical = btn_margin;
		if (! Highscores.Has) {
			g_highscores.Enabled = false;
		}

		g_credits = new GButton(250, 40, GString.GetString("Credits"), a_credits);
		g_credits.Padding.all = btn_padding;
		g_credits.Border.all = btn_border;
		g_credits.Margin.vertical = btn_margin;

		//Quit nicht im WebPlayer zeigen
		if(!(Application.isWebPlayer)){
			g_quit = new GButton(250, 40, GString.GetString("Spiel beenden"), a_quit);
			g_quit.Padding.all = btn_padding;
			g_quit.Border.all = btn_border;
			g_quit.Margin.vertical = btn_margin;
		}
	}



	void Resize(){
		if(sWidth != Screen.width || sHeight != Screen.height){
			sWidth = Screen.width;
			sHeight = Screen.height;
			
			double aspect = (sWidth / sHeight) / (1680/1050);
			s = (sHeight / 1050) * aspect;

			pos_title = new Vector2(
				(float)((sWidth - g_title.Width(size_title * s)) * 0.5),
				(float)( 100.0 * s )
			);

			pos_left = new Vector2(
				(float)( 20.0 * s ),
				(float)(sHeight - 20.0 * s - g_left.Height(size_img * s))
			);

			pos_right = new Vector2(
				(float)(sWidth - 20.0 * s - g_left.Width(size_img * s)),
				(float)(sHeight - 20.0 * s - g_left.Height(size_img * s))
				);

			double height = g_start.Height(size_button * s);
			float x  = (float)((sWidth - g_start.Width(size_button * s)) * 0.5);

			pos_start = new Vector2(x,
				(float)((sHeight - height * 3.0) * 0.5)
			);

			pos_highscores = new Vector2(x,
				(float)((sHeight - height) * 0.5)
			);

			pos_credits = new Vector2(x,
				(float)((sHeight + height) * 0.5)
			);

			if(g_quit != null){
				pos_quit = new Vector2(x,
					(float)((sHeight + height * 3.0) * 0.5)
				);
			}
		}
		
	}



	void OnGUI(){
		Resize();

		//Brain of teh Zwarm
		g_title.Draw(size_title * s, pos_title);

		//Bilder
		g_left.Draw(size_img * s, pos_left);
		g_right.Draw(size_img * s, pos_right);

		//Spiel starten
		g_start.Draw(size_button * s, pos_start);

		//Highscores
		g_highscores.Draw(size_button * s, pos_highscores);

		//Credits
		g_credits.Draw(size_button * s, pos_credits);

		//Spiel beenden
		if(g_quit != null)
			g_quit.Draw(size_button * s, pos_quit);
	}



	void Update(){
		if(Input.GetKeyDown(KeyCode.F)){
			MessageDispatcher.I.EmptyQueue();
			SceneManager.LoadScene("Levels/FontDemo");
		}
	}

}
