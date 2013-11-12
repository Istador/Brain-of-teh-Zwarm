using UnityEngine;
using System.Collections;

public class PlayerGUI : MonoBehaviour {
	
	Glyph g_title;
	Vector2 pos_title;
	float size_title;
	
	void Start(){
		size_title = 0.2f;
		g_title = GString.GetString(" Brainz");
		pos_title = new Vector2(
			Screen.width - g_title.Width(size_title), 
			Screen.height - g_title.Height(size_title));
	}
	
	void OnGUI(){
		PlayerObject p = PlayerObject.I;
		
		//Player existiert
		if(p != null){
			//Anzahl gefundener Gehirne
			Glyph g = GString.GetString(p.Brains.ToString());
			Vector2 pos = new Vector2(pos_title.x - g.Width(size_title), pos_title.y);
			g.Draw(size_title, pos);
			
			//" Brainz"
			g_title.Draw(size_title, pos_title);
		}
	}
	
}
