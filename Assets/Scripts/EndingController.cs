using Spine.Unity;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingController : MonoBehaviour {
    public SkeletonGraphic kar98k;
    public SkeletonGraphic sat8;
    public SkeletonGraphic m4sopmodii;
    public SkeletonGraphic mp7;
    private void Start() {
        string main = PlayerPrefs.GetString("main");
        string rescue = PlayerPrefs.GetString("rescue");

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
            case "MP7":
                mp7.gameObject.SetActive(true);
                break;
            default:
                break;
        }
        switch (rescue) {
            case "Kar98k":
                kar98k.gameObject.SetActive(true);
                break;
            case "SAT8":
                sat8.gameObject.SetActive(true);
                break;
            case "M4SOPMODII":
                m4sopmodii.gameObject.SetActive(true);
                break;
            case "MP7":
                mp7.gameObject.SetActive(true);
                break;
            default:
                break;
        }

        //구출 캐릭터 해금
        PlayerPrefs.SetInt(PlayerPrefs.GetString("rescue"), 1);

        MainMenuController.initPlayerPrefs();
    }

    public void click_toMain() {
        SceneManager.LoadScene("MainMenu");
    }
}
