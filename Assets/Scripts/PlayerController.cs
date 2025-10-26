using System;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public static PlayerController instance;

    private bool controlMode;
    public Friendly curDoll;
    public Friendly player;
    public Friendly player_rescue;
    public Transform trans_changePos;
    public GameObject crossHair;
    public GameObject targetArrow;
    [HideInInspector]
    public float hori;
    [HideInInspector]
    public bool changeable = true;
    private void Awake() {
        if (instance != null) {
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

    public void SetCrossHair(Transform target) {
        targetArrow.SetActive(target);
        crossHair.SetActive(target);
        
        if (target) {
            crossHair.transform.position = target.position;
            targetArrow.transform.rotation = Quaternion.Euler(0,0, (target.position - transform.position).x);
        }
        targetArrow.transform.position = curDoll.transform.position;
    }

    private void Update() {
        hori = Input.GetAxisRaw("Horizontal");

        //교체
        if (Input.GetKeyDown(KeyCode.C) && InGameManager.instance.level >= 2) {
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
    }

    public void ChangeDoll() {
        if (!changeable) return;

        controlMode = !controlMode;
        //메인
        if (controlMode) {
            //생성
            curDoll = player_rescue;
            player_rescue.transform.position = trans_changePos.position;
            player_rescue.gameObject.SetActive(true);
            if (player.currHP > 0) {
                player.gameObject.SetActive(false);
            }
            InGameUIController.instance.SetSlider(player_rescue, player);
        }
        //서브
        else {
            //생성
            curDoll = player;
            player.transform.position = trans_changePos.position;
            player.gameObject.SetActive(true);
            if (player_rescue.currHP > 0) {
                player_rescue.gameObject.SetActive(false);
            }
            InGameUIController.instance.SetSlider(player, player_rescue);
        }
        CameraController.instance.SetPlayer(curDoll);
    }
}
