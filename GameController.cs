using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    public GameObject normalEnemy;
    public GameObject rocketEnemy;

    float normalNextEnemy = 0f;
    float rocketNextEnemy = 0f;
    public float normalEnemyRate;
    public float rocketEnemyRate;
    public float ero;

    public float score;
    public float money;

    public GUIText healthText;
    public GUIText scoreText;
    public GUIText deadText;
    public GUIText restartText;
    public GUIText moneyText;

    public bool endGame;

    PlayerController playerController;

    void Start()
    {
        GameObject player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
        score = 0f;
        money = 0f;
        deadText.text = "";
        UpdateScore();
        PlayerHealthUpdate();
        restartText.text = "";
        endGame = false;
    }

    void Update()
    {
        PlayerHealthUpdate();
        Ending();
        UpdateMoney();
        EnemyCreation();
        RocketEnemyCreation();
        Quit();

        if (playerController.playerHealth <= 0f)
        {
            endGame = true;
        }
    }

    void EnemyCreation()
    {
        if (Time.time > normalNextEnemy)
        {
            normalNextEnemy = Time.time + normalEnemyRate;
            float x = Random.Range(-160f, 160f);
            float y = Random.Range(-160f, 160f);
            Vector2 spawnPosition = new Vector2(x, y);
            Instantiate(normalEnemy, spawnPosition, Quaternion.identity);
        }
    }

    void RocketEnemyCreation()
    {
        if (Time.time > rocketNextEnemy)
        {
            rocketNextEnemy = Time.time + rocketEnemyRate;
            float x = Random.Range(-160f, 160f);
            float y = Random.Range(-160f, 160f);
            Vector2 spawnPosition = new Vector2(x, y);
            Instantiate(rocketEnemy, spawnPosition, Quaternion.identity);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Vector2 offset = transform.position - other.transform.position;
        other.gameObject.GetComponent<Rigidbody2D>().AddForce(offset * ero, ForceMode2D.Impulse);
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }
    
    public void AddScore(float scoreValue)
    {
        score += scoreValue;
        UpdateScore();
    }

    void PlayerHealthUpdate()
    {
        healthText.text = "Health: " + playerController.playerHealth;
    }

    void Ending()
    {
        if (endGame)
        {
            deadText.text = "You are dead!!!";
            restartText.text = "Press 'R' for restart!";
            if (Input.GetKeyDown(KeyCode.R))
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }
    }

    void UpdateMoney()
    {
        moneyText.text = "Money: " + money;
    }

    public void AddMoney(float moneyValue)
    {
        money += moneyValue;
        UpdateMoney();
    }

    void Quit()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
