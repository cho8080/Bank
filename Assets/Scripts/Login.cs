using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Login : MonoBehaviour
{
    public InputField[] inputFields = new InputField[2];
    public GameObject popupBank;
    public GameObject popupSignUp;
    public GameObject errorWindow;
    public Text errorWindowText;

    public void GoLogin()
    {
        string id = inputFields[0].text;
        string password = inputFields[1].text;

        if (id == "" && password =="")
        {
            errorWindow.gameObject.SetActive(true);
            error("아이디와 비밀번호를 입력해주세요.");
            return;
        }


        else if (id != GameManager.Instance.userData.Id)
        {
            errorWindow.gameObject.SetActive(true);
            error("아이디가 틀렸습니다.");
            return;
        }
        else if (password != GameManager.Instance.userData.Password)
        {
            error("비밀번호가 틀렸습니다.");
            return;
        }
        popupBank.gameObject.SetActive(true);
        this.gameObject.SetActive(false);
    }
    public void SignUp()
    {
        popupSignUp.gameObject.SetActive(true);
    }
   public void error(string value)
    {
        errorWindow.gameObject.SetActive(true);
        errorWindowText.text = value;
    }
}
