using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ManageCredits : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
  
    }

    /* Carrega a cena pricipal do jogo */
    public void Menu()
    {
        SceneManager.LoadScene("Lab2_start");
    }

}
