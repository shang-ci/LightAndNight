using System.Collections.Generic;
using UnityEngine;

public class ExistingExcelHelper : MonoBehaviour
{
    public static List<CardGrowthConfig> cardLevelDataList;
    public static List<CardExperience> cardExperienceList;

    void Start()
    {
        cardLevelDataList = ReadExcelData<CardGrowthConfig>("CardGrowthConfig");
        cardExperienceList = ReadExcelData<CardExperience>("CardExperience");
    }

    // public List<T> ReadExcelData<T>(string tableName) 
    // { 
    //     // ...原有代码...
    //     return new List<T>(); // Ensure a return value
    // }

    public List<T> ReadExcelData<T>(string tableName) {
        
         /*...原有代码...*/ 
         return new List<T>(); // Ensure a return value
    }
}
