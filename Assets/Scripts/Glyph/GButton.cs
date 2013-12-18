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

		//transparenter Button
		Color tmp = GUI.backgroundColor;
		GUI.backgroundColor = Color.clear;
		bool pressed = GUI.Button(
			new Rect(pos.x, pos.y, (float)Width(size), (float)Height(size)),
			new GUIContent("", name)
		);
		GUI.backgroundColor = tmp;

		//if Mouseover
		if(GUI.tooltip == name){
			gFill.color = Color.grey;
			gBorder.Enabled = true;
		} else {
			gFill.color = Color.white;
			gBorder.Enabled = false;
		}


		//Button zeichnen
		gFill.Draw(size, pos);

		//Aktion ausführen beim Click
		if(Enabled && pressed){
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
