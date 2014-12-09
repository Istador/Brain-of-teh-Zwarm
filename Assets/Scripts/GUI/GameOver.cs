using UnityEngine;
using System;

//using System.Collections;
//using System.Reflection;

public class GameOver: MonoBehaviour {

	public static int Brains = 0;
	public static System.DateTime startTime = System.DateTime.Now;
	//TODO: betrügen möglich mittels Pause
	//TODO: wird das irgendwo neu gesetzt bei einem neuem Spiel?

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
	GButton g_menu;
	Vector2 pos_menu;

	double size_text = 0.3;

	Glyph g_brains;
	Vector2 pos_brains;

	Glyph g_alive;
	Vector2 pos_alive;

	void Start(){

		Action<GButton> a_menu = (b) => {
			Brains = 0;
			//Nachrichtenwarteschlange leeren
			MessageDispatcher.I.EmptyQueue();
			//Hauptmenü laden
			Application.LoadLevel(0);
		};

		g_title = GString.GetString("Game Over");

		g_left = new GImage(Resource.Texture["love_left"]);
		g_right = new GImage(Resource.Texture["love_right"]);

		g_menu = new GButton(250, 40, GString.GetString("Zum Hauptmenü"), a_menu);
		g_menu.Padding.all = 10.0;
		g_menu.Border.all = 4.0;

		//Zeit überlebt
		TimeSpan alive = System.DateTime.Now.Subtract(startTime);
		g_alive = GString.GetString("Zeit: "+alive.Hours+"h "+alive.Minutes+"m "+alive.Seconds+"s");

		//Anzahl Gehirne
		g_brains = GString.GetString(Brains+ (Brains != 1 ? " Brainz erbeutet" : " Brain erbeutet"));
	}



	void Resize(){
		if(sWidth != Screen.width || sHeight != Screen.height){
			sWidth = Screen.width;
			sHeight = Screen.height;
			
			double aspect = (sWidth / sHeight) / (1680/1050);
			s = (sHeight / 1050) * aspect;

			pos_title = new Vector2(
				(float)((sWidth - g_title.Width(size_title * s))/2.0),
				(float)( 100.0 * s )
			);

			pos_left = new Vector2(
				(float)( 20.0 * s ),
				(float)(sHeight - 20.0*s - g_left.Height(size_img * s))
			);

			pos_right = new Vector2(
				(float)(sWidth - 20.0*s - g_left.Width(size_img * s)),
				(float)(sHeight - 20.0*s - g_left.Height(size_img * s))
				);

			double height = g_brains.Height(size_text * s) / 2.0;
			pos_brains = new Vector2(
				(float)((sWidth - g_brains.Width(size_text * s))/2.0),
				(float)(sHeight/2.0  - height)
				);
			height += 20.0 * s;

			pos_alive = new Vector2(
				(float)((sWidth - g_alive.Width(size_text * s))/2.0),
				(float)(sHeight/2.0 + height)
				);
			height += g_alive.Height(size_text * s) + 20.0*s;

			pos_menu = new Vector2(
				(float)((sWidth - g_menu.Width(size_button * s))/2.0),
				(float)(sHeight/2.0 + height)
				);
		}
		
	}



	void OnGUI(){
		Resize();

		//Game Over
		g_title.Draw(size_title * s, pos_title);

		//Bilder
		g_left.Draw(size_img * s, pos_left);
		g_right.Draw(size_img * s, pos_right);

		//Text
		g_brains.Draw(size_text * s, pos_brains);
		g_alive.Draw(size_text * s, pos_alive);

		//Hauptmenü
		g_menu.Draw(size_button * s, pos_menu);
	}


}
