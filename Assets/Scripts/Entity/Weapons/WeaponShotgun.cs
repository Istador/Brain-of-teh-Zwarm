using UnityEngine;

public class WeaponShotgun : WeaponPistole, IWeapon {
	public override float Damage {get{return 30.0f;}} 
	public override float Range {get{return 4.0f;}}
	public override float ReloadTime {get{return 3f;}} // 3 sec
	public override float Accuracy {get{return 0.7f;}} // 70 %
	public override Material[] Material {get{return Resource.UsableMaterial("Human_Shotgun");}}

	public override void Shot(GeneralObject owner, Vector3 direction){
		//wohin geschossen wird
		Vector3[] targets = new Vector3[3];

		//Streuung auf 3 Projektile
		targets[0] = owner.Pos + direction.normalized * Range; // Mitte
		targets[1] = targets[0] + new Vector3(0f, 0f, 1f);
		targets[2] = targets[0] + new Vector3(0f, 0f, -1f);

		foreach(var target in targets)
			SingleShot(owner, target);

		if(!owner.Audio.isPlaying) owner.PlaySound("schuss", 1.0f);
	}
}

