using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class SOTiles : ScriptableObject
{
    public TileBase[] tiles;
    public TileBase wall;
}
