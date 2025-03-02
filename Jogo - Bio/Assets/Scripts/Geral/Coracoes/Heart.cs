using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heart : MonoBehaviour
{
    Movimento player;
    public int vida;
    public int vidaMax;
    public Image[] coracao;
    public Sprite cheio;
    public Sprite vazio;
   
    void Start()
    {
        player = GetComponent<Movimento>();
    }
   void Update()
   {
        HealthLogic();
        DeadState();
   }
    void HealthLogic()
    {
        if(vida > vidaMax)
        {
            vida = vidaMax;
        }

        for (int i = 0; i < coracao.Length; i++)
        {
            if(i < vida)
            {
                coracao[i].sprite = cheio;
            }
            else
            {
                coracao[i].sprite = vazio;
            }

            if(i < vidaMax)
            {
                coracao[i].enabled = true;
            }
            else
            {
                coracao[i].enabled = false;
            }
        }
    }
    void DeadState()
    {
        if(vida <= 0)
        {
            GetComponent<Movimento>().enabled = false;
            Destroy(gameObject, 1.0f);
        }
    }
}
