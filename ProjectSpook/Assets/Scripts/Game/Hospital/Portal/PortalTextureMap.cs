using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTextureMap : MonoBehaviour
{
    public Camera cam;

    public Material cameraMaterial;

    // Start is called before the first frame update
    void Start()
    {
        if (cam.targetTexture != null)
        {
            cam.targetTexture.Release();
        }
        cam.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        cameraMaterial.mainTexture = cam.targetTexture;
    }
}
