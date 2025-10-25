using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingController : MonoBehaviour {
    public TMP_Text txt_main;
    public TMP_Text txt_rescue;

    private void Start() {
        txt_main.text = PlayerPrefs.GetString("main");
        txt_rescue.text = PlayerPrefs.GetString("rescue");
    }

    public void click_toMain() {
        //보유 캐릭터 초기화
        PlayerPrefs.SetInt(PlayerPrefs.GetString("rescue"), 1);
        //진행도 초기화
        PlayerPrefs.SetString("main",null); //게임 진입 시 선택한 캐릭터
        PlayerPrefs.SetString("rescue",null); //게임에서 구출한 캐릭터
        PlayerPrefs.SetInt("level", 0);
        
        SceneManager.LoadScene("MainMenu");
    }
}
