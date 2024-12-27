using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Dialogue : MonoBehaviour
{
    public Sprite profile;
    public string[] speechText;
    public string actorName;
    public float radious;
    public LayerMask playerLayer;
    private DialogueControl dc;
    bool onRadious;
    private Movimento mov;

    private void Start()
    {
        dc = FindObjectOfType<DialogueControl>();
        mov = FindFirstObjectByType<Movimento>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && mov.estaNoChao)
        {
            if (onRadious && !dc.dialogOpened)
            {
                dc.Speech(profile, speechText, actorName);
            }
            else if (dc.dialogOpened)
            {
                dc.NextSentence();
            }
        }
    }
    private void FixedUpdate()
    {
        Interact();
    }

    public void Interact()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, radious, playerLayer);

        if(hit != null)
        {
            onRadious = true;
        }
        else
        {
            onRadious = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radious);
    }
}

