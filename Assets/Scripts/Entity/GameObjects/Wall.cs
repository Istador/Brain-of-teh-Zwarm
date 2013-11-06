using UnityEngine;
using System.Collections;

public class Wall : RandomObject {
	
	/// <summary>
	/// Die Texturen fertig zum Anwenden auf den Renderer.
	/// </summary>
	private static Material[][] mats = null;
	
	
	
	public Wall(){
		f_RandomObjectProbability = 0.2f; //20%
	}
	
	
	
	protected override void Start(){
		if(mats == null){
			mats = new Material[4][];
			mats[0] = Resource.UsableMaterial("Wall");
			mats[1] = Resource.UsableMaterial("Wall_Hearth");
			mats[2] = Resource.UsableMaterial("Wall_Cake");
			mats[3] = Resource.UsableMaterial("Wall_Peace");
			
			Debug.Log(mats[0][0]);
		}
		
		transform.GetChild(0).gameObject.renderer.materials = mats[rnd.Next(mats.Length)];
		
		base.Start();
	}
}
