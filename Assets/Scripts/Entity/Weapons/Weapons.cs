using UnityEngine;

public class Weapons {
	public readonly static IWeapon None = new WeaponNone();
	public readonly static IWeapon Pistole = new WeaponPistole();
	public readonly static IWeapon Shotgun = new WeaponShotgun();

	public static IWeapon Random{get{
			int rnd = Utility.Rnd.Next() % 3;
			switch(rnd){
			default:
			case 0: return None;
			case 1: return Pistole;
			case 2: return Shotgun;
			}
		}
	}

	private Weapons(){}
}

public interface IWeapon {
	float Damage {get;}
	float Range {get;}
	float ReloadTime {get;}
	float Accuracy {get;}
	Material[] Material {get;}
	void Shot(GeneralObject owner, Vector3 direction);
}

//Human ohne Waffe
public class WeaponNone : IWeapon {
	public float Damage {get{return 0f;}}
	public float Range {get{return 0f;}}
	public float ReloadTime {get{return float.PositiveInfinity;}}
	public float Accuracy {get{return 0f;}}
	public Material[] Material {get{return Resource.UsableMaterial("Human");}}
	public void Shot(GeneralObject owner, Vector3 direction){}
}