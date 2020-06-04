using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEmitter : MonoBehaviour
{

    [Header("Emission Properties")]
    public float emissionDegreeRadius;
    public int maxParticleCount;
    public int maxParticleLife;
    public bool singleEmissionPoint;
    public float emmitionRate;
    public int particlesPerEmission;
    public int currentLiveParticles;

    [Header("Particle Properties")]
    public GameObject prefab;
    public float verticalDrag;
    public float angularDrag;
    public float mass;
    public float emissionRangeFromCentre;
    public float sizeIncreaseCoeficient, sizeDecreaseCoefficient;
    public bool increaseSizeOnSpawn;
    public Vector3 startSize, minSize, maxSize;

    private List<Particle> pooledParticles;
    private int poolIndex;

    // Start is called before the first frame update
    void Start()
    {
        Quaternion currentRotation = transform.rotation;
        Vector3 currentPosition = transform.position;

        currentLiveParticles = 0;
        poolIndex = 0;
        pooledParticles = new List<Particle>();

        for (int i = 0; i < maxParticleCount; i++)
        {
            GameObject particle = (GameObject)Instantiate(prefab, currentPosition, currentRotation);
            Particle particleScript = particle.GetComponent<Particle>();

            particleScript.emitter = this;
            particleScript.InstantiateParticle();

            pooledParticles.Add(particleScript);
        }        

        StartCoroutine(Emmit());
    }

    private IEnumerator Emmit()
    {
        while (true)
        {
           
            for (int emittionCount = 0; emittionCount < particlesPerEmission; emittionCount++)
            {
                if (!pooledParticles[poolIndex].particle.activeSelf && currentLiveParticles < maxParticleCount)
                {
                    pooledParticles[poolIndex].ResetParticle();
                }

                poolIndex = (poolIndex + 1) % maxParticleCount;
            }

            yield return new WaitForSeconds(emmitionRate);

        }
              
    }

    public void IncreaseParticleCount()
    {
        currentLiveParticles++;
    }

    public void ReduceParticleCount()
    {
        currentLiveParticles--;
    }
}
