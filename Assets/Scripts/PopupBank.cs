using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class PopupBank : MonoBehaviour
{
    Regex regex;

    public GameObject rightPanel;
    public GameObject deposit;
    public GameObject withDraw;
    public GameObject remittance;

    public TMP_InputField[] inputFields = new TMP_InputField[2];

    public GameObject impossibleWindow; // 불가능 창
    private void Start()
    {
        regex = GetComponent<Regex>();
    }
    public void DepositOpen()
    {
        rightPanel.SetActive(false);
        deposit.SetActive(true);
        withDraw.SetActive(false);
        remittance.SetActive(false);
    }
    public void WithDrawOpen()
    {
        rightPanel.SetActive(false);
        deposit.SetActive(false);
        withDraw.SetActive(true);
        remittance.SetActive(false);
    }
    public void RemittanceOpen()
    {
        rightPanel.SetActive(false);
        deposit.SetActive(false);
        withDraw.SetActive(false);
        remittance.SetActive(true);
    }
    public void BackBtn()
    {
        if (deposit.activeSelf == true)
        {
            deposit.SetActive(false);
        }
        else if (withDraw.activeSelf == true)
        {
            withDraw.SetActive(false);
        }
        else if (remittance.activeSelf == true)
        {
            remittance.SetActive(false);
        }
        rightPanel.SetActive(true);
    }
    // 입금하기 (input field 입력 후 입금 버튼 클릭)
    public void Deposit()
    {
        string inputText = inputFields[0].text;

        // 입력값이 존재하거나 숫자가 아닌지 체크
        if (CheckInput(inputText)) { return; }

        // 입금 처리
        ComeInMoney(int.Parse(inputText));
    }
    // 입금하기 (숫자 입금 버튼 클릭)
    public void Deposit(int money)
    {
        ComeInMoney(money);
    }
    // 입금 처리
    void ComeInMoney(int value)
    {
        int x = GameManager.Instance.userData.Cash;
        int y = GameManager.Instance.userData.Balance;

        // 잔액이 충분한지 확인
        if (!Impossible(x, value)) { impossibleWindow.SetActive(true); return; }

        // 입금 처리
        GameManager.Instance.userData.ChangeCash(GameManager.Instance.userData.Cash-value);
        GameManager.Instance.userData.ChangeBalance(GameManager.Instance.userData.Balance + value);
        GameManager.Instance.Refresh();

        // 입금 단위 별로 , 출력
        regex.StartRegex();

        // 데이터 저장
        GameManager.Instance.SaveUserData();
    }
    // 출금하기 (input field 입력 후 입금 버튼 클릭)
    public void WithDraw()
    {
        string inputText = inputFields[1].text;

        // 입력값이 존재하거나 숫자가 아닌지 체크
        if (CheckInput(inputText)) { return; }
 
        RunOutOfMoney(int.Parse(inputText));
    }
    // 출금하기 (숫자 입금 버튼 클릭)
    public void WithDraw(int money)
    {
        RunOutOfMoney(money);
    }
    // 출금 처리
    void RunOutOfMoney(int value)
    {
        int x = GameManager.Instance.userData.Cash;
        int y = GameManager.Instance.userData.Balance;

        // 잔액이 충분한지 확인
        if (!Impossible(y, value)) { impossibleWindow.SetActive(true); return;  }

        // 출금 처리
        GameManager.Instance.userData.ChangeCash(GameManager.Instance.userData.Cash + value);
        GameManager.Instance.userData.ChangeBalance(GameManager.Instance.userData.Balance - value);
        GameManager.Instance.Refresh();

        // 입금 단위 별로 , 출력
        regex.StartRegex();

        // 데이터 저장
        GameManager.Instance.SaveUserData();
    }
    // 잔액이 충분한지 확인
    bool Impossible(int a, int b)
    {
        return a - b < 0 ? false : true;
    }
    //  입력값이 존재하지 않거나 숫자가 아닌지 체크
    bool CheckInput(string inputText)
    {
        if (string.IsNullOrWhiteSpace(inputText) || !int.TryParse(inputText, out int inputValue)) { return true; }
        return false;
    }
}
