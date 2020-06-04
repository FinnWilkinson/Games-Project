using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    public ParticleEmitter emitter;
    public GameObject particle;


    private Vector3 emitterPosition, emitterScale, minEmissionDistance, maxEmissionDistance;

    private float emissionDegreeRadius;
    private float lifeSpan;
    private int maxParticleLife;
    private Vector3 particleStartSize, minParticleSize, maxParticleSize, singleEmissionVector;
    private Rigidbody rigidBody;
    private float scaleUpCoefficient, scaleDownCoefficient;
    private float emissionRangeFromCentre;
    private float randomX, randomY, randomZ;
    private float randomXRotation, randomYRotation, randomZRotation;
    private float initalVerticalDrag;
    private float xRange, yRange, zRange;
    private readonly float scalingRate = 0.05f;
    private bool increaseSizeOnSpawn;

    public void InstantiateParticle()
    {
        scaleUpCoefficient = emitter.sizeIncreaseCoeficient;
        scaleDownCoefficient = emitter.sizeDecreaseCoefficient;

        emissionDegreeRadius = emitter.emissionDegreeRadius;
        maxParticleLife = emitter.maxParticleLife;

        particleStartSize = emitter.startSize;
        minParticleSize = emitter.minSize;
        maxParticleSize = emitter.maxSize;

        emitterScale = emitter.transform.localScale;
        minEmissionDistance = new Vector3(0, emitter.transform.localPosition.y - (emitterScale.y), 0);
        maxEmissionDistance = new Vector3(0, emitter.transform.localPosition.y + (emitterScale.y), 0);
        singleEmissionVector = new Vector3(0, emitterScale.y + minEmissionDistance.y * 0.05f, 0);

        emissionRangeFromCentre = emitter.emissionRangeFromCentre;

        xRange = (emitterScale.x / 2) + emissionRangeFromCentre;
        zRange = (emitterScale.z / 2) + emissionRangeFromCentre;

        initalVerticalDrag = emitter.verticalDrag;

        emissionRangeFromCentre = emitter.emissionRangeFromCentre;

        increaseSizeOnSpawn = emitter.increaseSizeOnSpawn;

        rigidBody = this.GetComponent<Rigidbody>();
        rigidBody.freezeRotation = true;
        rigidBody.drag = initalVerticalDrag;
        rigidBody.angularDrag = emitter.angularDrag;
        rigidBody.mass = emitter.mass;

        particle = this.gameObject;

    }

    private void SetPosition()
    {
        emitterPosition = emitter.transform.position;

        // Attributes used for positioning particles around the emmiter object
        maxEmissionDistance.x = emitterPosition.x + xRange;
        minEmissionDistance.x = emitterPosition.x - xRange;
        maxEmissionDistance.y = emitterPosition.y + emitterScale.y;
        minEmissionDistance.y = emitterPosition.y - emitterScale.y;
        maxEmissionDistance.z = emitterPosition.z + zRange;
        minEmissionDistance.z = emitterPosition.z - zRange;

        randomXRotation = UnityEngine.Random.Range(-emissionDegreeRadius / 2, emissionDegreeRadius / 2);
        randomYRotation = UnityEngine.Random.Range(-emissionDegreeRadius / 2, emissionDegreeRadius / 2);
        randomZRotation = UnityEngine.Random.Range(-emissionDegreeRadius / 2, emissionDegreeRadius / 2);

        transform.eulerAngles = new Vector3(randomXRotation, randomYRotation, randomZRotation);

        if (emitter.singleEmissionPoint) transform.localPosition = emitterPosition + singleEmissionVector;
        else
        {
            randomX = UnityEngine.Random.Range(minEmissionDistance.x, maxEmissionDistance.x);
            randomY = UnityEngine.Random.Range(minEmissionDistance.y, maxEmissionDistance.y);
            randomZ = UnityEngine.Random.Range(minEmissionDistance.z, maxEmissionDistance.z);

            transform.position = new Vector3(randomX, randomY, randomZ);
        }
    }

    public void ResetParticle()
    {
        transform.localScale = maxParticleSize;
        lifeSpan = maxParticleLife + UnityEngine.Random.Range(-1, 1);

        SetPosition();
        particle.SetActive(true);
        emitter.IncreaseParticleCount();

        StartCoroutine(ReduceLife());

        if (increaseSizeOnSpawn) StartCoroutine(ScaleUp());
        else StartCoroutine(ScaleDown());
    }

    private IEnumerator ReduceLife()
    {

        while (lifeSpan > 0)
        {
            lifeSpan--;
            yield return new WaitForSeconds(1.0f);
        }


        emitter.ReduceParticleCount();
        particle.SetActive(false);

    }

    private IEnumerator ScaleUp()
    {
        transform.localScale = particleStartSize;
        Vector3 scalingVector = Vector3.one * scaleUpCoefficient;

        while (particle.activeSelf && transform.localScale.x < maxParticleSize.x)
        {
            transform.localScale += scalingVector * Time.deltaTime;
            yield return new WaitForSeconds(scalingRate);
        }

        StartCoroutine(ScaleDown());
    }

    private IEnumerator ScaleDown()
    {
        Vector3 scalingVector = Vector3.one * scaleDownCoefficient;
        Vector3 newScale = scalingVector;

        while (particle.activeSelf && transform.localScale.x > minParticleSize.z)
        {
            newScale = transform.localScale + (scalingVector * Time.deltaTime);
            newScale = new Vector3(Mathf.Max(0, newScale.x), Mathf.Max(0, newScale.y), Mathf.Max(0, newScale.z));
            transform.localScale = newScale;
            yield return new WaitForSeconds(scalingRate);
        }
    }

    void OnCollisionEnter(Collision c)
    {
        //do bounce stuff here
    }
}
