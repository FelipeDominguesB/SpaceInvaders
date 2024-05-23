using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyScript : PawnScript
{

    public GameObject parentObject;
    public GameObject enemyProjectile;
    public TextMeshPro textMesh;
    public bool forward = true;


    // Start is called before the first frame update
    void Start()
    {
        textMesh.text = Health.ToString();
    }
    
    // Update is called once per frame

    void Update()
    {
        Move();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.layer)
        {
            case 3:
                Shoot();
                break;
        }

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        switch (collision.gameObject.layer)
        {
            case 8:
                TakeDamage();
                textMesh.text = Health.ToString();
                Destroy(collision.gameObject);
                break;
            case 6:
            case 9:
                this.forward = !this.forward;
                break;
        }
    }
    void Move()
    {
        this.GetComponent<Rigidbody2D>().velocity = (forward ? Vector2.right : Vector2.left) * Speed;
        this.GetComponent<Rigidbody2D>().rotation = 0;
        transform.rotation = Quaternion.identity;
        transform.position = new Vector3(transform.position.x, 2.5F, transform.position.z);

    }

    public override void Shoot()
    {
        var gameobj = Instantiate(enemyProjectile, new Vector3(transform.position.x, (transform.position.y - 0.4F)), Quaternion.Euler(0, 0, 0));
        var body = gameobj.GetComponents<Rigidbody2D>()[0];
        body.velocity = Vector2.down * Speed;

    }

}
