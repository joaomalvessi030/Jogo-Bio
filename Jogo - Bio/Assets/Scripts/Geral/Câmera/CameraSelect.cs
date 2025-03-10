using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSelect : MonoBehaviour
{
    public GameObject virtualCam; // A câmera virtual a ser ativada

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            // Verifica se virtualCam não é null antes de ativar
            if (virtualCam != null)
            {
                virtualCam.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            // Verifica se virtualCam não é null antes de desativar
            if (virtualCam != null)
            {
                virtualCam.SetActive(false);
            }
        }
    }
}
