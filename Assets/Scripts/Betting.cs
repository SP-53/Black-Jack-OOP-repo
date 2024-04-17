using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// Manages the betting logic for the game , including updating the player's money,
/// handling bets and determining game outcomes based on player's bet.
/// </summary>




public class Betting : MonoBehaviour
{
    // Instance of the betting class.
    public static Betting instance;

    // Initial amount of money the player start with.
    [SerializeField] int startMoney = 50;

    // To track the current amount of money the player has.
    int currentMoney;
    // The current amount of player is betting.
    int currentBet;

    // UI to display the current amount of money
    [SerializeField] TMP_Text moneyText;

    // Panel containing the betting buttons.
    [SerializeField] GameObject betPanel;

    // Button with different value for money.
    [SerializeField] Button fiveDollar;
    [SerializeField] Button tenDollar;
    [SerializeField] Button twentyDollar;
    [SerializeField] Button fiftyDollar;
    [SerializeField] Button hundredDollar;

    // Game over display.
    [SerializeField] GameObject gameOver;
    // Flag to indicate if the game has been lost.
    bool lostGame;



    // Sets it to inital state.
    void Awake()
    {
        instance = this;
    }

    // Sets the initial money and update the  UI.
    void Start()
    {
        currentMoney = startMoney;
        UpdateMoneyAmount();
        gameOver.SetActive(false);
        //ActivatePanel(true);
    }
    

    // Update the display money amount on UI.
    void UpdateMoneyAmount()
    {
        moneyText.text = currentMoney.ToString() + "$";
    }

    /// <summary>
    /// Activate or deactive the betting panel and update the interaction 
    /// of betting display based on the money.
    /// </summary>
    /// <param name="on"></param>
    public void ActivatePanel(bool on)
    {
        betPanel.SetActive(on);
        if(on)
        {
            currentBet = 0;
            fiveDollar.interactable = (currentMoney >= 5)? true: false;
            tenDollar.interactable = (currentMoney >= 10)? true: false;
            twentyDollar.interactable = (currentMoney >= 20)? true: false;
            fiftyDollar.interactable = (currentMoney >= 50)? true: false;
            hundredDollar.interactable = (currentMoney >= 100)? true: false;
        }
    }

    /// <summary>
    /// Sets the current bet and update the money amounts and UI accordingly.
    /// </summary>
    /// <param name="bet"></param>
    public void SetBet(int bet)
    {
        currentMoney += currentBet;
        currentBet = bet;
        currentMoney -= bet;
        UpdateMoneyAmount();
        
    }
    /// <summary>
    /// Initiates the dealing of cards if a bet has been placed; 
    /// otherwise prompts the user to place a bet.
    /// </summary>
    public void Deal()
    {
        if (currentBet > 0)
        {
            ActivatePanel(false);
            GameManager.instance.GetCards();
            
        }
        else
        {
            //Debug.Log("Cannot deal without placing a bet.");
            Messages.instance.SetTheMessage("Place a <color=green>BET</color> before you <color=purple>DEAL</color>");
        }
    }


    /// <summary>
    /// Handles the outcome of game round, updates the player money based on result.
    /// </summary>
    /// <param name="result"></param>
    /// <param name="isBlackJack"></param>
    public void RoundGameResult(string result, bool isBlackJack)
    {
        switch(result)
        {
            case"win":
            currentMoney += currentBet * 2;
            UpdateMoneyAmount();
            if(isBlackJack)
            {
                Messages.instance.SetTheMessage("<color=purple>BLACK JACK!</color>");
            }
            else
            {
                Messages.instance.SetTheMessage("<color=green>YOU WON</color>");
            }
            Debug.Log("Player Win");
            break;
            case"lose":
            Debug.Log("Player Lost");
            Messages.instance.SetTheMessage("<color=red>YOU LOST</color>");

            if(currentMoney <= 0)
            {
                lostGame = true;
                Messages.instance.SetTheMessage("<color=red>GAME OVER</color>");
            }

            break;
            case"draw":
            currentMoney += currentBet;
            UpdateMoneyAmount();
            Debug.Log("Draw");
            Messages.instance.SetTheMessage("<color=yellow>IS A DRAW</color>");

            break;
        }
        //ActivatePanel(true);
        //reset gameManager and Deck
        if(!lostGame)
        {
            GameManager.instance.ActivadeNewRound();
        }
        else
        {
            gameOver.SetActive(true);
        }
    }
}
