using System.Collections; 
using System.Collections.Generic; 
using TMPro; 
using Unity.VisualScripting; 
using UnityEngine; 
using UnityEngine.UIElements; 
using UnityEditor; 
using UnityEngine.Events;

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
    bool eDash;
    float poderDash = 50f;
    float tempoDash = 0.3f;
    float esperaDash = 0.5f;
    [SerializeField] TrailRenderer tr;

    void Start()
    {
        pulosRestantes = quantidadeDePulos;
    }

    void Update()
    {
        Pular();
        MoveAnim();
        JumpAnim();
        Dano();

        if ((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetMouseButtonDown(1)) && podeDash)
        {
            StartCoroutine(Dash());
        }

        if (eDash)
        {
            return;
        }
    }

    void FixedUpdate()
    {
        KnockLogic();
        if (eDash)
        {
            return;
        }
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

    IEnumerator Dash()
    {
        podeDash = false;
        eDash = true;

        float gravidadeOriginal = oRigidBody.gravityScale;
        oRigidBody.gravityScale = 0f;

        float dashDistancia = 5f;
        Vector2 direcaoDash = transform.right;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direcaoDash, dashDistancia, LayerMask.GetMask("Obstaculos"));
        if (hit.collider != null)
        {
            transform.position = new Vector2(hit.point.x, transform.position.y);
        }
        else
        {
            Vector2 novaPosicao = (Vector2)transform.position + direcaoDash * dashDistancia;
            transform.position = novaPosicao;
        }

        if (tr != null)
        {
            tr.emitting = true;
        }

        yield return new WaitForSeconds(tempoDash);

        if (tr != null)
        {
            tr.emitting = false;
        }

        oRigidBody.gravityScale = gravidadeOriginal;
        eDash = false;

        yield return new WaitForSeconds(esperaDash);
        podeDash = true;
    }
}
