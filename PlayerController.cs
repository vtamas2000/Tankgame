using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    Rigidbody2D rb;

    GameController gameController;

    public GameObject ammo;
    public GameObject helper;
    public Transform cannonThroat;

    float h;
    float v;
    float nextFire = 0f;

    public float speed;
    public float fireRate;
    public float playerHealth;
    float nextHealthRegen = 0f;
    public float healthRegenRate;

    void Start()
    {
        GameObject controller = GameObject.FindWithTag("GameController");
        gameController = controller.GetComponent<GameController>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        playerHealth = 100f;
    }

    void FixedUpdate()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(h, v);

        rb.AddForce(movement * speed);
    }

    void Update()
    {

        Vector2 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mouseScreenPosition - (Vector2)transform.position).normalized;
        transform.up = direction;

        Shoot();
        PlayerHealthRegen();
        HelperSpawn();
        PlayerFastHealthRegen();

        if (playerHealth <= 0f)
        {
            Destroy(gameObject);
        }

    }

    void Shoot()
    {
        if (Input.GetKey(KeyCode.Mouse0) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(ammo, cannonThroat.position, cannonThroat.rotation);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "EnemyBullet")
        {
            playerHealth = playerHealth - 5f;
        }
        if (other.gameObject.tag == "Enemy")
        {
            playerHealth = playerHealth - 5f;
        }
    }

    void PlayerHealthRegen()
    {
        if (Time.time > nextHealthRegen && playerHealth < 100f)
        {
            nextHealthRegen = Time.time + healthRegenRate;
            playerHealth = playerHealth + 2f;
        }
    }

    void HelperSpawn()
    {
        if (Input.GetKeyDown(KeyCode.E) && gameController.money >= 70f)
        {
            gameController.money = gameController.money - 70f;
            float x = transform.position.x;
            float y = transform.position.y;
            float randomX = Random.Range(x - 10f, x + 10f);
            float randomY = Random.Range(y - 10f, y + 10f);
            Instantiate(helper, new Vector2(randomX, randomY) , Quaternion.identity);
        }
    }

    void PlayerFastHealthRegen()
    {
        if (Input.GetKeyDown(KeyCode.Space) && gameController.money >= 0f)
        {
            playerHealth += gameController.money;
            gameController.money = 0f;

            if (playerHealth > 100f)
            {
                playerHealth = 100f;
            }
        }
    }
}
