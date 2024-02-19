using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisuals : MonoBehaviour
{
    public ParticleSystem Lighting;
    public Color LightingColorFullStamina = Color.white;

    public Color LightingColorLowStamina = Color.red;
    public int MaxParticles = 200;
    public GameObject Smoke;
    public Color SmokeColorFullStamina = Color.white;
    public Color SmokeColorLowStamina = Color.black;

    public ParticleSystem Sparks;
    public int Stamina = 100;
    private Material smokeMaterial;

    public void UpdateStaminaVisuals(int stamina)
    {
        stamina = Mathf.Clamp(stamina, 0, 100);

        var lightingEmission = Lighting.emission;
        var sparksEmission = Sparks.emission;

        float particleRatio = (float)stamina / 100f;
        lightingEmission.rateOverTime = MaxParticles * particleRatio;
        sparksEmission.rateOverTime = MaxParticles * particleRatio;

        var lightingMain = Lighting.main;

        if (smokeMaterial != null)
        {
            smokeMaterial.color = Color.Lerp(SmokeColorLowStamina, SmokeColorFullStamina, particleRatio);
        }
        lightingMain.startColor = Color.Lerp(LightingColorLowStamina, LightingColorFullStamina, particleRatio);

    }

    private void Start()
    {
        if (Smoke != null)
        {
            Renderer smokeRenderer = Smoke.GetComponent<Renderer>();
            if (smokeRenderer != null)
            {
                smokeMaterial = smokeRenderer.material;
            }
        }

        UpdateStaminaVisuals(100); // Initialize with full stamina visuals
    }

    private void Update()
    {
        UpdateStaminaVisuals(Stamina);
    }
}