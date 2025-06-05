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

    public List<UserData> allUsers = new List<UserData>();
    public UserData userData;
    public Text[] userInfo = new Text[3];

    string path= "C:\\Users\\LENOVO\\AppData\\LocalLow\\DefaultCompany\\Bank"; // 저장 경로
    string filename = "UserInfo"; // 저장 파일 이름
    public GameObject PopupLogin;
    public GameObject errorWindow;
    public Text errorWindowText;
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

        List<UserData> users = new List<UserData>();

        string filePath = Path.Combine(path, filename);
        //  기존 파일이 있으면 읽어서 리스트에 넣기
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            users = JsonConvert.DeserializeObject<List<UserData>>(json) ?? new List<UserData>();
        }

        // 같은 ID가 있는지 검사해서 업데이트하거나 추가
        bool found = false;
        for (int i = 0; i < users.Count; i++)
        {
            if (users[i].Id == userData.Id)
            {
                allUsers[i] = userData; // 기존 정보 업데이트
                found = true;
                break;
            }
        }
        if (!found)
        {
            allUsers.Add(userData); // 새 유저면 추가
        }

        // 리스트 전체를 JSON으로 직렬화해 다시 저장
        string updatedJson = JsonConvert.SerializeObject(allUsers, Formatting.Indented);
        File.WriteAllText(filePath, updatedJson);

    }
    public void LoadUserData()
    {
        string fullPath = Path.Combine(path, filename);
        if (File.Exists(fullPath))
        {
            string json = File.ReadAllText(Path.Combine(path, filename));
            allUsers = JsonConvert.DeserializeObject<List<UserData>>(json) ?? new List<UserData>();
        }
        else
        {
            Debug.Log("회원가입이 필요합니다");
        }
     
    }
    public void Error(string value)
    {
        errorWindow.gameObject.SetActive(true);
        errorWindowText.text = value;
    }
}
