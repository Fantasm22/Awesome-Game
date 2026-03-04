using TMPro;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI highScoreText;

    public GameObject block;
    public float maxX;
    public Transform spawnPoint;
    public float spawnRate;


    bool gameStarted = false;

    public GameObject tapText;
    public TextMeshProUGUI scoreText;

    int score = 0;

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetMouseButtonDown(0) && !gameStarted)
        {
            StartSpawning();

            gameStarted = true;
            tapText.SetActive(false);





        }



    }

    private void Start()
    {
        UpdateHighScoreText();
    }


    private void StartSpawning()
    {
        InvokeRepeating("SpawnBlock", 0.5f, spawnRate);
    }


    private void SpawnBlock()
    {
        Vector3 spawnPos = spawnPoint.position;

        spawnPos.x = Random.Range(-maxX, maxX);

        Instantiate(block, spawnPos, Quaternion.identity);

        score++;

        scoreText.text = score.ToString();
        CheckHighScore();

        PlayerPrefs.SetInt("HighScore", score);
        PlayerPrefs.GetInt("HighScore");
    }

    void CheckHighScore()
    {
        if(score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
    }

    void UpdateHighScoreText()
    {
        highScoreText.text = $"HighScore: {PlayerPrefs.GetInt("HighScore", 0)}";
    }
}
