using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    private string GAMEPLAY_SCENE = "Gameplay";
    public void PlayGame()
    {
        int selectedCharacter = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
        
        GameManager.instance.CharIndex = selectedCharacter;

        SceneManager.LoadScene(GAMEPLAY_SCENE);
    }
}