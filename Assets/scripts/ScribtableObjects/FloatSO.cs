using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class FloatSO : ScriptableObject
{
	[SerializeField]
	private float _newMoney;

	public float NewMoney
	{
		get { return _newMoney; }
		set { _newMoney = value; }
	}

	[SerializeField]
	private float _oldMoney;

	public float OldMoney
	{
		get { return _oldMoney; }
		set { _oldMoney = value; }
	}


}
