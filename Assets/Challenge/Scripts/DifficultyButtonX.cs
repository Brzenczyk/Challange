using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButtonX : MonoBehaviour
{
    private Button button;
    private GameManagerX gameManagerX;
    public int difficulty;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SetDifficulty);

        // Znajdź obiekt GameManagerX w hierarchii sceny lub utwórz nowy obiekt GameManagerX
        gameManagerX = FindObjectOfType<GameManagerX>();
    }

    void SetDifficulty()
    {
        Debug.Log(button.gameObject.name + " was clicked");

        // Sprawdź, czy gameManagerX nie jest null przed wywołaniem metody na nim
        if (gameManagerX != null)
        {
            gameManagerX.StartGame(difficulty);
        }
        else
        {
            Debug.LogError("GameManagerX is missing or not assigned");
        }
    }
}

