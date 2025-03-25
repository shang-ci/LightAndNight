using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using ExcelDataReader;
using UnityEngine;

public class ExcelDataLoader : MonoBehaviour
{
    public static ExcelDataLoader Instance { get; private set; }

    public string filePath;
    public List<CardDataSO> cardDataList = new List<CardDataSO>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        filePath = Application.dataPath + "/Data/DreamOfTheKing.xlsx";
        LoadExcelData(filePath);
    }

    private void LoadExcelData(string path)
    {
        using (var stream = File.Open(path, FileMode.Open, FileAccess.Read))
        {
            using (var reader = ExcelReaderFactory.CreateReader(stream))
            {
                //�����һ�е���������ȡ����
                var configuration = new ExcelDataSetConfiguration
                {
                    ConfigureDataTable = _ => new ExcelDataTableConfiguration
                    {
                        UseHeaderRow = true // ���߶�ȡ����һ��������
                    }
                };

                var result = reader.AsDataSet(configuration);

                // ��ȡ��������
                var cardsTable = result.Tables["Cards"];

                // ��ӡ�����Խ��е���
                foreach (DataColumn column in cardsTable.Columns)
                {
                    Debug.Log($"Column: {column.ColumnName}");
                }

                foreach (DataRow row in cardsTable.Rows)
                {
                    try
                    {
                        CardDataSO cardData = ScriptableObject.CreateInstance<CardDataSO>();

                        string name = row["Name"].ToString();
                        Sprite image = LoadSprite(row["ImagePath"].ToString());
                        int cost = Convert.ToInt32(row["Cost"].ToString());
                        CardType cardType = Enum.Parse<CardType>(row["CardType"].ToString());
                        string description = row["Description"].ToString();
                        List<Effect> effects = LoadEffects(row["Effects"].ToString());
                        List<StatusEffect> statusEffects = LoadStatusEffects(row["StatusEffects"].ToString());

                        cardData.Initialize(name, image, cost, cardType, description, effects, statusEffects);
                        cardData.itemType = ItemType.CardData;//��ʼ��Ĭ��ֵ
                        cardDataList.Add(cardData);

                        // ��ӡÿһ�е������Խ��е���
                        Debug.Log($"Loaded card: {name}, {image}, {cost}, {cardType}, {description}, {effects.Count} effects, {statusEffects.Count} status effects");
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError($"Error reading row: {ex.Message}");
                    }
                }
            }
        }
    }

    private Sprite LoadSprite(string path)
    {
        return Resources.Load<Sprite>(path);
    }

    private List<Effect> LoadEffects(string effectsData)
    {
        // ������� effectsData ��һ�����ŷָ���Ч�������б�
        List<Effect> effects = new List<Effect>();

        if (string.IsNullOrEmpty(effectsData))
        {
            return effects;
        }

        string[] effectNames = effectsData.Split(',');
        foreach (string effectName in effectNames)
        {
            // ��������һ������������Effectʵ��
            Effect effect = Resources.Load<Effect>($"Effects/{effectName}");
            if (effect != null)
            {
                effects.Add(effect);
            }
            else
            {
                Debug.LogError($"Effect not found: {effectName}");
            }
        }
        return effects;
    }

    private List<StatusEffect> LoadStatusEffects(string statusEffectsData)
    {
        // ������� statusEffectsData ��һ�����ŷָ���״̬Ч�������б�
        List<StatusEffect> statusEffects = new List<StatusEffect>();

        if (string.IsNullOrEmpty(statusEffectsData))
        {
            return statusEffects;
        }

        string[] statusEffectNames = statusEffectsData.Split(',');
        foreach (string statusEffectName in statusEffectNames)
        {
            // ��������һ������������StatusEffectʵ��
            StatusEffect statusEffect = Resources.Load<StatusEffect>($"StatusEffects/{statusEffectName}");
            if (statusEffect != null)
            {
                statusEffects.Add(statusEffect);
            }
            else
            {
                Debug.LogError($"StatusEffect not found: {statusEffectName}");
            }
        }
        return statusEffects;
    }
}