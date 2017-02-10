using UnityEngine;
using System.Collections;

public class RocketController : MonoBehaviour {

    Rigidbody2D rb;
    GameObject target;
    public float speed;

    GameController gameController;

    public float rocketHealth;

    void Start()
    {
        GameObject controller = GameObject.FindWithTag("GameController");
        gameController = controller.GetComponent<GameController>();
        rb = GetComponent<Rigidbody2D>();
        target = FindTheClosest();
        rocketHealth = 5f;
        Destroy(gameObject, 4f);
    }

    void Update()
    {
        if (rocketHealth <= 0f)
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        Follow();
    }

    void Follow()
    {
        if (target != null)
        {
            Vector2 offset = target.transform.position - transform.position;
            transform.up = offset;
            rb.velocity = transform.up * speed;
        }

        if (target == null)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            rocketHealth -= 5f;
        }

        if (other.gameObject.tag == "Friendly")
        {
            Destroy(gameObject);
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
