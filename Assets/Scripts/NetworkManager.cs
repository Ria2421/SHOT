//---------------------------------------------------------------
//
// ネットワークマネージャー [ NetWorkManager.cs ]
// Author:Kenta Nakamoto
// Data:2024/08/26
// Update:2024/09/17
//
//---------------------------------------------------------------
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkManager : MonoBehaviour
{
    //-------------------------------------------------------------------
    // フィールド

    /// <summary>
    /// APIベースURL
    /// </summary>
    const string API_BASE_URL = "https://api-shot.japaneast.cloudapp.azure.com/api/";

    /// <summary>
    /// プレイ中のユーザーID
    /// </summary>
    private int userID = 0;

    /// <summary>
    /// プレイ中のユーザー名
    /// </summary>
    private string userName = "";

    /// <summary>
    /// プレイ中のステージNo
    /// </summary>
    public int PlayStageNo {  get; set; }

    /// <summary>
    /// プレイ中のステージタイプ (1:ノーマル 2:クリエイト)
    /// </summary>
    public int PlayStageType {  get; set; }

    /// <summary>
    /// 最後のノーマルステージNo
    /// </summary>
    public int LastStageNo { get; set; }

    // getプロパティを呼び出した初回時にインスタンス生成してstaticで保持
    private static NetworkManager instance;

    /// <summary>
    /// NetworkManagerプロパティ
    /// </summary>
    public static NetworkManager Instance
    {
        get
        {
            if (instance == null)
            {
                // GameObjectを生成し、NetworkManagerを追加
                GameObject gameObject = new GameObject("NetworkManager");
                instance = gameObject.AddComponent<NetworkManager>();

                // シーン遷移で破棄されないように設定
                DontDestroyOnLoad(gameObject);
            }

            return instance;
        }
    }

    //-------------------------------------------------------------------
    // メソッド

    /// <summary>
    /// ユーザーID取得
    /// </summary>
    /// <returns></returns>
    public int GetUserID()
    {
        return userID;
    }

    /// <summary>
    /// ユーザー名取得
    /// </summary>
    /// <returns>ユーザー名</returns>
    public string GetUserName()
    {
        return userName;
    }

    /// <summary>
    /// ユーザーデータ保存処理
    /// </summary>
    private void SaveUserData()
    {
        // セーブデータクラスの生成
        SaveData saveData = new SaveData();
        saveData.UserName = this.userName;
        saveData.UserID = this.userID;

        // データをJSONシリアライズ
        string json = JsonConvert.SerializeObject(saveData);

        // 指定した絶対パスに"saveData.json"を保存
        var writer = new StreamWriter(Application.persistentDataPath + "/saveData.json");
        writer.Write(json); // 書き出し
        writer.Flush();     // バッファに残っている値を全て書き出し
        writer.Close();     // ファイル閉
    }

    /// <summary>
    /// ユーザーデータ読み込み処理
    /// </summary>
    /// <returns></returns>
    public bool LoadUserData()
    {
        if(!File.Exists(Application.persistentDataPath + "/saveData.json"))
        {   // 指定のパスのファイルが存在しなかった時、早期リターン
            return false;
        }

        //  ローカルファイルからユーザーデータの読込処理
        var reader = new StreamReader(Application.persistentDataPath + "/saveData.json");
        string json = reader.ReadToEnd();
        reader.Close();

        // セーブデータJSONをデシリアライズして取得
        SaveData saveData = JsonConvert.DeserializeObject<SaveData>(json);
        this.userID = saveData.UserID;
        this.userName = saveData.UserName;

        // 読み込み結果をリターン
        return true;
    }

    //=============================
    // GET処理

    /// <summary>
    /// ノーマルステージ取得処理
    /// </summary>
    /// <param name="result"></param>
    /// <returns></returns>
    public IEnumerator GetNormalStage(Action<List<NormalStageResponse>> result)
    {
        // リクエスト送信処理
        UnityWebRequest request = UnityWebRequest.Get(API_BASE_URL + "stages/normal");
        yield return request.SendWebRequest();  // 結果を受信するまで待機

        // 受信情報格納用
        List<NormalStageResponse> response = new List<NormalStageResponse>();

        if (request.result == UnityWebRequest.Result.Success
            && request.responseCode == 200)
        {   // 通信が成功した時

            string resultJson = request.downloadHandler.text;   // レスポンスボディ(json)の文字列を取得
            response = JsonConvert.DeserializeObject<List<NormalStageResponse>>(resultJson);  // JSONデシリアライズ
        }

        // 呼び出し元のresult処理を呼び出す
        result?.Invoke(response);
    }

    /// <summary>
    /// 指定IDのギミック情報を取得
    /// </summary>
    /// <param name="id">ステージID</param>
    /// <param name="result"></param>
    /// <returns></returns>
    public IEnumerator GetIDCreate(int id, Action<CreateStageResponse> result)
    {
        // リクエスト送信処理
        UnityWebRequest request = UnityWebRequest.Get(API_BASE_URL + "stages/create/" + id.ToString());
        yield return request.SendWebRequest();  // 結果を受信するまで待機

        // 受信情報格納用
        CreateStageResponse response = new CreateStageResponse();

        if (request.result == UnityWebRequest.Result.Success
            && request.responseCode == 200)
        {   // 通信が成功した時

            string resultJson = request.downloadHandler.text;   // レスポンスボディ(json)の文字列を取得
            response = JsonConvert.DeserializeObject<CreateStageResponse>(resultJson);  // JSONデシリアライズ
        }

        // 呼び出し元のresult処理を呼び出す
        result?.Invoke(response);
    }

    /// <summary>
    /// 自作ステージ取得処理
    /// </summary>
    /// <param name="userID">自分のユーザーID</param>
    /// <param name="result"></param>
    /// <returns></returns>
    public IEnumerator GetPlayerCreateStage(Action<List<CreateStageInfoResponse>> result)
    {
        // リクエスト送信処理
        UnityWebRequest request = UnityWebRequest.Get(API_BASE_URL + "stages/create/user/" + userID.ToString());
        yield return request.SendWebRequest();  // 結果を受信するまで待機

        // 受信情報格納用
        List<CreateStageInfoResponse> response = new List<CreateStageInfoResponse>();

        if (request.result == UnityWebRequest.Result.Success
            && request.responseCode == 200)
        {   // 通信が成功した時

            string resultJson = request.downloadHandler.text;   // レスポンスボディ(json)の文字列を取得
            response = JsonConvert.DeserializeObject<List<CreateStageInfoResponse>>(resultJson);  // JSONデシリアライズ
        }

        // 呼び出し元のresult処理を呼び出す
        result?.Invoke(response);
    }

    /// <summary>
    /// カスタムプレイ情報取得処理
    /// </summary>
    /// <param name="result"></param>
    /// <returns></returns>
    public IEnumerator GetCustomList(Action<List<List<CreateStageInfoResponse>>> result)
    {
        List<List<CreateStageInfoResponse>> responses = new List<List<CreateStageInfoResponse>>();

        //===================================
        // フォローのステージ情報受信処理

        // リクエスト送信処理
        UnityWebRequest request1 = UnityWebRequest.Get(API_BASE_URL + "stages/create/follow/" + userID.ToString());
        yield return request1.SendWebRequest();  // 結果を受信するまで待機

        if (request1.result == UnityWebRequest.Result.Success
            && request1.responseCode == 200)
        {   // 通信が成功した時

            string resultJson = request1.downloadHandler.text;   // レスポンスボディ(json)の文字列を取得
            List<CreateStageInfoResponse> response = JsonConvert.DeserializeObject<List<CreateStageInfoResponse>>(resultJson);
            responses.Add(response);
        }

        //=======================================
        // イイネ数降順のステージ情報受信処理
        UnityWebRequest request2 = UnityWebRequest.Get(API_BASE_URL + "stages/create/good");
        yield return request2.SendWebRequest();

        if (request2.result == UnityWebRequest.Result.Success
            && request2.responseCode == 200)
        {
            string resultJson = request2.downloadHandler.text;
            List<CreateStageInfoResponse> response = JsonConvert.DeserializeObject<List<CreateStageInfoResponse>>(resultJson);
            responses.Add(response);
        }

        //=======================================
        // フォローの共有ステージ情報受信処理
        UnityWebRequest request3 = UnityWebRequest.Get(API_BASE_URL + "stages/create/follow/" + userID.ToString());
        yield return request3.SendWebRequest();

        if (request3.result == UnityWebRequest.Result.Success
            && request3.responseCode == 200)
        {
            string resultJson = request3.downloadHandler.text;
            List<CreateStageInfoResponse> response = JsonConvert.DeserializeObject<List<CreateStageInfoResponse>>(resultJson);
            responses.Add(response);
        }

        // 呼び出し元のresult処理を呼び出す
        result?.Invoke(responses);
    }

    /// <summary>
    /// プロフィール情報取得
    /// </summary>
    /// <param name="result"></param>
    /// <returns></returns>
    public IEnumerator GetProfileInfo(Action<ProfileInfoResponse> result)
    {
        ProfileInfoResponse response = new ProfileInfoResponse();

        // リクエスト送信処理
        UnityWebRequest request = UnityWebRequest.Get(API_BASE_URL + "users/summary/" + userID.ToString());
        yield return request.SendWebRequest();  // 結果を受信するまで待機

        if (request.result == UnityWebRequest.Result.Success
            && request.responseCode == 200)
        {   // 通信が成功した時

            string resultJson = request.downloadHandler.text;   // レスポンスボディ(json)の文字列を取得
            response = JsonConvert.DeserializeObject<ProfileInfoResponse>(resultJson);
        }

        // 呼び出し元のresult処理を呼び出す
        result?.Invoke(response);
    }

    /// <summary>
    /// フォロー情報取得
    /// </summary>
    /// <returns></returns>
    public IEnumerator GetFollow(Action<FollowResponse> result)
    {
        FollowResponse responses = new FollowResponse();

        // リクエスト送信処理
        UnityWebRequest request = UnityWebRequest.Get(API_BASE_URL + "users/follows/" + userID.ToString());
        yield return request.SendWebRequest();  // 結果を受信するまで待機

        if (request.result == UnityWebRequest.Result.Success
            && request.responseCode == 200)
        {   // 通信が成功した時

            string resultJson = request.downloadHandler.text;   // レスポンスボディ(json)の文字列を取得
            responses = JsonConvert.DeserializeObject<FollowResponse>(resultJson);
        }

        // 呼び出し元のresult処理を呼び出す
        result?.Invoke(responses);
    }

    /// <summary>
    /// ランダムなユーザーデータを20件取得
    /// </summary>
    /// <param name="result"></param>
    /// <returns></returns>
    public IEnumerator GetRandom(Action<List<FollowInfo>> result)
    {
        List<FollowInfo> responses = new List<FollowInfo>();

        // リクエスト送信処理
        UnityWebRequest request = UnityWebRequest.Get(API_BASE_URL + "users/random/" + userID.ToString());
        yield return request.SendWebRequest();  // 結果を受信するまで待機

        if (request.result == UnityWebRequest.Result.Success
            && request.responseCode == 200)
        {   // 通信が成功した時

            string resultJson = request.downloadHandler.text;   // レスポンスボディ(json)の文字列を取得
            responses = JsonConvert.DeserializeObject<List<FollowInfo>>(resultJson);
        }

        // 呼び出し元のresult処理を呼び出す
        result?.Invoke(responses);
    }

    //=============================
    // POST処理

    /// <summary>
    /// ユーザー登録処理
    /// </summary>
    /// <param name="name">ユーザー名</param>
    /// <param name="result">通信完了辞に呼び出す関数</param>
    /// <returns></returns>
    public IEnumerator StoreUser(string name, Action<bool> result)
    {
        // サーバーに送信するオブジェクトを作成
        StoreUserRepuest repuestData = new StoreUserRepuest();

        repuestData.Name = name;    // 名前を代入

        // サーバーに送信するオブジェクトをJSONに変換
        string json = JsonConvert.SerializeObject(repuestData);

        // リクエスト送信処理
        UnityWebRequest request = UnityWebRequest.Post(API_BASE_URL + "users/store", json, "application/json");
        yield return request.SendWebRequest();  // 結果を受信するまで待機

        bool isSuccess = false; // 受信結果

        if (request.result == UnityWebRequest.Result.Success
            && request.responseCode == 200)
        {
            // 通信が成功した場合、帰ってきたJSONをオブジェクトに変換
            string resultJson = request.downloadHandler.text;   // レスポンスボディ(json)の文字列を取得
            StoreUserResponse response = JsonConvert.DeserializeObject<StoreUserResponse>(resultJson);  // JSONデシリアライズ

            // ファイルにユーザーデータを保存
            this.userName = name;
            this.userID = response.UserID;
            SaveUserData();
            isSuccess = true;
        }

        // 呼び出し元のresult処理を呼び出す
        result?.Invoke(isSuccess);
    }

    /// <summary>
    /// クリエイトステージ登録処理
    /// </summary>
    /// <param name="name">ステージ名</param>
    /// <param name="gimmickData">ギミックデータ</param>
    /// <param name="result"></param>
    /// <returns></returns>
    public IEnumerator StoreCreateStage(string name, string gimmickData, Action<bool> result)
    {
        // サーバーに送信するオブジェクトを作成
        CreateStageRequest repuestData = new CreateStageRequest();
        repuestData.Name = name;                // ステージ名
        repuestData.UserID = this.userID;       // ユーザーID
        repuestData.GimmickPos = gimmickData;   // ステージデータ

        // サーバーに送信するオブジェクトをJSONに変換
        string json = JsonConvert.SerializeObject(repuestData);

        // リクエスト送信処理
        UnityWebRequest request = UnityWebRequest.Post(API_BASE_URL + "stages/create/store", json, "application/json");
        yield return request.SendWebRequest();  // 結果を受信するまで待機

        bool isSuccess = false; // 受信結果

        if (request.result == UnityWebRequest.Result.Success
            && request.responseCode == 200)
        {
            // 通信が成功した場合、帰ってきたJSONをオブジェクトに変換
            string resultJson = request.downloadHandler.text;   // レスポンスボディ(json)の文字列を取得
            isSuccess = true;
        }

        // 呼び出し元のresult処理を呼び出す
        result?.Invoke(isSuccess);
    }

    /// <summary>
    /// 名前変更処理
    /// </summary>
    /// <param name="name">ユーザー名</param>
    /// <param name="result"></param>
    /// <returns></returns>
    public IEnumerator ChangeName(string name, Action<bool> result)
    {
        // 変更内容の保存
        this.userName = name;
        SaveUserData();

        // サーバーに送信するオブジェクトを作成
        NameChangeRequest repuestData = new NameChangeRequest();
        repuestData.Name = name;                // ステージ名
        repuestData.UserID = this.userID;       // ユーザーID

        // サーバーに送信するオブジェクトをJSONに変換
        string json = JsonConvert.SerializeObject(repuestData);

        // リクエスト送信処理
        UnityWebRequest request = UnityWebRequest.Post(API_BASE_URL + "users/update", json, "application/json");
        yield return request.SendWebRequest();  // 結果を受信するまで待機

        bool isSuccess = false; // 受信結果

        if (request.result == UnityWebRequest.Result.Success
            && request.responseCode == 200)
        {
            // 通信が成功した場合、帰ってきたJSONをオブジェクトに変換
            string resultJson = request.downloadHandler.text;   // レスポンスボディ(json)の文字列を取得
            isSuccess = true;
        }

        // 呼び出し元のresult処理を呼び出す
        result?.Invoke(isSuccess);
    }

    /// <summary>
    /// アイコン変更処理
    /// </summary>
    /// <param name="name">アイコンid</param>
    /// <param name="result"></param>
    /// <returns></returns>
    public IEnumerator ChangeIcon(int id, Action<bool> result)
    {
        // サーバーに送信するオブジェクトを作成
        IconChangeRequest repuestData = new IconChangeRequest();
        repuestData.UserID = this.userID;       // ユーザーID
        repuestData.IconID = id;                // アイコンID

        // サーバーに送信するオブジェクトをJSONに変換
        string json = JsonConvert.SerializeObject(repuestData);

        // リクエスト送信処理
        UnityWebRequest request = UnityWebRequest.Post(API_BASE_URL + "users/update", json, "application/json");
        yield return request.SendWebRequest();  // 結果を受信するまで待機

        bool isSuccess = false; // 受信結果

        if (request.result == UnityWebRequest.Result.Success
            && request.responseCode == 200)
        {
            // 通信が成功した場合、帰ってきたJSONをオブジェクトに変換
            string resultJson = request.downloadHandler.text;   // レスポンスボディ(json)の文字列を取得
            isSuccess = true;
        }

        // 呼び出し元のresult処理を呼び出す
        result?.Invoke(isSuccess);
    }

    /// <summary>
    /// イイネ更新ボタン
    /// </summary>
    /// <param name="result"></param>
    /// <returns></returns>
    public IEnumerator UpdateGood(int id,int goodVol,Action<bool> result)
    {
        // サーバーに送信するオブジェクトを作成
        UpdateGoodRequest repuestData = new UpdateGoodRequest();
        repuestData.ID = id;            // ステージID取得
        repuestData.GoodVol = goodVol;  // イイネ数取得

        // サーバーに送信するオブジェクトをJSONに変換
        string json = JsonConvert.SerializeObject(repuestData);

        // リクエスト送信処理
        UnityWebRequest request = UnityWebRequest.Post(API_BASE_URL + "stages/update/good", json, "application/json");
        yield return request.SendWebRequest();  // 結果を受信するまで待機

        bool isSuccess = false; // 受信結果

        if (request.result == UnityWebRequest.Result.Success
            && request.responseCode == 200)
        {
            // 通信が成功した場合、帰ってきたJSONをオブジェクトに変換
            string resultJson = request.downloadHandler.text;   // レスポンスボディ(json)の文字列を取得
            isSuccess = true;
        }

        // 呼び出し元のresult処理を呼び出す
        result?.Invoke(isSuccess);
    }

    /// <summary>
    /// ステージ共有処理
    /// </summary>
    /// <param name="stageID">共有ステージID</param>
    /// <param name="result"></param>
    /// <returns></returns>
    public IEnumerator ShereStage(int stageID, Action<bool> result)
    {
        // サーバーに送信するオブジェクトを作成
        ShareStageRequest repuestData = new ShareStageRequest();
        repuestData.UserID = userID;    
        repuestData.StageID = stageID;  // ステージID取得

        // サーバーに送信するオブジェクトをJSONに変換
        string json = JsonConvert.SerializeObject(repuestData);

        // リクエスト送信処理
        UnityWebRequest request = UnityWebRequest.Post(API_BASE_URL + "stages/share", json, "application/json");
        yield return request.SendWebRequest();  // 結果を受信するまで待機

        bool isSuccess = false; // 受信結果

        if (request.result == UnityWebRequest.Result.Success
            && request.responseCode == 200)
        {
            // 通信が成功した場合、帰ってきたJSONをオブジェクトに変換
            string resultJson = request.downloadHandler.text;   // レスポンスボディ(json)の文字列を取得
            isSuccess = true;
        }

        // 呼び出し元のresult処理を呼び出す
        result?.Invoke(isSuccess);
    }

    /// <summary>
    /// フォロー登録処理
    /// </summary>
    /// <param name="followID">フォローユーザーID</param>
    /// <param name="result"></param>
    /// <returns></returns>
    public IEnumerator RegistFollow(int followID, Action<string> result)
    {
        string resultJson = "";

        // サーバーに送信するオブジェクトを作成
        FollowRequest repuestData = new FollowRequest();
        repuestData.UserID = userID;
        repuestData.FollowID = followID;

        // サーバーに送信するオブジェクトをJSONに変換
        string json = JsonConvert.SerializeObject(repuestData);

        // リクエスト送信処理
        UnityWebRequest request = UnityWebRequest.Post(API_BASE_URL + "users/follows/store", json, "application/json");
        yield return request.SendWebRequest();  // 結果を受信するまで待機

        if (request.responseCode == 200 && request.result == UnityWebRequest.Result.Success)
        {   // 通信成功
            resultJson = request.responseCode.ToString();   // レスポンスボディ(json)の文字列を取得
        }
        else if(request.responseCode == 400 && request.result == UnityWebRequest.Result.ProtocolError)
        {   // 既に登録済み
            resultJson = request.responseCode.ToString();
        }
        else if (request.responseCode == 404 && request.result == UnityWebRequest.Result.ProtocolError)
        {   // 指定IDが存在しない
            resultJson = request.responseCode.ToString();
        }

        // 呼び出し元のresult処理を呼び出す
        result?.Invoke(resultJson);
    }

    /// <summary>
    /// フォロー解除処理
    /// </summary>
    /// <param name="followID">フォローユーザーID</param>
    /// <param name="result"></param>
    /// <returns></returns>
    public IEnumerator DestroyFollow(int followID,Action<bool> result) 
    {
        // サーバーに送信するオブジェクトを作成
        FollowRequest repuestData = new FollowRequest();
        repuestData.UserID = userID;
        repuestData.FollowID = followID;

        // サーバーに送信するオブジェクトをJSONに変換
        string json = JsonConvert.SerializeObject(repuestData);

        // リクエスト送信処理
        UnityWebRequest request = UnityWebRequest.Post(API_BASE_URL + "users/follows/destroy", json, "application/json");
        yield return request.SendWebRequest();  // 結果を受信するまで待機

        bool isSuccess = false; // 受信結果

        if (request.result == UnityWebRequest.Result.Success
            && request.responseCode == 200)
        {
            // 通信が成功した場合、帰ってきたJSONをオブジェクトに変換
            string resultJson = request.downloadHandler.text;   // レスポンスボディ(json)の文字列を取得
            isSuccess = true;
        }

        // 呼び出し元のresult処理を呼び出す
        result?.Invoke(isSuccess);
    }

    /// <summary>
    /// プレイログ登録処理
    /// </summary>
    /// <param name="stageID">ステージID</param>
    /// <param name="type">   1:ノーマル 2:クリエイト</param>
    /// <param name="flag">   クリアフラグ</param>
    /// <param name="result"></param>
    /// <returns></returns>
    public IEnumerator StorePlayLog(int stageID,int type,bool flag, Action<bool> result)
    {
        // サーバーに送信するオブジェクトを作成
        PlayLogRequest repuestData = new PlayLogRequest();
        repuestData.UserID = userID;
        repuestData.StageID = stageID;
        repuestData.StageType = type;
        repuestData.ClearFlag = flag;

        // サーバーに送信するオブジェクトをJSONに変換
        string json = JsonConvert.SerializeObject(repuestData);

        // リクエスト送信処理
        UnityWebRequest request = UnityWebRequest.Post(API_BASE_URL + "stages/store/result", json, "application/json");
        yield return request.SendWebRequest();  // 結果を受信するまで待機

        bool isSuccess = false; // 受信結果

        if (request.result == UnityWebRequest.Result.Success
            && request.responseCode == 200)
        {
            // 通信が成功した場合、帰ってきたJSONをオブジェクトに変換
            string resultJson = request.downloadHandler.text;   // レスポンスボディ(json)の文字列を取得
            isSuccess = true;
        }

        // 呼び出し元のresult処理を呼び出す
        result?.Invoke(isSuccess);
    }
}
