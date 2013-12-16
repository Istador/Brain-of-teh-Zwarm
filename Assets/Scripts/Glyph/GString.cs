using UnityEngine;
using System.Collections.Generic;

// 
// Composite Pattern
// 

public class GString : Glyph {
	
	
	
	private List<Glyph> str = new List<Glyph>();



	private GString(params Glyph[] gls){
		foreach(Glyph g in gls)
			this.str.Add(g);
	}
	
	
	private GString(string str){
		foreach(char c in str)
			this.str.Add(GCharacter.GetCharacter(c));
	}
	
	
	
	public void Draw(double size, Vector2 pos){
		foreach(Glyph g in str){
			double w = g.Width(size);
			g.Draw(size, pos);
			pos = new Vector2((float)(pos.x + w), pos.y);
		}
	}
	
	
	
	public double Width(double size){
		double w = 0.0;
		foreach(Glyph g in str)
			w += g.Width(size);
		return w;
	}
	
	
	
	public double Height(double size){
		double h = 0.0;
		foreach(Glyph g in str){
			double nh = g.Height(size);
			if(nh > h) h = nh;
		}
		return h;
	}
	
	
	
	//HashMap
	private static Dictionary<string, GString> map = new Dictionary<string, GString>();
	
	
	
	//Factory Method
	public static GString GetString(string str){
		if(!map.ContainsKey(str))
			map.Add(str, new GString(str));
		return map[str];
	}

	public static GString Concat(params Glyph[] glyphs){
		return new GString(glyphs);
	}
	
	
	
}