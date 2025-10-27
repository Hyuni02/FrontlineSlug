using UnityEngine;

public class CameraController : MonoBehaviour {
    public static CameraController instance;
    
    private Friendly player;
    private Transform bossRoom;
    private bool followPlayer = true;
    private float speed = 0.03f;
    public Vector3 offset;

    public Vector2 clampMax;
    public Vector2 clampMin;
    private Camera cam;
    private readonly float defaultSize = 5;

    private void Awake() {
        if (instance == null) {
            instance = this;
            return;
        }
        Destroy(gameObject);
    }

    private void Start() {
        cam = GetComponent<Camera>();
    }

    public void SetPlayer(Friendly _player) {
        player = _player;
    }

    public void SetCameraClamp(Vector2 cmax, Vector2 cmin) {
        clampMax = cmax;
        clampMin = cmin;
    }

    public void EnterBossRoom(Transform pivot, float size) {
        followPlayer = false;
        bossRoom = pivot;
        cam.orthographicSize = size;
    }

    public void ExitBossRoom() {
        followPlayer = true;
        cam.orthographicSize = defaultSize;
    }

    private void LateUpdate() {
        Vector3 camPos = Vector3.zero;
        //플레이어 따라다니기
        if (followPlayer) {
            camPos = player.transform.position + offset;
        }
        //보스룸 카메라 고정
        else {
            camPos = bossRoom.position;
        }
        
        transform.position = Vector2.Lerp(transform.position, camPos, speed);
        float x = Mathf.Clamp(transform.position.x, clampMin.x, clampMax.x);
        float y = Mathf.Clamp(transform.position.y, clampMin.y, clampMax.y);
        transform.position = new Vector3(x, y, -10);
    }
}
