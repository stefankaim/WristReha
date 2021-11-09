using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetColor : MonoBehaviour
{
    private Renderer thisObject;
    public Color color;

    /// <summary>
    /// Change the color of the object on startup
    /// </summary>
    private void Awake()
    {
        thisObject = GetComponent<Renderer>();
        thisObject.material.color = color;
    }
}
