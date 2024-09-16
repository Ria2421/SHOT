//---------------------------------------------------------------
//
// ホームマネージャー [ HomeManager.cs ]
// Author:Kenta Nakamoto
// Data:2024/08/01
// Update:2024/09/13
//
//---------------------------------------------------------------
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeManager : MonoBehaviour
{
    //-------------------------------------------
    // フィールド

    /// <summary>
    /// ネットワークマネージャー
    /// </summary>
    private NetworkManager networkManager;

    /// <summary>
    /// アイコンID
    /// </summary>
    private int iconID = 0;

    [Header(" ホーム画面関連 ")]

    /// <summary>
    /// メニュー表示ボタン
    /// </summary>
    [SerializeField] private GameObject upManuButton;

    /// <summary>
    /// メニュー非表示ボタン
    /// </summary>
    [SerializeField] private GameObject downManuButton;

    /// <summary>
    /// メニュー画面
    /// </summary>
    [SerializeField] private GameObject menuPanel;

    /// <summary>
    /// アチーブメント画面
    /// </summary>
    [SerializeField] private GameObject achievementPanel;

    /// <summary>
    /// フォローパネル
    /// </summary>
    [SerializeField] private GameObject followPanel;

    [Header(" アカウント画面関連 ")]

    /// <summary>
    /// ユーザー名入力欄
    /// </summary>
    [SerializeField] private InputField nameInput;

    /// <summary>
    /// プロフィール情報テキスト
    /// [0:ユーザー名 1:総プレイ数 2:クリア数 3:作成ステージ数 4:フォロー数 5:フォロワー数]
    /// </summary>
    [SerializeField] private List<Text> contentTexts;

    /// <summary>
    /// 完了通知位置情報
    /// </summary>
    [SerializeField] private RectTransform changeComplete;

    /// <summary>
    /// 失敗通知位置情報
    /// </summary>
    [SerializeField] private RectTransform changeFailed;

    /// <summary>
    /// 名前入力欄
    /// </summary>
    [SerializeField] private GameObject namePanel;

    /// <summary>
    /// アイコン一覧パネル
    /// </summary>
    [SerializeField] private GameObject iconPanel;

    /// <summary>
    /// アイコン集
    /// </summary>
    [SerializeField] private List<Sprite> iconSprite;

    /// <summary>
    /// アイコン画像
    /// </summary>
    [SerializeField] private Image iconImage;

    [Header(" フォロー画面関連 ")]

    /// <summary>
    /// ユーザー情報プレハブ
    /// </summary>
    [SerializeField] private GameObject userInfoPrefab;

    /// <summary>
    /// ノーデータプレハブ
    /// </summary>
    [SerializeField] private GameObject noDataPrefab;

    /// <summary>
    /// リスト切り替えボタン
    /// </summary>
    [SerializeField] private List<GameObject> listButtons;

    /// <summary>
    /// 切り替えボタンImageリスト
    /// </summary>
    [SerializeField] private List<Image> buttonColors;

    /// <summary>
    /// スクロールリスト    [0:フォロー 1:フォロワー 2:相互]
    /// </summary>
    [SerializeField] private List<GameObject> scrollList;

    /// <summary>
    /// スクロールコンテンツ
    /// </summary>
    [SerializeField] private List<GameObject> scrolltContents;

    //--------------------------------------------
    // メソッド

    /// <summary>
    /// 初期処理
    /// </summary>
    void Start()
    {
        // ネットワークマネージャー取得
        networkManager = NetworkManager.Instance;

        // ユーザ名の取得・反映
        contentTexts[0].text = networkManager.GetUserName();

        // ユーザーデータが保存されていない場合は登録
        StartCoroutine(NetworkManager.Instance.GetProfileInfo(
            result =>
            {   // 情報反映
                iconID = result.IconID;                                      // アイコンID取得 
                iconImage.sprite = iconSprite[iconID - 1];                   // アイコン反映
                contentTexts[1].text = result.PlayCnt.ToString() + "回";     // 総プレイ数
                contentTexts[2].text = result.ClearCnt.ToString() + "回";    // クリア数
                contentTexts[3].text = result.CreateCnt.ToString() + "回";   // ステージ作成数
                contentTexts[4].text = result.FollowCnt.ToString() + "回";   // フォロー数
                contentTexts[5].text = result.FollowerCnt.ToString() + "回"; // フォロワー数
            }));
    }

    //======================
    // シーン遷移メソッド

    /// <summary>
    /// ノーマルモード遷移処理
    /// </summary>
    public void TransNormalMode()
    {
        /* フェード処理 (黒)  
                        ( "シーン名",フェードの色, 速さ);  */
        Initiate.DoneFading();
        Initiate.Fade("StageSelectScene", Color.white, 2.5f);
    }

    /// <summary>
    /// カスタムプレイモード遷移処理
    /// </summary>
    public void TransCustomPlayMode()
    {
        /* フェード処理 (黒)  
                        ( "シーン名",フェードの色, 速さ);  */
        Initiate.DoneFading();
        Initiate.Fade("CustumStageSelect", Color.white, 2.5f);
    }

    /// <summary>
    /// カスタムプレイモード遷移処理
    /// </summary>
    public void TransCreateMode()
    {
        /* フェード処理 (黒)  
                        ( "シーン名",フェードの色, 速さ);  */
        Initiate.DoneFading();
        Initiate.Fade("CreateHomeScene", Color.white, 2.5f);
    }

    //=======================================================
    // メニュー関連メソッド

    /// <summary>
    /// メニュー表示処理
    /// </summary>
    public void PushMenuUpButton()
    {
        // メニュー表示ボタンを非アクティブに
        upManuButton.SetActive(false);

        // メニュー非表示ボタンをアクティブに
        downManuButton.SetActive(true);

        // メニューを表示
        menuPanel.SetActive(true);
    }

    /// <summary>
    /// メニュー非表示処理
    /// </summary>
    public void PushMenuDownButton()
    {
        // メニュー表示ボタンをアクティブに
        upManuButton.SetActive(true);

        // メニュー非表示ボタンを非アクティブに
        downManuButton.SetActive(false);

        // メニューを閉じる
        menuPanel.SetActive(false);
    }

    /// <summary>
    /// メニュー閉じる処理
    /// </summary>
    public void PushCloseButton(GameObject menuPanel)
    {
        menuPanel.SetActive(false);
    }

    /// <summary>
    /// 各アイコン押下処理
    /// </summary>
    public void PushMenuIconButton(GameObject iconPanel)
    {
        iconPanel.SetActive(true);
    }

    /// <summary>
    /// フォローボタン押下時
    /// </summary>
    public void PushFollowButton()
    {
        followPanel.SetActive(true);

        StartCoroutine(NetworkManager.Instance.GetFollow(
            result =>
            {
                foreach(var data in result.Follow)
                {   // フォローリスト
                    if(result.Follow.Count == 0) 
                    {
                        // データ無し表示
                        GameObject noData = Instantiate(noDataPrefab, Vector3.zero, Quaternion.identity, scrolltContents[0].transform);
                        break; 
                    }

                    // ユーザーデータ生成
                    GameObject userData = Instantiate(userInfoPrefab, Vector3.zero, Quaternion.identity, scrolltContents[0].transform);

                    userData.transform.GetChild(0).GetComponent<Image>().sprite = iconSprite[data.IconID - 1];  // アイコン設定
                    userData.transform.GetChild(1).GetComponent<Text>().text = data.Name;   // 名前設定
                    userData.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() =>
                    {   // フォロー解除処理
                        //StartCoroutine(NetworkManager.Instance.GetIDCreate(
                        //    result =>
                        //    {

                        //    }));
                    });
                }

                foreach (var data in result.Follower)
                {   // フォロワーリスト
                    if (result.Follower.Count == 0)
                    {
                        // データ無し表示
                        GameObject noData = Instantiate(noDataPrefab, Vector3.zero, Quaternion.identity, scrolltContents[1].transform);
                        break;
                    }

                    // ユーザーデータ生成
                    GameObject userData = Instantiate(userInfoPrefab, Vector3.zero, Quaternion.identity, scrolltContents[1].transform);

                    userData.transform.GetChild(0).GetComponent<Image>().sprite = iconSprite[data.IconID - 1];  // アイコン設定
                    userData.transform.GetChild(1).GetComponent<Text>().text = data.Name;   // 名前設定
                    userData.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() =>
                    {   // フォロー解除処理
                        //StartCoroutine(NetworkManager.Instance.GetIDCreate(
                        //    result =>
                        //    {

                        //    }));
                    });
                }

                foreach (var data in result.Mutual)
                {   // 相互リスト
                    if (result.Mutual.Count == 0)
                    {
                        // データ無し表示
                        GameObject noData = Instantiate(noDataPrefab, Vector3.zero, Quaternion.identity, scrolltContents[2].transform);
                        break;
                    }

                    // ユーザーデータ生成
                    GameObject userData = Instantiate(userInfoPrefab, Vector3.zero, Quaternion.identity, scrolltContents[2].transform);

                    userData.transform.GetChild(0).GetComponent<Image>().sprite = iconSprite[data.IconID - 1];  // アイコン設定
                    userData.transform.GetChild(1).GetComponent<Text>().text = data.Name;   // 名前設定
                    userData.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() =>
                    {   // フォロー解除処理
                        //StartCoroutine(NetworkManager.Instance.GetIDCreate(
                        //    result =>
                        //    {

                        //    }));
                    });
                }

                Debug.Log("リスト生成完了");

                foreach (GameObject button in listButtons)
                {   // ボタンの有効化
                    button.GetComponent<Button>().interactable = true;
                }

                SetList(0); // フォローリストの有効化
            }));
    }

    /// <summary>
    /// 通知押下処理
    /// </summary>
    /// <param name="gameObject"></param>
    public void PushNoticeButton(RectTransform rectTransform)
    {
        // 初期位置に移動
        rectTransform.anchoredPosition = new Vector2(700.0f, rectTransform.anchoredPosition.y);
    }

    /// <summary>
    /// 通知表示処理
    /// </summary>
    /// <param name="cautionImage">移動対象</param>
    private void MoveCaution(RectTransform cautionImage)
    {
        cautionImage.DOAnchorPos(new Vector2(0f, cautionImage.anchoredPosition.y), 0.6f).SetEase(Ease.OutBack);
    }

    /// <summary>
    /// 名前変更ボタン押下時
    /// </summary>
    public void PushNameChange()
    {
        StartCoroutine(NetworkManager.Instance.ChangeName(
            nameInput.text,
            result =>
            {
                if (result)
                {
                    // 名前の変更処理
                    contentTexts[0].text = nameInput.text;
                    MoveCaution(changeComplete);    // 成功通知
                    namePanel.SetActive(false);     // 入力欄非表示
                }
                else
                {
                    MoveCaution(changeFailed);      // 失敗通知
                    namePanel.SetActive(false);     // 入力欄非表示
                }
            }));
    }

    /// <summary>
    /// アイコン変更押下処理
    /// </summary>
    /// <param name="id">アイコンID</param>
    public void PushIconChange(int id)
    {
        StartCoroutine(NetworkManager.Instance.ChangeIcon(
            id,
            result =>
            {
                if (result)
                {
                    // アイコンの変更処理
                    iconImage.sprite = iconSprite[id - 1];
                    MoveCaution(changeComplete);    // 成功通知
                    iconPanel.SetActive(false);     // 入力欄非表示
                }
                else
                {
                    MoveCaution(changeFailed);      // 失敗通知
                    iconPanel.SetActive(false);     // 入力欄非表示
                }
            }));
    }

    /// <summary>
    /// リスト・ボタンの表示初期化
    /// </summary>
    private void ResetList()
    {
        // ボタン・スクロールウィンドウ初期化
        for (int i = 0; i < listButtons.Count; i++)
        {
            scrollList[i].SetActive(false);         // 全リストを非表示
            buttonColors[i].color = Color.white;    // カラー白
        }
    }

    /// <summary>
    /// 指定Noのリストを表示
    /// </summary>
    /// <param name="num"></param>
    public void SetList(int no)
    {
        ResetList();   // 初期化処理
        scrollList[no].SetActive(true);
        buttonColors[no].color = Color.gray;
    }
}
