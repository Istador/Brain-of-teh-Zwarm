using UnityEngine;
using System;

public class Credits: GUIMenu {
	private static readonly double size_subtitle = 0.30;
	private static readonly double size_ava = 0.35;


	private Glyph _g_main;
	protected override Glyph g_main { get{ return _g_main; } }
	protected override string str_title { get{ return "Credits"; } }


	protected override void Start() {
		base.Start();

		Action<GButton> a_www = (b) => {
			Application.OpenURL("https://rcl.blackpinguin.de/");
		};


		GButton g_www = new GButton(GString.GetString("https://rcl.blackpinguin.de/", size_text), a_www);
		g_www.Border.all = 0.0;
		g_www.Border.bottom = 5.0;
		g_www.Filled = false;

		GImage g_ava = new GImage(Resource.Texture["avatar"], size_ava);
		Glyph g_desc =
			GVConcat.Concat(
				GString.GetString("Robin Christopher Ladiges", size_text * 1.2)
				, g_www
				, GString.GetString("(c) 2013 - 2016", size_text * 0.8)
			)
			.Align(HorAlignment.left)
			.Space(20.0)
		;

		Glyph g_me = GConcat.Concat(g_ava, g_desc).Align(VertAlignment.middle).Space(20.0);
		_g_main =
			GVConcat.Concat(
				GString.GetString("This game was made by:", size_subtitle),
				g_me,
				g_menu
			)
			.Align(HorAlignment.center)
			.Space(40.0)
		;
	}


}
