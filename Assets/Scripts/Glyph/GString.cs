using UnityEngine;
using System.Collections.Generic;

// 
// Composite Pattern
// 

public class GString : GConcat {
	
	

	public readonly string String;



	private GString(string str) : base(FromString(str)) {
		this.String = str;
	}



	private static Glyph[] FromString(string str){
		Glyph[] gs = new Glyph[str.Length];
		
		for(int i=0; i<str.Length; i++)
			gs[i] = GCharacter.GetCharacter(str[i]);

		return gs;
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