using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

    Rigidbody2D rb;

    public float strength;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Shoot();
    }

    void Shoot()
    {
        rb.AddForce(transform.up * strength, ForceMode2D.Impulse);
        Destroy(gameObject, 1f);
    }

}
