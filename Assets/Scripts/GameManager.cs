using TMPro;
using UnityEngine;
using System.Collections;


public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI highScoreText;

    public GameObject block;
    public float maxX;
    public Transform spawnPoint;

    public float spawnRate = 2f;
    private float currentSpawnRate;
    public float minimumSpawnRate = 0.3f;

    bool gameStarted = false;

    public GameObject tapText;
    public TextMeshProUGUI scoreText;

    int score = 0;

    private void Start()
    {
        UpdateHighScoreText();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !gameStarted)
        {
            StartGame();
        }
    }

    void StartGame()
    {
        gameStarted = true;
        tapText.SetActive(false);

        currentSpawnRate = spawnRate;

        StartCoroutine(SpawnRoutine());
        StartCoroutine(ScoreRoutine()); // 👈 starter point pr sekund
    }

    IEnumerator SpawnRoutine()
    {
        while (gameStarted)
        {
            SpawnBlock();
            yield return new WaitForSeconds(currentSpawnRate);
        }
    }

    IEnumerator ScoreRoutine()
    {
        while (gameStarted)
        {
            yield return new WaitForSeconds(1f);
            AddScore();
        }
    }

    void SpawnBlock()
    {
        Vector3 spawnPos = spawnPoint.position;
        spawnPos.x = Random.Range(-maxX, maxX);

        Instantiate(block, spawnPos, Quaternion.identity);
    }

    void AddScore()
    {
        score++;
        scoreText.text = score.ToString();
        CheckHighScore();

        // Difficulty scaling hver 10 point
        if (score % 10 == 0)
        {
            currentSpawnRate -= 0.1f;

            if (currentSpawnRate < minimumSpawnRate)
            {
                currentSpawnRate = minimumSpawnRate;
            }
        }
    }

    void CheckHighScore()
    {
        if (score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", score);
            UpdateHighScoreText();
        }
    }

    void UpdateHighScoreText()
    {
        highScoreText.text = "HighScore: " + PlayerPrefs.GetInt("HighScore", 0);
    }
}