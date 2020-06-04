using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTeleport : TriggerEvent
{
    public GameObject telepad;
    public GameObject player;
    public List<Light> pointLights;
    public float intensity;

    public void Start()
    {
        foreach(Light light in pointLights)
        {
            light.intensity = intensity;
        }
    }

    public override void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hello");
        if(other.gameObject.CompareTag("Player"))
        {
            player.transform.SetPositionAndRotation(telepad.transform.position, telepad.transform.rotation);
            StartCoroutine("DimLights", 0.0f);
        }
    }

    private IEnumerator DimLights()
    {
        while (pointLights[0].intensity > 1.0f)
        {
            yield return new WaitForSeconds(0.01f);
            Debug.Log(pointLights[0].intensity);
            foreach (Light light in pointLights)
            {
                light.intensity -= 0.01f;
            }
        }
        Debug.Log(pointLights[0].intensity);
    }
}
