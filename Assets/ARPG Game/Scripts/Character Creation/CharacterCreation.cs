using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterCreation : MonoBehaviour
{
    public GameObject characterPanel;
    public GameObject confirmationPanel;
    // Start is called before the first frame update
    void Start()
    {
        characterPanel.SetActive(true);
        confirmationPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CreateCharacter()
    {
        // Logic for character creation goes here
        Debug.Log("Character created!");
        characterPanel.SetActive(false);
        confirmationPanel.SetActive(true);
        confirmationPanel.GetComponentInChildren<TMP_Text>().text = "Are you sure you want this character?";
    }

    public void ConfirmCharacterCreation()
    {
        Debug.Log("Character creation confirmed.");
        // Load the next scene or perform any action needed to continue
        UnityEngine.SceneManagement.SceneManager.LoadScene(5); // Assuming scene index 5 is the game world scene
    }

    public void CancelConfirmation()
    {
        Debug.Log("Character creation cancelled.");
        confirmationPanel.SetActive(false);
        characterPanel.SetActive(true); // Return to character creation panel
    }

    public void CancelCharacterCreation()
    {
        Debug.Log("Character creation cancelled.");
        confirmationPanel.SetActive(false);
        characterPanel.SetActive(true); // Return to character creation panel
        UnityEngine.SceneManagement.SceneManager.LoadScene(1); // Assuming scene index 1 is the class selection scene
    }
}
