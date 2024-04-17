using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;

/// <summary>
/// This class generates the card’s properties, such as suit and value and updates the visual 
/// representation depending upon the value of the card.
/// </summary>

public class Card : MonoBehaviour
{
    [SerializeField] Sprite cardBack;
    [SerializeField] Sprite cardFront;
    [SerializeField] Image cardBackground;
    [SerializeField] GameObject spades;
    [SerializeField] GameObject hearths;
    [SerializeField] GameObject clubs;
    [SerializeField] GameObject diamonds;
    [SerializeField] TMP_Text numberText;

    int pointsOfCard;
    string symbol;
    string number;
    bool revealed;

    /// <summary>
    /// Initializes the card with a number, a symbol and a value.
    /// </summary>
    /// <param name="_number"></param>
    /// <param name="_symbol"></param>
    /// <param name="IsPlayer"></param>
    public void SetUpCard(string _number, string _symbol, bool IsPlayer)
    {
        number = _number;
        numberText.text = number;
        symbol = _symbol;
        revealed = IsPlayer;
        
        /* This is to generate the GUI of cards */
        switch(_symbol)
        {
            case "spades":
            spades.SetActive(true);
            hearths.SetActive(false);
            clubs.SetActive(false);
            diamonds.SetActive(false);
            break;

            case "hearths":
            spades.SetActive(false);
            hearths.SetActive(true);
            clubs.SetActive(false);
            diamonds.SetActive(false);
            break;

            case "clubs":
            spades.SetActive(false);
            hearths.SetActive(false);
            clubs.SetActive(true);
            diamonds.SetActive(false);
            break;

            case "diamonds":
            spades.SetActive(false);
            hearths.SetActive(false);
            clubs.SetActive(false);
            diamonds.SetActive(true);
            break;

        }

        /* Provide a value to the card. */
        switch(_number)
        {
            case "2":
            pointsOfCard = 2;
            break;

            case "3":
            pointsOfCard = 3;
            break;

            case "4":
            pointsOfCard = 4;
            break;

            case "5":
            pointsOfCard = 5;
            break;

            case "6":
            pointsOfCard = 6;
            break;

            case "7":
            pointsOfCard = 7;
            break;

            case "8":
            pointsOfCard = 8;
            break;

            case "9":
            pointsOfCard = 9;
            break;

            case "10":
            pointsOfCard = 10;
            break;

            case "J":
            pointsOfCard = 10;
            break;

            case "Q":
            pointsOfCard = 10;
            break;

            case "K":
            pointsOfCard = 10;
            break;

            case "A":
            pointsOfCard = 11;
            break;
        }

        /* Reveal the card in player's hand.*/
        if(!IsPlayer)
        {
            cardBackground.sprite = cardBack;
            spades.transform.parent.gameObject.SetActive(false);
            numberText.gameObject.SetActive(false);

        }


    }

    /// <summary>
    /// Return the value of the card.
    /// </summary>
    /// <returns></returns>
    public int GetPoints()
    {
        return pointsOfCard;
    }

    /// <summary>
    /// To  reveal the cards.
    /// </summary>
    public void RevealCard()
    {
        if(!revealed)
        {
            cardBackground.sprite = cardFront;
            spades.transform.parent.gameObject.SetActive(true);
            numberText.gameObject.SetActive(true);
            revealed = true;
        }
    }
}
