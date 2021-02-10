using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Player2DController : MonoBehaviour
{
    private GameMasterPhoton gameMaster;   

    // Start is called before the first frame update
    void Start()
    {
        gameMaster = GameMasterPhoton.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        

        if (gameMaster == null) return;

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            
            gameMaster.Recenter_Camera();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            gameMaster.ChangeColor1();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            gameMaster.ChangeColor2();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            
        }


    }

}
