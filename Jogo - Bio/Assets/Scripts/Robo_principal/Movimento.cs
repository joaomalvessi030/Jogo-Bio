using System.Collections; 
using System.Collections.Generic; 
using TMPro; 
using Unity.VisualScripting; 
using UnityEngine; 
using UnityEngine.UIElements; 
using UnityEditor; 
using UnityEngine.Events;
using System;

public class Movimento : MonoBehaviour
{
    public Rigidbody2D oRigidBody;
    public float velocidadeDoJogador;
    public SpriteRenderer oSpriteRenderer;
    public bool estaNoChao;
    public Transform verificadorDeChao;
    public LayerMask layerDoChao;
    public float alturaDoPulo;
    public float raioDeVerificacao;

    public int quantidadeDePulos = 2;
    private int pulosRestantes;

    public float kBForce;
    public float kBCount;
    public float kBTime;
    public bool isKnock;

    public Animator anim;
    public GameObject menu;

    public GameObject damageArea;

    // Sistema de dash
    bool podeDash = true;
    bool estaDash;
    private float poderDash = 80f;
    float tempoDash = 0.1f;
    private float recargaDash = 1f;
    [SerializeField] TrailRenderer tr;

    void Start()
    {
        pulosRestantes = quantidadeDePulos;
    }

    void Update()
    {
        if (estaDash)
        {
            return;
        }
        Pular();
        MoveAnim();
        JumpAnim();
        Dano();

        //Dash
        if (Input.GetKeyDown(KeyCode.LeftShift) && podeDash)
        {
            StartCoroutine(Dash());
        }
    }

    void FixedUpdate()
    {
        if (estaDash)
        {
            return;
        }
        KnockLogic();
    }

    void KnockLogic()
    {
        if (kBCount < 0)
        {
            MovimentarJogador();
        }
        else if (isKnock)
        {
            oRigidBody.velocity = new Vector2(-kBForce, kBForce);
        }
        else
        {
            oRigidBody.velocity = new Vector2(kBForce, kBForce);
        }
        kBCount -= Time.deltaTime;
    }

    public void MovimentarJogador()
    {
        float inputDoMovimento = Input.GetAxisRaw("Horizontal");

        oRigidBody.velocity = new Vector2(inputDoMovimento * velocidadeDoJogador, oRigidBody.velocity.y);

        if (inputDoMovimento > 0)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            anim.SetTrigger("TriggerMove");
        }
        if (inputDoMovimento < 0)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            anim.SetTrigger("TriggerMove");
        }
    }

    public void Pular()
    {
        estaNoChao = Physics2D.OverlapCircle(verificadorDeChao.position, raioDeVerificacao, layerDoChao);

        // Reseta o número de pulos ao tocar o chão
        if (estaNoChao)
        {
            pulosRestantes = quantidadeDePulos;
        }

        // Primeiro ou segundo pulo (inclusive durante a queda)
        if (Input.GetKeyDown(KeyCode.Space) && pulosRestantes > 0)
        {
            oRigidBody.velocity = new Vector2(oRigidBody.velocity.x, alturaDoPulo);
            pulosRestantes--;
        }

        // Cancela o pulo se soltar o botão enquanto está subindo
        if (Input.GetKeyUp(KeyCode.Space) && oRigidBody.velocity.y > 0)
        {
            oRigidBody.velocity = new Vector2(oRigidBody.velocity.x, 0);
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
        if (Input.GetButtonDown("Fire1"))
        {
            anim.SetTrigger("TriggerAtq");
        }
    }

    public void IniciaAtaque()
    {
        if (damageArea != null)
            damageArea.SetActive(true);
    }

    public void TerminaAtaque()
    {
        if (damageArea != null)
            damageArea.SetActive(false);
    }

    private IEnumerator Dash()
    {
        podeDash = false;
        estaDash = true;

        float originalGravity = oRigidBody.gravityScale;
        oRigidBody.gravityScale = 0;

        oRigidBody.velocity = new Vector2(transform.right.x * poderDash, 0f);

        yield return new WaitForSeconds(tempoDash);

        oRigidBody.gravityScale = originalGravity;
        estaDash = false;
        StartCoroutine(RecargaDash());
    }

    private IEnumerator RecargaDash()
    {
        yield return new WaitForSeconds(recargaDash);
        podeDash = true;
    }
    

}
