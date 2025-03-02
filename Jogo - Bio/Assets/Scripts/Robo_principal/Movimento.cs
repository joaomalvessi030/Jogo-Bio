using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Movimento : MonoBehaviour
{
    public Rigidbody2D oRigidBody; // Declaração correta, dentro de uma classe.
    public float velocidadeDoJogador;
    public SpriteRenderer oSpriteRenderer;
    public bool estaNoChao;
    public Transform verificadorDeChao;
    public LayerMask layerDoChao;
    public float alturaDoPulo;
    public float raioDeVerificacao;

    
    public float kBForce;
    public float kBCount;
    public float kBTime;
    public bool isKnock;

    public Animator anim;
    public GameObject menu;

    public GameObject damageArea;
    void Update()
    {
        Pular();
        MoveAnim();
        JumpAnim();
        Dano();
    }

    void FixedUpdate()
    {
	    KnockLogic();
    }

    void KnockLogic()
    {
        if(kBCount < 0)
        {
            MovimentarJogador();
        }
        else if(isKnock == true)
        {
            oRigidBody.velocity = new Vector2(-kBForce, kBForce);
        }
        else if(isKnock == false)
        {
            oRigidBody.velocity = new Vector2(kBForce, kBForce);
        }
        kBCount -= Time.deltaTime;
    }

public void MovimentarJogador()
{
	float inputDoMovimento = Input.GetAxisRaw("Horizontal"); //Pega as coordenadas horizontais
	
	oRigidBody.velocity = new UnityEngine.Vector2(inputDoMovimento * velocidadeDoJogador, oRigidBody.velocity.y); //define e velocidade do jogador

    if (inputDoMovimento > 0)
    {
	    Vector3 rotator = new Vector3(transform.rotation.x, 0f, transform.rotation.z); 
        transform.rotation = Quaternion. Euler(rotator);
        anim.SetTrigger("TriggerMove"); 
    }
    if (inputDoMovimento < 0)
    {   
	    Vector3 rotator = new Vector3(transform.rotation.x, 180f, transform.rotation.z); 
        transform.rotation = Quaternion. Euler(rotator);
        anim.SetTrigger("TriggerMove");
    }
}

public void Pular()
{
	estaNoChao = Physics2D.OverlapCircle(verificadorDeChao.position, raioDeVerificacao, layerDoChao);

	if (Input.GetKeyDown(KeyCode.Space) && estaNoChao == true)
	{
		oRigidBody.velocity = Vector2.up * alturaDoPulo;
	}
    if (Input.GetKeyUp(KeyCode.Space) && oRigidBody.velocity.y > 0)
    {
	    oRigidBody.velocity = new UnityEngine.Vector2(oRigidBody.velocity.x, 0);
    }
}

void MoveAnim()
{
    anim.SetFloat("Horizontal", oRigidBody.velocity.x);
}

void JumpAnim()
{
    anim.SetFloat("Vertical", oRigidBody.velocity.y);
    anim.SetBool("groundCheck", estaNoChao);
    anim.SetTrigger("TriggerMove");
}

void Dano()
{
    if(Input.GetButtonDown("Fire1"))
    {
        anim.SetTrigger("TriggerAtq");
    }
}

public void IniciaAtaque()
{
    damageArea.SetActive(true);
}
public void TerminaAtaque()
{
    damageArea.SetActive(false);
}

}
