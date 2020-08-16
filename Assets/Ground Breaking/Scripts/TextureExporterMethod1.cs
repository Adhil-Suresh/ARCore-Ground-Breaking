using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureExporterMethod1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public RenderTexture renderTexture;
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination);
        Graphics.Blit(source, renderTexture);
    }

    
}
