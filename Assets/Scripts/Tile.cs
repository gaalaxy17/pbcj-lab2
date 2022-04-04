using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private bool isRevealed = false; // Indicador da carta virada ou n�o
    public Sprite cardFront; // Sprite da carta desejada
    public Sprite cardBack; // Sprite do avesso da Carta

    public Sprite newCard; // Atualiza a carta

    /* Start is called before the first frame update */
    void Start()
    {
        HideCard();
    }

    /* Update is called once per frame */
    void Update()
    {
        
    }

    /* Chama a fun��o que seleciona a carta quando ele clica no Tile */
    public void OnMouseDown()
    {
        GameObject.Find("gameManager").GetComponent<ManageCards>().SelectedCard(gameObject);
    }

    /* Esconde a carta */
    public void HideCard()
    {
        GetComponent<SpriteRenderer>().sprite = cardBack;
        isRevealed = false;
    }

    /* Mostra a carta */
    public void ShowCard()
    {
        GetComponent<SpriteRenderer>().sprite = cardFront;
        isRevealed = true;
    }

    /* Define a nova sprite da frente da carta */
    public void SetFrontCard(Sprite newFrontCard)
    {
        cardFront = newFrontCard;
    }
}
