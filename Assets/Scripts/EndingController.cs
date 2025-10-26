using Spine.Unity;
using TMPro;
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
    }

    public void click_toMain() {
        //보유 캐릭터 초기화
        PlayerPrefs.SetInt(PlayerPrefs.GetString("rescue"), 1);
        //진행도 초기화
        PlayerPrefs.SetString("main",null); //게임 진입 시 선택한 캐릭터
        PlayerPrefs.SetString("rescue",null); //게임에서 구출한 캐릭터
        PlayerPrefs.SetInt("level", 0);
        
        SceneManager.LoadScene("MainMenu");
    }
}
