using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class Credits: MonoBehaviour {



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

	static double size_subtitle = 0.30;
	static double size_text = 0.30;
	static double size_ava = 0.35;


	void Start(){
		
		Action<GButton> a_menu = (b) => {
			//Nachrichtenwarteschlange leeren
			MessageDispatcher.I.EmptyQueue();
			//Hauptmenü laden
			SceneManager.LoadScene("Levels/MainMenu");
		};

		Action<GButton> a_www = (b) => {
			Application.OpenURL("https://rcl.blackpinguin.de/");
		};

		g_title = GString.GetString("Credits", size_title);

		g_left = new GImage(Resource.Texture["love_left"], size_img);
		g_right = new GImage(Resource.Texture["love_right"], size_img);

		GButton g_menu = new GButton(250, 40, GString.GetString("Zum Hauptmenü"), a_menu);
		g_menu.Size = size_button;
		g_menu.Padding.all = 10.0;
		g_menu.Border.all = 4.0;

		GButton g_www = new GButton(GString.GetString("https://rcl.blackpinguin.de/", size_text), a_www);
		g_www.Border.all = 0.0;
		g_www.Border.bottom = 5.0;
		g_www.Filled = false;

		GImage g_ava = new GImage(Resource.Texture["avatar"], size_ava);
		Glyph g_desc =
			GVConcat.Concat(
				GString.GetString("Robin Christopher Ladiges", size_text * 1.2)
				, g_www
				, GString.GetString("(c) 2013 - 2016", size_text * 0.8)
			)
			.Align(HorAlignment.left)
			.Space(20.0)
		;

		Glyph g_me = GConcat.Concat(g_ava, g_desc).Align(VertAlignment.middle).Space(20.0);
		g_main =
			GVConcat.Concat(
				GString.GetString("This game was made by:", size_subtitle),
				g_me,
				g_menu
			)
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

		//Main
		g_main.Draw(s, pos_main);

		//Bilder
		g_left.Draw(s, pos_left);
		g_right.Draw(s, pos_right);
	}


}
