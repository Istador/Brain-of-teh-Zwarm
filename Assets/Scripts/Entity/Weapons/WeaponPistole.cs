using UnityEngine;

public class WeaponPistole : IWeapon {
	public virtual float Damage {get{return 40.0f;}}
	public virtual float Range {get{return 6f;}}
	public virtual float ReloadTime {get{return 2f;}} // 1 sec
	public virtual float Accuracy {get{return 0.90f;}} // 90 %
	public virtual Material[] Material {get{return Resource.UsableMaterial("Human_Pistole");}}

	public virtual void Shot(GeneralObject owner, Vector3 direction){
		//wohin geschossen wird
		Vector3 target = owner.Pos + direction.normalized * Range;
		SingleShot(owner, target);
		if(!owner.Audio.isPlaying) owner.PlaySound("schuss", 1.0f);
	}

	protected void SingleShot(GeneralObject owner, Vector3 target){
		//ob etwas getroffen wird
		RaycastHit hit;
		Debug.DrawLine(owner.Pos, target, Color.red);
		bool isHit = owner.Linecast(target, out hit, GeneralObject.Layer.Entity);
		if(isHit){
			//Wer getroffen wird
			Entity victim = hit.transform.GetComponent<Entity>();
			if(victim == null) return;
			
			float dmg = Damage;
			
			//weiter als Accurace weg genau
			if(Range * Accuracy < hit.distance){
				//50 % des Schadens basiert auf der Distanz
				float impact = (1.0f - hit.distance / Range) * 0.5f;
				//die anderen 50% basieren auf Accuracy und Zufall
				float rnd = Random.Range(Accuracy, 1.0f) * 0.5f;
				
				dmg *= rnd + impact;
			}
			
			//Schaden anrichten
			owner.DoDamage(victim, Mathf.RoundToInt(dmg));
		}
	}
}

