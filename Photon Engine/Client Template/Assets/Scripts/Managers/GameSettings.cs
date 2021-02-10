using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Manager/GameSettings")]
public class GameSettings : SingletonScriptableObject<GameSettings>
{
    [SerializeField]
    private string _gameversion = "0.0.0";
    public string GameVersion {get { return _gameversion; } }

    [SerializeField]
    private string _nickName = "Codingape";
    public string NickName
    {
        get
        {
            int value = Random.Range(0, 99);
            return _nickName + value.ToString();
        }
    }
}
