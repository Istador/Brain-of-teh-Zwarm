using UnityEngine;
using System.Collections.Generic;

public class GConcat : Glyph {
	
	
	
	private List<Glyph> glyphs = new List<Glyph>();
	


	protected GConcat(params Glyph[] gls){
		foreach(Glyph g in gls)
			this.glyphs.Add(g);
	}
	
	
	
	public void Draw(double size, Vector2 pos){
		foreach(Glyph g in glyphs){
			double w = g.Width(size);
			g.Draw(size, pos);
			pos = new Vector2((float)(pos.x + w), pos.y);
		}
	}
	
	
	
	public double Width(double size){
		double w = 0.0;
		foreach(Glyph g in glyphs)
			w += g.Width(size);
		return w;
	}
	
	
	
	public double Height(double size){
		double h = 0.0;
		foreach(Glyph g in glyphs){
			double nh = g.Height(size);
			if(nh > h) h = nh;
		}
		return h;
	}


	
	public static GConcat Concat(params Glyph[] glyphs){
		return new GConcat(glyphs);
	}



}
