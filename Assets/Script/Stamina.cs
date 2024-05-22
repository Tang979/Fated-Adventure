using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
    public Slider stamina;
    // Start is called before the first frame update
    public void SetMaxStamina(float staminaPlayer)
    {
        stamina.maxValue = staminaPlayer;
        stamina.value = staminaPlayer;
    }

    public void SetStamina(float staminaPlayer)
    {
        stamina.value = staminaPlayer;
    }
}
