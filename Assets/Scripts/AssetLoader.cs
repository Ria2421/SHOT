//---------------------------------------------------------------
//
// アセット読み込み [ AssetLoader.cs ]
// Author:Kenta Nakamoto
// Data:2024/08/29
// Update:2024/08/29
//
//---------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AssetLoader : MonoBehaviour
{
    //-------------------------------------------------------------------
    // フィールド

    /// <summary>
    /// ローディングスライダー
    /// </summary>
    [SerializeField] private Slider loadingSlider;

    //-------------------------------------------------------------------
    // メソッド

    /// <summary>
    /// 初期処理
    /// </summary>
    void Start()
    {
        StartCoroutine(loading());
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        
    }

    /// <summary>
    /// 更新データ読み込み処理
    /// </summary>
    /// <returns></returns>
    private IEnumerator loading()
    {
        // カタログ更新処理
        var handle = Addressables.UpdateCatalogs(); // 最新のカタログ(json)を取得
        yield return handle;

        // ダウンロードの実行                                                           ↓グループで設定したラベル
        AsyncOperationHandle downloadingHandle = Addressables.DownloadDependenciesAsync("default", false);  

        // ダウンロード完了するまでスライダーのUIを更新
        while(downloadingHandle.Status == AsyncOperationStatus.None)
        {
            loadingSlider.value = downloadingHandle.GetDownloadStatus().Percent * 100;  // Percentは0～1で取得
            yield return null;  // 1フレーム待つ
        }

        loadingSlider.value = 100;  // 完了後、バーを最大値に設定
        Addressables.Release(downloadingHandle);

        // 次のシーンへ移動
        Initiate.DoneFading();
        Initiate.Fade("HomeScene", Color.black, 1.5f);
    }
}
