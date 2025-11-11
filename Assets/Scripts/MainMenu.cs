using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    const string MoneyKey = "Money";
    const int DefaultMoney = 1000;
    public string gameSceneName = "SampleScene";

    public void NewGame()
    {
        PlayerPrefs.SetInt(MoneyKey, DefaultMoney);
        PlayerPrefs.Save();
        SceneManager.LoadScene(gameSceneName);
    }

    public void LoadGame()
    {
        if (!PlayerPrefs.HasKey(MoneyKey))
        {
            // if now save detect == new game
            PlayerPrefs.SetInt(MoneyKey, DefaultMoney);
            PlayerPrefs.Save();
        }
        SceneManager.LoadScene(gameSceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
