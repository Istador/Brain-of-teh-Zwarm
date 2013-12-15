using UnityEngine;
using System.Collections;
using System;


public class PlayerHealthBar : MonoBehaviour {
	/*
	Glyph hb;

	void Start(){
		hb = new GHealthBar(150, 40, ()=>PlayerObject.I);
	}

	void OnGUI(){
		hb.Draw(1f, new Vector2(0f, 0f));
	}
	*/
}

public class GHealthBar : Glyph {


	public GHealthBar(float width, float height, float size, Func<Entity> entity){
		_width = width;
		_height = height;
		basicsize = size;
		this.entity = entity;
	}

	float basicsize;

	float _width;
	public float Width(float size){return _width * basicsize * size;}
	float _height;
	public float Height(float size){return _height * basicsize * size;}
	
	/// <summary>
	/// Referenz auf den Spieler von dem die HP angezeigt wird
	/// </summary>
	Func<Entity> entity;
	

	
	
	
	//konstante Farben

	/// <summary>
	/// Rand des Lebensbalkens
	/// </summary>
	private static Color cBox = Color.black;
	/// <summary>
	/// Hintergrund, für Leben das der Boss bereits verloren hat
	/// </summary>
	private static Color cBar = Color.white;
	
	/// <summary>
	/// Lebensbalken Farbe
	/// </summary>
	private static Color cHP = Color.black;

	//Variablen fürs Smoothes Verringern der HP
	
	private float hp_lerp = 1.0f;
	private float last_width = 0.0f;
	
	
	
	//GUI-Zeichnen
	public void Draw(float size, Vector2 pos){

		if(entity() != null){

			float height = Height(size);
			float width = Width(size);
			float left = pos.x;
			float top = pos.y;
		

			float px1 = Mathf.CeilToInt(1f * size * basicsize);
			float px2 = Mathf.CeilToInt(2f * size * basicsize);
			float px4 = Mathf.CeilToInt(4f * size * basicsize);

			//Positionen/Breiten/Höhen der Rechtecke berechnen
			Rect ra = new Rect(left, top, width, height);
			Rect rb = new Rect(left + px1, top + px1, width - px2, height - px2);
			Rect rc = new Rect(left + px2, top + px2, width - px4, height - px4);
		
		
			//Smoothes HP verringern
		
			//Breite des aktuellen HP-Wertes
			float new_width = Mathf.RoundToInt((float)rc.width * entity().HealthFactor);
		
			//keine HP-Änderung
			if(new_width >= last_width){
				rc.width = new_width;
				last_width = new_width;
				hp_lerp = 1.0f;
			}
			//HP hat sich verändert
			else{
				hp_lerp -= 0.025f;
				Utility.MinMax(ref hp_lerp, 0.0f, 1.0f);
				rc.width = Mathf.Lerp(new_width, last_width, hp_lerp);
				if(hp_lerp <= 0.0f) last_width = new_width;
			}
	
			//Zeichnen
			Utility.DrawRectangle(ra, cBox);
			Utility.DrawRectangle(rb, cBar);
			if(entity().HealthFactor > 0.0f)
				Utility.DrawRectangle(rc, cHP);
		}
	}
	
	
	
}
