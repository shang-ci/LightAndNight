using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;//单例模式——玩家管理器的单例
    public List<Player> PlayerList;//玩家列表——出场列表
    public List<CardLibrarySO> CardLibraryList;//当前角色的牌库——也应该是读取的
    public List<PlayerData> PlayerDataList;//存储玩家数据的列表——存储玩家数据
    public Player currentPlayer;//当前玩家——当前出战的角色


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        SetPlayerData();
        InitPlayers();
    }

    private void Update()
    {
        
    }

    public void InitPlayers()
    {
        int i = 0;
        foreach (Player player in PlayerList)
        {
            player.SetCharacterBase(PlayerDataList[i]);
            player.gameObject.SetActive(false);
            i++;
        }
        currentPlayer = PlayerList[0];
        currentPlayer.gameObject.SetActive(true);
    }

    public void SetPlayerData()
    {
        PlayerDataList = new List<PlayerData>
        {
            new PlayerData { playerName = "Player1", playerId = 101, library = CardLibraryList[0] },
            new PlayerData { playerName = "Player2", playerId = 102, library = CardLibraryList[1] },
            new PlayerData { playerName = "Player3", playerId = 103, library = CardLibraryList[2] },
        };
    }

    public void ChangeCurrentPlayer(Player _player)
    {
        currentPlayer.gameObject.SetActive(false);
        currentPlayer = _player;
        currentPlayer.gameObject.SetActive(true);
    }
}

public struct PlayerData
{
    public CardLibrarySO library;
    public string playerName;
    public int playerId;
}
