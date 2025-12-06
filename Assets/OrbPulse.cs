using UnityEngine;

public class OrbPulse : MonoBehaviour
{
    public Color baseColor = Color.cyan;
    public float minIntensity = 1f;
    public float maxIntensity = 4f;
    public float pulseSpeed = 2f;
    public float rotationSpeed = 30f;
    public ParticleSystem burstParticles;   // assign in Inspector

    Material orbMaterial;

    void Start()
    {
        orbMaterial = GetComponent<Renderer>().material;
        orbMaterial.EnableKeyword("_EMISSION");
    }

    void Update()
    {
        // Pulse emission intensity
        float t = (Mathf.Sin(Time.time * pulseSpeed) + 1f) / 2f;
        float intensity = Mathf.Lerp(minIntensity, maxIntensity, t);
        Color emissionColor = baseColor * intensity;
        orbMaterial.SetColor("_EmissionColor", emissionColor);

        // Slow rotation
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

        // On Space key: change color + play burst
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeColor();
            PlayBurst();
        }
    }

    void ChangeColor()
    {
        // Pick a random bright color
        baseColor = Random.ColorHSV(0f, 1f, 0.7f, 1f, 0.8f, 1f);
    }

    void PlayBurst()
    {
        if (burstParticles != null)
        {
            burstParticles.Stop();
            burstParticles.Play();
        }
    }
}

