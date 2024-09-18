//---------------------------------------------------------------
//
// �l�b�g���[�N�}�l�[�W���[ [ NetWorkManager.cs ]
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
    // �t�B�[���h

    /// <summary>
    /// API�x�[�XURL
    /// </summary>
    const string API_BASE_URL = "https://api-shot.japaneast.cloudapp.azure.com/api/";

    /// <summary>
    /// �v���C���̃��[�U�[ID
    /// </summary>
    private int userID = 0;

    /// <summary>
    /// �v���C���̃��[�U�[��
    /// </summary>
    private string userName = "";

    /// <summary>
    /// �v���C���̃X�e�[�WNo
    /// </summary>
    public int PlayStageNo {  get; set; }

    /// <summary>
    /// �v���C���̃X�e�[�W�^�C�v (1:�m�[�}�� 2:�N���G�C�g)
    /// </summary>
    public int PlayStageType {  get; set; }

    // get�v���p�e�B���Ăяo�������񎞂ɃC���X�^���X��������static�ŕێ�
    private static NetworkManager instance;

    /// <summary>
    /// NetworkManager�v���p�e�B
    /// </summary>
    public static NetworkManager Instance
    {
        get
        {
            if (instance == null)
            {
                // GameObject�𐶐����ANetworkManager��ǉ�
                GameObject gameObject = new GameObject("NetworkManager");
                instance = gameObject.AddComponent<NetworkManager>();

                // �V�[���J�ڂŔj������Ȃ��悤�ɐݒ�
                DontDestroyOnLoad(gameObject);
            }

            return instance;
        }
    }

    //-------------------------------------------------------------------
    // ���\�b�h

    /// <summary>
    /// ���[�U�[ID�擾
    /// </summary>
    /// <returns></returns>
    public int GetUserID()
    {
        return userID;
    }

    /// <summary>
    /// ���[�U�[���擾
    /// </summary>
    /// <returns>���[�U�[��</returns>
    public string GetUserName()
    {
        return userName;
    }

    /// <summary>
    /// ���[�U�[�f�[�^�ۑ�����
    /// </summary>
    private void SaveUserData()
    {
        // �Z�[�u�f�[�^�N���X�̐���
        SaveData saveData = new SaveData();
        saveData.UserName = this.userName;
        saveData.UserID = this.userID;

        // �f�[�^��JSON�V���A���C�Y
        string json = JsonConvert.SerializeObject(saveData);

        // �w�肵����΃p�X��"saveData.json"��ۑ�
        var writer = new StreamWriter(Application.persistentDataPath + "/saveData.json");
        writer.Write(json); // �����o��
        writer.Flush();     // �o�b�t�@�Ɏc���Ă���l��S�ď����o��
        writer.Close();     // �t�@�C����
    }

    /// <summary>
    /// ���[�U�[�f�[�^�ǂݍ��ݏ���
    /// </summary>
    /// <returns></returns>
    public bool LoadUserData()
    {
        if(!File.Exists(Application.persistentDataPath + "/saveData.json"))
        {   // �w��̃p�X�̃t�@�C�������݂��Ȃ��������A�������^�[��
            return false;
        }

        //  ���[�J���t�@�C�����烆�[�U�[�f�[�^�̓Ǎ�����
        var reader = new StreamReader(Application.persistentDataPath + "/saveData.json");
        string json = reader.ReadToEnd();
        reader.Close();

        // �Z�[�u�f�[�^JSON���f�V���A���C�Y���Ď擾
        SaveData saveData = JsonConvert.DeserializeObject<SaveData>(json);
        this.userID = saveData.UserID;
        this.userName = saveData.UserName;

        // �ǂݍ��݌��ʂ����^�[��
        return true;
    }

    //=============================
    // GET����

    /// <summary>
    /// �m�[�}���X�e�[�W�擾����
    /// </summary>
    /// <param name="result"></param>
    /// <returns></returns>
    public IEnumerator GetNormalStage(Action<List<NormalStageResponse>> result)
    {
        // ���N�G�X�g���M����
        UnityWebRequest request = UnityWebRequest.Get(API_BASE_URL + "stages/normal");
        yield return request.SendWebRequest();  // ���ʂ���M����܂őҋ@

        // ��M���i�[�p
        List<NormalStageResponse> response = new List<NormalStageResponse>();

        if (request.result == UnityWebRequest.Result.Success
            && request.responseCode == 200)
        {   // �ʐM������������

            string resultJson = request.downloadHandler.text;   // ���X�|���X�{�f�B(json)�̕�������擾
            response = JsonConvert.DeserializeObject<List<NormalStageResponse>>(resultJson);  // JSON�f�V���A���C�Y
        }

        // �Ăяo������result�������Ăяo��
        result?.Invoke(response);
    }

    /// <summary>
    /// �w��ID�̃M�~�b�N�����擾
    /// </summary>
    /// <param name="id">�X�e�[�WID</param>
    /// <param name="result"></param>
    /// <returns></returns>
    public IEnumerator GetIDCreate(int id, Action<CreateStageResponse> result)
    {
        // ���N�G�X�g���M����
        UnityWebRequest request = UnityWebRequest.Get(API_BASE_URL + "stages/create/" + id.ToString());
        yield return request.SendWebRequest();  // ���ʂ���M����܂őҋ@

        // ��M���i�[�p
        CreateStageResponse response = new CreateStageResponse();

        if (request.result == UnityWebRequest.Result.Success
            && request.responseCode == 200)
        {   // �ʐM������������

            string resultJson = request.downloadHandler.text;   // ���X�|���X�{�f�B(json)�̕�������擾
            response = JsonConvert.DeserializeObject<CreateStageResponse>(resultJson);  // JSON�f�V���A���C�Y
        }

        // �Ăяo������result�������Ăяo��
        result?.Invoke(response);
    }

    /// <summary>
    /// ����X�e�[�W�擾����
    /// </summary>
    /// <param name="userID">�����̃��[�U�[ID</param>
    /// <param name="result"></param>
    /// <returns></returns>
    public IEnumerator GetPlayerCreateStage(Action<List<CreateStageInfoResponse>> result)
    {
        // ���N�G�X�g���M����
        UnityWebRequest request = UnityWebRequest.Get(API_BASE_URL + "stages/create/user/" + userID.ToString());
        yield return request.SendWebRequest();  // ���ʂ���M����܂őҋ@

        // ��M���i�[�p
        List<CreateStageInfoResponse> response = new List<CreateStageInfoResponse>();

        if (request.result == UnityWebRequest.Result.Success
            && request.responseCode == 200)
        {   // �ʐM������������

            string resultJson = request.downloadHandler.text;   // ���X�|���X�{�f�B(json)�̕�������擾
            response = JsonConvert.DeserializeObject<List<CreateStageInfoResponse>>(resultJson);  // JSON�f�V���A���C�Y
        }

        // �Ăяo������result�������Ăяo��
        result?.Invoke(response);
    }

    /// <summary>
    /// �J�X�^���v���C���擾����
    /// </summary>
    /// <param name="result"></param>
    /// <returns></returns>
    public IEnumerator GetCustomList(Action<List<List<CreateStageInfoResponse>>> result)
    {
        List<List<CreateStageInfoResponse>> responses = new List<List<CreateStageInfoResponse>>();

        //===================================
        // �t�H���[�̃X�e�[�W����M����

        // ���N�G�X�g���M����
        UnityWebRequest request1 = UnityWebRequest.Get(API_BASE_URL + "stages/create/follow/" + userID.ToString());
        yield return request1.SendWebRequest();  // ���ʂ���M����܂őҋ@

        if (request1.result == UnityWebRequest.Result.Success
            && request1.responseCode == 200)
        {   // �ʐM������������

            string resultJson = request1.downloadHandler.text;   // ���X�|���X�{�f�B(json)�̕�������擾
            List<CreateStageInfoResponse> response = JsonConvert.DeserializeObject<List<CreateStageInfoResponse>>(resultJson);
            responses.Add(response);
        }

        //=======================================
        // �C�C�l���~���̃X�e�[�W����M����
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
        // �t�H���[�̋��L�X�e�[�W����M����
        UnityWebRequest request3 = UnityWebRequest.Get(API_BASE_URL + "stages/create/follow/" + userID.ToString());
        yield return request3.SendWebRequest();

        if (request3.result == UnityWebRequest.Result.Success
            && request3.responseCode == 200)
        {
            string resultJson = request3.downloadHandler.text;
            List<CreateStageInfoResponse> response = JsonConvert.DeserializeObject<List<CreateStageInfoResponse>>(resultJson);
            responses.Add(response);
        }

        // �Ăяo������result�������Ăяo��
        result?.Invoke(responses);
    }

    /// <summary>
    /// �v���t�B�[�����擾
    /// </summary>
    /// <param name="result"></param>
    /// <returns></returns>
    public IEnumerator GetProfileInfo(Action<ProfileInfoResponse> result)
    {
        ProfileInfoResponse response = new ProfileInfoResponse();

        // ���N�G�X�g���M����
        UnityWebRequest request = UnityWebRequest.Get(API_BASE_URL + "users/summary/" + userID.ToString());
        yield return request.SendWebRequest();  // ���ʂ���M����܂őҋ@

        if (request.result == UnityWebRequest.Result.Success
            && request.responseCode == 200)
        {   // �ʐM������������

            string resultJson = request.downloadHandler.text;   // ���X�|���X�{�f�B(json)�̕�������擾
            response = JsonConvert.DeserializeObject<ProfileInfoResponse>(resultJson);
        }

        // �Ăяo������result�������Ăяo��
        result?.Invoke(response);
    }

    /// <summary>
    /// �t�H���[���擾
    /// </summary>
    /// <returns></returns>
    public IEnumerator GetFollow(Action<FollowResponse> result)
    {
        FollowResponse responses = new FollowResponse();

        // ���N�G�X�g���M����
        UnityWebRequest request = UnityWebRequest.Get(API_BASE_URL + "users/follows/" + userID.ToString());
        yield return request.SendWebRequest();  // ���ʂ���M����܂őҋ@

        if (request.result == UnityWebRequest.Result.Success
            && request.responseCode == 200)
        {   // �ʐM������������

            string resultJson = request.downloadHandler.text;   // ���X�|���X�{�f�B(json)�̕�������擾
            responses = JsonConvert.DeserializeObject<FollowResponse>(resultJson);
        }

        // �Ăяo������result�������Ăяo��
        result?.Invoke(responses);
    }

    /// <summary>
    /// �����_���ȃ��[�U�[�f�[�^��20���擾
    /// </summary>
    /// <param name="result"></param>
    /// <returns></returns>
    public IEnumerator GetRandom(Action<List<FollowInfo>> result)
    {
        List<FollowInfo> responses = new List<FollowInfo>();

        // ���N�G�X�g���M����
        UnityWebRequest request = UnityWebRequest.Get(API_BASE_URL + "users/random/" + userID.ToString());
        yield return request.SendWebRequest();  // ���ʂ���M����܂őҋ@

        if (request.result == UnityWebRequest.Result.Success
            && request.responseCode == 200)
        {   // �ʐM������������

            string resultJson = request.downloadHandler.text;   // ���X�|���X�{�f�B(json)�̕�������擾
            responses = JsonConvert.DeserializeObject<List<FollowInfo>>(resultJson);
        }

        // �Ăяo������result�������Ăяo��
        result?.Invoke(responses);
    }

    //=============================
    // POST����

    /// <summary>
    /// ���[�U�[�o�^����
    /// </summary>
    /// <param name="name">���[�U�[��</param>
    /// <param name="result">�ʐM�������ɌĂяo���֐�</param>
    /// <returns></returns>
    public IEnumerator StoreUser(string name, Action<bool> result)
    {
        // �T�[�o�[�ɑ��M����I�u�W�F�N�g���쐬
        StoreUserRepuest repuestData = new StoreUserRepuest();

        repuestData.Name = name;    // ���O����

        // �T�[�o�[�ɑ��M����I�u�W�F�N�g��JSON�ɕϊ�
        string json = JsonConvert.SerializeObject(repuestData);

        // ���N�G�X�g���M����
        UnityWebRequest request = UnityWebRequest.Post(API_BASE_URL + "users/store", json, "application/json");
        yield return request.SendWebRequest();  // ���ʂ���M����܂őҋ@

        bool isSuccess = false; // ��M����

        if (request.result == UnityWebRequest.Result.Success
            && request.responseCode == 200)
        {
            // �ʐM�����������ꍇ�A�A���Ă���JSON���I�u�W�F�N�g�ɕϊ�
            string resultJson = request.downloadHandler.text;   // ���X�|���X�{�f�B(json)�̕�������擾
            StoreUserResponse response = JsonConvert.DeserializeObject<StoreUserResponse>(resultJson);  // JSON�f�V���A���C�Y

            // �t�@�C���Ƀ��[�U�[�f�[�^��ۑ�
            this.userName = name;
            this.userID = response.UserID;
            SaveUserData();
            isSuccess = true;
        }

        // �Ăяo������result�������Ăяo��
        result?.Invoke(isSuccess);
    }

    /// <summary>
    /// �N���G�C�g�X�e�[�W�o�^����
    /// </summary>
    /// <param name="name">�X�e�[�W��</param>
    /// <param name="gimmickData">�M�~�b�N�f�[�^</param>
    /// <param name="result"></param>
    /// <returns></returns>
    public IEnumerator StoreCreateStage(string name, string gimmickData, Action<bool> result)
    {
        // �T�[�o�[�ɑ��M����I�u�W�F�N�g���쐬
        CreateStageRequest repuestData = new CreateStageRequest();
        repuestData.Name = name;                // �X�e�[�W��
        repuestData.UserID = this.userID;       // ���[�U�[ID
        repuestData.GimmickPos = gimmickData;   // �X�e�[�W�f�[�^

        // �T�[�o�[�ɑ��M����I�u�W�F�N�g��JSON�ɕϊ�
        string json = JsonConvert.SerializeObject(repuestData);

        // ���N�G�X�g���M����
        UnityWebRequest request = UnityWebRequest.Post(API_BASE_URL + "stages/create/store", json, "application/json");
        yield return request.SendWebRequest();  // ���ʂ���M����܂őҋ@

        bool isSuccess = false; // ��M����

        if (request.result == UnityWebRequest.Result.Success
            && request.responseCode == 200)
        {
            // �ʐM�����������ꍇ�A�A���Ă���JSON���I�u�W�F�N�g�ɕϊ�
            string resultJson = request.downloadHandler.text;   // ���X�|���X�{�f�B(json)�̕�������擾
            isSuccess = true;
        }

        // �Ăяo������result�������Ăяo��
        result?.Invoke(isSuccess);
    }

    /// <summary>
    /// ���O�ύX����
    /// </summary>
    /// <param name="name">���[�U�[��</param>
    /// <param name="result"></param>
    /// <returns></returns>
    public IEnumerator ChangeName(string name, Action<bool> result)
    {
        // �ύX���e�̕ۑ�
        this.userName = name;
        SaveUserData();

        // �T�[�o�[�ɑ��M����I�u�W�F�N�g���쐬
        NameChangeRequest repuestData = new NameChangeRequest();
        repuestData.Name = name;                // �X�e�[�W��
        repuestData.UserID = this.userID;       // ���[�U�[ID

        // �T�[�o�[�ɑ��M����I�u�W�F�N�g��JSON�ɕϊ�
        string json = JsonConvert.SerializeObject(repuestData);

        // ���N�G�X�g���M����
        UnityWebRequest request = UnityWebRequest.Post(API_BASE_URL + "users/update", json, "application/json");
        yield return request.SendWebRequest();  // ���ʂ���M����܂őҋ@

        bool isSuccess = false; // ��M����

        if (request.result == UnityWebRequest.Result.Success
            && request.responseCode == 200)
        {
            // �ʐM�����������ꍇ�A�A���Ă���JSON���I�u�W�F�N�g�ɕϊ�
            string resultJson = request.downloadHandler.text;   // ���X�|���X�{�f�B(json)�̕�������擾
            isSuccess = true;
        }

        // �Ăяo������result�������Ăяo��
        result?.Invoke(isSuccess);
    }

    /// <summary>
    /// �A�C�R���ύX����
    /// </summary>
    /// <param name="name">�A�C�R��id</param>
    /// <param name="result"></param>
    /// <returns></returns>
    public IEnumerator ChangeIcon(int id, Action<bool> result)
    {
        // �T�[�o�[�ɑ��M����I�u�W�F�N�g���쐬
        IconChangeRequest repuestData = new IconChangeRequest();
        repuestData.UserID = this.userID;       // ���[�U�[ID
        repuestData.IconID = id;                // �A�C�R��ID

        // �T�[�o�[�ɑ��M����I�u�W�F�N�g��JSON�ɕϊ�
        string json = JsonConvert.SerializeObject(repuestData);

        // ���N�G�X�g���M����
        UnityWebRequest request = UnityWebRequest.Post(API_BASE_URL + "users/update", json, "application/json");
        yield return request.SendWebRequest();  // ���ʂ���M����܂őҋ@

        bool isSuccess = false; // ��M����

        if (request.result == UnityWebRequest.Result.Success
            && request.responseCode == 200)
        {
            // �ʐM�����������ꍇ�A�A���Ă���JSON���I�u�W�F�N�g�ɕϊ�
            string resultJson = request.downloadHandler.text;   // ���X�|���X�{�f�B(json)�̕�������擾
            isSuccess = true;
        }

        // �Ăяo������result�������Ăяo��
        result?.Invoke(isSuccess);
    }

    /// <summary>
    /// �C�C�l�X�V�{�^��
    /// </summary>
    /// <param name="result"></param>
    /// <returns></returns>
    public IEnumerator UpdateGood(int id,int goodVol,Action<bool> result)
    {
        // �T�[�o�[�ɑ��M����I�u�W�F�N�g���쐬
        UpdateGoodRequest repuestData = new UpdateGoodRequest();
        repuestData.ID = id;            // �X�e�[�WID�擾
        repuestData.GoodVol = goodVol;  // �C�C�l���擾

        // �T�[�o�[�ɑ��M����I�u�W�F�N�g��JSON�ɕϊ�
        string json = JsonConvert.SerializeObject(repuestData);

        // ���N�G�X�g���M����
        UnityWebRequest request = UnityWebRequest.Post(API_BASE_URL + "stages/update/good", json, "application/json");
        yield return request.SendWebRequest();  // ���ʂ���M����܂őҋ@

        bool isSuccess = false; // ��M����

        if (request.result == UnityWebRequest.Result.Success
            && request.responseCode == 200)
        {
            // �ʐM�����������ꍇ�A�A���Ă���JSON���I�u�W�F�N�g�ɕϊ�
            string resultJson = request.downloadHandler.text;   // ���X�|���X�{�f�B(json)�̕�������擾
            isSuccess = true;
        }

        // �Ăяo������result�������Ăяo��
        result?.Invoke(isSuccess);
    }

    /// <summary>
    /// �X�e�[�W���L����
    /// </summary>
    /// <param name="stageID">���L�X�e�[�WID</param>
    /// <param name="result"></param>
    /// <returns></returns>
    public IEnumerator ShereStage(int stageID, Action<bool> result)
    {
        // �T�[�o�[�ɑ��M����I�u�W�F�N�g���쐬
        ShareStageRequest repuestData = new ShareStageRequest();
        repuestData.UserID = userID;    
        repuestData.StageID = stageID;  // �X�e�[�WID�擾

        // �T�[�o�[�ɑ��M����I�u�W�F�N�g��JSON�ɕϊ�
        string json = JsonConvert.SerializeObject(repuestData);

        // ���N�G�X�g���M����
        UnityWebRequest request = UnityWebRequest.Post(API_BASE_URL + "stages/share", json, "application/json");
        yield return request.SendWebRequest();  // ���ʂ���M����܂őҋ@

        bool isSuccess = false; // ��M����

        if (request.result == UnityWebRequest.Result.Success
            && request.responseCode == 200)
        {
            // �ʐM�����������ꍇ�A�A���Ă���JSON���I�u�W�F�N�g�ɕϊ�
            string resultJson = request.downloadHandler.text;   // ���X�|���X�{�f�B(json)�̕�������擾
            isSuccess = true;
        }

        // �Ăяo������result�������Ăяo��
        result?.Invoke(isSuccess);
    }

    /// <summary>
    /// �t�H���[�o�^����
    /// </summary>
    /// <param name="followID">�t�H���[���[�U�[ID</param>
    /// <param name="result"></param>
    /// <returns></returns>
    public IEnumerator RegistFollow(int followID, Action<string> result)
    {
        string resultJson = "";

        // �T�[�o�[�ɑ��M����I�u�W�F�N�g���쐬
        FollowRequest repuestData = new FollowRequest();
        repuestData.UserID = userID;
        repuestData.FollowID = followID;

        // �T�[�o�[�ɑ��M����I�u�W�F�N�g��JSON�ɕϊ�
        string json = JsonConvert.SerializeObject(repuestData);

        // ���N�G�X�g���M����
        UnityWebRequest request = UnityWebRequest.Post(API_BASE_URL + "users/follows/store", json, "application/json");
        yield return request.SendWebRequest();  // ���ʂ���M����܂őҋ@

        if (request.responseCode == 200 && request.result == UnityWebRequest.Result.Success)
        {   // �ʐM����
            resultJson = request.responseCode.ToString();   // ���X�|���X�{�f�B(json)�̕�������擾
        }
        else if(request.responseCode == 400 && request.result == UnityWebRequest.Result.ProtocolError)
        {   // ���ɓo�^�ς�
            resultJson = request.responseCode.ToString();
        }
        else if (request.responseCode == 404 && request.result == UnityWebRequest.Result.ProtocolError)
        {   // �w��ID�����݂��Ȃ�
            resultJson = request.responseCode.ToString();
        }

        // �Ăяo������result�������Ăяo��
        result?.Invoke(resultJson);
    }

    /// <summary>
    /// �t�H���[��������
    /// </summary>
    /// <param name="followID">�t�H���[���[�U�[ID</param>
    /// <param name="result"></param>
    /// <returns></returns>
    public IEnumerator DestroyFollow(int followID,Action<bool> result) 
    {
        // �T�[�o�[�ɑ��M����I�u�W�F�N�g���쐬
        FollowRequest repuestData = new FollowRequest();
        repuestData.UserID = userID;
        repuestData.FollowID = followID;

        // �T�[�o�[�ɑ��M����I�u�W�F�N�g��JSON�ɕϊ�
        string json = JsonConvert.SerializeObject(repuestData);

        // ���N�G�X�g���M����
        UnityWebRequest request = UnityWebRequest.Post(API_BASE_URL + "users/follows/destroy", json, "application/json");
        yield return request.SendWebRequest();  // ���ʂ���M����܂őҋ@

        bool isSuccess = false; // ��M����

        if (request.result == UnityWebRequest.Result.Success
            && request.responseCode == 200)
        {
            // �ʐM�����������ꍇ�A�A���Ă���JSON���I�u�W�F�N�g�ɕϊ�
            string resultJson = request.downloadHandler.text;   // ���X�|���X�{�f�B(json)�̕�������擾
            isSuccess = true;
        }

        // �Ăяo������result�������Ăяo��
        result?.Invoke(isSuccess);
    }

    /// <summary>
    /// �v���C���O�o�^����
    /// </summary>
    /// <param name="stageID">�X�e�[�WID</param>
    /// <param name="type">   1:�m�[�}�� 2:�N���G�C�g</param>
    /// <param name="flag">   �N���A�t���O</param>
    /// <param name="result"></param>
    /// <returns></returns>
    public IEnumerator StorePlayLog(int stageID,int type,bool flag, Action<bool> result)
    {
        // �T�[�o�[�ɑ��M����I�u�W�F�N�g���쐬
        PlayLogRequest repuestData = new PlayLogRequest();
        repuestData.UserID = userID;
        repuestData.StageID = stageID;
        repuestData.StageType = type;
        repuestData.ClearFlag = flag;

        // �T�[�o�[�ɑ��M����I�u�W�F�N�g��JSON�ɕϊ�
        string json = JsonConvert.SerializeObject(repuestData);

        // ���N�G�X�g���M����
        UnityWebRequest request = UnityWebRequest.Post(API_BASE_URL + "stages/store/result", json, "application/json");
        yield return request.SendWebRequest();  // ���ʂ���M����܂őҋ@

        bool isSuccess = false; // ��M����

        if (request.result == UnityWebRequest.Result.Success
            && request.responseCode == 200)
        {
            // �ʐM�����������ꍇ�A�A���Ă���JSON���I�u�W�F�N�g�ɕϊ�
            string resultJson = request.downloadHandler.text;   // ���X�|���X�{�f�B(json)�̕�������擾
            isSuccess = true;
        }

        // �Ăяo������result�������Ăяo��
        result?.Invoke(isSuccess);
    }
}
