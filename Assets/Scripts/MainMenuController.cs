using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {
    private enum e_type {
        main,
        newgame,
        loadgame,
        setting
    }

    public TMP_Text txt_title;
    [Header("Panel")]
    public GameObject pnl_background;
    public GameObject pnl_newGame;
    public GameObject pnl_setting;

    [Header("Button Group")]
    public List<Button> btgrp_main;
    public List<Button> btgrp_newGame;
    public List<Button> btgrp_loadGame;
    public List<Button> btgrp_setting;

    [Header("Character Select")]
    public GameObject pnl_state;
    public TMP_Text txt_name;
    public TMP_Text txt_state;
    public GameObject sd_kar98k;
    public GameObject sd_m4sopmodii;
    public GameObject sd_mp7;
    public GameObject sd_sat8;
    private string curr_Select;
    public Button btn_M4SOPMODII;
    public Button btn_MP7;
    public Button btn_SAT8;
    public Button btn_Kar98k;
    public Button btn_start;

    private List<Button> buttonGroup;
    private e_type type = e_type.main;

    //클릭한 버튼 유형에 따른 함수 분기
    public void btn_Click(string _type) {
        switch (_type) {
            case "newgame":
                click_NewGame();
                break;
            case "loadgame":
                click_LoadGame();
                break;
            case "setting":
                click_Setting();
                break;
            case "exit":
                click_Exit();
                break;
        }
    }

    //UI 상태에 따른 분기
    private void Change_UI(e_type _type) {
        type = _type;
        switch (type) {
            case e_type.main:
                buttonGroup = btgrp_main;
                break;
            case e_type.newgame:
                buttonGroup = btgrp_newGame;
                break;
            case e_type.loadgame:
                buttonGroup = btgrp_loadGame;
                break;
            case e_type.setting:
                buttonGroup = btgrp_setting;
                break;
            default:
                print($"not defined action : {type.ToString()}");
                break;
        }
    }

    //선택한 캐릭터 이름을 임시 저장
    public void click_SelectCharacter(string name) {
        curr_Select = name;
        btn_start.interactable = true;
        ShowCharacterState(curr_Select);
    }

    private void ShowCharacterState(string name = null) {
        if (name == null) {
            pnl_state.SetActive(false);
        }
        else {
            pnl_state.SetActive(true);
            txt_name.text = name;
            sd_kar98k.SetActive(name == "Kar98k");
            sd_mp7.SetActive(name == "MP7");
            sd_m4sopmodii.SetActive(name == "M4SOPMODII");
            sd_sat8.SetActive(name == "SAT8");
            txt_state.text = CharacterState(name);
        }
    }

    private string CharacterState(string name) {
        switch (name) {
            case "Kar98k":
                return "speed : 4\r\ndmg : 90\r\nattack speed : 2\r\nHP : 100\r\nrange : 16";
            case "MP7":
                return "speed : 7\r\ndmg : 10\r\nattack speed : 1\r\nHP : 80\r\nrange : 10";
            case "M4SOPMODII":
                return "speed : 5\r\ndmg : 20\r\nattack speed : 1.5\r\nHP : 100\r\nrange : 13";
            case "SAT8":
                return "speed : 4\r\ndmg : 40*3\r\nattack speed : 1.5\r\nHP : 120\r\nrange : 7";
            default:
                return "";
        }
    }

    //게임 시작 버튼 - 선택한 캐릭터를 저장(이후 InGame 씬에서 사용)
    public void click_StartGame() {
        PlayerPrefs.SetString("main", curr_Select);
        PlayerPrefs.SetString("rescue", null);
        PlayerPrefs.SetInt("level", 0);
        PlayerPrefs.SetInt("playing", 1);
        SceneManager.LoadScene("InGame");
    }

    //저장 데이터 초기화
    public void ClearData() {
        PlayerPrefs.DeleteAll();
    }

    //새로운 게임 생성 버튼
    private void click_NewGame() {
        pnl_newGame.SetActive(true);
        pnl_background.SetActive(true);
        pnl_background.GetComponent<UIBackground>().Active(pnl_newGame);
        btn_start.interactable = false;
        Change_UI(e_type.newgame);

        //데이터 초기화
        if (!PlayerPrefs.HasKey("playing")) {
            initPlayerPrefs(true);
        }

        //보유한 캐릭터에 따라 버튼 활성화
        btn_M4SOPMODII.interactable = PlayerPrefs.GetInt("M4SOPMODII") == 1;
        btn_MP7.interactable = PlayerPrefs.GetInt("MP7") == 1;
        btn_SAT8.interactable = PlayerPrefs.GetInt("SAT8") == 1;
        btn_Kar98k.interactable = PlayerPrefs.GetInt("Kar98k") == 1;
    }

    //데이터 초기화
    public static void initPlayerPrefs(bool hardReset = false) {
        PlayerPrefs.SetInt("playing", 0); //게임 진행 여부
        //보유 캐릭터 초기화
        if (hardReset) {
            PlayerPrefs.SetInt("M4SOPMODII", 1); //초기 캐릭터 지급
            PlayerPrefs.SetInt("MP7", 0);
            PlayerPrefs.SetInt("SAT8", 0);
            PlayerPrefs.SetInt("Kar98k", 0);
        }
        //진행도 초기화
        PlayerPrefs.SetString("main", null); //게임 진입 시 선택한 캐릭터
        PlayerPrefs.SetString("rescue", null); //게임에서 구출한 캐릭터
        PlayerPrefs.SetInt("level", 0);
    }

    //이어하기 버튼
    private void click_LoadGame() {
        Change_UI(e_type.loadgame);
        //진행 중인 게임이 없으면 새 게임 생성으로 이동
        if (PlayerPrefs.GetInt("playing") == 0) {
            pnl_background.GetComponent<UIBackground>().click_Close();
            click_NewGame();
        }
        //진행 중인 게임이 있으면 인게임 씬으로 이동
        else {
            SceneManager.LoadScene("InGame");
        }
    }

    //설정 버튼
    private void click_Setting() {
        pnl_setting.SetActive(true);
        pnl_background.SetActive(true);
        pnl_background.GetComponent<UIBackground>().Active(pnl_setting);
        Change_UI(e_type.setting);
    }

    //종료 버튼
    private void click_Exit() {
        print("게임 종료");
        Application.Quit();
    }

    //심심한 게임 타이틀에 활기를
    private void FixedUpdate() {
        txt_title.color = new Color(
            Mathf.PingPong(Time.time * 0.5f, 1f),
            Mathf.PingPong(Time.time * 0.3f, 1f),
            Mathf.PingPong(Time.time * 0.7f, 1f)
        );
    }

    public void Update() {
        if (Input.GetKeyDown(KeyCode.P)) {

            PlayerPrefs.SetInt("M4SOPMODII", 1); //초기 캐릭터 지급
            PlayerPrefs.SetInt("MP7", 1);
            PlayerPrefs.SetInt("SAT8", 1);
            PlayerPrefs.SetInt("Kar98k", 1);
        }
    }
}
