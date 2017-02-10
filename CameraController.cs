using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public GameObject target;

    Vector3 offset;


	void Start()
    {
        offset = transform.position - target.transform.position;
    }

    void Update()
    {
        if (target != null)
        {
            transform.position = target.transform.position + offset;
        }
    }
}
