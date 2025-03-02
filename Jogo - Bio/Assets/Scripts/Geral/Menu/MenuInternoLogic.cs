using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInternoLogic : MonoBehaviour
{
    public GameObject menu;
    public GameObject player;
    bool menuAberto;
    private Movimento mov;
    
    void Start()
    {
        mov = FindObjectOfType<Movimento>();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(menuAberto)
            {
                CloseMenu();
            }
            else
            {
                OpenMenu();
            }
        }
    }

    void OpenMenu()
    {    
        menu.SetActive(true);
        mov.enabled = false;
        menuAberto = true;
    }

    public void CloseMenu()
    {
        menu.SetActive(false);
        mov.enabled = true;
        menuAberto = false;
    }
}
