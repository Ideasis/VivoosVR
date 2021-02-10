using System;
using System.Collections;
using UnityEngine;
using UnityEngine.XR;
using TMPro;
public class GameMasterPhoton : MonoBehaviour
{

    public static GameMasterPhoton Instance = null;
    public MeshRenderer[] cubes;
    public Color color1;
    public Color color2;


    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

       
    }

    void Start()
    {
        
        Recenter_Camera();
            
    }

    float timer;
    // Update is called once per frame
    void Update()
    {
        
        
    }

    public void ChangeColor1()
    {
        foreach (var cube in cubes)
        {
            cube.GetComponent<Cubes>().colorType = 1;
        }
    }

    public void ChangeColor2()
    {
        foreach (var cube in cubes)
        {
            cube.GetComponent<Cubes>().colorType = 2;
        }
    }

    public void Recenter_Camera()
    {
        
    }


   

   

   

   

}
