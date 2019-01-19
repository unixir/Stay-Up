using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuBehaviour : MonoBehaviour
{
    public TextMeshProUGUI HighScoreText;
    public SaveSystemSetup saveSystemSetup;
    public Animator panelAC;
    public Toggle soundToggle;
    public AudioSource[] audioSources;
    private void Start()
    {
        if (SaveSystem.GetBool("SoundOn") != null)
            soundToggle.isOn = SaveSystem.GetBool("SoundOn");
        else
            SaveSystem.SetBool("SoundOn", true);
        panelAC.SetBool("PanelVisible", false);
        HighScoreText.text = SaveSystem.GetInt("HighScore").ToString();
        if (HighScoreText.text == "0")
        {
            SaveSystem.SetInt("HighScore", 0);
        }
    }

    public void SoundChange()
    {
        SaveSystem.SetBool("SoundOn", soundToggle.isOn);
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.volume = soundToggle.isOn ? 1 : 0;
        }
        SaveSystem.SaveToDisk();
    }
    public void PlayGame()
    {
        Debug.Log("In playgame");
        panelAC.SetBool("PanelVisible",true);
        Invoke("LoadGame", 0.5f);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
