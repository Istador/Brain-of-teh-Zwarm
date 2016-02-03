using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public abstract class GUIMenu : MonoBehaviour {

	// resize variables
	private double sHeight;
	private double sWidth;
	private double s;

	public static readonly double size_text = 0.30;
	public static readonly double size_button = 1.5;
	public static readonly double size_title = 0.65;
	public static readonly double size_img = 0.18;

	// Titel
	private GBounds g_title;
	private Vector2 pos_title;

	// Zombies Links & Rechts
	private GBounds g_left, g_right;
	private Vector2 pos_left, pos_right;

	// abstract
	protected abstract string str_title { get; }
	protected abstract Glyph g_main { get; }


	private GBounds g_content { get {
		if(_g_content == null) {
			_g_content = new GBounds(g_main, Screen.width, Screen.height);
		}
		return _g_content;
	}}
	private GBounds _g_content;
	private Vector2 pos_content;


	public static Action<GButton> a_load_level(string level) {
		return (b) => {
			//Nachrichtenwarteschlange leeren
			MessageDispatcher.I.EmptyQueue();
			//Hauptmenü laden
			SceneManager.LoadScene(level);
		};
	}

	private static readonly Action<GButton> a_menu = a_load_level("Levels/MainMenu");

	public static GButton create_g_button(string label, Action<GButton> action){
		GButton g = new GButton(250, 40, GString.GetString(label), action);
		g.Size = size_button;
		g.Padding.all = 10.0;
		g.Border.all = 4.0;
		return g;
	}

	public static GButton create_g_to_menu(Action<GButton> a_before = null, Action<GButton> a_after = null){
		Action<GButton> a_do = (b) => {
			if (a_before != null){ a_before(b); }
			a_menu(b);
			if (a_after != null){ a_after(b); }
		};

		return create_g_button("Zum Hauptmenü", a_do);
	}

	public static GButton g_menu { get {
		if (_g_menu == null) { _g_menu = create_g_to_menu(); }
		return _g_menu;
	} }

	private static GButton _g_menu = null;


	protected virtual void Start () {
		g_title = new GBounds(GString.GetString(str_title, size_title), Screen.width, Screen.height);
		g_left = new GBounds(new GImage(Resource.Texture["love_left"], size_img), Screen.width, Screen.height);
		g_right = new GBounds(new GImage(Resource.Texture["love_right"], size_img), Screen.width, Screen.height);
	}


	private void Resize(){
		if(sWidth != Screen.width || sHeight != Screen.height){
			sWidth = Screen.width;
			sHeight = Screen.height;
			
			double aspect = (sWidth / sHeight) / (1680/1050);
			//Utility.MinMax(ref aspect, 0.9, 1.25);
			s = (sHeight / 1050) * aspect;
			//Utility.MinMax(ref s, 0.6, 0.9);

			double sa = s / aspect;

			//Debug.Log(s);

			g_title.maxWidth = Screen.width;
			g_title.maxHeight = Screen.height * 0.15;

			pos_title = new Vector2(
				(float)((sWidth - g_title.Width(s)) * 0.5),
				(float)( 80.0 * sa )
			);

			double y = pos_title.y + g_title.Height(s) + 20.0 * sa;
			double h = sHeight - y - 20.0 * sa;

			g_left.maxHeight = h;
			g_left.maxWidth = sWidth * 0.2;
			g_right.maxHeight = h;
			g_right.maxWidth = sWidth * 0.2;

			pos_left = new Vector2(
				(float)( 20.0 * s ),
				(float)(sHeight - 20.0 * sa - g_left.Height(s))
			);

			pos_right = new Vector2(
				(float)(sWidth - 20.0 * s - g_right.Width(s)),
				(float)(sHeight - 20.0 * sa - g_right.Height(s))
			);

			g_content.maxHeight = h - 20.0 * sa;
			g_content.maxWidth = sWidth;

			if (y + Math.Max(0.0, (sHeight - y - g_content.Height(s)) * 0.5) + g_content.Height(s) >= pos_left.y) {
				g_content.maxWidth = sWidth - g_left.Width(s) - g_right.Width(s) - 80.0 * s;
			}

			pos_content = new Vector2(
				(float)((sWidth - g_content.Width(s)) * 0.5),
				(float)(y + Math.Max(0.0, (sHeight - y - g_content.Height(s)) * 0.5))
			);

			OnResize(s);
		}
	}

	protected virtual void OnResize(double s){}


	protected virtual void OnGUI(){
		Resize();

		// Title
		g_title.Draw(s, pos_title);

		// Bilder
		g_left.Draw(s, pos_left);
		g_right.Draw(s, pos_right);

		// Content (Menu Specific)
		g_content.Draw(s, pos_content);

		OnDraw(s);
	}

	protected virtual void OnDraw(double s){}

}
