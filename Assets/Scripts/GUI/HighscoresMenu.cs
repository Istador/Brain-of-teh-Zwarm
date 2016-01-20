using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;

public class HighscoresMenu: MonoBehaviour {



	double sHeight;
	double sWidth;
	double s;

	static double size_title = 0.65;
	GString g_title;
	Vector2 pos_title;

	static double size_img = 0.18;
	GImage g_left, g_right;
	Vector2 pos_left, pos_right;

	static double size_button = 1.5;
	Glyph g_main;
	Vector2 pos_main;



	void Start(){
		
		Action<GButton> a_menu = (b) => {
			//Nachrichtenwarteschlange leeren
			MessageDispatcher.I.EmptyQueue();
			//Hauptmenü laden
			SceneManager.LoadScene("Levels/MainMenu");
		};

		g_title = GString.GetString("Highscores", size_title);

		g_left = new GImage(Resource.Texture["love_left"], size_img);
		g_right = new GImage(Resource.Texture["love_right"], size_img);

		GButton g_menu = new GButton(250, 40, GString.GetString("Zum Hauptmenü"), a_menu);
		g_menu.Size = size_button;
		g_menu.Padding.all = 10.0;
		g_menu.Border.all = 4.0;

		List<Glyph> g_l_times = Highscores.Times;
		if(g_l_times.Count == 0) { g_l_times.Add(GString.GetString("---", Highscores.size_text)); }
		g_l_times.Insert(0, GString.GetString("Zeit", 0.4));
		Glyph g_times = GVConcat.Concat(g_l_times.ToArray())
			.Align(HorAlignment.center)
			.Space(30.0)
		;

		List<Glyph> g_l_brains = Highscores.Brains;
		if(g_l_brains.Count == 0) { g_l_brains.Add(GString.GetString("---", Highscores.size_text)); }
		g_l_brains.Insert(0, GString.GetString("Brainz", 0.4));
		Glyph g_brains = GVConcat.Concat(g_l_brains.ToArray())
			.Align(HorAlignment.center)
			.Space(30.0)
		;
		
		Glyph g_scores = 
			GConcat.Concat(g_times, g_brains)
				.Space(100.0)
				.AutoAlign(HorAlignment.center)
		;
		g_main = GVConcat.Concat(g_scores, g_menu)
			.Align(HorAlignment.center)
			.Space(40.0)
		;
	}




	void Resize(){
		if(sWidth != Screen.width || sHeight != Screen.height){
			sWidth = Screen.width;
			sHeight = Screen.height;
			
			double aspect = (sWidth / sHeight) / (1680/1050);
			s = (sHeight / 1050) * aspect;

			pos_title = new Vector2(
				(float)((sWidth - g_title.Width(s))/2.0),
				(float)( 100.0 * s )
			);

			pos_left = new Vector2(
				(float)( 20.0 * s ),
				(float)(sHeight - 20.0 * s - g_left.Height(s))
			);

			pos_right = new Vector2(
				(float)(sWidth - 20.0 * s - g_left.Width(s)),
				(float)(sHeight - 20.0 * s - g_left.Height(s))
			);

			pos_main = new Vector2(
				(float)((sWidth - g_main.Width(s)) * 0.5),
				(float)((sHeight - g_main.Height(s)) * 0.5)
			);
		}
		
	}



	void OnGUI(){
		Resize();

		//Game Over
		g_title.Draw(s, pos_title);

		//Bilder
		g_left.Draw(s, pos_left);
		g_right.Draw(s, pos_right);

		//Main
		g_main.Draw(s, pos_main);
	}


}
