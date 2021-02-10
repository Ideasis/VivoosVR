using System;
using System.Collections;
using UnityEngine;
using UnityEngine.XR;
using TMPro;
public class GameMaster : MonoBehaviour
{

    public static GameMaster Instance = null;

    public static event Action<int, Vector3> rndWalkEvent;
    public static event Action<int, int> boredEvent;
    public static event Action<bool> applauseEvent;    

    [HideInInspector]
    public bool bIsApplauseEvent = false;

    [SerializeField]
    private GridGenerator grid;

    [SerializeField]
    private Transform destination;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip ambientNoise;

    [SerializeField]
    private AudioClip applauseClip;

    [SerializeField]
    GameObject[] playerPrefabs;

    [SerializeField]
    Transform spawnTransform;

    int maxBoredRow = 3;   
    
    private Transform playerHeightTransform;

    GameObject main;
    GameObject camL;
    GameObject camR;

    Transform player;

    Vector3 defaultPlayerPos;

    [SerializeField]
    TextMeshPro text;

    [SerializeField]
    TextMeshPro debugText;

    [SerializeField]
    TextMeshPro debugText2;

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

       // SpawnPlayers(1);
       // SpawnPlayers(0);        
    }

    void Start()
    {
        //playerHeightTransform = Camera.main.transform.parent;       

        //Uncomment below after everything is DONE...
        Recenter_Camera();
            
    }

    float timer;
    // Update is called once per frame
    void Update()
    {
        if (Time.unscaledTime > timer)
        {
            text.text = ((int)(1.0f / Time.unscaledDeltaTime)).ToString() + " FPS";
            timer = Time.unscaledTime + 1.0f;
        }
        
    }

    public void Recenter_Camera()
    {
        playerHeightTransform.localPosition = Vector3.zero;

        if (XRSettings.loadedDeviceName == "Oculus")
        {
            InputTracking.Recenter();
        }

        else
        {
            Vector3 diff = defaultPlayerPos - main.transform.position;            
            player.position += diff;

            /*Valve.VR.OpenVR.System.ResetSeatedZeroPose();
            Valve.VR.OpenVR.Compositor.SetTrackingSpace(Valve.VR.ETrackingUniverseOrigin.TrackingUniverseSeated);*/
        }
        
        playerHeightTransform.localPosition = new Vector3(0f, 1.5f, 0f);
    }


    public void FireRandowWalkEvent()
    {
        int rndRow = UnityEngine.Random.Range(0, grid.gridSize.x);
        rndWalkEvent(rndRow, destination.position);
    }

    public void FireRandomBoredEvent()
    {
        int rndRow = UnityEngine.Random.Range(0, maxBoredRow);
        int rndCol = UnityEngine.Random.Range(0, grid.gridSize.y);       

        boredEvent(rndRow, rndCol);
    }

    public void ToggleAmbientNoise()
    {
        if (audioSource.clip != ambientNoise)
        {
            audioSource.clip = ambientNoise;
            audioSource.volume = 0.1f;
            audioSource.loop = true;

            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }

            audioSource.Play();
        }
        else
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
            else
            {
                audioSource.Play();
            }
        }

    }

    public void StartApplause()
    {
        StartCoroutine(ApplauseTimer());      
    }

    // We want to start NPC to appluase then stop right after the clap sound is finished.
    IEnumerator ApplauseTimer()
    {
        bIsApplauseEvent = true;
        applauseEvent(bIsApplauseEvent);

        if (audioSource.clip != applauseClip)
        {
            audioSource.clip = applauseClip;
            audioSource.volume = 1.0f;
            audioSource.loop = false;            
        }

        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        audioSource.Play();

        yield return new WaitForSeconds(applauseClip.length); // Wait for clap sound to finish and stop applause motion

        bIsApplauseEvent = false;
        applauseEvent(bIsApplauseEvent);

        if (audioSource.clip != applauseClip && audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        
    }

    void SpawnPlayers(int id)
    {
        Vector3 spawnPos = (id != 0) ? spawnTransform.position - new Vector3(0, 0, 0.5f) : spawnTransform.position;

        Instantiate(playerPrefabs[id], spawnPos, Quaternion.identity);


#if UNITY_EDITOR
        main = GameObject.Find("CameraMain");
        if(main != null)
        {
            main.tag = "MainCamera";
            player = main.transform.root;
        }


#elif UNITY_WEBGL
        camL = GameObject.Find("CameraL");
        camR = GameObject.Find("CameraR");
        if(camL != null && camR != null)
        {
            camL.tag = "MainCamera";
            player = camL.transform.root;        
        }   
        
        // Debug ---------------
        debugText.text = player.name;        

#endif
        if (id == 0)
        {
            defaultPlayerPos = player.position;
            playerHeightTransform = player.transform.GetChild(0);

            // Debug ---------------
            debugText2.text = playerHeightTransform.gameObject.name;
        }

        
    }  

}
