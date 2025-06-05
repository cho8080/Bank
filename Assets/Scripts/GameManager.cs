using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public UserData userData;
    public Text[] userInfo = new Text[3];

    string path= "C:\\Users\\LENOVO\\AppData\\LocalLow\\DefaultCompany\\Bank"; // 저장 경로
    string filename = "UserInfo"; // 저장 파일 이름
    public GameObject PopupLogin;

    // Start is called before the first frame update
    void Awake()
    {
      if(Instance != null && Instance !=this)
        {
            Destroy(Instance);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // 저장 결로 설정
        path = Application.persistentDataPath;
    }
    private void Start()
    {
       // userData = new UserData("조하늘",100000,50000);
     //   SaveUserData();
        LoadUserData();
        PopupLogin.SetActive(true);
    }
   public void Refresh()
    {
        userInfo[0].text = userData.UserName;
        userInfo[1].text = userData.Cash.ToString();
        userInfo[2].text = userData.Balance.ToString();
    }
    public void SaveUserData()
    {
        string data = JsonConvert.SerializeObject(userData);
        Debug.Log("저장되는 데이터: " + path); // 디버깅용 로그
        Debug.Log("저장되는 데이터: " + userData); // 디버깅용 로그
        File.WriteAllText(Path.Combine(path, filename), data);
        Debug.Log("데이터 로드 : " + data);
    }
    public void LoadUserData()
    {
        string fullPath = Path.Combine(path, filename);
        if (File.Exists(fullPath))
        {
            string data = File.ReadAllText(Path.Combine(path, filename));
            userData = JsonConvert.DeserializeObject<UserData>(data);

            // 데이터 표시
            Regex regex = GetComponent<Regex>();
            regex.LoadMooney();

            Debug.Log("데이터 로드 : " + userData);
        }
        else
        {
            Debug.Log("회원가입이 필요합니다");
        }
     
    }
}
