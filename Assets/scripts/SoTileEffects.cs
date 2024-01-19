using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "Tileeffects", menuName = "Tiles")]
public class SoTileEffects : ScriptableObject
{
    public TileBase tiles;

    public int poisonDamage;
    public int Slow;
    public int damage;
}