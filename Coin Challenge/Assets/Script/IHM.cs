using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IHM : MonoBehaviour
{
    public List<Transform> LifePanelTransform;
    public static IHM Instance;
    public GameObject[] lifes = new GameObject[3];
    [SerializeField] TextMeshProUGUI score;
    private int i;
    private int value;
    public float chrono;
    [SerializeField] TextMeshProUGUI time;
    [SerializeField] GameObject gameOverGo;
    [SerializeField] GameObject gameWinGo;

    private void Start()
    {
        chrono = 60;                                    //valeur du chrono 
        DisplayGameOverMenu(false);
    }

    private void Update()
    {
        chrono -= Time.deltaTime;
        (time).text = ((int)chrono).ToString();                   //me sert a reduire le temps du chrono 
        if (chrono <= 0)
        {
            chrono = 0;
            DisplayGameOverMenu(true);
        }
    }


    private void Awake()
    {
        Instance = this;
    }

    public void UpdateLife()                                                            //Gere l'affichage de la vie du joueur 
    {
        
        foreach (Transform child in LifePanelTransform)
        {
            child.gameObject.SetActive(false);
        }
        for (int i = 0; i < InfosPlayer.Instance.life; i++)
        {
            LifePanelTransform[i].gameObject.SetActive(true);

        }
    }
    public void UpdateScore()
    {
        score.text = "score" + InfosPlayer.Instance.score;
    }

    public void OnRestartButtonClick()                                                      // Toute la partie controle des bouton 
    {
        SceneManager.LoadScene("GameScene");

    }
    public void OnMainMenuButtonClick()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void DisplayGameOverMenu(bool setActive)                                         //active le game over 
    {
        gameOverGo.SetActive(setActive);
    }

    public void DisplayWin(bool setActive)                                                              //active le win
    {
        gameWinGo.SetActive(setActive);
    }
}
