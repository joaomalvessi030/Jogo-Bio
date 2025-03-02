using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalk : MonoBehaviour
{
    private Animator anim;
    public Rigidbody rb;
    
    void Start()
    {
        
    }


    void Update()
    {
        
    }
    void MoveAnim()
    {
        anim.SetFloat("Horizontal", rb.velocity.x);
    }
}
