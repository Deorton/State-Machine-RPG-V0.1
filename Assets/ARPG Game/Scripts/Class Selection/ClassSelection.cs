using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClassSelection : MonoBehaviour
{
    public GameObject confirmationPanel;

    void Start()
    {
        confirmationPanel.SetActive(false);
    }

    public void ContinueButton()
    {
        confirmationPanel.SetActive(true);
        confirmationPanel.GetComponentInChildren<TMP_Text>().text = "Are you sure you want to continue?";


    }

    public void ConfirmSelection()
    {
        Debug.Log("Continuing to the game...");
        // Load the next scene or perform any action needed to continue
        UnityEngine.SceneManagement.SceneManager.LoadScene(2); // Assuming scene index 1 is the game scene
    }

    public void CancelSelection()
    {
        Debug.Log("Cancelled class selection.");
        confirmationPanel.SetActive(false);
    }

    public void BackToMainMenu()
    {
        Debug.Log("Returning to main menu...");
        UnityEngine.SceneManagement.SceneManager.LoadScene(0); // Assuming scene index 0 is the main menu
    }
}
