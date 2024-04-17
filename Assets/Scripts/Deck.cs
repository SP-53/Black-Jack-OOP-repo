using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;

/// <summary>
/// Manage a deck of cards. which included initialization, shuffling and dealing to player.
/// 
/// </summary>


public class Deck : MonoBehaviour
{
    // Instance of a deck
    public static Deck instance; 


    // Represents a single class within a deck
    [System.Serializable]
    public class DeckCard
    {
        public string number;
        public string symbol;

        // Constructor to create a new card with a specified number and symbol.
        public DeckCard(string _number, string _symbol)
        {
            number = _number; 
            symbol = _symbol;
        }
    }

    // List of the card in the deck.
    public List<DeckCard> cardDeck = new List<DeckCard>();

    // values and symbol of cards
    string[] numberList = new string[13]{"2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A"};
    string[] symbolList = new string[4]{"spades", "hearths", "clubs", "diamonds"};

    // Prefab to create game object
    public GameObject cardprefab;
    [SerializeField] Transform playerHand;
    [SerializeField] Transform dealerHand;


    // void Start()
    // {
    //    InitializeDeck();
    //    ShuffleDeck(cardDeck);  
    //    HandOutCards(true);
    //    HandOutCards(false);
    // }
    


    /// <summary>
    /// To make sure the deck is initialize it only once.
    /// </summary>
    void Awake() 
    {
        instance = this;
    }

    /// <summary>
    /// Public method to create a deck of cards and shuffle the card.
    /// </summary>
    public void Initialize()
    {
        InitializeDeck();
        ShuffleDeck(cardDeck);  
    }
    

    /// <summary>
    /// Creates a full deck of cards with all combination of number and symbol.
    /// </summary>
    void InitializeDeck()
    {
        for (int j = 0; j < symbolList.Length; j++)
        {
            for (int i = 0; i < numberList.Length; i++)
            {
                cardDeck.Add(new DeckCard (numberList [i], symbolList [j]));
            }
        }
    }

    /// <summary>
    /// Shuffles the deck of cards.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="List"></param>
    static void ShuffleDeck<T>(IList<T> List)
    {
        int n = List.Count;
        System.Random range = new System.Random();
        while(n > 1)
        {
            n--;
            int val = range.Next(n + 1);
            T value = List[val];
            List[val] = List[n];
            List[n] = value;

        }
    }


    /// <summary>
    /// Hands out cards to the player or dealer based on the boolean IsPlayer
    /// </summary>
    /// <param name="IsPlayer"></param>
    /// <returns></returns>
    public Card HandOutCards(bool IsPlayer)
    {
        GameObject newCard =  Instantiate(cardprefab);
        DeckCard temp = cardDeck[0];
        Card nCard = newCard.GetComponent<Card>();
        nCard.SetUpCard(temp.number, temp.symbol, IsPlayer);
        cardDeck.Remove(temp);
        newCard.transform.SetParent(IsPlayer?playerHand:dealerHand, false);   
        
        return nCard;
    }



    /// <summary>
    /// Resets the deck by clearing it and re-initialzing.
    /// </summary>
    public void RestoreCardsDeck()
    {
        cardDeck.Clear();
        Initialize();
    }
}
