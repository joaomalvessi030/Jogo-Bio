using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimento : MonoBehaviour
{
    public Rigidbody2D oRigidBody; // Declaração correta, dentro de uma classe.
    public float velocidadeDoJogador;
    public SpriteRenderer oSpriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
	    oSpriteRenderer.flipX = false; //Não gira o personagem
    }
    if (inputDoMovimento < 0)
    {   
	    oSpriteRenderer.flipX = true; //Gira o personagem
    }
}
}
