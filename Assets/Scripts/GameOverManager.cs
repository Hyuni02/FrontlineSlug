using Spine.Unity;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour {
    public Transform spotLight;
    public SkeletonGraphic kar98k;
    public SkeletonGraphic sat8;
    public SkeletonGraphic m4sopmodii;
    private void Start() {
        string main = PlayerPrefs.GetString("main");
        switch (main) {
            case "Kar98k":
                kar98k.gameObject.SetActive(true);
                break;
            case "SAT8":
                sat8.gameObject.SetActive(true);
                break;
            case "M4SOPMODII":
                m4sopmodii.gameObject.SetActive(true);
                break;
            default:
                break;
        }

        string sub = PlayerPrefs.GetString("rescue");
        switch (sub) {
            case "Kar98k":
                kar98k.gameObject.SetActive(true);
                spotLight.localScale = new Vector3(5.7f, 1, 1);
                break;
            case "SAT8":
                sat8.gameObject.SetActive(true);
                spotLight.localScale = new Vector3(5.7f, 1, 1);
                break;
            case "M4SOPMODII":
                m4sopmodii.gameObject.SetActive(true);
                spotLight.localScale = new Vector3(5.7f, 1, 1);
                break;
            default:
                break;
        }
        print(main);
        print(sub);
    }

    public void ToMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }
}
