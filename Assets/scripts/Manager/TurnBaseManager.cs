using EventSystem;
using System.Collections.Generic;
using UnityEngine;

//ս���еĻغϹ����Ƿ���ս������ҵ���ʾ����
public class TurnBaseManager : MonoBehaviour
{
    public static TurnBaseManager instance;

    [SerializeField]private bool isPlayerTurn = false;
    [SerializeField]private bool isEnemyTurn = false;
    public bool battleEnd = true;//ս���Ƿ��������ֻ�д���ս��ʱ���Ż���лغϹ���

    [Header("�׶ι���")]
    private float timeCounter;//��ʱ��������ÿ���غϵ�ʱ��
    public float enemyTurnDuration;
    public float playerTurnDuration;//���û��ʱ��Ҫ��ֻ�е�����ʱ��Ҫ�󡪡����߸��������ʱ��
    public float drawCardDuration;//����ʱ�䡪�����ƽ׶�

    [Header("�غϼ�����")]
    private int playerTurnCount = 0; // ��һغϼ�������Ҳ�������Ҫ��¼�غ��������ڱ�ĵط�����������
    private int enemyTurnCount = 0; // ���˻غϼ���

    [Header("�¼��㲥")]
    public ObjectEventSO playerTurnBegin;//��һغϽ����õ��˻غϿ�ʼ�����ʾ��Ҳ����������ѳ��ƽ׶γ�ȡ����
    public ObjectEventSO enemyTurnBegin;
    public ObjectEventSO enemyTurnEnd;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        EventManager.Instance.AddListener("PlayerTurnEnd", EnemyTurnBegin);//��һغϽ��������˻غϿ�ʼ
        EventManager.Instance.AddListener<object>("GameOver", StopTurnBaseSystem);//ս��������ֹͣ�غϹ���
        EventManager.Instance.AddListener("NewGame", NewGame);//��ʼ�˵��������Ϸ����ʼ�����
        EventManager.Instance.AddListener("GameStart", GameStart);//���뷿��/����ս���¼��������غϹ���
    }

    private void OnDisable()
    {
        EventManager.Instance.RemoveListener("PlayerTurnEnd", EnemyTurnBegin);
        EventManager.Instance.RemoveListener<object>("GameOver", StopTurnBaseSystem);
        EventManager.Instance.RemoveListener("NewGame", NewGame);
        EventManager.Instance.RemoveListener("GameStart", GameStart);
    }

    private void Update()
    {
        if (battleEnd)
        {
            return;
        }

        if (isEnemyTurn)
        {
            timeCounter += Time.deltaTime;
            if (timeCounter >= enemyTurnDuration)
            {
                timeCounter = 0;
                // ���˻غϽ���
                EnemyTurnEnd();
            }
        }

        if (isPlayerTurn)
        {
            timeCounter += Time.deltaTime;
            if (timeCounter >= playerTurnDuration)
            {
                timeCounter = 0f;
                // ��һغϿ�ʼ
                PlayerTurning();
                isPlayerTurn = false;
            }
        }
    }

    //����ս��ʱ�Ϳ����غϹ�����һ���ǽ���ս������
    [ContextMenu("Game Start")]
    public void GameStart()
    {
        //���������ϵ�����״̬
        //player = playerObj.GetComponent<Player>();
        //TOOD�����ﷵ�صĵ��˿����Ƕ�������Բ���ֻ��Enemy���ͽ��գ��Ժ�Ҫ�ĳ�Enemy[]

        isPlayerTurn = true;
        isEnemyTurn = false;
        battleEnd = false;
        timeCounter = 0;
        playerTurnCount = 0;
        enemyTurnCount = 0;
    }

    public void PlayerTurning()
    {
        playerTurnCount++;
        playerTurnBegin.RaiseEvent(null, this);
        EventManager.Instance.TriggerEvent("PlayerTurnBegin");
        //player.UpdateStatusEffectRounds();//�������״̬Ч���غ���
    }

    //��һغϽ������á���������غ�ת����ťʱ��һغϽ���
    public void EnemyTurnBegin()
    {
        enemyTurnCount++;
        isEnemyTurn = true;
        EventManager.Instance.TriggerEvent("EnemyTurnBegin");//���˻غϴ����������˹���
        //enemy.UpdateStatusEffectRounds();//���µ���״̬Ч���غ���
    }

    public void EnemyTurnEnd()
    {
        isEnemyTurn = false;
        enemyTurnEnd.RaiseEvent(null, this);
        isPlayerTurn = true;
    }





    //ֹͣ��ս��������սȥ��ʤ��ʱ���á���gameover��loadma�¼�����ֹͣ�غϹ���
    public void StopTurnBaseSystem(object obj)
    {
        battleEnd = true;
        //playerObj.SetActive(false);
    }

    //��ʼ����ҡ����ڵ������Ϸʱ���ã���Ϊ������������ص��޷�����������Ϸ�¼�������������Ϸʱ
    public void NewGame()
    {
        foreach(var item in GameManager.instance.playerCharacters)
        {
            item.GetComponent<Player>().NewGame();
        }
    }
}
