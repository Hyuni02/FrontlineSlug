using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class InGameManager : MonoBehaviour {
    public static InGameManager instance;

    [Header("Tilemps")]
    public Transform trans_grid;
    private List<GameObject> lst_map = new List<GameObject>();

    [Header("Prefabs")]
    public List<GameObject> pref_lst_start;
    public List<GameObject> pref_lst_battle;
    public List<GameObject> pref_lst_event;
    public List<GameObject> pref_lst_boss;
    public List<GameObject> pref_lst_rescue;

    public Transform trans_Canvas;

    private int level = 0;

    private void Awake() {
        if (instance != null) {
            Destroy(gameObject);
            return;
        }
        instance = this;

        if (!PlayerPrefs.HasKey("level")) {
            PlayerPrefs.SetInt("level", 0);
        }

        level = PlayerPrefs.GetInt("level");
        print("Map : " + level);
    }

    void Start() {
        //Start Point
        SelectFromTilemaps(ref pref_lst_start);
        if (level != 0) {
            //Tutorial Battle Point
            SelectFromTilemaps(ref pref_lst_battle, 1, 2);

            //Event Point
            SelectFromTilemaps(ref pref_lst_event);

            //2nd Battle Point
            SelectFromTilemaps(ref pref_lst_battle, 1, 3);

            //Boss Point
            SelectFromTilemaps(ref pref_lst_boss);

            //Rescue Point
            SelectFromTilemaps(ref pref_lst_rescue);
        }

        //Generate Tilemap
        Vector2 clampx = InstantiateTilemap(); 
        
        CameraController.instance.SetCameraClamp(new Vector2(clampx.x - 9, 15), Vector2.zero);
    }

    private void SelectFromTilemaps(ref List<GameObject> from, int min = 1, int max = 2) {
        List<GameObject> lst_filtered
            = new List<GameObject>(from.Where(x => (x.GetComponent<TilemapData>().level == level)));
        int count = Random.Range(min, max);
        while (count > 0) {
            GameObject battle2 = Utility.Pop(ref lst_filtered);
            lst_map.Add(battle2);
            count--;
        }
    }

    private Vector2 InstantiateTilemap() {
        Vector2 prev_pos = Vector2.zero;
        for (int i = 0; i < lst_map.Count; i++) {
            GameObject tileMap = Instantiate(lst_map[i], trans_grid);
            TilemapData data = tileMap.GetComponent<TilemapData>();
            if (data.startPos != null) {
                tileMap.transform.position = prev_pos - (Vector2)data.startPos.position;
            }
            prev_pos = data.endPos.position;
        }
        return prev_pos;
    }

    public void ToNextLevel() {
        level++;
        PlayerPrefs.SetInt("level", level);
        UnityEngine.SceneManagement.SceneManager.LoadScene("InGame");
    }
}
