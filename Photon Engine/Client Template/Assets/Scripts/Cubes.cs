using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cubes : MonoBehaviour, IPunObservable
{
    public Color matColor;
    public Color matColor2;
    public int colorType;
    MeshRenderer meshRenderer;


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(colorType);
        }
        else
        {
            colorType = (int)stream.ReceiveNext();
            if (colorType == 1)
            {
                meshRenderer.material.color = matColor;
            }
            else if (colorType == 2)
            {
                meshRenderer.material.color = matColor2;
            }


        }
    }

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();

    }

    // Update is called once per frame
    void Update()
    {

    }
}
