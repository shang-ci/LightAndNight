using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;//����ģʽ������ҹ������ĵ���
    public List<Player> PlayerList;//����б��������б�
    public List<CardLibrarySO> CardLibraryList;//��ǰ��ɫ���ƿ⡪��ҲӦ���Ƕ�ȡ��
    public List<PlayerData> PlayerDataList;//�洢������ݵ��б����洢�������
    public Player currentPlayer;//��ǰ��ҡ�����ǰ��ս�Ľ�ɫ


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
