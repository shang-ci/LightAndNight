using System;

[Flags]
public enum RoomType
{
    MinorEnemy = 1 << 0,
    EliteEnemy = 1 << 1,
    Shop = 1 << 2,
    Treasure = 1 << 3,
    RestRoom = 1 << 4,
    Boss = 1 << 5
}

public enum RoomState
{
    Locked,
    Visited,
    Attainable
}

public enum CardType
{
    Attack,
    Defense,
    Abilities
}


////����
//public enum EffectType
//{
//    Purification,//����
//    Corruption,//����
//    Strengthen,//ǿ��
//    Reset,//����
//    Transform,//ת��
//    Extract,//��ȡ
//    Shield//����
//}

//effect�����͡�������effect��Ŀ��
public enum EffectTargetType
{
    Self,//�Լ�
    Target,//��������
    ALL,//���н�ɫ
    Our,//����������
    Enemies,//���е���
    Random,//���һ��Ŀ��
}

public enum ItemType
{
    EquipmentEffect,
    CardEffect,
    CardData,
    Equipment,
    CardShop,
    EquipmentShop,
}



public enum StatusEffectTargetType
{
    Self,//�Լ�
    Target,//��������
    ALL,//���н�ɫ
    Our,//����������
    Enemies,//���е���
    Random,//���һ��Ŀ��
}



//����ʱ��
public enum EffectTiming
{
    Reseach,//�о����ƴ�������о���һʱ��
    Strengthen,//ǿ�����ƴ������ǿ����һʱ��
    OnPlayerTurn,//��һغ�
    OnEnemyTurn,//���˻غ�
    OnDamagePlayer,//�������
    OnDamageEnemy,//��������
    PlayerThorn,//��Ҿ���
    EnemyThorn,//���˾���
    Thorn,//����
    ThickSkin,//��Ƥ
    PlayerThickSkin,//��Һ�Ƥ
    EnemyThickSkin,//���˺�Ƥ
    Shield,//����
    EnemyShield,//���˻���
    PlayerShield,//��һ���
    CrimsonMark,//�ɺ�ӡ��
    PlayerCrimsonMark,//����ɺ�ӡ��
    EnemyCrimsonMark,//�����ɺ�ӡ��
    None
    // �������ִ��ʱ��
}

//����
public enum Attribute
{
    gold,
    wood,
    water,
    fire,
    earth
}


