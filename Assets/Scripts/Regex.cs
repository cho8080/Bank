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
            numbers[i] = int.Parse(texts[i].text);
            texts[i].text = numbers[i].ToString("N0");
        }
    }
}
