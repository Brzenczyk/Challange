using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetX : MonoBehaviour
{
    private Rigidbody rb;
    private GameManagerX gameManagerX;
    public int pointValue;
    public GameObject explosionFx;

    private float minValueX = -3.75f; // the x value of the center of the left-most square
    private float minValueY = -3.75f; // the y value of the center of the bottom-most square
    private float spaceBetweenSquares = 2.5f; // the distance between the centers of squares on the game board

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gameManagerX = GameObject.Find("Game Manager").GetComponent<GameManagerX>();

        transform.position = RandomSpawnPosition();
        gameManagerX.RemoveOccupiedPosition(transform.position);
    }

    // When target is clicked, destroy it, update score, and generate explosion if object is bad - GameOver
    private void OnMouseDown()
    {
        if (gameManagerX.IsGameActive)
        {
            Destroy(gameObject);
            gameManagerX.UpdateScore(pointValue);
            Explode();
            gameManagerX.RemoveOccupiedPosition(transform.position);
        }
    }

    Vector3 RandomSpawnPosition()
    {
        int randomIndexX = RandomSquareIndex();
        int randomIndexY = RandomSquareIndex();

        float spawnPosX = minValueX + (randomIndexX * spaceBetweenSquares);
        float spawnPosY = minValueY + (randomIndexY * spaceBetweenSquares);

        Vector3 spawnPosition = new Vector3(spawnPosX, spawnPosY, 0);
        return spawnPosition;
    }


    int RandomSquareIndex()
    {
        return Random.Range(0, 4);
    }

    void Explode()
    {
        Instantiate(explosionFx, transform.position, explosionFx.transform.rotation);
    }
}

