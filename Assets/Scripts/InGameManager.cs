using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class InGameManager : MonoBehaviour {
    [Header("Tilemps")]
    public Transform trans_grid;
    private List<GameObject> lst_map = new List<GameObject>();

    [Header("Prefabs")]
    public GameObject pref_start;
    public List<GameObject> pref_lst_map;
    public List<GameObject> pref_lst_event;
    public GameObject pref_boss;
    
    void Start()
    {
        lst_map.Add(pref_start);

        int tileCount = Random.Range(5, 10);
        while (tileCount > 0) {
            lst_map.Add(pref_lst_map[Random.Range(0, pref_lst_map.Count)]);
            tileCount--;
        }
        
        InstantiateTilemap();
    }

    private void InstantiateTilemap() {
        GameObject prevTile = null;
        Vector3 pos_tile = Vector3.zero;
        for (int i = 0; i < lst_map.Count; i++) {
            GameObject curTile = lst_map[i];
            pos_tile = prevTile == null ? 
                Vector3.zero : 
                new Vector3(
                prevTile.transform.position.x
                + GetTilemapBoundLength(prevTile.GetComponent<Tilemap>()) * 0.5f
                + GetTilemapBoundLength(curTile.GetComponent<Tilemap>()) * 0.5f,
                0,
                0
            );
            
            print($"prev : {prevTile?.name} : {prevTile?.transform.position}");
            print($"curr : {curTile.name} : {pos_tile}");
            
            GameObject tile = Instantiate(curTile, trans_grid);
            tile.transform.position = pos_tile;
            prevTile = tile;
        }
    }

    private int GetTilemapBoundLength(Tilemap tilemap) {
        BoundsInt bounds = tilemap.cellBounds;
        int minX = int.MaxValue, maxX = int.MinValue;

        foreach (Vector3Int pos in bounds.allPositionsWithin)
        {
            if (tilemap.HasTile(pos))
            {
                if (pos.x < minX) minX = pos.x;
                if (pos.x > maxX) maxX = pos.x;
            }
        }

        return maxX - minX + 1;
    }
}
