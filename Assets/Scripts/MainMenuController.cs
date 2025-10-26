using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {
    private enum e_type {
        main,
        newgame,
        loadgame,
        setting
    }
    
    [Header("Panel")]
    public GameObject pnl_background;
    public GameObject pnl_newGame;
    public GameObject pnl_saveSlot;
    public GameObject pnl_setting;
    
    [Header("Button Group")]
    public List<Button> btgrp_main;
    public List<Button> btgrp_newGame;
    public List<Button> btgrp_loadGame;
    public List<Button> btgrp_setting;

    [Header("Character Select")]
    private string curr_Select;
    public Button btn_M4SOPMODII;
    public Button btn_MP7;
    public Button btn_SAT8;
    public Button btn_Kar98k;
    public Button btn_start;
    
    private List<Button> buttonGroup;
    private e_type type = e_type.main;
    
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

    public void click_SelectCharacter(string name) {
        curr_Select = name;
        btn_start.interactable = true;
    }

    public void click_StartGame() {
        PlayerPrefs.SetString("main", curr_Select);
        PlayerPrefs.SetString("rescue", null);
        PlayerPrefs.SetInt("level", 0);
        SceneManager.LoadScene("InGame");
    }

    private void click_NewGame() {
        pnl_newGame.SetActive(true);
        pnl_background.SetActive(true);
        pnl_background.GetComponent<UIBackground>().Active(pnl_newGame);
        btn_start.interactable = false;
        Change_UI(e_type.newgame);

        if (!PlayerPrefs.HasKey("M4SOPMODII")) {
            initPlayerPrefs();
        }

        btn_M4SOPMODII.interactable = PlayerPrefs.GetInt("M4SOPMODII") == 1;
        btn_MP7.interactable = PlayerPrefs.GetInt("MP7") == 1;
        btn_SAT8.interactable = PlayerPrefs.GetInt("SAT8") == 1;
        btn_Kar98k.interactable = PlayerPrefs.GetInt("Kar98k") == 1;
    }

    private void initPlayerPrefs() {
        //보유 캐릭터 초기화
        PlayerPrefs.SetInt("M4SOPMODII", 1);
        PlayerPrefs.SetInt("MP7", 0);
        PlayerPrefs.SetInt("SAT8", 0);
        PlayerPrefs.SetInt("Kar98k", 0);
        //진행도 초기화
        PlayerPrefs.SetString("main",null); //게임 진입 시 선택한 캐릭터
        PlayerPrefs.SetString("rescue",null); //게임에서 구출한 캐릭터
        PlayerPrefs.SetInt("level", 0);
        //업적 초기화
        
    }

    private void click_LoadGame() {
        pnl_saveSlot.SetActive(true);
        pnl_background.SetActive(true);
        pnl_background.GetComponent<UIBackground>().Active(pnl_saveSlot);
        Change_UI(e_type.loadgame);
    }

    private void click_Setting() {
        pnl_setting.SetActive(true);
        pnl_background.SetActive(true);
        pnl_background.GetComponent<UIBackground>().Active(pnl_setting);
        Change_UI(e_type.setting);
    }

    private void click_Exit() {
        print("게임 종료");
        Application.Quit();
    }
}
