using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Das Pausen-Menü wird aktiviert wenn man im Spiel Escape drückt (oder 
/// vergleichbare Buttons auf anderen Plattformen).
/// 
/// Das Spiel wird angehalten, und der Spieler kann entweder das Spiel wieder
/// fortsetzen, oder das laufende Spiel beenden und zum Hauptmenü zurückkehren
/// </summary>
public class Pause : GUIMenu {

	private Glyph _g_main;
	protected override Glyph g_main { get{ return _g_main; } }
	protected override string str_title { get{ return "Pause"; } }

	// unpausierte Spielzeit
	public static TimeSpan Runtime {
		get { return _runtime.Add(DateTime.Now.Subtract(lastResume)); }
		private set { _runtime = value; }
	}
	private static TimeSpan _runtime;
	private static DateTime lastResume;

	//bool prellschutz = false;

	protected override void Start(){
		base.Start();

		lastResume = DateTime.Now;
		Runtime = new TimeSpan();

		//Spiel fortsetzen falls noch pausiert
		Time.timeScale = 1.0f;
		paused = false;

		Action<GButton> a_resume = (b) => {
			ResumeGame();
		};

		GButton g_resume = create_g_button("Spiel fortsetzen", a_resume);
		GButton g_mymenu = create_g_to_menu(null, a_resume);

		_g_main = GVConcat.Concat(g_resume, g_mymenu)
			.Align(HorAlignment.center)
			.Space(40.0)
		;

		Inputs.I.Register("Pause", ()=>{
			//prüfen ob das Spiel pausiert
		    if(!paused) PauseGame();
		    //oder fortgesetzt werden soll
		    else ResumeGame();
		});
	}
	
	
	/// <summary>
	/// Ist das Spiel jetzt gerade pausiert?
	/// </summary>
	private bool paused = false;
	public bool Paused{get{return paused;}}
	
	
	
	/// <summary>
	/// Pausiere das Spiel
	/// </summary>
	private void PauseGame(){
		paused = true;

		//Zeit anhalten
		Time.timeScale = 0.0f;

		//Spielzeit aufaddieren
		Runtime = Runtime;
	}

	
	/// <summary>
	/// Setze das pausierte Spiel fort
	/// </summary>
	private void ResumeGame(){
		paused = false;
				
		//Zeit weiterlaufen lassen
		Time.timeScale = 1.0f;

		//Timestamp zurücksetzen für die Spielzeitmessung
		lastResume = DateTime.Now;
	}


	//GUI zeichnen
	protected override void OnGUI(){
		//nur wenn das Spiel pausiert wurde den Pausen-Bildschirm zeichnen
		if (paused) { //draw
			Utility.DrawRectangle(new Rect( 0, 0, Screen.width, Screen.height), Color.white);
			base.OnGUI();
		}
	}
	
	
	
}
