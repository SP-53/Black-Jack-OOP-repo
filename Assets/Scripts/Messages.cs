using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Message accoss the game display.
/// </summary>
public class Messages : MonoBehaviour
{
    /// <summary>
    /// Instance of the Messages class to ensure only one instance exists throughout the game.
    /// </summary>
    public static Messages instance;
    /// <summary>
    /// The text component where messages are displayed.
    /// </summary>
    [SerializeField] TMP_Text messageText;

    /// <summary>
    /// Initializes the singleton instance during the Unity Awake lifecycle phase.
    /// </summary>
    void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// Sets the displayed message in the UI to the given message string.
    /// </summary>
    /// <param name="message"></param>
    public void SetTheMessage(string message)
    {
        messageText.text = message;
    }
}

