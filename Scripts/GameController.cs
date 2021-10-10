using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour
{
    public GameObject PlayerPrefab;
    public GameObject[] AsteroidPrefabs;
    public GameObject Player;
    public int MaxAsteroids = 15;
    public GameObject SpawnPoint;
    public GameObject ScoreText;
    public float score = 0;
    public float gameSpeed = 0.1f;
    public float loseDistance = 1f;
    public float distanceSincePlacedAsteroid = 0f;
    public float minAsteroidPlaceDistance = 3f;
    public float maxAsteroidPlaceDistance = 5f;
    void Start()
    {
        Player = Instantiate(PlayerPrefab);
        loseDistance = Camera.main.orthographicSize * 1.25f;
        Player.transform.position = SpawnPoint.transform.position;

        minAsteroidPlaceDistance = Camera.main.orthographicSize / 4;
        maxAsteroidPlaceDistance = Camera.main.orthographicSize;
    }
    void Update()
    {
        UpdateScore();
        MoveCamera();
        if (HasLost()) {
            FindObjectOfType<AudioManager>().Play("scream");
            SceneManager.LoadScene(0);
        }
        HandleAsteroidPlacement();
    }
    void HandleAsteroidPlacement() {
        distanceSincePlacedAsteroid += gameSpeed * Time.deltaTime;
        if (distanceSincePlacedAsteroid > Random.Range(minAsteroidPlaceDistance, maxAsteroidPlaceDistance)) {
            GameObject newAsteroid = Instantiate(AsteroidPrefabs[Random.Range(0, AsteroidPrefabs.Length)]);
            newAsteroid.transform.position = new Vector3(
                Random.Range(Camera.main.transform.position.x - (Camera.main.orthographicSize * Camera.main.aspect), Camera.main.transform.position.x + (Camera.main.orthographicSize * Camera.main.aspect)),
                distanceSincePlacedAsteroid + Camera.main.transform.position.y + Camera.main.orthographicSize,
                0f
            );
            distanceSincePlacedAsteroid = 0;
        }
    }
    void MoveCamera() {
        Camera.main.transform.position = new Vector3(
            Camera.main.transform.position.x,
            Camera.main.transform.position.y + gameSpeed * Time.deltaTime,
            Camera.main.transform.position.z
        );
    }
    bool HasLost() {
        return (
            Mathf.Abs(Camera.main.transform.position.y - Player.transform.position.y) > loseDistance ||
            Mathf.Abs(Camera.main.transform.position.x - Player.transform.position.x) > (Camera.main.aspect * Camera.main.orthographicSize)
            );
    }
    int GetScore() {
        return (int)(Player.transform.position.y - SpawnPoint.transform.position.y);
    }

    void UpdateScoreText() {
        ScoreText.GetComponent<Text>().text = "SCORE: " + score.ToString();
    }

    void UpdateScore() {
        score = GetScore();
        UpdateScoreText();
    }

}
