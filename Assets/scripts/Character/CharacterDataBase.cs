// CharacterData.cs
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterData 
{
    public string character;
    public int sincere;
    public int brave;
    public int fearless;
    public int concentration;
    public int faith;
    public int happy;
}

public class CharacterDataBase : MonoBehaviour
{
    public static Dictionary<string, CharacterData> characters = new Dictionary<string, CharacterData>();

    void Awake()
    {
        TextAsset csvData = Resources.Load<TextAsset>("GuangYeCharacterSheet");

        if (csvData == null)
        {
            Debug.LogError("CSV文件未找到！路径应为：Assets/Resources/CharactersData.csv");
            return;
        }
        //未debug，说明csv文件存在

        //Debug.LogError("CSV文件已找到！");

        string[] data = csvData.text.Split('\n');

        Debug.Log($"成功加载 {data.Length - 2} 个角色数据"); // 减1排除标题行

        // for (int i = 1; i < data.Length; i++)
        // { // 跳过标题行
        //     if (!string.IsNullOrEmpty(data[i]))
        //     {
        //         string[] row = data[i].Split(',');
        //         CharacterData charData = new CharacterData
        //         {
        //             character = row[0],
        //             sincere = int.Parse(row[7]),
        //             brave = int.Parse(row[8]),
        //             fearless = int.Parse(row[9]),
        //             concentration = int.Parse(row[10]),
        //             faith = int.Parse(row[11]),
        //             happy = int.Parse(row[12])
        //         };
        //         characters.Add(charData.character, charData);
        //     }
        // }

    }

    // void Awake()
    // {
    //     TextAsset csvData = Resources.Load<TextAsset>("GuangYeCharacterSheet");

    //     // 文件存在性检查
    //     if (csvData == null)
    //     {
    //         Debug.LogError("CSV文件未找到！路径应为：Assets/Resources/GuangYeCharacterSheet.csv");
    //         return;
    //     }

    //     string[] data = csvData.text.Split('\n');
    //     Debug.Log($"CSV原始内容：\n{csvData.text}"); // 打印原始数据
    //     Debug.Log($"成功加载 {data.Length - 1} 行数据");

    //     // 遍历所有行
    //     for (int i = 1; i < data.Length; i++)
    //     {
    //         if (string.IsNullOrWhiteSpace(data[i])) continue;

    //         try
    //         {
    //             string[] row = data[i].Split(',');
    //             Debug.Log($"解析第{i}行：{string.Join("|", row)}");

    //             // 强制列数检查
    //             if (row.Length < 7)
    //             {
    //                 Debug.LogError($"第{i}行列数不足，应有7列，实际{row.Length}列");
    //                 continue;
    //             }

    //             CharacterData charData = new CharacterData
    //             {
    //                 character = row[0].Trim(), // 去除首尾空格
    //                 sincere = int.Parse(row[7]),
    //                 brave = int.Parse(row[8]),
    //                 fearless = int.Parse(row[9]),
    //                 concentration = int.Parse(row[10]),
    //                 faith = int.Parse(row[11]),
    //                 happy = int.Parse(row[12])
    //             };
    //             characters.Add(charData.character, charData);
    //         }
    //         catch (System.Exception e)
    //         {
    //             Debug.LogError($"解析第{i}行失败：{e.Message}\n原始数据：{data[i]}");
    //         }
    //     }

    //     // 打印所有加载的角色名
    //     Debug.Log("已加载角色列表：" + string.Join(", ", characters.Keys));
    // }
}