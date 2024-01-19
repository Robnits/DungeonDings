using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject player;
    [SerializeField]
    private Tilemap tilemap;

    public SoTileEffects poisonTiles;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        Vector3Int gridPosition = tilemap.WorldToCell(player.transform.position);

        if (tilemap.GetTile(gridPosition) == poisonTiles.tiles)
        {
            player.GetComponent<Player_behjaviour>().OnSlow(poisonTiles.Slow);
            player.GetComponent<Player_Stats>().GetDamage(poisonTiles.damage);
        }
        else
        {
            player.GetComponent<Player_behjaviour>().OnSlow(0);
        }
    }
}
