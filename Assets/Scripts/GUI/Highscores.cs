using UnityEngine;
using System;
using System.Collections.Generic;

public class Highscores {

	private class Score{
		public int brains;
		public TimeSpan time;
		public Score(int brains, TimeSpan time){
			this.brains = brains;
			this.time = time;
		}

		public static System.Converter<Score, Glyph> toGlyph = s => {
			return GVConcat.Concat(
				GString.GetString(s.brains+ (s.brains != 1 ? " Brainz" : " Brain"), GUIMenu.size_text)
				, GString.GetString(Utility.TimeToString(s.time), GUIMenu.size_text)
			).Align(HorAlignment.center);
		};
		
	}

	private static List<Score> times = null;
	private static List<Score> brains = null;

	public static Boolean Has {get { return Times.Count > 0 || Brains.Count > 0; }}

	public static List<Glyph> Times {
		get { Load(); return times.GetRange(0, System.Math.Min(3, times.Count)).ConvertAll(Score.toGlyph); }
	}

	public static List<Glyph> Brains {
		get { Load(); return brains.GetRange(0, System.Math.Min(3, brains.Count)).ConvertAll(Score.toGlyph); }
	}

	public static void Load(){
		if (times != null && brains != null) { return; }

		times = new List<Score>();
		int n = PlayerPrefs.GetInt("Highscore.Time.length", 0);
		for(int i=0; i < n; i++){
			times.Add(new Score(
				PlayerPrefs.GetInt("Highscore.Time."+i+".brains", 0),
				TimeSpan.FromTicks(long.Parse(PlayerPrefs.GetString("Highscore.Time."+i+".time", "0")))
			));
		}

		brains = new List<Score>();
		int m = PlayerPrefs.GetInt("Highscore.Brains.length", 0);
		for(int i=0; i < m; i++){
			brains.Add(new Score(
				PlayerPrefs.GetInt("Highscore.Brains."+i+".brains", 0),
				TimeSpan.FromTicks(long.Parse(PlayerPrefs.GetString("Highscore.Brains."+i+".time", "0")))
			));
		}
	}

	private static void Save(){
		PlayerPrefs.SetInt("Highscore.Time.length", times.Count);
		for(int i=0; i < System.Math.Min(5, times.Count); i++){
			PlayerPrefs.SetInt("Highscore.Time."+i+".brains", times[i].brains);
			PlayerPrefs.SetString("Highscore.Time."+i+".time", times[i].time.Ticks.ToString());
		}

		PlayerPrefs.SetInt("Highscore.Brains.length", brains.Count);
		for(int i=0; i < System.Math.Min(5, brains.Count); i++){
			PlayerPrefs.SetInt("Highscore.Brains."+i+".brains", brains[i].brains);
			PlayerPrefs.SetString("Highscore.Brains."+i+".time", brains[i].time.Ticks.ToString());
		}
	}

	public static void Add(int brainz, TimeSpan time){
		Load();

		Score s = new Score(brainz, time);

		times.Add(s);
		brains.Add(s);

		times.Sort(delegate(Score a, Score b) {
			int r =	a.time.CompareTo(b.time);
			if (r==0) { r = a.brains.CompareTo(b.brains);}
			return -r;
		});
		brains.Sort(delegate(Score a, Score b) {
			int r =	a.brains.CompareTo(b.brains);
			if (r==0) { r = a.time.CompareTo(b.time);}
			return -r;
		});

		Save();
	}

}
