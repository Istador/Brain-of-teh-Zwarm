using UnityEngine;
using System.Collections;

public class FontDemo : MonoBehaviour {

	double size_title = 1.0f;
	Glyph g_title;
	Vector2 pos_title;

	double size_chars = 0.65f;
	Glyph g_upper;
	Vector2 pos_upper;
	Glyph g_lower;
	Vector2 pos_lower;
	Glyph g_numeric;
	Vector2 pos_numeric;

	Vector2 pos_user;
	GString g_user;

	double sHeight;
	double sWidth;
	double s;
	
	void Start(){
		g_title = GString.GetString("Brain of teh Zwarm");
		g_upper = GString.GetString("AÄBCDEFGHIJKLMNOÖPQRSßTUÜVWZYX");
		g_lower = GString.GetString("aäbcdefghijklmnoöpqrstuüvwxyz");
		g_numeric = GString.GetString("0123456789 .:,;!?+-_/\\(){}[] ♡");

		g_user = GString.GetString("");
	}

	void Resize(){
		if(sWidth != Screen.width || sHeight != Screen.height){
			sWidth = Screen.width;
			sHeight = Screen.height;

			double aspect = (sWidth / sHeight) / (1680.0/1050.0);
			s = (sHeight / 1050.0) * aspect;

			//Zentriert
			pos_title = new Vector2(
				(float)((sWidth - g_title.Width(size_title * s)) * 0.5), 
				(float)((sHeight - g_title.Height(size_title * s)) * 0.5)
			);

			ResizeUser();

			pos_upper = new Vector2(10.0f, 10.0f);
			pos_lower = pos_upper + new Vector2(0.0f, (float)(10.0 +  g_upper.Height(size_chars * s)));
			pos_numeric = new Vector2(10.0f, (float)(sHeight - 10.0 -  g_numeric.Height(size_chars * s)));
		}

	}

	void ResizeUser(){
		pos_user = new Vector2(
			(float)((sWidth - g_user.Width(size_chars * s)) * 0.5), 
			(float)(sHeight * 0.5 + g_title.Height(size_title * s))
			);
	}
	
	void OnGUI(){
		//wenn Spiel nicht pausiert
		if(Time.timeScale != 0.0f){
			Resize();

			//"Brain of teh Zwarm"
			g_title.Draw(size_title * s, pos_title);

			//Bwenutzereingabe
			g_user.Draw(size_chars * s, pos_user);

			g_upper.Draw(size_chars * s, pos_upper);
			g_lower.Draw(size_chars * s, pos_lower);
			g_numeric.Draw(size_chars * s, pos_numeric);
		}
	}


	void Update(){
		//wenn Spiel nicht pausiert
		if(Time.timeScale != 0.0f){
			string str = g_user.String;

			foreach(char c in Input.inputString) 
				// Backspace
				if(c == '\b') {
					//remove one char
					if(str.Length > 0)
						str = str.Substring(0, str.Length - 1);
				}
				// text input
				else str += c; //Append

			//Text hat sich geändert
			if(!str.Equals(g_user.String)){
				g_user = GString.GetString(str);
				ResizeUser();
			}
		}
	}

}
