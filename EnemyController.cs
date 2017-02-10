using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

    public Transform cannonThroat;
    public GameObject enemyBullet;

    GameController gameController;

    Rigidbody2D rb;

    float nextFire = 0f;
    public float enemyFireRate;
    public float speed;
    public float enemyHealth;
    public float enemyScoreValue;

    bool farEnough;

    void Start()
    {
        GameObject controller = GameObject.FindWithTag("GameController");
        gameController = controller.GetComponent<GameController>();
        rb = GetComponent<Rigidbody2D>();
        farEnough = true;
        enemyHealth = 25f;
    }

    void Update()
    {
        LookAt();
        Shoot();
        if (enemyHealth <= 0f)
        {
            gameController.AddScore(enemyScoreValue);
            gameController.AddMoney(enemyScoreValue);
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        Move();
    }

    void LookAt()
    {
        if (gameController.endGame == false)
        { 
            Vector2 offset = (FindTheClosest().transform.position - transform.position).normalized;
            transform.up = offset;
        }
    }

    void Shoot()
    {
        if (gameController.endGame == false)
        {
            if (Time.time > nextFire)
            {
                nextFire = Time.time + enemyFireRate;
                Instantiate(enemyBullet, cannonThroat.position, cannonThroat.rotation);
            }
        }
    }

    void Move()
    {
        if (farEnough)
        {
            rb.AddForce(transform.up * speed);
        }
        if (farEnough == false)
        {
            rb.AddForce(-transform.up * speed * 0.25f);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Friendly")
        {
            farEnough = false;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Friendly")
        {
            farEnough = true;
        }
    }

    void OnCollisionEnter2D(Collision2D enemyCollider)
    {
        if (enemyCollider.gameObject.tag == "Bullet")
        {
            enemyHealth -= 5f;
        }
    }

    GameObject FindTheClosest()
    {
        float closest = Mathf.Infinity;
        GameObject[] EnemyList;
        EnemyList = GameObject.FindGameObjectsWithTag("Friendly");   
        int closestNumber = EnemyList.Length + 100;
        float[] EnemyDistances = new float[EnemyList.Length];
        for (int i = 0; i < EnemyList.Length; i++)
        {
            EnemyDistances[i] = (transform.position - EnemyList[i].transform.position).sqrMagnitude;
            if (EnemyDistances[i] < closest)
            {
                closest = EnemyDistances[i];
            }
        }
        for (int j = 0; j < EnemyList.Length; j++)
        {
            if (EnemyDistances[j] == closest)
            {
                closestNumber = j;
            }
        }
        return EnemyList[closestNumber];
    }
}
