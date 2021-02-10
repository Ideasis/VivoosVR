using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRenderTexture : MonoBehaviour
{
    [SerializeField]
    RenderTexture renderTexture;   


    // Start is called before the first frame update
    void Start()
    {
        //GL.Clear(true, true, Color.clear);

        renderTexture.DiscardContents();        

        renderTexture.width = Screen.currentResolution.width;
        renderTexture.height = Screen.currentResolution.height;

        Camera renderTargetCamera = GameObject.Find("RenderTargetCam").GetComponent<Camera>();

        if (renderTargetCamera != null)
        {
            renderTargetCamera.targetTexture = renderTexture;
            renderTargetCamera.transform.parent = Camera.main.transform; 
            //Camera.main.targetTexture = renderTexture;
        }

        

       // print(Screen.currentResolution);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
