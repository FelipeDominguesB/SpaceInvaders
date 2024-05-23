

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public abstract class PawnScript : MonoBehaviour
{
    public Rigidbody2D RigidBody;
    public float Speed = 1;
    public int Health = 100;
    public int DamageTaken = 20;
    public abstract void Shoot();
    public virtual void TakeDamage()
    {
        Health -= DamageTaken;
        if (Health <= 0) Destroy(this.gameObject);
    }


    
}

