using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ManageEnd : MonoBehaviour
{
    public GameObject message; // Mensagem exibida no topo da tela de finalização

    /* Start is called before the first frame update */
    void Start()
    {
        this.message = GameObject.Find("message");                
        PlayerPrefs.SetInt("score", 0);

        this.HandleStatusCondition(PlayerPrefs.GetString("status"));
    }

    /* Update is called once per frame */
    void Update()
    {
        
    }

    /* Gerencia o status final do jogo */
    public void HandleStatusCondition(string statusValue)
    {
        if(statusValue == "victory"){
            GameObject.Find("congratulationsSound").GetComponent<AudioSource>().Play();
            GameObject.Find("successMusic").GetComponent<AudioSource>().Play();
            this.message.GetComponent<Text>().color = Color.green;
            this.message.GetComponent<Text>().text = "VICTORY!";
        }
        else{
            GameObject.Find("youLoseSound").GetComponent<AudioSource>().Play();
            GameObject.Find("defeatMusic").GetComponent<AudioSource>().Play();
            this.message.GetComponent<Text>().color = Color.red; 
            this.message.GetComponent<Text>().text = "DEFEAT!";
        }
    }

    /* Carrega a cena pricipal do jogo */
    public void StartGameWorld()
    {
        SceneManager.LoadScene("Lab2");
    }
    /* Carrega a cena de créditos */
    public void GoToCredits()
    {
        SceneManager.LoadScene("Lab2_endCredits");
    }
    /* Fecha o jogo */
    public void QuitGame()
    {
        
    }
}
