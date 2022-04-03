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
    
    int triesCount = 0; // Número de tentativas
    int hitCount = 0; // Número de matches (acertos)
    AudioSource okSound; // Som de sucesso

    int lastGameScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        ShowCards();
        UpdateTries();
        okSound = GetComponent<AudioSource>();
        lastGameScore = PlayerPrefs.GetInt("Tries", 0);
        GameObject.Find("lastPlayed").GetComponent<Text>().text = "Jogo Anterior: " + lastGameScore;
    }

    // Update is called once per frame
    void Update()
    {
        if (triggeredTime)
        {
            timer += Time.deltaTime;
            print(timer);
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
                        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    }
                }
                else
                {
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
            }
        }
    }

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

    void AddCard(int row, int rank, int value)
    {
        GameObject center = GameObject.Find("CenterOfScreen");
        float scaleX = card.transform.localScale.x;
        float scaleFactorX = (650 * scaleX) / 100.0f;
        float scaleFactorY = (945 * scaleX) / 100.0f;

        Vector3 newPos = new Vector3(center.transform.position.x + ((rank - 13 / 2) * scaleFactorX), center.transform.position.y + (row-2/2) * scaleFactorY, center.transform.position.z);
        // GameObject c = (GameObject)(Instantiate(card, new Vector3(0, 0, 0), Quaternion.identity));
        // GameObject c = (GameObject)(Instantiate(card, new Vector3(rank*1.5f, 0, 0), Quaternion.identity));
        GameObject c = (GameObject)(Instantiate(card, newPos, Quaternion.identity));
        c.tag = "" + (value + 1);
        c.name = "" + row + "_" + value ;
        string cardName = "";
        string cardNumber = "";

        /* if (rank == 0) cardNumber = "ace";
        else if (rank == 10) cardNumber = "jack";
        else if (rank == 11) cardNumber = "queen";
        else if (rank == 12) cardNumber = "king";
        else cardNumber = "" + (rank + 1); */

        if (value == 0) cardNumber = "ace";
        else if (value == 10) cardNumber = "jack";
        else if (value == 11) cardNumber = "queen";
        else if (value == 12) cardNumber = "king";
        else cardNumber = "" + (value + 1);

        if(row == 1) cardName = cardNumber + "_of_hearts";
        else cardName = cardNumber + "_of_clubs";


        Sprite s1 = (Sprite)(Resources.Load<Sprite>("Cards/" + cardName));
        print(s1);
        GameObject.Find("" + row + "_" + value).GetComponent<Tile>().SetFrontCard(s1);

    }

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

    public void VerifyCards()
    {
        TriggerTimer();
        triesCount++;
        UpdateTries();
    }

    public void TriggerTimer()
    {
        pausedTimer = false;
        triggeredTime = true;
    }

    void UpdateTries()
    { 
        GameObject.Find("triesCount").GetComponent<Text>().text = "Tentativas: " + triesCount;
    }

}
