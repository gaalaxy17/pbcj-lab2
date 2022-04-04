using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class ManageCards : MonoBehaviour
{
    public GameObject card; // A carta a ser descartada
    private bool isFirstCardSelected, isSecondCardSelected; // indicadores para cada carta escolhida em cada linha
    private GameObject card1, card2; // gameObjects da primeira e segunda carta selecionada
    private string cardRow1, cardRow2; // Linha da carta selecionada

    bool pausedTimer, triggeredTime; // Indicador de pausa no Timer ou StartTimer
    float timer; // Variavel de tempo
    
    int triesCount = 0; // N�mero de tentativas
    int hitCount = 0; // N�mero de matches (acertos)
    AudioSource okSound; // Som de sucesso

    int lastGameScore = 0; // Variavel que salva o score da partida anterior

    /* Start is called before the first frame update */
    void Start()
    {
        ShowCards();
        UpdateTries();
        okSound = GetComponent<AudioSource>();
        lastGameScore = PlayerPrefs.GetInt("Tries", 0);
        GameObject.Find("lastPlayed").GetComponent<Text>().text = "Jogo Anterior: " + lastGameScore;
    }

    /* 
     * Update is called once per frame
     * A cada frame, caso o timer esteja ativo, valida se as duas cartas escolhidas se referem a mesma tag, caso positivo, destroi as cartas e soma 1 no contador de acertos
     * Caso contr�rio, esconde as cartas novamente 
    */
    void Update()
    {
        if (triggeredTime)
        {
            timer += Time.deltaTime;
            if(timer > 1)
            {
                pausedTimer = true;
                triggeredTime = false;
                if(card1.tag == card2.tag)
                {
                    okSound.Play();
                    Destroy(card1);
                    Destroy(card2);
                    hitCount++;
                    if(hitCount == 13)
                    {
                        PlayerPrefs.SetInt("Tries", triesCount);
                        PlayerPrefs.SetString("status", "victory");
                        SceneManager.LoadScene("Lab2_end");
                    }
                }
                else
                {
                    GameObject.Find("errorSound").GetComponent<AudioSource>().Play();
                    card1.GetComponent<Tile>().HideCard();
                    card2.GetComponent<Tile>().HideCard();
                }
                isFirstCardSelected = false;
                isSecondCardSelected = false;
                card1 = null;
                card2 = null;
                cardRow1 = "";
                cardRow2 = "";
                timer = 0;

                if(triesCount > 40)
                {
                    PlayerPrefs.SetString("status", "defeat");
                    SceneManager.LoadScene("Lab2_end");
                }

            }
        }
    }

    /* Exibe as cartas na tela */
    void ShowCards()
    {
        int[] shuffledArray = CreateShuffledArray();
        int[] shuffledArray2 = CreateShuffledArray();

        //Instantiate(card, new Vector3(0, 0, 0), Quaternion.identity);
        for (int i = 0; i < 13; i++)
        {
            // AddCard(i);
            AddCard(0, i, shuffledArray[i]);
            AddCard(1, i, shuffledArray2[i]);
        }
    }

    /* Adiciona uma nova carta, configurando os parametros do componente e carregando a sprite desejada */
    void AddCard(int row, int rank, int value)
    {
        GameObject center = GameObject.Find("CenterOfScreen");
        float scaleX = card.transform.localScale.x;
        float scaleFactorX = (650 * scaleX) / 100.0f;
        float scaleFactorY = (945 * scaleX) / 100.0f;

        Vector3 newPos = new Vector3(center.transform.position.x + ((rank - 13 / 2) * scaleFactorX), center.transform.position.y + (row-2/2) * scaleFactorY, center.transform.position.z);
        GameObject c = (GameObject)(Instantiate(card, newPos, Quaternion.identity));
        c.tag = "" + (value + 1);
        c.name = "" + row + "_" + value ;
        string cardName = "";
        string cardNumber = "";

        if (value == 0) cardNumber = "ace";
        else if (value == 10) cardNumber = "jack";
        else if (value == 11) cardNumber = "queen";
        else if (value == 12) cardNumber = "king";
        else cardNumber = "" + (value + 1);

        if(row == 1) cardName = cardNumber + "_of_hearts";
        else cardName = cardNumber + "_of_clubs";

        Sprite s1 = (Sprite)(Resources.Load<Sprite>("Cards/" + cardName));
        GameObject.Find("" + row + "_" + value).GetComponent<Tile>().SetFrontCard(s1);

    }

    /* Cria um array de inteiros aleat�rio */
    public int[] CreateShuffledArray()
    {
        int[] newArray = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
        int temp;
        for(int t=0; t<13; t++)
        {
            temp = newArray[t];
            int r = Random.Range(t, 13);
            newArray[t] = newArray[r];
            newArray[r] = temp;
        }
        return newArray;
    }

    /* Seleciona as duas primeiras cartas clicadas */
    public void SelectedCard(GameObject card)
    {
        if (!isFirstCardSelected)
        {
            string row = card.name.Substring(0, 1);
            cardRow1 = row;
            isFirstCardSelected = true;
            card1 = card;
            card1.GetComponent<Tile>().ShowCard();
        }
        else if(isFirstCardSelected && !isSecondCardSelected)
        {
            string row = card.name.Substring(0, 1);
            cardRow2 = row;
            isSecondCardSelected = true;
            card2 = card;
            card2.GetComponent<Tile>().ShowCard();
            VerifyCards();
        }

    }

    /* Aciona o temporizador que junto com o m�todo update validam as cartas */
    public void VerifyCards()
    {
        TriggerTimer();
        triesCount++;
        UpdateTries();
    }

    /* Aciona o timer atrav�s da atribui��o de valores nas variaveis de controle do timer */
    public void TriggerTimer()
    {
        pausedTimer = false;
        triggeredTime = true;
    }

    /* Atualiza o contador de tentativas em tela */
    void UpdateTries()
    {
        GameObject.Find("triesCount").GetComponent<Text>().text = "Tentativas: " + triesCount;
    }

}
