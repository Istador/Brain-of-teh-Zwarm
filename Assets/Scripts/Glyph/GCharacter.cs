using UnityEngine;
using System.Collections.Generic;

// 
// Flyweight Pattern
// 

public class GCharacter : Glyph {
	
	
	
	private char c;
	
	
	
	private GCharacter(char c){
		this.c = c;
	}
	
	
	
	public void Draw(double size, Vector2 pos){
		AbstractFont.Selected.Draw(c, size, pos);
	}
	
	
	
	public double Width(double size){return AbstractFont.Selected.Width(c, size);}
	
	
	
	public double Height(double size){return AbstractFont.Selected.Height(c, size);}
	
	
	
	//HashMap
	private static Dictionary<char, GCharacter> map = new Dictionary<char, GCharacter>();
	
	
	
	//Factory Method
	public static GCharacter GetCharacter(char c){
		if(!map.ContainsKey(c))
			map.Add(c, new GCharacter(c));
		return map[c];
	}
	
	
	
}