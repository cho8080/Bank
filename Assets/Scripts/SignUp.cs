using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
public class SignUp : MonoBehaviour
{
    public InputField[] inputFields = new InputField[4];
    public GameObject errorWindow;
    public Text errorWindowText;
    public Text informationText;

    // Start is called before the first frame update
    void Start()
    {
       
        if (GameManager.Instance.userData == null )
        {
            return;
        }
        GameManager.Instance.LoadUserData();
    }
    public void GoSignUp()
    {
        string id = inputFields[0].text;
        string name = inputFields[1].text;
        string password = inputFields[2].text;
        string passwordConf = inputFields[3].text;

        // 빈칸이 존재한다면
        if(id =="" && name == "" && password == "" && passwordConf == "" )
        {
            Error("정보를 확인해주세요.");
            return;
        }
        else if(password != passwordConf)
        {
            informationText.text = "비밀번호가 서로 다릅니다.";
            return;
        }
        else if (GameManager.Instance.userData.Id == id)
        {
            informationText.text = "아이디가 이미 존재합니다.";
            return;
        }
        else if (id.Length < 2)
        {
            informationText.text = "아이디는 두글자 이상 입력해주세요.";
            return;
        }
        else if (!IsValidId(id))
        {
            informationText.text = "아이디는 영어와 숫자만 입력해주세요";
            return;
        }
        else if (name.Length < 2)
        {
            informationText.text = "이름은 두글자 이상 입력해주세요.";
            return;
        }
        else if (!IsValidName(name))
        {
            informationText.text = "이름에 한글과 영어만 입력해주세요";
            return;
        }
        else if (password.Length < 8)
        {
            informationText.text = "비밀번호는 8글자 이상 입력해주세요.";
            return;
        }

        ChangeData(id, name, password);
        GameManager.Instance.SaveUserData();

        // 회원가입 완료창
        Error("회원가입이 완료되었습니다.");

        // 현재 창 비활성화
        this.gameObject.SetActive(false);
    }
    public void Error(string value)
    {
        errorWindow.gameObject.SetActive(true);
        errorWindowText.text = value;
    }
    void ChangeData(string id, string name, string password)
    {
        // 데이터 변경
        GameManager.Instance.userData.ChangeUserId(id);
        GameManager.Instance.userData.ChangeUserName(name);
        GameManager.Instance.userData.ChangePassword(password);
        GameManager.Instance.userData.ChangeCash(100000);
        GameManager.Instance.userData.ChangeBalance(50000);
    }
    // 영어와 숫자만 허용
    public bool IsValidId(string input)
    {
        return System.Text.RegularExpressions.Regex.IsMatch(input, @"^[a-zA-Z0-9]+$");
    }
    // 한글과 영어만 허용
    public bool IsValidName(string input)
    {
     
        return System.Text.RegularExpressions.Regex.IsMatch(input, @"^[가-힣a-zA-Z]+$");
    }
}
