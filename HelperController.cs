using UnityEngine;
using System.Collections;

public class HelperController : MonoBehaviour {

    Rigidbody2D rb;
    public GameObject bullet;
    public Transform cannonThroat;

    Transform player;

    public float helperHealth;
    public float speed;

    public float helperFireRate;
    float helperNextFire = 0f;

    GameController gameController;

    bool closeEnough;

    void Start()
    {
        GameObject controller = GameObject.FindWithTag("GameController");
        gameController = controller.GetComponent<GameController>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        helperHealth = 40f;
        closeEnough = true;
        player = GameObject.Find("Player").transform;
    }

    void Update()
    {
        Shoot();
        LookAt();
        if (helperHealth <= 0f)
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        Move();
        MoveCloser();
    }

    void Shoot()
    {
        if (gameController.endGame == false)
        {
            if (Time.time > helperNextFire)
            {
                helperNextFire = Time.time + helperFireRate;
                Instantiate(bullet, cannonThroat.position, cannonThroat.rotation);
            }
        }
    }

    void LookAt()
    {
        Vector2 offset = (FindTheClosest().transform.position - transform.position).normalized;
        transform.up = offset;
    }

    void Move()
    {
        rb.AddForce(transform.up * speed);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "EnemyBullet")
        {
            helperHealth -= 5f;
        }
        if (other.gameObject.tag == "Enemy")
        {
            helperHealth -= 10f;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            closeEnough = false;       
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            closeEnough = true;
        }
    }

    void MoveCloser()
    {
        if (closeEnough == false)
        {
            Vector2 offset = player.position - transform.position;
            rb.AddForce(offset * speed * 2);
        }
    }

    GameObject FindTheClosest()
    {
        float closest = Mathf.Infinity;
        GameObject[] EnemyList;
        EnemyList = GameObject.FindGameObjectsWithTag("Enemy");
        int closestNumber = EnemyList.Length + 100;
        float[] EnemyDistances = new float [EnemyList.Length];
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
