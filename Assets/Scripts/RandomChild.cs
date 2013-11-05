using UnityEngine;
using System.Collections;

public class RandomChild : MonoBehaviour {

	// Use this for initialization
	void Start () {
		int n = transform.childCount;
		int r = Utility.Rnd.Next(n);
		
		for(int i=0; i<n; i++){
			GameObject child = transform.GetChild(i).gameObject;
			if(i!=r){
				child.SetActive(false);
				Destroy(child);
			}
		}
	}
}
