using UnityEngine;

public class RescueTrigger : MonoBehaviour {
    public Rescuable doll;
    public GameObject jail;
    public void OnTriggerEnter2D(Collider2D other) {
        doll.rescued = true;
        PlayerPrefs.SetString("rescue", doll.gameObject.name.Replace("(Clone)", ""));

        var rigid = jail.GetComponent<Rigidbody2D>();
        rigid.simulated = true;
        Vector2 force = new Vector2(120, 1200);
        rigid.AddForce(force);
    }
}
