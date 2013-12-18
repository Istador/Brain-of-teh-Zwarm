using UnityEngine;
using System.Collections;

public class PlayerGUI : MonoBehaviour {

	float sHeight;
	float sWidth;
	float s;

	Glyph g_bl;
	Vector2 pos_bl;
	float size_bl = 0.5f;

	void Start(){
		Glyph g_int = new GInteger(() => {
			if(PlayerObject.I == null) return null;
			else return PlayerObject.I.Brains;
		});
		Glyph g_brainz = GString.GetString(" Brainz ");
		Glyph g_hp = new GHealthBar(150, 40, 3, ()=>PlayerObject.I);

		g_bl = GString.Concat(g_int, g_brainz, g_hp);
	}
	
	void Resize(){
		if(sWidth != Screen.width || sHeight != Screen.height){
			sWidth = Screen.width;
			sHeight = Screen.height;
			
			float aspect = (sWidth / sHeight) / (1680f/1050f);
			s = (sHeight / 1050f) * aspect;

			pos_bl = new Vector2(
				(float)(sWidth - g_bl.Width(size_bl * s) - 10f*s),
				(float)(sHeight - g_bl.Height(size_bl * s) - 10f*s)
				);
		}
		
	}

	
	void OnGUI(){
		Resize();
		//"x Brainz"
		g_bl.Draw(size_bl * s, pos_bl);
	}
	
}
