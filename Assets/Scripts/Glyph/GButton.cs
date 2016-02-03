using UnityEngine;
using System;
using System.Collections.Generic;

/// 
/// GUI Button that uses a Glyph as Text
/// 
public class GButton : Glyph {



	private static int buttons = 0;
	private int id;
	private String name; //für den Mouseover-Effekt



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

	public double Size { get; set;}

	public GButton(Glyph g, Action<GButton> action){
		Size = 1.0;
		this.action = action;

		id = buttons++;
		name = "GButton"+id;

		gContent = g;
		gBorder = new GBordered(gContent);
		gFill = new GFilled(Color.white, gBorder);

		gBorder.Enabled = false;
	}

	public GButton(double width, double height, Glyph g, Action<GButton> action){
		Size = 1.0;
		this.action = action;

		id = buttons++;
		name = "GButton"+id;

		gContent = g;
		gLimit = new GLimited(gContent, width, height);
		gBorder = new GBordered(gLimit);
		gFill = new GFilled(Color.white, gBorder);

		gBorder.Enabled = false;
	}

	public void Draw(double size, Vector2 pos){
		double s = size * Size;

		//transparenter Button (zum anklicken)
		Color tmp = GUI.backgroundColor;
		GUI.backgroundColor = Color.clear;
		bool pressed = GUI.Button(
			gBorder.Inside(s, pos),
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
			gFill.Draw(s, pos);
			//Rand zukünftig nicht zeichnen
			gBorder.Enabled = false;
			//Ausgrauen deaktivieren
			GUI.color = old;
		} else if(Enabled){ //Aktiviert
			gFill.Draw(s, pos);
		} else { //Deaktiviert
			//Transparent zeichnen
			Color old = GUI.color;
			GUI.color = new Color(old.r, old.g, old.b, 0.5f);
			gFill.Draw(s, pos);
			GUI.color = old;
		}

		//Aktion ausführen beim Click
		if(Enabled && pressed && action != null){
		//	Enabled = false;
			action(this);
		}
	}

	public double Width(double size){ return gFill.Width(size * Size); }

	public double Height(double size){ return gFill.Height(size * Size); }

}
