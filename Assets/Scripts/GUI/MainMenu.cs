using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Reflection;

public class MainMenu: GUIMenu {
	
	
	private GVConcat _g_main;
	protected override Glyph g_main { get{ return _g_main; } }
	protected override string str_title { get{ return "Brain of teh Zwarm"; } }


	Action<GButton> a_start      = a_load_level("Levels/TestLevel");
	Action<GButton> a_highscores = a_load_level("Levels/Highscores");
	Action<GButton> a_credits    = a_load_level("Levels/Credits");

	Action<GButton> a_quit = (b) => {
		//Beendet das Spiel
		#if UNITY_EDITOR
		//Unity Editor
		UnityEditor.EditorApplication.isPlaying = false;
		#elif UNITY_WEBPLAYER
		//Webplayer
		Application.OpenURL("https://games.blackpinguin.de/BrainOfTehZwarm/play.html");
		#elif UNITY_WEBGL
		Application.OpenURL("https://games.blackpinguin.de/BrainOfTehZwarm/play.html");
		#else
		//Standalone Build
		Application.Quit();
		#endif

	};


	protected override void Start(){
		base.Start();
		
		GButton g_start = create_g_button("Spiel starten", a_start);

		GButton g_highscores = create_g_button("Highscores", a_highscores);
		if (! Highscores.Has) { g_highscores.Enabled = false; }

		GButton g_credits = create_g_button("Credits", a_credits);

		//Quit nicht im WebPlayer zeigen
		switch(Application.platform){
			case RuntimePlatform.OSXWebPlayer:
			case RuntimePlatform.WebGLPlayer:
			case RuntimePlatform.WindowsWebPlayer:
				_g_main = GVConcat.Concat(g_start, g_highscores, g_credits);
			break;
			default:
				GButton g_quit = create_g_button("Spiel beenden", a_quit);
				_g_main = GVConcat.Concat(g_start, g_highscores, g_credits, g_quit);
			break;
		}

		_g_main
			.Align(HorAlignment.center)
			.Space(40.0)
		;
	}


	void Update(){
		if(Input.GetKeyDown(KeyCode.F)){
			MessageDispatcher.I.EmptyQueue();
			SceneManager.LoadScene("Levels/FontDemo");
		}
	}


}
