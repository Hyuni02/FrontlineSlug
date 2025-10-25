using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    private bool controlMode;
    public Friendly curDoll;
    public Friendly player;
    public Friendly player_rescue;
    public Transform trans_changePos;
    [HideInInspector]
    public float hori;

    private void Awake() {
        if(instance != null) {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    private void Start() {
        curDoll = player;       
        CameraController.instance.SetPlayer(curDoll);
    }

    public void SetPlayer(GameObject doll, GameObject rescue = null) {
        player = doll.GetComponent<Friendly>();
        player_rescue = rescue?.GetComponent<Friendly>();
    }

    private void Update() {
        hori = Input.GetAxisRaw("Horizontal");

        //교체
        if (Input.GetKeyDown(KeyCode.C)) {
            ChangeDoll();
        }
        //공격
        if (Input.GetKey(KeyCode.Z)) {
            curDoll.TryAttack(true);
        }
        //이동
        else {
            curDoll.TryAttack(false);
            curDoll.Move(hori);
            //점프
            if (Input.GetKeyDown(KeyCode.X)) {
                curDoll.Jump();
            }
        }
        
        //temp
        // if (Input.GetKeyDown(KeyCode.Space)) {
        //     foreach (var enemy in FindObjectsOfType<EnemyAI>()) {
        //         enemy.Activate();
        //     }
        // }
    }

    public void ChangeDoll() {
        controlMode = !controlMode;
        //메인
        if (controlMode) {
            //생성
            curDoll = player_rescue;
            player_rescue.transform.position = trans_changePos.position;
            player_rescue.gameObject.SetActive(true);
            player.gameObject.SetActive(false);
            InGameUIController.instance.SetSlider(player_rescue, player);
        }
        //서브
        else {
            //생성
            curDoll = player;
            player.transform.position = trans_changePos.position;
            player.gameObject.SetActive(true);
            player_rescue.gameObject.SetActive(false);
            InGameUIController.instance.SetSlider(player, player_rescue);
        }
        CameraController.instance.SetPlayer(curDoll);
    }
}
