using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class LifeBar : MonoBehaviour
{
    public Slider slider;
    public void MaxLife(float vida)
    {
        slider.maxValue = vida;
        slider.value = vida;
    }
    public void AlterarVida(float vida) 
    {
        slider.value = vida;
    }
}
