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
    /// [0:ユーザーID 1:ユーザー名 2:総プレイ数 3:クリア数 4:作成ステージ数 5:フォロー数 6:フォロワー数]
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

    /// <summary>
    /// 登録画像スプライト
    /// </summary>
    [SerializeField] private Sprite registSprite;

    /// <summary>
    /// フォローID入力欄
    /// </summary>
    [SerializeField] private InputField followIDInput;

    /// <summary>
    /// フォロー登録ボタン
    /// </summary>
    [SerializeField] private Button followRegistButton;

    /// <summary>
    /// エラーパネル [0:完了 1:フォロー済 2:404エラー]
    /// </summary>
    [SerializeField] private List<RectTransform> noticePanels;

    //--------------------------------------------
    // メソッド

    /// <summary>
    /// 初期処理
    /// </summary>
    void Start()
    {
        // ネットワークマネージャー取得
        networkManager = NetworkManager.Instance;

        // ユーザIDの取得・反映
        contentTexts[0].text = "ID : " + networkManager.GetUserID().ToString();

        // ユーザ名の取得・反映
        contentTexts[1].text = networkManager.GetUserName();

        // ユーザーデータが保存されていない場合は登録
        StartCoroutine(NetworkManager.Instance.GetProfileInfo(
            result =>
            {   // 情報反映
                iconID = result.IconID;                                      // アイコンID取得 
                iconImage.sprite = iconSprite[iconID - 1];                   // アイコン反映
                contentTexts[2].text = result.PlayCnt.ToString() + "回";     // 総プレイ数
                contentTexts[3].text = result.ClearCnt.ToString() + "回";    // クリア数
                contentTexts[4].text = result.CreateCnt.ToString() + "回";   // ステージ作成数
                contentTexts[5].text = result.FollowCnt.ToString() + "回";   // フォロー数
                contentTexts[6].text = result.FollowerCnt.ToString() + "回"; // フォロワー数
            }));
    }

    //======================
    // シーン遷移メソッド

    public void TransScene(string name)
    {
        /* フェード処理 (黒)  
                        ( "シーン名",フェードの色, 速さ);  */
        Initiate.DoneFading();
        Initiate.Fade(name, Color.white, 2.5f);
    }

    //=======================================================
    // メニュー関連メソッド

    /// <summary>
    /// 各アイコン押下処理
    /// </summary>
    public void PushMenuIconButton(GameObject iconPanel)
    {
        iconPanel.SetActive(true);
    }

    /// <summary>
    /// パネル閉じる処理
    /// </summary>
    public void PushCloseButton(GameObject menuPanel)
    {
        menuPanel.SetActive(false);
    }

    /// <summary>
    /// フォローボタン押下時
    /// </summary>
    public void PushFollowButton()
    {
        followPanel.SetActive(true);

        // おすすめユーザーリストの生成
        StartCoroutine(NetworkManager.Instance.GetRandom(
            result =>
            {
                if (result.Count == 0)
                {
                    // データ無し表示
                    GameObject noData = Instantiate(noDataPrefab, Vector3.zero, Quaternion.identity, scrolltContents[2].transform);
                }
                foreach (var data in result)
                {   // フォローリスト

                    // ユーザーデータ生成
                    GameObject userData = Instantiate(userInfoPrefab, Vector3.zero, Quaternion.identity, scrolltContents[2].transform);

                    userData.transform.GetChild(0).GetComponent<Image>().sprite = iconSprite[data.IconID - 1];  // アイコン設定
                    userData.transform.GetChild(1).GetComponent<Text>().text = "ID : " + data.ID.ToString();    // ID設定
                    userData.transform.GetChild(2).GetComponent<Text>().text = data.Name;                       // 名前設定
                    userData.transform.GetChild(3).GetComponent<Image>().sprite = registSprite;                 // 画像設定
                    userData.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(() =>
                    {   // フォロー登録処理
                        // ボタンの無効化
                        userData.transform.GetChild(3).GetComponent<Button>().interactable = false;

                        StartCoroutine(NetworkManager.Instance.RegistFollow(
                            data.ID,
                            result =>
                            {
                                Debug.Log("登録完了");
                            }));
                    });
                }
            }));

        // フォロー・フォロワーリストの生成
        StartCoroutine(NetworkManager.Instance.GetFollow(
            result =>
            {
                if (result.Follow.Count == 0)
                {
                    // データ無し表示
                    GameObject noData = Instantiate(noDataPrefab, Vector3.zero, Quaternion.identity, scrolltContents[0].transform);
                }
                foreach (var data in result.Follow)
                {   // フォローリスト

                    // ユーザーデータ生成
                    GameObject userData = Instantiate(userInfoPrefab, Vector3.zero, Quaternion.identity, scrolltContents[0].transform);

                    userData.transform.GetChild(0).GetComponent<Image>().sprite = iconSprite[data.IconID - 1];  // アイコン設定
                    userData.transform.GetChild(1).GetComponent<Text>().text = "ID : " + data.ID.ToString();    // ID設定
                    userData.transform.GetChild(2).GetComponent<Text>().text = data.Name;                       // 名前設定
                    userData.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(() =>
                    {   // フォロー解除処理
                        StartCoroutine(NetworkManager.Instance.DestroyFollow(
                            data.ID,
                            result =>
                            {   // ユーザInfoオブジェの削除
                                Destroy(userData);
                            }));
                    });
                }

                if (result.Follower.Count == 0)
                {
                    // データ無し表示
                    GameObject noData = Instantiate(noDataPrefab, Vector3.zero, Quaternion.identity, scrolltContents[1].transform);
                }
                foreach (var data in result.Follower)
                {   // フォロワーリスト
                    
                    // ユーザーデータ生成
                    GameObject userData = Instantiate(userInfoPrefab, Vector3.zero, Quaternion.identity, scrolltContents[1].transform);

                    userData.transform.GetChild(0).GetComponent<Image>().sprite = iconSprite[data.IconID - 1];  // アイコン設定
                    userData.transform.GetChild(1).GetComponent<Text>().text = "ID : " + data.ID.ToString();    // ID設定
                    userData.transform.GetChild(2).GetComponent<Text>().text = data.Name;                       // 名前設定
                    Destroy(userData.transform.GetChild(3).gameObject);    // ボタンの破棄 
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
    /// フォローパネル閉じる処理
    /// </summary>
    public void PushFollowClose()
    {
        followPanel.SetActive(false);

        for(int i=0; i<scrolltContents.Count; i++)
        {
            foreach (Transform content in scrolltContents[i].transform)
            {
                //自分の子供をDestroyする
                Destroy(content.gameObject);
            }
        }
    }

    /// <summary>
    /// 通知押下処理
    /// </summary>
    /// <param name="gameObject"></param>
    public void PushNoticeButton(RectTransform rectTransform)
    {
        // 初期位置に移動
        rectTransform.anchoredPosition = new Vector2(1000.0f, rectTransform.anchoredPosition.y);
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
                    contentTexts[1].text = nameInput.text;
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

    /// <summary>
    /// フォロー登録処理
    /// </summary>
    public void PushRegistFollow()
    {
        if(followIDInput.text == "") { return; }  // 入力無い時

        followRegistButton.interactable = false;    // ボタン無効化

        Invoke("RevivalFollowButton", 2.5f);        // 指定時間経過後ボタン有効化

        StartCoroutine(NetworkManager.Instance.RegistFollow(
            int.Parse(followIDInput.text),
            result =>
            {
                switch (result)
                {
                    case "200": // 登録成功
                        Debug.Log("登録完了");
                        MoveCaution(noticePanels[0]);
                        break;

                    case "400": // 登録済
                        Debug.Log("登録済");
                        MoveCaution(noticePanels[1]);
                        break;

                    case "404": // 指定IDが存在しない
                        Debug.Log("IDが存在しない");
                        MoveCaution(noticePanels[2]);
                        break;

                    default:
                        break;
                }
            }));
    }

    /// <summary>
    /// ボタン有効化処理
    /// </summary>
    /// <param name="button"></param>
    private void RevivalFollowButton()
    {
        followRegistButton.interactable = true;
    }
}
