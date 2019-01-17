using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuBehaviour : MonoBehaviour
{
    public TextMeshProUGUI HighScoreText;
    public SaveSystemSetup saveSystemSetup;

    private void Start()
    {
        HighScoreText.text = SaveSystem.GetInt("HighScore").ToString();
        if (HighScoreText.text == "0")
        {
            SaveSystem.SetInt("HighScore", 0);
        }
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("MainGame");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
