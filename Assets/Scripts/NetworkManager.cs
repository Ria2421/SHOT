//---------------------------------------------------------------
//
// �l�b�g���[�N�}�l�[�W���[ [ NetWorkManager.cs ]
// Author:Kenta Nakamoto
// Data:2024/08/26
// Update:2024/08/26
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
}
