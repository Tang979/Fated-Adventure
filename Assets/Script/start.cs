using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public TextDisplay textDisplay;
    // Start is called before the first frame update
    public void OnButtonClick()
    {
        textDisplay.StartDisplayTextAndLoadScene();
    }
}
