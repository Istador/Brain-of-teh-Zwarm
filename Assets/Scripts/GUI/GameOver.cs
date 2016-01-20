﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System;

//using System.Collections;
//using System.Reflection;

public class GameOver: MonoBehaviour {

	public static int Brains = 0;
	
	double sHeight;
	double sWidth;
	double s;

	GString g_title;
	Vector2 pos_title;
	double size_title = 0.65;

	double size_img = 0.18;
	GImage g_left;
	Vector2 pos_left;
	GImage g_right;
	Vector2 pos_right;

	double size_button = 1.5;
	Glyph g_main;
	Vector2 pos_main;

	double size_text = 0.3;

	void Start(){

		Action<GButton> a_menu = (b) => {
			Brains = 0;
			//Nachrichtenwarteschlange leeren
			MessageDispatcher.I.EmptyQueue();
			//Hauptmenü laden
			SceneManager.LoadScene("Levels/MainMenu");
		};

		g_title = GString.GetString("Game Over", size_title);

		g_left = new GImage(Resource.Texture["love_left"], size_img);
		g_right = new GImage(Resource.Texture["love_right"], size_img);

		GButton g_menu = new GButton(250, 40, GString.GetString("Zum Hauptmenü"), a_menu);
		g_menu.Size = size_button;
		g_menu.Padding.all = 10.0;
		g_menu.Border.all = 4.0;

		//Zeit überlebt
		TimeSpan rt = Pause.Runtime;
		Glyph g_alive = GString.GetString("Zeit: "+Utility.TimeToString(rt), size_text);

		//Anzahl Gehirne
		Glyph g_brains = GString.GetString(Brains+ (Brains != 1 ? " Brainz erbeutet" : " Brain erbeutet"), size_text);

		g_main = GVConcat.Concat(g_brains, g_alive, g_menu)
			.Align(HorAlignment.center)
			.Space(40.0)
		;

		Highscores.Add(Brains, rt);
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
				(float)(sHeight - 20.0*s - g_left.Height(s))
			);

			pos_right = new Vector2(
				(float)(sWidth - 20.0*s - g_left.Width(s)),
				(float)(sHeight - 20.0*s - g_left.Height(s))
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

		//Hauptmenü
		g_main.Draw(s, pos_main);
	}


}
