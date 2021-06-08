using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTextureSetup : MonoBehaviour
{
    public Camera portalCam;

    public Material portalCamMat;


    void Start()
    {
        if (portalCam.targetTexture != null)
        {
            portalCam.targetTexture.Release();
        }
        portalCam.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        portalCamMat.mainTexture = portalCam.targetTexture;
    }

}
