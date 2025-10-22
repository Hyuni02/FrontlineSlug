using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class InGameManager : MonoBehaviour {
    public static InGameManager instance;
    
    [Header("Tilemps")]
    public Transform trans_grid;
    private List<GameObject> lst_map = new List<GameObject>();

    [Header("Prefabs")]
    public GameObject pref_start;
    public List<GameObject> pref_lst_map;
    public List<GameObject> pref_lst_event;
    public GameObject pref_boss;

    public Transform trans_Canvas;
    
    private int level = 0;

    private void Awake() {
        if(instance != null) {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

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
        Vector2 prev_pos = Vector2.zero;
        for (int i = 0; i < lst_map.Count; i++) {
            GameObject tileMap = Instantiate(lst_map[i], trans_grid);
            TilemapData data = tileMap.GetComponent<TilemapData>();
            if(data.startPos != null) {
                tileMap.transform.position = prev_pos - (Vector2)data.startPos.position;
            }
            prev_pos = data.endPos.position;
        }
    }

    public void ToNextLevel() {
        level++;
        print(level);
    }
}
