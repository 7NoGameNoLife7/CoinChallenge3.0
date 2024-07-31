using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void OnPlayButtonClick()                                         //Sert au lancement du jeux 
    {
        SceneManager.LoadScene("GameScene");

    }
    public void OnQuitButtonClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
#if UNITY_STANDALONE                                                        // Gere la fermeture du jeux 
        Application.Quit();
#endif
    }
}
