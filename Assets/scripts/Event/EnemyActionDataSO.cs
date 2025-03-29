using System.Collections.Generic;
using UnityEngine;

// ������Ϊ���ݣ�ÿ�����˶����Լ�����Ϊ����
[CreateAssetMenu(fileName = "EnemyActionDataSO", menuName = "Enemy/EnemyActionDataSO")]
public class EnemyActionDataSO : ScriptableObject
{
    public List<EnemyAction> actions;
}

//��ͼ��ͼƬ����Ч��
[System.Serializable]
public struct EnemyAction
{
    public Sprite intentSprite;
    public Effect effect;
    public float probability; //������
    public float accuracy; //��׼��
    public float criticalRate; //������
    public int originalValue; //ԭʼֵ
}