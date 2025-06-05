using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Login : MonoBehaviour
{
    public InputField[] inputFields = new InputField[2];
    public GameObject popupBank;
    public GameObject popupSignUp;

    Regex regex;
    bool userFound = false;

    private void Start()
    {
        regex = FindObjectOfType<Regex>();
    }
    public void GoLogin()
    {
        string id = inputFields[0].text;
        string password = inputFields[1].text;

        if (id == "" && password =="")
        {
            GameManager.Instance.Error("아이디와 비밀번호를 입력해주세요.");
            return;
        }
        // 정보가 일치하는 회원이 있는지 검사
        for (int i = 0; i < GameManager.Instance.allUsers.Count; i++)
        {
            if (id == GameManager.Instance.allUsers[i].Id)
            {
                // 정보가 일치하는 회원이 있다면
                if (password == GameManager.Instance.allUsers[i].Password)
                {
                    GameManager.Instance.userData = GameManager.Instance.allUsers[i];
                   // 데이터 표시
                   regex.LoadMooney();
                    popupBank.gameObject.SetActive(true);
                    this.gameObject.SetActive(false);

                }
                // 비밀번호가 틀렸다면
                else
                {
                    GameManager.Instance.Error("비밀번호가 틀렸습니다.");
                }
                return;
            }
        }
        // 같은 아이디를 가진 회원을 찾지 못했다면
        if(!userFound)
        {
            GameManager.Instance.Error("아이디가 틀렸습니다.");
        }
  
    }
    // 회원가입 창 활성화
    public void SignUp()
    {
        popupSignUp.gameObject.SetActive(true);
    }
  
}
