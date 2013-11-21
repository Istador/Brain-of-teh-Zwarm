using UnityEngine;
using System.Collections.Generic;

// 
// Flyweight Pattern
// 



public interface Glyph {
	void Draw(float size, Vector2 pos);
	float Width(float size);
	float Height(float size);
}



public class GCharacter : Glyph {
	
	
	
	private char c;
	
	
	
	private GCharacter(char c){
		this.c = c;
	}
	
	
	
	public void Draw(float size, Vector2 pos){
		AbstractFont.Selected.Draw(c, size, pos);
	}
	
	
	
	public float Width(float size){return AbstractFont.Selected.Width(c, size);}
	
	
	
	public float Height(float size){return AbstractFont.Selected.Height(c, size);}
	
	
	
	//HashMap
	private static Dictionary<char, GCharacter> map = new Dictionary<char, GCharacter>();
	
	
	
	//Factory Method
	public static GCharacter GetCharacter(char c){
		if(!map.ContainsKey(c))
			map.Add(c, new GCharacter(c));
		return map[c];
	}
	
	
	
}



// 
// Composite Pattern
// 
public class GString : Glyph {
	
	
	
	private List<GCharacter> str = new List<GCharacter>();
	
	
	
	private GString(string str){
		foreach(char c in str)
			this.str.Add(GCharacter.GetCharacter(c));
	}
	
	
	
	public void Draw(float size, Vector2 pos){
		foreach(GCharacter c in str){
			c.Draw(size, pos);
			pos = new Vector2(pos.x + c.Width(size), pos.y);
		}
	}
	
	
	
	public float Width(float size){
		float w = 0.0f;
		foreach(GCharacter c in str)
			w += c.Width(size);
		return w;
	}
	
	
	
	public float Height(float size){
		float h = 0.0f;
		foreach(GCharacter c in str){
			float nh = c.Height(size);
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
	
	
	
}



public abstract class AbstractFont {
	public abstract float Width(char c, float size);
	public abstract float Height(char c, float size);
	public abstract void Draw(char c, float size, Vector2 pos);
	
	public static AbstractFont Selected = WhiteboaRdCL.I;
}



public class WhiteboaRdCL : AbstractFont {
	
	//abstand zwischen einzelnen Zeichen
	private const float charSpacing = 1.0f; // 1 pixel
	//pixel um die sich die Breite von einzelnen Zeichen erhöht
	private const float widthSpacing = charSpacing * 2.0f;
	
	
	
	private Texture2D font;
	
	
	
	private float sWidth;
	
	

	

	
	//offset from left to the start of character
	//calculated by: offset[px] / (image width of one char-field)[px] = off / 150
	private float[] charoffset = {
		0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f,		// A-J
		0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f,		// K-T
		0f, 0f, 0f, 0f, 0f, 0f,						// U-Z
		
		0.24000000f, 0.30666667f, 0.28000000f, 0.26666667f, 0.30000000f, 
		0.28000000f, 0.28666667f, 0.22000000f, 0.32666667f, 0.18666667f, 
		0.28666667f, 0.32000000f, 0.02000000f, 0.16666667f, 0.22000000f, 
		0.24666667f, 0.26000000f, 0.16666667f, 0.27333333f, 0.22666667f, 
		0.22000000f, 0.24666667f, 0.11333333f, 0.28000000f, 0.27333333f, 
		0.24666667f, 
		
		0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f,		// 0-9, ...
		0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f,		// ...
		0f, 0f, 0f, 0f, 0f, 0f,						// ...
		
		0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f,		// ...
		0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f,		// ...
		0f, 0f, 0f, 0f, 0f, 0f,						// ..., Space
	};
	
	//width of char without whitespace
	//calculated by: 1 - offset[px] / (image width of one char-field)[px] * 2 = 1 - off / 75
	private float[] charscale = {
		1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f,		// A-J
		1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 	// K-T
		1f, 1f, 1f, 1f, 1f, 1f,						// U-Z
		
		0.52000000f, 0.38666667f, 0.44000000f, 0.46666667f, 0.40000000f, // a-e
		0.44000000f, 0.42666667f, 0.56000000f, 0.34666667f, 0.62666667f, // f-j
		0.42666667f, 0.36000000f, 0.96000000f, 0.66666667f, 0.56000000f, // k-o
		0.50666667f, 0.48000000f, 0.66666667f, 0.45333333f, 0.54666667f, // p-t
		0.56000000f, 0.50666667f, 0.77333333f, 0.44000000f, 0.45333333f, // u-y
		0.50666667f,													 // x
		
		1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 	// 0-9, ...
		1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 	// ...
		1f, 1f, 1f, 1f, 1f, 1f,						// ...
		
		1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 	// ...
		1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 	// ...
		1f, 1f, 1f, 1f, 1f, 1f						// ..., Space
	};
	
	
	
	//to store char vectors, defining their offsets on the image
	private Vector2[] pos = new Vector2[4*26];
	
	
	
	private WhiteboaRdCL(){
		font = Resource.Texture["Font"];
		_width = (float)font.width / 26.0f;
		_height = (float)font.height / 4.0f;
		sWidth = _height / _width;
		_width *= sWidth;
		
		//Positionen initialisieren
		// 'A' = (0,0), 'B' = (1,0), ..., 'Z' = (25,0)
		// 'a' = (0,1), 'b' = (1,1), ..., 'z' = (25,1)
		// '0' = (0,2), '1' = (1,2), ..., '9' = (9,2)
		for(int i=0; i<4; i++) for(int j=0; j<26; j++)
			pos[i*26+j] = new Vector2(j, i);
	}
	
	
	
	private float _width;
	private float Width(float size){return _width * size;}
	private float Width(int index, float size){return Width(size) * charscale[index] + widthSpacing;}
	public override float Width(char c, float size){return Width(index(c), size);}
	
	
	
	private float _height;
	private float Height(float size){return _height * size;}
	private float Height(int index, float size){return Height(size);}
	public override float Height(char c, float size){return Height(index(c), size);}
	
	
	
	public override void Draw(char c, float size, Vector2 pos){
		int i = index(c);
		GUI.BeginGroup(new Rect(pos.x, pos.y, Width(i, size), Height(i, size)));
		GUI.DrawTexture(getCrop(i, size), font);
		GUI.EndGroup();
		//Debug.Log(getCrop(c, size) + " : " + Width(c, size) + "x" + Height(c, size) );
	}
	
	
	private int index(char c){
		if('A' <= c && c <= 'Z') return (c-'A');
		if('a' <= c && c <= 'z') return 26+(c-'a');
		if('0' <= c && c <= '9') return 2*26+(c-'0');
		else return 4*26-1;
	}
	
	
	private Vector2 getPos(int index){
		return pos[index];
	}
	
	private Vector2 getPos(char c){
		return pos[index(c)];
	}
	
	
	
	private static Dictionary<float, Dictionary<int,Rect>> sizemap = new Dictionary<float, Dictionary<int,Rect>>();
	
	
	
	private Rect getCrop(int c, float size){
		//SubMap erstellen wenn nicht vorhanden
		if(!sizemap.ContainsKey(size))
			sizemap.Add(size, new Dictionary<int, Rect>());
		
		//Char noch nicht vorhanden
		if(!sizemap[size].ContainsKey(c)){
			Vector2 v = getPos(c);
			Rect r = new Rect(Width(size) * (-v.x - charoffset[c]) + charSpacing, -Height(size) * v.y, font.width*sWidth*size, font.height*size);
			sizemap[size].Add(c, r);
		}
		
		return sizemap[size][c];
	}
	
	
	
	/**
	 * Singleton
	*/
	private static WhiteboaRdCL instance = null;
	public static WhiteboaRdCL Instance{get{
			if(instance == null)
				instance = new WhiteboaRdCL();
			return instance;
		}}
	public static WhiteboaRdCL I{get{return Instance;}}
}
