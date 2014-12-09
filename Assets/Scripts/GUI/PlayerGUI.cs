using UnityEngine;
using System;
using System.Collections;

public class PlayerGUI : MonoBehaviour, IObserver {

	float sHeight;
	float sWidth;
	float s;

	//Bottom left
	Glyph g_bl;
	Vector2 pos_bl;
	float size_bl = 0.5f;

	//Bottom right
	Glyph g_br;
	Vector2 pos_br;
	float size_br = 0.5f;


	void Start(){
		Action<bool> a1 = (start) => {
			//beim ausführen erhöhen, danach verringern
			float change = start ? +1f : -1f;
			//Player beschleunigen
			PlayerObject.I.SpeedBonus += change;
			//Zombies beschleunigen
			foreach(var zombie in PlayerObject.I.Zombies)
				if(zombie != null)
					zombie.SpeedBonus += change;
		};

		Action<bool> a2 = (start) => {
			if(start && PlayerObject.I != null && !PlayerObject.I.IsDead){
				//Spieler und seinen Schwarm heilen
				PlayerObject.I.ApplyHeal(1000);
				foreach(var z in PlayerObject.I.Zombies)
					if(z != null && !z.IsDead)
						z.ApplyHeal(1000);
			}
		};

		Action<bool> a3 = (start) => {
			//TODO: Aktion Druckwelle - Nahe Gegner umwerfen
		};

		//Bottom Left
		GActionButton g_run = new GActionButton("Aktion 1", "buttons/actionbutton_run", a1, 1, 10f, 20f);
		GActionButton g_heal = new GActionButton("Aktion 2", "buttons/actionbutton_health", a2, 1, 0f, 30f);
		GActionButton g_druck = new GActionButton("Aktion 3", "buttons/actionbutton_druckwelle", a3, 0, 0f, 5f);
		//alle zusammensetzen zu einem Glyph
		g_bl = GConcat.Concat(g_run, g_heal, g_druck);

		//Bottom Right
		Observer.I.Add("Brains", this);
		Glyph g_int = new GInteger("Brains");
		Glyph g_brainz = GString.GetString(" Brainz ");
		Glyph g_hp = new GHealthBar(150, 40, 3, ()=>PlayerObject.I);
		//alle 3 zusammensetzen, einen unsichtbaren Rahmen drum zeichnen fürs (Padding)
		GBordered g = new GBordered(GConcat.Concat(g_int, g_brainz, g_hp));
		g.Enabled = false;
		g.Border.all = 0.0;
		g.Padding.all = 10.0;
		//weiße Hintergrundfarve
		g_br = new GFilled(Color.white, g);
	}
	
	private bool resizeNeeded = false;
	
	public void ObserveUpdate(string msg, object x){
		if(msg != "Brains") return;
		resizeNeeded = true;
	}
	
	void Resize(){
		if(sWidth != Screen.width || sHeight != Screen.height || resizeNeeded){
			ResizeNow();
			resizeNeeded = false;
		}
	}
	
	private void ResizeNow(){
		sWidth = Screen.width;
		sHeight = Screen.height;
			
		float aspect = (sWidth / sHeight) / (1680f/1050f);
		s = (sHeight / 1050f) * aspect;

		pos_bl = new Vector2(
			0f,
			(float)(sHeight - g_bl.Height(size_bl * s))
		);

		pos_br = new Vector2(
			(float)(sWidth - g_br.Width(size_br * s)),
			(float)(sHeight - g_br.Height(size_br * s))
		);
	}
	
	void OnGUI(){
		//Spiel ist nicht pausiert
		if(Time.timeScale != 0.0f){
			Resize();

			//Bottom Left
			g_bl.Draw(size_bl * s, pos_bl);

			//Bottom Right: "x Brainz"
			g_br.Draw(size_br * s, pos_br);
		}
	}
	
}
