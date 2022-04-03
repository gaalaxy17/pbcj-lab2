using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private bool isRevealed = false; // indicador da carta viorada ou não
    public Sprite cardFront; // Sprite da carta desejada
    public Sprite cardBack; // Sprite do avesso da Carta

    public Sprite newCard; // Atualiza a carta

    public

    // Start is called before the first frame update
    void Start()
    {
        hideCard();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseDown()
    {
        print("Pressinou num Tile");
        if (isRevealed) hideCard();
        else showCard();
        
    }

    public void hideCard()
    {
        GetComponent<SpriteRenderer>().sprite = cardBack;
        isRevealed = false;
    }

    public void showCard()
    {
        GetComponent<SpriteRenderer>().sprite = cardFront;
        isRevealed = true;
    }

    public void setFrontCard(Sprite newFrontCard)
    {
        cardFront = newFrontCard;
    }
}
