using UnityEngine;
using System.Collections.Generic;

// 
// Composite Pattern
// 

public class GString : GConcat {
	
	

	public readonly string String;



	private GString(string str, double size) : base(FromString(str)) {
		this.String = str;
		this.Size = size;
	}



	private static Glyph[] FromString(string str){
		Glyph[] gs = new Glyph[str.Length];
		
		for(int i=0; i<str.Length; i++)
			gs[i] = GCharacter.GetCharacter(str[i]);

		return gs;
	}



	//HashMap
	private static Dictionary<string, Dictionary<double, GString>> map = new Dictionary<string, Dictionary<double, GString>>();
	
	
	
	//Factory Method
	public static GString GetString(string str, double size = 1.0){
		if(! map.ContainsKey(str)){
			Dictionary<double, GString> s = new Dictionary<double, GString>();
			map.Add(str, s);
		}
		if(! map[str].ContainsKey(size)){
			map[str].Add(size, new GString(str, size));
		}
		return map[str][size];
	}


	
}