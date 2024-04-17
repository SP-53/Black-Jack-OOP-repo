using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
/// <summary>
/// Manages the main logic and all the interactions between the gameobject, player and dealer.
/// Also includes all the methods of different class and also the udpates game 
/// state and UI.
/// </summary>



public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Singleton instance of the class
    /// </summary>
    public static GameManager instance; 

    /// <summary>
    /// List to store cards created by player and dealer.
    /// </summary>
    List<Card> playerCards = new List<Card>();
    List<Card> dealerCards = new List<Card>();

    /// <summary>
    /// To display points or player and dealer.
    /// </summary>
    [SerializeField] TMP_Text playerPointsText;
    [SerializeField] TMP_Text dealerPointsText;




    void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// Initializes the game by setting up the deck and displaying a welcome message.
    /// </summary>
    void Start()
    {
        InitializeGame();
    }

    /// <summary>
    /// Sets up the initial game environment, including shuffling the deck and preparing UI components.
    /// </summary>
    void InitializeGame()
    {
        Deck.instance.Initialize();
        Messages.instance.SetTheMessage("<color=red>BLACK JACK</color>, make a bet and start the game!");
    }


    /// <summary>
    /// Begins the process of dealing initial cards to both the player and the dealer.
    /// </summary>
    public void GetCards()
    {
        StartCoroutine(GetInitialCards());
        Betting.instance.ActivatePanel(false);
    }


    /// <summary>
    /// Coroutine to deal two cards each to the player and the dealer with a slight delay between each card.
    /// </summary>
    IEnumerator GetInitialCards()
    {
        playerCards.Add(Deck.instance.HandOutCards(true));
        yield return new WaitForSeconds(0.2f); 
        playerCards.Add(Deck.instance.HandOutCards(true));
        yield return new WaitForSeconds(0.2f); 
        dealerCards.Add(Deck.instance.HandOutCards(false));
        yield return new WaitForSeconds(0.2f); 
        dealerCards.Add(Deck.instance.HandOutCards(false));
        yield return new WaitForSeconds(0.2f); 
        //CountPlayerPoints();
        CalculateResult(true, true);
        //CountDealerPoints();
    }

    /// <summary>
    /// Counts and returns the total points for the dealer's cards and updates the UI.
    /// </summary>
    int CountDealerPoints()
    {
        int points = 0;
        for (int i = 0; i < dealerCards.Count; i++)
        {
            points += dealerCards[i].GetPoints();
        }
        dealerPointsText.text = points.ToString();

        return points;
    }

    /// <summary>
    /// Counts and returns the total points for the player's cards and updates the UI.
    /// </summary>
    int CountPlayerPoints()
    {
        int points = 0;
        for (int i = 0; i < playerCards.Count; i++)
        {
            points += playerCards[i].GetPoints();
        }
        playerPointsText.text = points.ToString();
        return points;
    }


    /// <summary>
    /// Allows the player to take another card ("hit") and recalculates the result.
    /// </summary>
    public void Hit()
    {
        playerCards.Add(Deck.instance.HandOutCards(true));
        //CountPlayerPoints();
        CalculateResult(true, false);
    }



    /// <summary>
    /// Ends the player's turn and begins the dealer's turn.
    /// </summary>
    public void Stay()
    {
        StartCoroutine(DealerTurn());
    }

    /// <summary>
    /// Coroutine to manage the dealer's turn, dealing additional cards until a certain score threshold is reached.
    /// </summary>
    IEnumerator DealerTurn()
    {
        RevealDealerCards();
        int score = CountDealerPoints();
        int scoreToStop = 17;
        while(score <= scoreToStop)
        {
            yield return new WaitForSeconds(1f);
            Card newCard = Deck.instance.HandOutCards(false); // Assign the newly dealt card to a variable
            dealerCards.Add(newCard); // Add the newly dealt card to dealer's cards
            score = CountDealerPoints();
            RevealDealerCards();
        }
        CalculateResult(false, false);
    }

    /// <summary>
    /// Calculates the result of the game based on the current scores of the player and dealer.
    /// </summary>
    /// <param name="playerRequest"></param>
    /// <param name="isInitial"></param>
    void CalculateResult(bool playerRequest, bool isInitial)
    {
        int playerScore = CountPlayerPoints();
        if(playerRequest)
        {
            if(playerScore == 21 && isInitial)
            {
                //Win
                Betting.instance.RoundGameResult("win", true);
                return;
            } 

            if(playerScore > 21)
            {
                //lose
                Betting.instance.RoundGameResult("lose", false);
                return;
            }
        }
        else
        {
            int dealerScore = CountDealerPoints();
            
            if (dealerScore > 21)
            {
                // Win: Dealer busts
                Betting.instance.RoundGameResult("win", false);
                return;
            }

            if (dealerScore == playerScore)
            {
                // Draw: Equal scores
                Betting.instance.RoundGameResult("draw", false);
                return;
            }

            if (dealerScore < playerScore && playerScore <= 21)
            {
                // Win: Player score greater than dealer's without busting
                Betting.instance.RoundGameResult("win", false);
                return;
            }

            if (dealerScore > playerScore && dealerScore <= 21)
            {
                // Lose: Dealer score greater than player's without busting
                Betting.instance.RoundGameResult("lose", false);
                return;
            }
        }

        //restart game or end gamwe 
    }

    /// <summary>
    /// Initiates the process to start a new game round after a delay.
    /// </summary>
    public void ActivadeNewRound()
    {
        Invoke("StarANewRound",2f);
    }

    /// <summary>
    /// Resets the game environment for a new round, clearing old cards and resetting the deck.
    /// </summary>
    public void StarANewRound()
    {
        Deck.instance.RestoreCardsDeck();
        foreach(var card in playerCards)
        {
            Destroy(card.gameObject);
        }
        foreach(var card in dealerCards)
        {
            Destroy(card.gameObject);
        }
        playerCards.Clear();
        dealerCards.Clear();
        Betting.instance.ActivatePanel(true);

        playerPointsText.text = "--";
        dealerPointsText.text = "--";
    }




    /// <summary>
    /// Reveals all the dealer's cards to the player at the appropriate time.
    /// </summary>
    void RevealDealerCards()
    {
        foreach(var card in dealerCards)
        {
            card.RevealCard();
        }
    }
}
