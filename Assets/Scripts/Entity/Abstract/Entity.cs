﻿using UnityEngine;
using System.Collections;

/// 
/// Abstrakte Oberklasse für alle lebenden Spiel-Objekte im Spiel
/// 
/// Trefferpunkte und Sterben
/// 
public abstract class Entity : GeneralObject {
	
	
	
	// Konstruktor
	
	/// <summary>
	/// Initializes a new instance of the <see cref="Entity"/> class.
	/// </summary>
	/// <param name='maxHealth'>
	/// Maximale Trefferpunkte des Subjektes.
	/// Bei 0 Trefferpunkten stirbt das Subjekt.
	/// </param>
	public Entity(int maxHealth){
		//Instanzvariablen setzen
		MaxHealth = maxHealth;
		Health = maxHealth;
		HealthFactor = 1.0f;
	}
	
	
	
	//Start
	
	/// <summary>
	/// Unity-Konstruktor zur Initialisierung von Variablen.
	/// Im Gegensatz zum richtigen Konstruktor mit Zugriff auf andere GameObjekte
	/// und Unity-spezifische Dinge.
	/// </summary>
	protected override void Start(){
		base.Start();
		Health = MaxHealth;
		HealthFactor = 1.0f;
		IsDead = false;
	}

	protected override void Update(){
		if(!IsDead){
			base.Update();
		
			//Objekt faellt durch den Boden
			if(Pos.y < -20f) Death();
		}
	}
	
	
	// Trefferunkte Instanzvariablen
	
	/// <summary>
	/// Die maximalen Trefferunkte des Subjektes
	/// </summary>
	public int MaxHealth {get; private set;}
	
	/// <summary>
	/// Die aktuellen Trefferunkte des Subjektes
	/// </summary>
	public int Health {
		get {return _Health;}
		private set {
			//Wert verändern
			_Health = value;
			//Health in bestimmten Wertebereich halten
			Utility.MinMax(ref _Health, 0, MaxHealth);
			//Health Faktor neu berechnen
			HealthFactor = ((float)Health) / ((float)MaxHealth);
		}
	}
	private int _Health; //Instanzvariable die von der Property verwendet wird
	
	/// <summary>
	/// Das Verhältnis zwischen aktuellen und maximalen Trefferunkten.
	/// Wertebereich: 0.0 bis 1.0
	/// </summary>
	public float HealthFactor {
		get {return _HealthFactor;}
		private set {
			//Wert verändern
			_HealthFactor = value;
			//Health Faktor in bestimmten Wertebereich halten (evtl. float-divisions-ungenauigkeit ?)
			Utility.MinMax(ref _HealthFactor, 0.0f, 1.0f);
		}
	}
	private float _HealthFactor;
	
	
	
	// Methoden um Trefferunkte zu verändern
	
	/// <summary>
	/// Schaden erhalten, der die Trefferunkte verringert, und zum Tode führen kann
	/// </summary>
	/// <param name='damage'>
	/// Schaden der dem Subjekt zugefügt wird, inklusive Richtung woher der Schaden kommt
	/// </param>
	public virtual void ApplyDamage(Vector3 damage){
		//nichts tun, bei unserblichkeit
		if(Invincible) return;
		
		//Runde float zum nächsten int
		int dmg = System.Convert.ToInt32(damage.magnitude);
		
		//Debug.Log(name+"<"+tag+">("+GetInstanceID()+"): "+dmg+" dmg received");
		
		//Trefferpunkte verringern
		Health -= dmg;
		
		//Wenn Trefferpunkte auf 0 fallen, Sterben
		if(Health <= 0) Death();
	}
	
	/// <summary>
	/// Heilung erhalten, überschreitet nicht die maximalen Trefferpunkte
	/// </summary>
	/// <param name='hp'>
	/// Trefferpunkte die geheilt werden
	/// </param>
	public virtual void ApplyHeal(int hp){
		Health += hp;
	}
	
	
	
	// Unbesiegbarkeit
	
	public bool Invincible {get; set;}
	
	
	// Sterben
	
	/// <summary>
	/// Lässt das Subjekt sterben, z.B. weil die HP auf 0 gefallen sind
	/// </summary>
	public virtual void Death(){
		//Debug.Log(name+"<"+tag+">("+GetInstanceID()+"): death");
		IsDead = true;
		Destroy(gameObject);
	}

	//Referenzen auf das objekt könnten noch existieren
	public bool IsDead { get; private set; }
	
	
	
}
