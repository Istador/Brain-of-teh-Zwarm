using UnityEngine;
using System;
using System.Collections.Generic;

public class HighscoresMenu: GUIMenu {

	private Glyph _g_main;
	protected override Glyph g_main { get{ return _g_main; } }
	protected override string str_title { get{ return "Highscores"; } }

	protected override void Start(){
		base.Start();

		List<Glyph> g_l_times = Highscores.Times;
		if(g_l_times.Count == 0) { g_l_times.Add(GString.GetString("---", size_text)); }
		g_l_times.Insert(0, GString.GetString("Zeit", 0.4));
		Glyph g_times = GVConcat.Concat(g_l_times.ToArray())
			.Align(HorAlignment.center)
			.Space(30.0)
		;

		List<Glyph> g_l_brains = Highscores.Brains;
		if(g_l_brains.Count == 0) { g_l_brains.Add(GString.GetString("---", size_text)); }
		g_l_brains.Insert(0, GString.GetString("Brainz", 0.4));
		Glyph g_brains = GVConcat.Concat(g_l_brains.ToArray())
			.Align(HorAlignment.center)
			.Space(30.0)
		;
		
		Glyph g_scores = 
			GConcat.Concat(g_times, g_brains)
				.Space(100.0)
				.AutoAlign(HorAlignment.center)
		;
		_g_main = GVConcat.Concat(g_scores, g_menu)
			.Align(HorAlignment.center)
			.Space(40.0)
		;
	}


}
