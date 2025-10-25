using UnityEngine;

public class CameraController : MonoBehaviour {
    public static CameraController instance;
    
    private Friendly player;
    private float speed = 0.02f;
    public Vector3 offset;

    public Vector2 clampMax;
    public Vector2 clampMin;

    private void Awake() {
        if (instance == null) {
            instance = this;
            return;
        }
        Destroy(gameObject);
    }

    public void SetPlayer(Friendly _player) {
        player = _player;
    }

    public void SetCameraClamp(Vector2 cmax, Vector2 cmin) {
        clampMax = cmax;
        clampMin = cmin;
    }

    private void LateUpdate() {
        var playerPos = player.transform.position;
        
        transform.position = Vector2.Lerp(transform.position, playerPos + offset, speed);
        float x = Mathf.Clamp(transform.position.x, clampMin.x, clampMax.x);
        float y = Mathf.Clamp(transform.position.y, clampMin.y, clampMax.y);
        transform.position = new Vector3(x, y, - 10);
    }
}
