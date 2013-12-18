using UnityEngine;
using System.Collections;

public class PlayerGUI : MonoBehaviour {

	float sHeight;
	float sWidth;
	float s;

	//Bottom right
	Glyph g_br;
	Vector2 pos_br;
	float size_br = 0.5f;


	void Start(){
		Glyph g_int = new GInteger(() => {
			if(PlayerObject.I == null) return null;
			else return PlayerObject.I.Brains;
		});
		Glyph g_brainz = GString.GetString(" Brainz ");
		Glyph g_hp = new GHealthBar(150, 40, 3, ()=>PlayerObject.I);

		GBordered g = new GBordered(GConcat.Concat(g_int, g_brainz, g_hp));
		g.Enabled = false;
		g.Border.all = 0.0;
		g.Padding.all = 10.0;
		g_br = new GFilled(Color.white, g);
	}
	
	void Resize(){
		if(sWidth != Screen.width || sHeight != Screen.height){
			sWidth = Screen.width;
			sHeight = Screen.height;
			
			float aspect = (sWidth / sHeight) / (1680f/1050f);
			s = (sHeight / 1050f) * aspect;

			pos_br = new Vector2(
				(float)(sWidth - g_br.Width(size_br * s)),
				(float)(sHeight - g_br.Height(size_br * s))
				);
		}
		
	}

	
	void OnGUI(){
		//Spiel ist nicht pausiert
		if(Time.timeScale != 0.0f){
			Resize();
			//"x Brainz"
			g_br.Draw(size_br * s, pos_br);
		}
	}
	
}
