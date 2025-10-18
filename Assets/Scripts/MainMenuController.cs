using System;
using System.Collections;
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

    private void click_NewGame() {
        pnl_newGame.SetActive(true);
        pnl_background.SetActive(true);
        pnl_background.GetComponent<UIBackground>().Active(pnl_newGame);
        Change_UI(e_type.newgame);
        
        //temp
        SceneManager.LoadScene("InGame");
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
