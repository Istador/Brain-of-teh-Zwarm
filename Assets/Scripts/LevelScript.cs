using UnityEngine;
using System.Collections;

public class LevelScript : GeneralObject {
	
	/**
	 * Singleton
	*/
	private static LevelScript instance;
	public LevelScript(){
		instance = this;
	}
	public static LevelScript Instance{get{return instance;}}
	public static LevelScript I{get{return Instance;}}
	
	
	
	
	public Object[] zombies;
	public Object[] humans;
	public Object RandomEntity{ get{
		int i = rnd.Next(zombies.Length + humans.Length);
		if(i < zombies.Length) return zombies[i];
		return humans[i - zombies.Length];
	} }
		
	
	
	public Object[] musterBloecke;
	private Block[] gameBloecke = new Block[6];
	
	int current = 2;
	
	
	
	protected override void Start() {
		base.Start();		
		
		//alle Bloecke erstellen
		for(int i=0; i<gameBloecke.Length; i++)
			gameBloecke[i] = randomBlock( (float)((i-3)*25) );
	}
	
	
	
	private Block randomBlock(float x){
		
		//einen zufälligen Musterblock auswählen
		int r = rnd.Next(musterBloecke.Length);
		Object pref = musterBloecke[r];
		
		//erstellen
		GameObject newBlock = Instantiate(pref, new Vector3(x ,0.0f, 0.0f));
		
		//in der Hierarchie unter diesem einordnen, statt global
		newBlock.transform.parent = transform;
		
		//zurückgeben
		return (Block)newBlock.GetComponent<Block>();
	}
	
	
	
	private void nextBlock(){
		current = (current + 1) % gameBloecke.Length;
		int last = (current + 2) % gameBloecke.Length;
		int replace = (current + 3) % gameBloecke.Length;
		
		Block oldBlock = gameBloecke[replace];
		Block lastBlock = gameBloecke[last];
		Block newBlock = randomBlock(lastBlock.Pos.x + lastBlock.Width/2.0f);
		
		oldBlock.Remove();
		
		gameBloecke[replace] = newBlock;
	}
	
	
	
	public override bool HandleMessage(Telegram msg){
		switch(msg.message){
			case "nextBlock":
				nextBlock();
				return true;
			default:
				return false;
		}
	}
	
	
	
}
