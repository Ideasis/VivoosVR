using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeNPC : MonoBehaviour
{

    Renderer[] rends;    

    public Gradient gradient;

    // Start is called before the first frame update
    void Start()
    {
        rends = GetComponentsInChildren<Renderer>();

        float t = Random.Range(0.0f, 1.0f);

        print(t);
        rends[6].material.SetColor("Color", gradient.Evaluate(t));
        rends[6].material.SetColor("_EmissionColor", gradient.Evaluate(t));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
