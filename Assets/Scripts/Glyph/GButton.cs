using UnityEngine;
using System;
using System.Collections.Generic;

/// 
/// GUI Button that uses a Glyph as Text
/// 
public class GButton : Glyph {



	private static int buttons = 0;
	private int id;
	private String name;



	private Action<GButton> action;

	private GFilled gFill;
	private GBordered gBorder;
	private GLimited gLimit;
	private Glyph gContent;

	public Position Padding {get{return gBorder.Padding;}}
	public Position Border {get{return gBorder.Border;}}
	public Position Margin {get{return gBorder.Margin;}}

	public bool Filled {
		get{return gFill.Enabled;}
		set{gFill.Enabled = value;}
	}

	public bool Enabled = true;

	public GButton(double width, double height, Glyph g, Action<GButton> action){
		this.action = action;

		id = buttons++;
		name = "GButton"+id;

		gContent = g;
		gLimit = new GLimited(width, height, gContent);
		gBorder = new GBordered(gLimit);
		gFill = new GFilled(Color.white, gBorder);

		gBorder.Enabled = false;
	}

	public void Draw(double size, Vector2 pos){

		//transparenter Button (zum anklicken)
		Color tmp = GUI.backgroundColor;
		GUI.backgroundColor = Color.clear;
		bool pressed = GUI.Button(
			new Rect(pos.x, pos.y, (float)Width(size), (float)Height(size)),
			new GUIContent("", name)
		);
		GUI.backgroundColor = tmp;

		//Button zeichnen
		if(Enabled && GUI.tooltip == name){ //Mouseover
			//ausgrauen
			Color old = GUI.color;
			GUI.color = new Color(old.r * 0.5f, old.g * 0.5f, old.b * 0.5f);
			//Rand zeichnen
			gBorder.Enabled = true;
			//Zeichnen
			gFill.Draw(size, pos);
			//Rand zukünftig nicht zeichnen
			gBorder.Enabled = false;
			//Ausgrauen deaktivieren
			GUI.color = old;
		} else if(Enabled){ //Aktiviert
			gFill.Draw(size, pos);
		} else { //Deaktiviert
			//Transparent zeichnen
			Color old = GUI.color;
			GUI.color = new Color(old.r, old.g, old.b, 0.5f);
			gFill.Draw(size, pos);
			GUI.color = old;
		}

		//Aktion ausführen beim Click
		if(Enabled && pressed && action != null){
		//	Enabled = false;
			action(this);
		}
	}

	public double Width(double size){
		return gFill.Width(size);
	}

	public double Height(double size){
		return gFill.Height(size);
	}

}
