using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
public class Regex : MonoBehaviour
{
    public Text[] texts = new Text[2];
    int[] numbers = new int[2];

    public void LoadMooney()
    {
        texts[0].text = GameManager.Instance.userData.Balance.ToString();
        texts[1].text = GameManager.Instance.userData.Cash.ToString();

        StartRegex();
    }
    public void StartRegex()
    {
        for (int i = 0; i < texts.Length; i++)
        {
           
            texts[i].text = texts[i].text.Replace(",", ""); // 쉼표 제거

            numbers[i] = int.Parse(texts[i].text); // 숫자로 변환
            texts[i].text = numbers[i].ToString("N0");// 다시 쉼표 붙여서 출력
        }
    }
}
