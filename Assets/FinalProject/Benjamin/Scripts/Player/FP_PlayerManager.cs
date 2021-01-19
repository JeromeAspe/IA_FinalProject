using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FP_PlayerManager : FP_Singleton<FP_PlayerManager>,IHandler<int, FP_Player>
{
	Dictionary<int, FP_Player> handle = new Dictionary<int, FP_Player>();

	public Dictionary<int, FP_Player> Handler => handle;

	[SerializeField] bool isDebugActive = true;
	[SerializeField] Color visualDebugColor = Color.white;


	public override bool IsValid => base.IsValid;
	public bool IsDebugActive => isDebugActive;

	public void Add(FP_Player _item)
	{
		if (Exists(_item)) return;
		_item.name += " [MANAGED]";
		handle.Add(_item.ID, _item);
	}

	public void Disable(FP_Player _item)
	{
		if (!Exists(_item)) return;
		Get(_item.ID).Disable();
	}

	public void Disable(int _id)
	{
		if (!Exists(_id)) return;
		Get(_id).Disable();
	}

	public void Enable(FP_Player _item)
	{
		if (!Exists(_item)) return;
		Get(_item.ID).Enable();
	}

	public void Enable(int _id)
	{
		if (!Exists(_id)) return;
		Get(_id).Enable();
	}

	public bool Exists(FP_Player _item)
	{
		if (!handle.ContainsKey(_item.ID)) return false;
		return true;
	}

	public bool Exists(int _id)
	{
		if (!handle.ContainsKey(_id)) return false;
		return true;
	}

	public FP_Player Get(int _id)
	{
		if (!Exists(_id)) return null;
		return handle[_id];
	}

	public void Remove(FP_Player _item)
	{
		if (!Exists(_item)) return;
		handle.Remove(_item.ID);
	}

	public void Remove(int _id)
	{
		if (!Exists(_id)) return;
		handle.Remove(_id);
	}


	#region Gizmos
	public override void OnDrawGizmos()
	{
		base.OnDrawGizmos();
		DrawVisualDebug();
	}
	public void DrawVisualDebug()
	{
		if (!isDebugActive) return;
		DrawAllHandledItems();
	}
	/// <summary>
	/// Show the connection between the manager and the managed Items
	/// </summary>
	void DrawAllHandledItems()
	{
		Gizmos.color = visualDebugColor;
		foreach (KeyValuePair<int, FP_Player> player in handle)
			Gizmos.DrawLine(transform.position, player.Value.transform.position);
	}
	#endregion

}
