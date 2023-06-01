using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerX : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI gameOverText;
    public GameObject titleScreen;
    public Button restartButton;
    private int time;

    public List<GameObject> targetPrefabs;

    private int score;
    private float spawnRate = 1.5f;
    private bool isGameActive;

    private float spaceBetweenSquares = 2.5f;
    private float minValueX = -3.75f; // x value of the center of the left-most square
    private float minValueY = -3.75f; // y value of the center of the bottom-most square

    private Coroutine spawnCoroutine;
    private Coroutine timeCoroutine;

    private List<Vector3> occupiedPositions = new List<Vector3>();

    public bool IsGameActive { get { return isGameActive; } }

    public void StartGame(int difficulty)
    {
        spawnRate /= difficulty;
        isGameActive = true;
        spawnCoroutine = StartCoroutine(SpawnTarget());
        timeCoroutine = StartCoroutine(TimeUpdate());
        score = 0;
        time = 30; // Ustal czas gry
        UpdateScore(0);
        titleScreen.SetActive(false);
    }

    IEnumerator TimeUpdate()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(1);
            UpdateTime();
        }
    }

    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targetPrefabs.Count);

            if (isGameActive)
            {
                Vector3 spawnPosition = RandomSpawnPosition();
                while (IsPositionOccupied(spawnPosition))
                {
                    spawnPosition = RandomSpawnPosition();
                }

                Instantiate(targetPrefabs[index], spawnPosition, targetPrefabs[index].transform.rotation);
                occupiedPositions.Add(spawnPosition);
            }
        }
    }

    Vector3 RandomSpawnPosition()
    {
        float spawnPosX = minValueX + (RandomSquareIndex() * spaceBetweenSquares);
        float spawnPosY = minValueY + (RandomSquareIndex() * spaceBetweenSquares);

        Vector3 spawnPosition = new Vector3(spawnPosX, spawnPosY, 0);
        return spawnPosition;
    }

    int RandomSquareIndex()
    {
        return Random.Range(0, 4);
    }

    bool IsPositionOccupied(Vector3 position)
    {
        foreach (Vector3 occupiedPosition in occupiedPositions)
        {
            if (Vector3.Distance(occupiedPosition, position) < spaceBetweenSquares)
            {
                return true;
            }
        }
        return false;
    }

    public void RemoveOccupiedPosition(Vector3 position)
    {
        occupiedPositions.Remove(position);
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void UpdateTime()
    {
        time -= 1;
        timeText.text = "Time: " + time;

        if (time <= 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        StopCoroutine(spawnCoroutine);
        StopCoroutine(timeCoroutine);

        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        isGameActive = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

