using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Player2DController : MonoBehaviour
{
    private GameMaster gameMaster;   

    // Start is called before the first frame update
    void Start()
    {
        gameMaster = GameMaster.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        // Burada network için bir check gerekebilir. Local veya server mı? Gibi....

        if (gameMaster == null) return;

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Debug.Log("Pressed 5");
            gameMaster.Recenter_Camera();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            gameMaster.FireRandowWalkEvent();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            gameMaster.FireRandomBoredEvent();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            gameMaster.ToggleAmbientNoise();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) && !gameMaster.bIsApplauseEvent)
        {
            gameMaster.StartApplause();
        }


    }

}
