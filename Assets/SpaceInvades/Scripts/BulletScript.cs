using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D bulletBody;
    public float bulletSpeed = 5;
    void Start()
    {
        bulletBody.velocity = Vector2.up * bulletSpeed;   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

    }
}
