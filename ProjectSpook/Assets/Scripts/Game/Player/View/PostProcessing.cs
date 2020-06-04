using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostProcessing : MonoBehaviour
{
    public Material material;

    private float value1 = 0.1f;
    private float value2 = 1.0f;
    private float startValue = 0.3f;
    private float endValue = 1.0f;

    private void Awake()
    {
        StartCoroutine(Vignette());
    }

    private IEnumerator Vignette()
    {
        
        float difference = 0.0f;
        while (true) {
            yield return new WaitForSeconds(2.5f);
            startValue = (startValue == value1) ? value2 : value1;
            endValue = (startValue == value1) ? value2 : value1;
            difference = startValue - endValue;
            if (difference < 0) {
                for (float i = startValue; i <= endValue; i += 0.01f)
                {
                    Debug.Log(i);
                    material.SetFloat("_VRadius", i);
                    yield return new WaitForSeconds(0.01f);
                }
            }
            if (difference > 0) {
                for (float i = startValue; i >= endValue; i -= 0.01f)
                {
                    Debug.Log(i);
                    material.SetFloat("_VRadius", i);
                    yield return new WaitForSeconds(0.01f);
                }

            }
            
        }
    }

    // private void OnRenderImage(RenderTexture source, RenderTexture destination)
   // {
    //    Graphics.Blit(source, destination, material);
    //}

}
