using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RZTetroScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Color tetroColor = new Color(
            Random.Range(0f, 1f), 
            Random.Range(0f, 1f), 
            Random.Range(0f, 1f)
        );
        changeTetroColor(tetroColor);
    }

    void changeTetroColor(Color newColor)
    {
        Renderer[] childRenderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer rend in childRenderers){
            rend.material.color = newColor;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
