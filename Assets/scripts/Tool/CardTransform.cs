using UnityEngine;

//��Ϊ���Ʋ��ְ���λ�ú���ת������������Ҫһ���ṹ���洢������ֵ�����㴫��ʹ��
public struct CardTransform
{
    public Vector3 pos;
    public Quaternion rotation;

    public CardTransform(Vector3 position, Quaternion rotation)
    {
        this.pos = position;
        this.rotation = rotation;
    }
}
