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

    // 입금, 출금, 송금 입력창
    public InputField[] inputFields = new InputField[4];
    public GameObject impossibleWindow; // 불가능 창
  
    private void Start()
    {
        regex = GetComponent<Regex>();
    }
    // 패널 열기
    public void DepositOpen()
    {
        OpenPanel("deposit");
    }
    public void WithDrawOpen()
    {
        OpenPanel("withDraw");
    }
    public void RemittanceOpen()
    {
        OpenPanel("remittance");
    }
    // 입금, 출금, 송금 패널 열기
    public void OpenPanel(string panelName)
    {
        rightPanel.SetActive(false);

        deposit.SetActive(panelName == "deposit");
        withDraw.SetActive(panelName == "withDraw");
        remittance.SetActive(panelName == "remittance");
    }
    // 뒤로 가기 버튼 클릭
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
    // 송금 버튼 클릭
    public void RemittanceClick()
    {
        // 송글할 사람 이름 가져옴
        string id = inputFields[2].text;
        string money = inputFields[3].text;

        // 입력값이 존재하는지 체크
        if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(money)) 
        { GameManager.Instance.Error("입력 정보를 확인해주세요."); return;}

        //  송금 금액 입력값이 숫자가 아닌지 체크
        if (!int.TryParse(money, out int inputValue)) 
        { GameManager.Instance.Error("숫자를 입력해주세요."); return; }

        // 잔액 확인
        if (!Impossible(GameManager.Instance.userData.Balance, int.Parse(money))) { GameManager.Instance.Error("잔액이 부족합니다."); return; }

        // 송금
        Remittance(id, int.Parse(money));
    }
    // 송금
    public void Remittance(string id, int money)
    {
        bool found = false;

        // 내 현금 줄어듦
        GameManager.Instance.userData.ChangeBalance(GameManager.Instance.userData.Balance - money);

        // 상대방 현금 올라감
        for (int i = 0; i < GameManager.Instance.allUsers.Count; i++)
        {
         
            if (GameManager.Instance.allUsers[i].Id == id)
            {
                found = true;

                GameManager.Instance.allUsers[i].ChangeBalance(GameManager.Instance.allUsers[i].Balance + money);
                // 데이터 저장
                GameManager.Instance.SaveUserData();
                // 입금 단위 별로 , 출력
                regex.LoadMooney();
                break;
            }
        }
        
        // 송금할 사람의 아이디가 존재하는지 검사
        if (!found) { GameManager.Instance.Error("대상이 없습니다."); return; }
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
