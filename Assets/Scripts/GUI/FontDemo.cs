using UnityEngine;
using System.Collections;

public class FontDemo : MonoBehaviour {

	Glyph g_title;
	Vector2 pos_title;
	double size_title = 1.0f;

	Glyph g_upper;
	Vector2 pos_upper;
	Glyph g_lower;
	Vector2 pos_lower;
	Glyph g_numeric;
	Vector2 pos_numeric;

	double size_chars = 0.7f;

	double sHeight;
	double sWidth;
	double s;
	
	void Start(){
		g_title = GString.GetString("Brain of teh Zwarm");
		g_upper = GString.GetString("ABCDEFGHIJKLMNOPQRSTUVWZYX");
		g_lower = GString.GetString("abcdefghijklmnopqrstuvwxyz");
		g_numeric = GString.GetString("0123456789");
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

			pos_upper = new Vector2(10.0f, 10.0f);
			pos_lower = pos_upper + new Vector2(0.0f, (float)(10.0 +  g_upper.Height(size_chars * s)));
			pos_numeric = new Vector2(10.0f, (float)(sHeight - 10.0 -  g_numeric.Height(size_chars * s)));
		}

	}
	
	void OnGUI(){
		Resize();

		//"Brain of teh Zwarm"
		g_title.Draw(size_title * s, pos_title);

		g_upper.Draw(size_chars * s, pos_upper);
		g_lower.Draw(size_chars * s, pos_lower);
		g_numeric.Draw(size_chars * s, pos_numeric);
	}

}
