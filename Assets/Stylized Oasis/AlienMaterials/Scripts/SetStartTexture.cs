using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetStartTexture : MonoBehaviour
{
    private Renderer thisRenderer;

    public Texture startTexture;
    public Renderer MushroomTop;
    public Renderer MushroomBase;

    public float normalStrength, roughnessStrength, baseStrength, metallicStrength;

    private void Awake()
    {
        thisRenderer = GetComponent<Renderer>();
        if (thisRenderer != null)
        {
            if (thisRenderer.gameObject.name.Contains("Mushroom"))
            {
                MushroomTop.material.SetTexture("_MainTex", startTexture);
                MushroomBase.material.SetTexture("_MainTex", startTexture);
            }
            else
            {
                thisRenderer.material.SetTexture("_MainTex", startTexture);
            }
        }
    }
}
