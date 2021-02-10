using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Signletons/MasterManager")]
public class MasterManager : SingletonScriptableObject<MasterManager>
{

    [SerializeField]
    private GameSettings _gameSettings;
    public static GameSettings GameSettings { get { return Instance._gameSettings; } }

}
