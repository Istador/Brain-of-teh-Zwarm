using UnityEngine;
using System.Collections;

public class SpriteController : MonoBehaviour
{



	private static System.Random rnd = new System.Random();
	
	public int index {get; private set;}
	
	//random positive integer -> random animation position for different entities
	public readonly int rndCol = System.Math.Abs(rnd.Next());
	
	
	
	public void  animate (int columnSize, int rowSize, int colFrameStart, int rowFrameStart, int totalFrames, int framesPerSecond){		
		//Quelltext von Bent Nürnberg entfernt
	}



}