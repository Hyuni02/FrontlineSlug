using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishPoint : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other) {
        SceneManager.LoadScene("EndingScene");
    }
}
