using UnityEngine;
using System.Collections;

public class Wall : RandomObject {
	
	
	
	public Wall(){
		f_RandomObjectProbability = 0.15f; //15%
	}
	
	
	
	protected override void Start(){
		
		//Zufällige Wand-Textur auswählen
		transform.GetChild(0).gameObject.GetComponent<Renderer>().materials = RandomWall;
		
		base.Start();
	}
	
	
		
	/// <summary>
	/// Die Texturen fertig zum Anwenden auf den Renderer.
	/// </summary>
	private static Material[][] mats = null;
	
	/// <summary>
	/// Eine zufällige Wandtextur
	/// </summary>
	private static Material[] RandomWall {
		get{
			//Wand-Texturen initialisieren
			if(mats == null){
				mats = new Material[4][];
				mats[0] = Resource.UsableMaterial("Wall");
				mats[1] = Resource.UsableMaterial("Wall_Hearth");
				mats[2] = Resource.UsableMaterial("Wall_Cake");
				mats[3] = Resource.UsableMaterial("Wall_Peace");
			}
			return mats[rnd.Next(mats.Length)];
		}
	}
	
	
	
}
