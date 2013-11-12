using UnityEngine;
using System.Collections;

/*
 * Steering Behaviors System
 * 
 * Ein zentrales Objekt um als Gegner aus einer Vielzahl von unterschiedlichen
 * Steering Behaviors wählen zu können, und bei mehreren ausgewählten diese
 * gemeinsam zu einem resultierenden Kraft-Vektor zu berechnen.
 * 
 * Quelle:
 * Mat Buckland - Programming Game AI by Example
*/
public class SteeringBehaviors<T> {
	
	
	
	/// <summary>
	/// Besitzer dieser Instanz
	/// </summary>
	private MovableEntity<T> owner;
	
	
	
	/// <summary>
	/// Ziel das für einige Behaviors benötigt wird
	/// </summary>
	public MovableEntity<Entity> Target {get; set;}
	
	
	
	/// <summary>
	/// Offset vom Ziel das fOffset Pursuit benötigt wird
	/// </summary>
	public Vector3 Offset {get; set;}
	
	
	
	/// <summary>
	/// Zielkoordinaten
	/// </summary>
	public Vector3 TargetPos {
		get{
			if(_TargetPos == Vector3.zero && Target != null)
				return Target.Pos;
			return _TargetPos;
		}
		set{_TargetPos = value;}
	}
	private Vector3 _TargetPos = Vector3.zero; //Instanzvariable
	
	
	
	/// <summary>
	/// Initializes a new instance of the <see cref="SteeringBehaviors`1"/> class.
	/// </summary>
	/// <param name='owner'>
	/// Besitzer dieser Instanz
	/// </param>
	public SteeringBehaviors(MovableEntity<T> owner){
		this.owner = owner;
	}
	
	
	
	/// <summary>
	/// Interne Methode.
	/// Anstreben der Zielkoordinaten mit maximaler Geschwindigkeit
	/// </summary>
	/// <param name='targetPos'>
	/// Zielkoordinaten
	/// </param>
	private Vector3 Seek(Vector3 targetPos){
		Vector3 desiredVelocity = 
			(targetPos - owner.Pos).normalized * owner.MaxForce;
		return desiredVelocity - owner.rigidbody.velocity;
	}
	
	/// <summary>
	/// Anstreben der Zielkoordinaten mit maximaler Geschwindigkeit
	/// </summary>
	/// <param name='targetPos'>
	/// Zielkoordinaten
	/// </param>
	public void DoSeek(Vector3 targetPos){
		Stop();
		Seeking = true;
		TargetPos = targetPos;
	}
	
	/// <summary>
	/// Anstreben der Zielkoordinaten des Objektes mit maximaler Geschwindigkeit
	/// </summary>
	/// <param name='targetPos'>
	/// Zielkoordinaten
	/// </param>
	public void DoSeek(GeneralObject target){
		DoSeek(target.Pos);
	}
	
	
	
	/// <summary>
	/// Interne Methode.
	/// Fliehen vor den Zielkoordinaten mit maximaler Geschwindigkeit
	/// </summary>
	/// <param name='targetPos'>
	/// Zielkoordinaten
	/// </param>
	private Vector3 Flee(Vector3 targetPos){
		Vector3 desiredVelocity = 
			(owner.Pos - targetPos).normalized * owner.MaxForce;
		
		return desiredVelocity - owner.rigidbody.velocity;
	}
	
	/// <summary>
	/// Fliehen vor den Zielkoordinaten mit maximaler Geschwindigkeit
	/// </summary>
	/// <param name='targetPos'>
	/// Zielkoordinaten
	/// </param>
	public void DoFlee(Vector3 targetPos){
		Stop();
		Fleeing = true;
		TargetPos = targetPos;
	}
	
	/// <summary>
	/// Fliehen vor den Zielkoordinaten des Objektes mit maximaler Geschwindigkeit
	/// </summary>
	/// <param name='targetPos'>
	/// Zielkoordinaten
	/// </param>
	public void DoFlee(GeneralObject target){
		DoFlee(target.Pos);
	}
	
	
	
	/// <summary>
	/// Abfangen eines Objektes anhand dessen vorraussichtlich zukünftigen Position.
	/// </summary>
	/// <param name='evader'>
	/// Objekt das abgefangen werden soll
	/// </param>
	private Vector3 Pursuit(MovableEntity<Entity> target){
		Vector3 toEvader = target.Pos - owner.Pos;
		
		/*
		if(
			( Vector3.Dot(toEvader, owner.transform.rotation.eulerAngles) > 0 )
			&& 
			( Vector3.Dot(owner.transform.rotation, evader.transform.rotation.eulerAngles) < -0.95f )
			){
			return Seek(evader.transform.position);
		}
		*/
						
		float LAT = toEvader.magnitude / ( owner.MaxForce + target.rigidbody.velocity.magnitude );
		return Seek(target.Pos + target.rigidbody.velocity * LAT);
	}
	
	
	
	/// <summary>
	/// Ausweichen anhand der vorraussichtlich zukünftigen Position eines Verfolgers.
	/// </summary>
	/// <param name='evader'>
	/// Der Verfolger dem man asuweichen will
	/// </param>
	private Vector3 Evade(MovableEntity<Entity> target){
		Vector3 toPersuer = target.Pos - owner.Pos;
		float LAT = toPersuer.magnitude / ( owner.MaxForce + target.rigidbody.velocity.magnitude );
		return Flee(target.Pos + target.rigidbody.velocity * LAT);
	}
	
	
	
	
	/// <summary>
	/// Folgen eines Targets mit Offset
	/// </summary>
	/// <param name='evader'>
	/// Objekt das abgefangen werden soll
	/// </param>
	private Vector3 OffsetPursuit(MovableEntity<Entity> target, Vector3 offset){
		Vector3 WorldOffset = target.Pos + offset;
		
		Vector3 ToOffset = WorldOffset - owner.Pos;
		
		
		float LAT = ToOffset.magnitude / ( owner.MaxForce + target.rigidbody.velocity.magnitude );
		return Seek(WorldOffset + target.rigidbody.velocity * LAT);
	}
	
	
	
	/// <summary>
	/// Anstreben ein-/ausschalten
	/// </summary>
	/// <param name='on'>
	/// true=ein, false=aus
	/// </param>
	public bool Seeking {get; set;}
	
	/// <summary>
	/// Fliehen ein-/ausschalten
	/// </summary>
	/// <param name='on'>
	/// true=ein, false=aus
	/// </param>
	public bool Fleeing {get; set;}
	
	/// <summary>
	/// Abfangen ein-/ausschalten
	/// </summary>
	/// <param name='on'>
	/// true=ein, false=aus
	/// </param>
	public bool Pursuing {get; set;}
	
	/// <summary>
	/// Ausweichen ein-/ausschalten
	/// </summary>
	/// <param name='on'>
	/// true=ein, false=aus
	/// </param>
	public bool Evading {get; set;}
	
	/// <summary>
	/// Offset Pursuit ein-/ausschalten
	/// </summary>
	/// <param name='on'>
	/// true=ein, false=aus
	/// </param>
	public bool OffsetPursuing {get; set;}
	
	
	
	
	
	/// <summary>
	/// Alles anhalten
	/// </summary>
	public void Stop(){
		Seeking = false;
		Fleeing = false;
		Pursuing = false;
		Evading = false;
	}
	
	
	
	/// <summary>
	/// berechnet die resultierende Kraft aller Steering Behaviours
	/// </summary>
	/// <returns>
	/// Kraft-Vektor zu der sich bewegt werden soll
	/// </returns>
	public Vector3 Calculate(){
		Vector3 f = Vector3.zero;
		
		if(Seeking) f += Seek(TargetPos);
		if(Fleeing) f += Flee(TargetPos);
		if(Pursuing && Target != null) f+= Pursuit(Target);
		if(Evading && Target != null) f+= Evade(Target);
		if(OffsetPursuing && Target != null && Offset != Vector3.zero) f += OffsetPursuit(Target, Offset);
		
		//truncat
		if(f != Vector3.zero && Mathf.Abs(f.magnitude) > owner.MaxForce)
			f = f.normalized * owner.MaxForce;
		
		return f;
	}
	
	
	
}
