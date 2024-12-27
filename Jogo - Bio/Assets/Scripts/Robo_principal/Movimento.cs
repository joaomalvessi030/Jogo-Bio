using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Update is called once per frame
    void Update()
    {
        Pular();
    }

    void FixedUpdate()
    {
	    MovimentarJogador(); //É um Update porém pra passos
    }

public void MovimentarJogador()
{
	float inputDoMovimento = Input.GetAxisRaw("Horizontal"); //Pega as coordenadas horizontais
	
	oRigidBody.velocity = new UnityEngine.Vector2(inputDoMovimento * velocidadeDoJogador, oRigidBody.velocity.y); //define e velocidade do jogador

    if (inputDoMovimento > 0)
    {
	    Vector3 rotator = new Vector3(transform.rotation.x, 0f, transform.rotation.z); 
        transform.rotation = Quaternion. Euler(rotator); 
    }
    if (inputDoMovimento < 0)
    {   
	    Vector3 rotator = new Vector3(transform.rotation.x, 180f, transform.rotation.z); 
        transform.rotation = Quaternion. Euler(rotator);
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

}
