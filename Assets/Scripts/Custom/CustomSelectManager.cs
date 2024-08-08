using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomSelectManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// ホーム遷移処理
    /// </summary>
    public void transitionHome()
    {
        /* フェード処理 (黒)  
                        ( "シーン名",フェードの色, 速さ);  */
        Initiate.DoneFading();
        Initiate.Fade("HomeScene", Color.gray, 2.5f);
    }
}
