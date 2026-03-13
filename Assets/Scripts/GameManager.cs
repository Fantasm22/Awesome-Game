using System.Collections;
using TMPro;
using UnityEngine;

using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;


public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI highScoreText;

    public Animator sequenceAnimator;

    public GameObject block;
    public float maxX;
    public Transform spawnPoint;

    public float spawnRate = 2f;
    private float currentSpawnRate;
    public float minimumSpawnRate = 0.3f;

    bool gameStarted = false;

    public GameObject tapText;
    public TextMeshProUGUI scoreText;
    public Transform turretGun;

    int score = 0;

    private void Start()
    {
        sequenceAnimator.enabled = false;
        UpdateHighScoreText();
    }

    void Update()
    {
        if (!gameStarted && IsScreenPressed())
        {
            StartGame();
        }

        if (gameStarted)
        {
            RotateTowardsNearestBlock();
        }
    }

    bool IsScreenPressed()
    {
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
            return true;

        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
            return true;

        return false;
    }

    void StartGame()
    {
        Application.targetFrameRate = 60;
        gameStarted = true;
        tapText.SetActive(false);
        highScoreText.gameObject.SetActive(false);

        currentSpawnRate = spawnRate;

        sequenceAnimator.enabled = true;
        sequenceAnimator.Play("Animation1", 0, 0f);

        StartCoroutine(SpawnRoutine());
        StartCoroutine(ScoreRoutine());
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
        if (block == null)
        {
            Debug.LogError("The Block prefab is missing! Please drag it into the Inspector.");
            return;
        }

        Vector3 spawnPos = spawnPoint.position;
        spawnPos.x = Random.Range(-maxX, maxX);

        Instantiate(block, spawnPos, Quaternion.identity);
    }

    void AddScore()
    {
        score++;
        scoreText.text = score.ToString();
        CheckHighScore();

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


    // TURRET SYSTEM
    void RotateTowardsNearestBlock()
    {
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");

        if (blocks.Length == 0)
            return;

        GameObject nearestBlock = blocks[0];
        Vector2 turretPos2 = new Vector2(turretGun.position.x, turretGun.position.y);
        Vector2 nearestPos2 = new Vector2(nearestBlock.transform.position.x, nearestBlock.transform.position.y);
        float shortestDistance = Vector2.Distance(turretPos2, nearestPos2);

        foreach (GameObject b in blocks)
        {
            Vector2 bPos2 = new Vector2(b.transform.position.x, b.transform.position.y);
            float distance = Vector2.Distance(turretPos2, bPos2);

            if (distance < shortestDistance)
            {
                nearestBlock = b;
                shortestDistance = distance;
            }
        }

        Vector2 direction = new Vector2(nearestBlock.transform.position.x - turretGun.position.x,
                                        nearestBlock.transform.position.y - turretGun.position.y);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        turretGun.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}