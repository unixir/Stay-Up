using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuBehaviour : MonoBehaviour
{
    public TextMeshProUGUI HighScoreText;
    public SaveSystemSetup saveSystemSetup;
    public Animator panelAC;
    private void Start()
    {
        //AnimationEvent animationEvent = new AnimationEvent
        //{
        //    functionName = "LoadGame"
        //};
        //AnimationClip clip= panelAC.runtimeAnimatorController.animationClips[0];
        //clip.AddEvent(animationEvent);

        HighScoreText.text = SaveSystem.GetInt("HighScore").ToString();
        if (HighScoreText.text == "0")
        {
            SaveSystem.SetInt("HighScore", 0);
        }
    }
    public void PlayGame()
    {
        Debug.Log("In playgame");
        panelAC.SetTrigger("FadeIn");
        Invoke("LoadGame", 0.5f);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("MainGame");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
