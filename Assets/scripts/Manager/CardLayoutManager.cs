using System.Collections.Generic;
using UnityEngine;

public class CardLayoutManager : MonoBehaviour
{
    public bool isHorizontal;
    // ������Ĵ�С��6���ֱ�����1920x1080��Ҳ����˵��������Կ���(-10.7,-6)~(10.7,6)�����壬���������Ƶ�λ�ô�-3.5~3.5
    public float maxWidth = 7f;
    // �����뿨��֮�����ļ����2
    public float cardSpacing = 2f;
    [Header("���β���")]
    // ��֪�����������7���뾶��17���������Ƕ�Ӧ���� arcsin(7/2/17)*2������� 23.76231234995012
    public float maxAngle = 24f;
    // �����뿨��֮������Ƕȼ����7��
    public float angleBetweenCards = 7f;
    // ������ת�İ뾶
    public float radius = 17f;
    // ���ĵ�λ�ã���ʾ��������Ļ�е�λ��
    public Vector3 centerPoint;

    [SerializeField]
    private List<Vector3> cardPositions = new List<Vector3>();//λ��
    private List<Quaternion> cardRotations = new List<Quaternion>();//��ת�Ƕ�

    private void Awake()
    {
        centerPoint = isHorizontal ? Vector3.up * -4.5f : Vector3.up * -21.5f;
    }

    /// <summary>
    /// ��һ���� totalCards �ſ��Ƶ�ʱ�򣬼������ index �ſ��Ƶ��������ת
    /// </summary>
    /// <param name="index"></param>
    /// <param name="totalCards"></param>
    /// <returns></returns>
    public CardTransform GetCardTransform(int index, int totalCards)
    {
        CalculatePositoin(totalCards, isHorizontal);

        return new CardTransform(cardPositions[index], cardRotations[index]);
    }

    /// <summary>
    /// ���㿨�Ƶ�λ�á���ÿ��ֻҪ�п��Ƽ�������뿪������Ҫ���¼���һ�ο��Ƶ�λ��
    /// </summary>
    /// <param name="numberOfCards">��ǰ�ж����ſ���</param>
    /// <param name="horizontal">����������</param>
    private void CalculatePositoin(int numberOfCards, bool horizontal)
    {
        // ����Ҫ����һ��֮ǰ��õ�λ��
        cardPositions.Clear();
        cardRotations.Clear();

        if (horizontal)
        {
            // �����Ǻ������е�

            // һ���� numberOfCards �ſ��ƣ������뿨��֮��ļ���� cardSpacing����ô����֮����ܼ���� currentWidth
            float currentWidth = cardSpacing * (numberOfCards - 1);
            // �������̫�೬�� maxWidth �Ļ�������Ҫ�����������Ϊ maxWidth
            float totalWidth = Mathf.Min(currentWidth, maxWidth);

            // �����ǰ�����뿨��֮��ļ��
            float currentSpacing = totalWidth > 0 ? totalWidth / (numberOfCards - 1) : 0;
            for (int i = 0; i < numberOfCards; i++)
            {
                // �ÿ��ƴ�������Դ��Ź���
                float xPos = 0 - totalWidth / 2 + currentSpacing * i;

                var pos = new Vector3(xPos, centerPoint.y, 0);
                var rotation = Quaternion.identity;

                // Ȼ���¼���ǵ�λ�ú���ת
                cardPositions.Add(pos);
                cardRotations.Add(rotation);
            }
        }
        else
        {
            // �����һ�ſ��ƵĽǶ�
            float totalAngle = (numberOfCards - 1) * angleBetweenCards;
            totalAngle = Mathf.Min(totalAngle, maxAngle);
            float currentAngleBetweenCards;
            if (numberOfCards <= 1)
            {
                currentAngleBetweenCards = 0;
            }
            else
            {
                currentAngleBetweenCards = totalAngle / (numberOfCards - 1);
            }
            float cardAngle = totalAngle / 2;
            for (int i = 0; i < numberOfCards; i++)
            {
                float angle = cardAngle - i * currentAngleBetweenCards;
                // Debug.Log($"angle = {angle}, cardAngle = {cardAngle}, currentAngleBetweenCards = {currentAngleBetweenCards}");
                // ����������ƵĽǶ�
                var pos = FanCardPosition(angle);
                // �������ֶ����� cardAngle Ϊ����ʱ�򣬿���������ת���� cardAngle Ϊ����ʱ�򣬿���������ת
                var rotation = Quaternion.Euler(0, 0, angle);
                cardPositions.Add(pos);
                cardRotations.Add(rotation);
            }
        }
    }

    private Vector3 FanCardPosition(float angle)
    {
        // ��Ϊ��һ���ƵĽǶ������ģ���������ϣ������������λ�ã�����ֻ��ͨ�� centerPoint.x - Mathf.Sin �����ʵ�ʵ� x ����
        return new Vector3(
            centerPoint.x - Mathf.Sin(Mathf.Deg2Rad * angle) * radius,
            centerPoint.y + Mathf.Cos(Mathf.Deg2Rad * angle) * radius,
            0
        );
    }
}
