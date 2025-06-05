using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
[System.Serializable]

public class UserData 
{
    [JsonProperty("id")]
    private string id;
    [JsonProperty("userName")]
    private string userName;
    [JsonProperty("password")]
    private string password;
    [JsonProperty("cash")]
    private int cash;
    [JsonProperty("balance")]
    private int balance;

    [JsonIgnore]
    public string Id => id;
    [JsonIgnore]
    public string UserName => userName;
    [JsonIgnore]
    public string Password => password;
    [JsonIgnore]
    public int Cash => cash;
    [JsonIgnore]
    public int Balance => balance;

    public void ChangeUserId(string userId) => id = userId;
    public void ChangeUserName(string name) => userName = name;
    public void ChangePassword(string value) => password = value;
    public void ChangeCash(int value) => cash = value;
    public void ChangeBalance(int value) => balance = value;

    public UserData() { }

    [JsonConstructor]
    public UserData(string _userName, int _cash, int _balance)
    {
        userName = _userName;
        cash = _cash;
        balance = _balance;
    }

    // 내부 데이터를 PlayerDTO 형태로 변환하는 팩토리 메서드
    // 팩토리 메서드 : 객체 생성 코드를 별도의 메서드로 분리해서 관리하는 디자인 패턴 기법 중 하나
    // 보통 new 키워드를 직접 쓰지 않고, 객체 생성 로직을 캡슐화해서 유연성과 재사용성을 높인다.
    public PlayerDTO ToDTO()
    {
        return new PlayerDTO
        {
            userName = this.userName,
            cash = this.cash,
            balance = this.balance
        };
    }
}
// 데이터 저장용 DTO 클래스
[System.Serializable]
public class PlayerDTO
{
    public string userName;
    public int cash;
    public int balance;
}
