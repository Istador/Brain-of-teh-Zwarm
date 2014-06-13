﻿using UnityEngine;
using System.Collections;

public class PlayerGUI : MonoBehaviour {

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
		//Bottom Left
		Glyph g_run_img = new GImage(Resource.Texture["buttons/actionbutton_run"]);
		Glyph g_druck_img = new GImage(Resource.Texture["buttons/actionbutton_druckwelle"]);
		GButton g_run = new GButton(40*3, 40*3, g_run_img, null);
		GButton g_druck = new GButton(40*3, 40*3, g_druck_img, null);
		//transparenter Hintergrund
		g_run.Filled = false;
		g_druck.Filled = false;
		//ausschalten weil Button vorerst ohne Funktion
		g_druck.Enabled = false;
		//alle zusammensetzen zu einem Glyph
		g_bl = GConcat.Concat(g_run, g_druck);

		//Bottom Right
		Glyph g_int = new GInteger(() => {
			if(PlayerObject.I == null) return 0;
			else return PlayerObject.I.Brains;
		});
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
	
	void Resize(){
		if(sWidth != Screen.width || sHeight != Screen.height){
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
