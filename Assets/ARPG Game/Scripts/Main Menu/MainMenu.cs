using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject MainMenuPanel;
    public GameObject loadGamePanel;
    public GameObject settingsPanel;

    void Start()
    {
        MainMenuPanel.SetActive(true);
        loadGamePanel.SetActive(false);
        settingsPanel.SetActive(false);
    }

    public void StartNewGame()
    {
        Debug.Log("Starting a new game...");
        SceneManager.LoadScene(1); // Assuming scene index 1 is the game scene
    }

    public void LoadGame()
    {
        Debug.Log("Loading game...");
        MainMenuPanel.SetActive(false);
        loadGamePanel.SetActive(true);
        settingsPanel.SetActive(false);
    }

    public void CancelLoadGame()
    {
        Debug.Log("Cancelled loading game.");
        MainMenuPanel.SetActive(true);
        loadGamePanel.SetActive(false);
    }

    public void SettingsSelection()
    {
        Debug.Log("Settings selected.");
        MainMenuPanel.SetActive(false);
        loadGamePanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void CancelSettings()
    {
        Debug.Log("Cancelled loading game.");
        MainMenuPanel.SetActive(true);
        loadGamePanel.SetActive(false);
        settingsPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
