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
	
	
	
	private Texture2D font;
	
	
	
	private float sWidth;
	
	
	
	private WhiteboaRdCL(){
		font = Resource.Texture["Font"];
		_width = (float)font.width / 26.0f;
		_height = (float)font.height / 4.0f;
		sWidth = _height / _width;
		_width *= sWidth;
	}
	
	
	
	private float _width;
	public override float Width(char c, float size){return _width * size;}
	
	
	
	private float _height;
	public override float Height(char c, float size){return _height * size;}
	
	
	
	public override void Draw(char c, float size, Vector2 pos){
		GUI.BeginGroup(new Rect(pos.x, pos.y, Width(c, size), Height(c, size)));
		GUI.DrawTexture(getCrop(c, size), font);
		GUI.EndGroup();
		//Debug.Log(getCrop(c, size) + " : " + Width(c, size) + "x" + Height(c, size) );
	}
	
	
	
	//HashMap
	private static Dictionary<char, Vector2> map = new Dictionary<char, Vector2>();
	
	
	
	private Vector2 getPos(char c){
		if(!map.ContainsKey(c)){
			if('A' <= c && c <= 'Z') map.Add(c, new Vector2((c-'A'), 0));
			else if('a' <= c && c <= 'z') map.Add(c, new Vector2((c-'a'), 1));
			else if('0' <= c && c <= '9') map.Add(c, new Vector2((c-'0'), 2));
			else map.Add(c, new Vector2(25, 3));
		}
		return map[c];
	}
	
	
	
	private static Dictionary<float, Dictionary<char,Rect>> sizemap = new Dictionary<float, Dictionary<char,Rect>>();
	
	
	
	private Rect getCrop(char c, float size){
		//SubMap erstellen wenn nicht vorhanden
		if(!sizemap.ContainsKey(size))
			sizemap.Add(size, new Dictionary<char, Rect>());
		
		//Char noch nicht vorhanden
		if(!sizemap[size].ContainsKey(c)){
			Vector2 v = getPos(c);
			Rect r = new Rect(-Width(c, size) * v.x, -Height(c, size) * v.y, font.width*sWidth*size, font.height*size);
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
