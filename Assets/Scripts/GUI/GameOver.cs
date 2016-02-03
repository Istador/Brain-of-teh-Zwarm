using UnityEngine;
using System;

public class GameOver: GUIMenu {

	public static int Brains = 0;


	private Glyph _g_main;
	protected override Glyph g_main { get{ return _g_main; } }
	protected override string str_title { get{ return "Game Over"; } }


	private static GButton g_mymenu = create_g_to_menu((b) => { Brains = 0; });


	protected override void Start(){
		base.Start();

		//Zeit überlebt
		TimeSpan rt = Pause.Runtime;
		Glyph g_alive = GString.GetString("Zeit: "+Utility.TimeToString(rt), size_text);

		//Anzahl Gehirne
		Glyph g_brains = GString.GetString(Brains+ (Brains != 1 ? " Brainz erbeutet" : " Brain erbeutet"), size_text);

		_g_main = GVConcat.Concat(g_brains, g_alive, g_mymenu)
			.Align(HorAlignment.center)
			.Space(40.0)
		;

		Highscores.Add(Brains, rt);
	}

}
