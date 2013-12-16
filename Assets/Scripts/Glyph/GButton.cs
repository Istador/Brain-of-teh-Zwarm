using UnityEngine;
using System;
using System.Collections.Generic;

/// 
/// GUI Button that uses a Glyph as Text
/// 
public class GButton : Glyph {

	private double maxWidth;
	private double maxHeight;

	private Action<GButton> action;
	private Glyph g;

	//in pixeln:
	public double Margin = 0f; //außen
	public double Border = 1f; //rand
	public double Padding = 1f; //innen

	public GButton(double width, double height, Glyph g, Action<GButton> action){
		this.maxWidth = width;
		this.maxHeight = height;
		this.action = action;
		this.g = g;
	}

	private Dictionary<double, object[]> cache = new Dictionary<double, object[]>();

	public void Draw(double size, Vector2 pos){
		if(!cache.ContainsKey(size)){
			double rand = (Margin+Border+Padding)*size;

			//Ausmaße des Content Bereichs
			double contWidth = Width(size) - 2f*rand;
			double contHeight = Height(size) - 2f*rand;
			 
			double corrSize = size;

			//width not bigger than max width
			double width = g.Width(corrSize);
			if(width > contWidth)
				corrSize *= contWidth/width;

			//height not bigger than max height
			double height = g.Height(corrSize);
			if(height > contHeight)
				corrSize *= contHeight/height;

			//Endwerte
			width = g.Width(corrSize);
			height = g.Height(corrSize);

			//mittig ausrichten
			float left = (float)(rand + (contWidth - width)/2.0 );
			float top = (float)(rand + (contHeight - height)/2.0 );

			cache.Add(size, new object[]{corrSize, new Vector2(left, top)});
		}

		Utility.DrawRectangle(new Rect(pos.x, pos.y, (float)Width(size), (float)Height(size)), Color.green);

		//zeichnen
		object[] c = cache[size];
		g.Draw((double)c[0], pos + (Vector2)c[1]);

	}

	public double Width(double size){
		return maxWidth * size;
	}

	public double Height(double size){
		return maxHeight * size;
	}

}
