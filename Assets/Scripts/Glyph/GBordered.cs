using UnityEngine;
using System.Collections.Generic;
using System;

public class Position {
	public double top, bottom, left, right;
	
	public Position(double forall){
		all = forall;
	}

	public double vertical {set{
			top = value;
			bottom = value;
		}}

	public double horizontal {set{
			left = value;
			right = value;
		}}

	public double all {set{
			top = value;
			bottom = value;
			left = value;
			right = value;
		}}
	
	public double Width { get{return left+right;} }
	public double Height { get{return top+bottom;} }
}

/// 
/// Draws a border arround the glyph
/// 
public class GBordered : Glyph {
	private Glyph g;

	public GBordered(Glyph g){
		this.g = g;
	}


	//in pixeln:
	public readonly Position Margin = new Position(0.0); //außen
	public readonly Position Border = new Position(1.0); //rand
	public readonly Position Padding = new Position(1.0); //innen

	public Color BorderColor = Color.black;

	public bool Enabled = true;


	private Dictionary<double, Rect[]> borderCache = new Dictionary<double, Rect[]>();

	private Rect RectFromDouble(double left, double top, double width, double height){
		return new Rect((float)left, (float)top, (float)width, (float)height);
	}

	private Rect Move(Rect r, Vector2 v){
		return new Rect(
			Mathf.Floor(r.x + v.x),
		    Mathf.Floor(r.y + v.y),
			r.width,
			r.height
		);
	}

	private void drawBorder(double size, Vector2 pos){
		if(!borderCache.ContainsKey(size)){
			double w = g.Width(size);
			double h = g.Height(size);

			double width = ((Border.left + Padding.left + Padding.right + Border.right)*size + w);
			double height = ((Border.top + Padding.top + Padding.bottom + Border.bottom)*size + h);

			double top = (Margin.top * size);
			double bottom = ((Margin.top + Border.top + Padding.top + Padding.bottom)*size + h);
			double left = (Margin.left * size);
			double right = ((Margin.left + Border.left + Padding.left + Padding.right)*size + w);

			Rect rTop = RectFromDouble(left, top, width, Border.top*size);
			Rect rBottom = RectFromDouble(left, bottom, width, Border.bottom*size);
			Rect rLeft = RectFromDouble(left, top, Border.left*size, height);
			Rect rRight = RectFromDouble(right, top, Border.right*size, height);

			borderCache.Add(size, new Rect[]{rTop, rBottom, rLeft, rRight});
		}

		foreach(Rect r in borderCache[size])
			Utility.DrawRectangle(Move(r,pos), BorderColor);
	}



	private Dictionary<double, Vector2> innerCache = new Dictionary<double, Vector2>();

	private Vector2 innerPos(double size){
		if(!innerCache.ContainsKey(size)){
			Vector2 pos = new Vector2(
				(float)(size * (Margin.left + Border.left + Padding.left)),
				(float)(size * (Margin.top + Border.top + Padding.top))
			);
			innerCache.Add(size, pos);
		}
		return innerCache[size];
	}



	public void Draw(double size, Vector2 pos){
		g.Draw(size, pos + innerPos(size));
		if(Enabled) drawBorder(size, pos);
	}



	public double Width(double size){
		return g.Width(size) + (Margin.Width + Border.Width + Padding.Width) * size;
	}



	public double Height(double size){
		return g.Height(size) + (Margin.Height + Border.Height + Padding.Height) * size;
	}

	public Rect Inside(double size, Vector2 pos){
		return new Rect(
			(float)( pos.x + Margin.left * size ),
			(float)( pos.y + Margin.top * size ),
			(float)( g.Width(size) + (Border.Width + Padding.Width) * size ), 
			(float)( g.Height(size) + (Border.Height + Padding.Height) * size )
		);
	}



}