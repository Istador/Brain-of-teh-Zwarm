public interface IState<in T> {
	void Enter(T owner);
	void Execute(T owner);
	void Exit(T owner);
	bool OnMessage(T owner, Telegram msg);
}

/**
 * ein einzelner Zustand
 * 
 * Die Methoden die nicht benötigt werden können weggelassen werden.
*/
public abstract class State<T> : IState<T> {
	
	
	
	/// <summary>
	/// Code der beim Betreten des Zustandes ausgeführt werden soll.
	/// </summary>
	/// <param name='owner'>
	/// Objekt das in diesem Zustand ist und für welches diese Methode ausgeführt wird
	/// </param>
	public virtual void Enter(T owner){}
	
	
	
	/// <summary>
	/// Code der bei jedem neuem Update ausgeführt werden soll.
	/// </summary>
	/// <param name='owner'>
	/// Objekt das in diesem Zustand ist und für welches diese Methode ausgeführt wird
	/// </param>
	public virtual void Execute(T owner){}
	
	
	
	/// <summary>
	/// Code der beim Verlassen des Zustandes ausgeführt werden soll.
	/// </summary>
	/// <param name='owner'>
	/// Objekt das in diesem Zustand ist und für welches diese Methode ausgeführt wird
	/// </param>
	public virtual void Exit(T owner){}
	
	
	
	/// <summary>
	/// Code der beim Erhalt einer Nachricht ausgeführt werden soll
	/// </summary>
	/// <param name='owner'>
	/// Objekt das in diesem Zustand ist und für welches diese Methode ausgeführt wird
	/// </param>
	/// <param name='msg'>
	/// Die übermittelte Nachricht
	/// </param>
	public virtual bool OnMessage(T owner, Telegram msg){
		return false;
	}
	
	
	
}
