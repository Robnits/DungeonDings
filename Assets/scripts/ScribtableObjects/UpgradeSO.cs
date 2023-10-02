using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class UpgradeSO : ScriptableObject
{
	[SerializeField]
	private float _damage;

	public float Damage
	{
		get { return _damage; }
		set { _damage = value; }
	}

    [SerializeField]
    private float _life;

    public float Life
    {
        get { return _life; }
        set { _life = value; }
    }

}
