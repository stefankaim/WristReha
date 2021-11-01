using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCollision : MonoBehaviour
{
    public int collisions;
    public int collisionsNeeded = 250;

    private float normalStrength, roughnessStrength, baseStrength, metallicStrength;
    public float normalStrengthDead = 0.5f, roughnessStrengthDead = 2.2f, baseStrengthDead = 0.0f, metallicStrengthDead = 1.2f;

    private Renderer renderer;

    public WateredPlantsCounter counter;
    private bool watered = false;

    private void Awake()
    {
        renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            if (gameObject.name.Contains("AlienFlower") || gameObject.name.Contains("AlienTree") || gameObject.name.Contains("AlienMushroom"))
            {
                normalStrength = renderer.material.GetFloat("_NormalStrength");
                roughnessStrength = renderer.material.GetFloat("_RoughnessStrength");
                baseStrength = renderer.material.GetFloat("_BaseStrength");
                metallicStrength = renderer.material.GetFloat("_MetallicStrength");
                renderer.material.SetFloat("_NormalStrength", normalStrengthDead);
                renderer.material.SetFloat("_RoughnessStrength", roughnessStrengthDead);
                renderer.material.SetFloat("_BaseStrength", baseStrengthDead);
                renderer.material.SetFloat("_MetallicStrength", metallicStrengthDead);
            }
        }
    }

    private void Update()
    {
        if (renderer != null && !watered)
        {
            if (collisions >= collisionsNeeded)
            {
                renderer.material.SetFloat("_NormalStrength", normalStrength);
                renderer.material.SetFloat("_RoughnessStrength", roughnessStrength);
                renderer.material.SetFloat("_BaseStrength", baseStrength);
                renderer.material.SetFloat("_MetallicStrength", metallicStrength);
                watered = true;
                counter.IncreaseWateredPlants();
            }
        }
    }

    /// <summary>
    /// What to do with colliding particles
    /// </summary>
    /// <param name="other">The particles that collided</param>
    private void OnParticleCollision(GameObject other)
    {
        collisions++;
    }
}
