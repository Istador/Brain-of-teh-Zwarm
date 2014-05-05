using UnityEngine;
using System.Collections.Generic;

public class GFilled : Glyph {


	public bool Enabled = true;



	public Color color;



	private Glyph g;



	public GFilled(Color c, Glyph g){
		this.color = c;
		this.g = g;
	}



	public void Draw(double size, Vector2 pos){
		if(Enabled){
			Rect r = new Rect(pos.x, pos.y, (float)Width(size), (float)Height(size));
			Utility.DrawRectangle(r, color);
		}
		g.Draw(size, pos);
	}


	
	public double Width(double size){
		return g.Width(size);
	}



	public double Height(double size){
		return g.Height(size);
	}


}
