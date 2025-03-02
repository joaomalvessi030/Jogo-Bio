using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class TriggerDamage : MonoBehaviour
{
    public Heart heart;
    public Movimento player;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            player.kBCount = player.kBTime;
            if(collision.transform.position.x <= transform.position.x)
            {
                player.isKnock = true;
            }
            if(collision.transform.position.x > transform.position.x)
            {
                player.isKnock = false;
            }
            heart.vida--;
        }
    }
}
