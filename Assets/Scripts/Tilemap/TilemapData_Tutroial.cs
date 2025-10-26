using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TilemapData_Tutroial : TilemapData {
    public GameObject pnl_Skip;
    
    private void Start() {
        StartCoroutine(cor_PopSkip());
    }

    IEnumerator cor_PopSkip() {
        yield return new WaitForSeconds(2);
        GameObject pnl = Instantiate(pnl_Skip, InGameManager.instance.trans_Canvas);
        
        Button btn = pnl.transform.GetChild(0).GetChild(1).GetComponent<Button>();
        btn.onClick.AddListener(() => InGameManager.instance.ToNextLevel());
    }
}
