using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ManageCards : MonoBehaviour
{
    public GameObject card; // A carta a ser descartada


    // Start is called before the first frame update
    void Start()
    {
        ShowCards();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ShowCards()
    {
        //Instantiate(card, new Vector3(0, 0, 0), Quaternion.identity);
        for(int i = 0; i < 13; i++)
        {
            AddCard(i);
        }
    }

    void AddCard(int rank)
    {
        GameObject center = GameObject.Find("CenterOfScreen");
        float scaleX = card.transform.localScale.x;
        float scaleFactorX = (650 * scaleX)/100.0f; 
        Vector3 newPos = new Vector3(center.transform.position.x + ((rank - 13 / 2) * scaleFactorX), center.transform.position.y, center.transform.position.z);
        // GameObject c = (GameObject)(Instantiate(card, new Vector3(0, 0, 0), Quaternion.identity));
        // GameObject c = (GameObject)(Instantiate(card, new Vector3(rank*1.5f, 0, 0), Quaternion.identity));
        GameObject c = (GameObject)(Instantiate(card, newPos, Quaternion.identity));
        c.tag = "" + rank;
        c.name = "" + rank;
        string cardName = "";
        string cardNumber = "";

        if (rank == 0) cardNumber = "ace";
        else if (rank == 10) cardNumber = "joke";
        else if (rank == 11) cardNumber = "queen";
        else if (rank == 12) cardNumber = "king";
        else cardNumber = "" + (rank + 1);

        cardName = cardNumber + "_of_clubs";
        Sprite s1 = (Sprite)(Resources.Load<Sprite>(cardName));
        print(s1);
        GameObject.Find("" + rank).GetComponent<Tile>().setFrontCard(s1);

    }
}
