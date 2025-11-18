using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class StringSpine {
    public string name;
    public GameObject doll;
}

public class InGameManager : MonoBehaviour {
    public static InGameManager instance;

    public Transform spawnPos;
    [Header("Tilemps")]
    public Transform trans_grid;
    private List<GameObject> lst_map = new List<GameObject>();

    [Header("Rescuable Dolls")]
    public List<StringSpine> lst_rescuable;
    
    [Header("Prefabs")]
    public List<GameObject> pref_lst_start;
    public List<GameObject> pref_lst_battle;
    public List<GameObject> pref_lst_boss;
    public List<GameObject> pref_lst_rescue;

    public Transform trans_Canvas;

    [HideInInspector]
    public int level = 0;
    
    //싱글톤 패턴
    private void Awake() {
        if (instance != null) {
            Destroy(gameObject);
            return;
        }
        instance = this;

        level = PlayerPrefs.GetInt("level");
    }

    public GameObject GetDollWithName(string name) {
        return lst_rescuable.Find(x => (x.name == name)).doll;
    }
    
    void Start() {
        //메인 인형
        var main = GetDollWithName(PlayerPrefs.GetString("main"));
        GameObject mainDoll = Instantiate(main, spawnPos.position, Quaternion.identity);
        PlayerController.instance.SetPlayer(mainDoll);
        //구출 인형
        if (level >= 2) {
            var rescue = GetDollWithName(PlayerPrefs.GetString("rescue"));
            GameObject rescueDoll = Instantiate(rescue, spawnPos.position, Quaternion.identity);
            PlayerController.instance.SetPlayer(mainDoll, rescueDoll);
            rescueDoll.SetActive(false);
        }

        //맵 생성
        //Start Point
        SelectFromTilemaps(ref pref_lst_start);
        if (level != 0) {
            //1st Battle Point
            SelectFromTilemaps(ref pref_lst_battle, 1, 2);

            //2nd Battle Point
            SelectFromTilemaps(ref pref_lst_battle, 1, 3);

            //Boss Point
            SelectFromTilemaps(ref pref_lst_boss);

            //Rescue Point
            SelectFromTilemaps(ref pref_lst_rescue);
        }

        //Generate Tilemap
        Vector2 clampx = InstantiateTilemap();
        //카메라 클램프 설정
        CameraController.instance.SetCameraClamp(new Vector2(clampx.x - 9, 15), Vector2.zero);
    }

    /// <summary>
    /// 주어진 배열에서 레벨에 맞는 타일맵을 최소 min개, 최대 max개 선택하여 lst_map에 추가
    /// </summary>
    /// <param name="from">주어진 배열</param>
    /// <param name="min">최속 선택 수</param>
    /// <param name="max">최대 선택 수</param>
    private void SelectFromTilemaps(ref List<GameObject> from, int min = 1, int max = 2) {
        List<GameObject> lst_filtered
            = new List<GameObject>(from.Where(x => (x.GetComponent<TilemapData>().level == level)));
        int count = Random.Range(min, max);
        while (count > 0) {
            GameObject battle = Utility.SelectRandom(lst_filtered);
            lst_filtered.Remove(battle);
            lst_map.Add(battle);
            count--;
        }
    }

    //타일맵 인스턴스화
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

    //다음 레벨로 이동
    public void ToNextLevel() {
        level++;
        PlayerPrefs.SetInt("level", level);
        UnityEngine.SceneManagement.SceneManager.LoadScene("InGame");
    }

    //인형 사망 처리
    public void DollDie() {
        if (level < 2) {
            ToGameOver();
        }
        else {
            //구출한 인형이 있으면 교체
            if (PlayerController.instance.changeable) {
                PlayerController.instance.ChangeDoll();
                PlayerController.instance.changeable = false;
            }
            else {
                ToGameOver();
            }
        }
    }

    //게임 오버 씬으로 이동
    private void ToGameOver() {
        StartCoroutine(cor_ToGameOver());
    }

    IEnumerator cor_ToGameOver() {
        yield return new WaitForSeconds(3);
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
    }
}
