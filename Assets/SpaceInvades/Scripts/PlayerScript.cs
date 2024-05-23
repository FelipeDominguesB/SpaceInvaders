using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerScript : PawnScript
{

    public CannonScript cannonScript;
    public TextMeshPro textMesh;
    public float shootingCadency = 0.5F;

    private float nextUpdate = 0.1F;
    private bool canShoot = false;

    private LevelScript levelScript;

    // Start is called before the first frame update
    void Start()
    {
        cannonScript = GameObject.FindObjectOfType<CannonScript>();
        levelScript = GameObject.FindObjectOfType<LevelScript>();
    }

    void Update()
    {

        if (!Input.anyKey) RigidBody.velocity = Vector2.zero;
        
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            RigidBody.velocity = Vector2.left * Speed;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            RigidBody.velocity = Vector2.right * Speed;
        }
        if (Input.GetKeyDown(KeyCode.Space)) Shoot();
        if (Input.GetKeyDown(KeyCode.Escape)) levelScript.CloseGame();
        if (Input.GetKeyDown(KeyCode.Backspace)) levelScript.ResetLevel();


        if (Time.time >= nextUpdate && !canShoot)
        {
            canShoot = true;

        }

        
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 7)
        {
            TakeDamage();
            Destroy(collision.gameObject);
            textMesh.text = Health.ToString();
        }
    }

    public override void TakeDamage()
    {
        Health -= DamageTaken;
        if (Health <= 0)
        {
            levelScript.GameOver();
            Destroy(this.gameObject);
        }
    }


    public override void Shoot()
    {
        if(canShoot)
        {
            cannonScript.Shoot();
            canShoot = false;
            nextUpdate = Time.time + shootingCadency;

        }
    }
}
