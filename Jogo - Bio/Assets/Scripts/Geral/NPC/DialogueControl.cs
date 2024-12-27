using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.UI;

public class DialogueControl : MonoBehaviour
{
    [Header("Componentes")]
    public GameObject dialogueObj;
    public Image profile;
    public Text speechText;
    public Text actorNameText;

    [Header("Configurações")]
    public float typingSpeed;
    private string[] sentences;
    private int index;
    public bool dialogOpened;
    private Movimento mov;
    
    private void Start()
    {
        mov = FindObjectOfType<Movimento>();
        dialogOpened = false;
    }

    public void Speech(Sprite p, string[] txt, string actorName)
    {
        dialogueObj.SetActive(true);
        mov.velocidadeDoJogador = 0;
        mov.alturaDoPulo = 0;
        dialogOpened = true;
        profile.sprite = p;
        sentences = txt;
        actorNameText.text = actorName;
        StartCoroutine(TypeSentence());
    }

    IEnumerator TypeSentence()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            speechText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void NextSentence()
    {
        if(speechText.text == sentences[index])
        {
            //ainda tem textos
            if(index < sentences.Length-1)
            {
                index++;
                speechText.text = "";
                StartCoroutine(TypeSentence());
            }
            else
            {
                speechText.text = "";
                index = 0;
                dialogueObj.SetActive(false);
                dialogOpened = false;
                mov.velocidadeDoJogador = 8;
                mov.alturaDoPulo = 20;
            }

        }
    }
}
