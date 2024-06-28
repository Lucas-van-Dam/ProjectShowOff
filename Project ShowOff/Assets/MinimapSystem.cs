using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinimapSystem : MonoBehaviour
{
    public Texture2D map;

    [SerializeField] private Camera screenShotCamera;

    [SerializeField] private Camera playerCamera;

    [SerializeField] private RawImage mapImage;
    // Start is called before the first frame update
    void Start()
    {
        playerCamera.clearFlags = CameraClearFlags.Depth;
    }

    private Texture2D MakeMapScreenShot()
    {
        int res = 1650;
        RenderTexture rt = new RenderTexture(res, res, 24);
        screenShotCamera.targetTexture = rt;
        Texture2D texture = new Texture2D(res, res, TextureFormat.RGB24, false);
        screenShotCamera.Render();
        RenderTexture.active = rt;
        texture.ReadPixels(new Rect(0, 0, res, res), 0, 0);
        screenShotCamera.targetTexture = null;
        RenderTexture.active = null;
        Destroy(rt);
        return texture;
    }

    private void LateUpdate()
    {
        if (map == null)
        {
            map = MakeMapScreenShot();
            screenShotCamera.gameObject.SetActive(false);
            map.Apply();
            mapImage.texture = map;
        }
        
    }
}
