using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SelectCharaDetail : MonoBehaviour
{
    [SerializeField]
    private Button btnSelectCharaDetail;

    [SerializeField]
    private Image imgChara;

    private PlacementCharaSelectPopUp placementCharaSelectPop;

    private CharaData charaData;

    //制御を行いたい各コンポーネントの情報をアサインするための変数群を追加する
   
   


    /// <summary>
    /// SelectCharaDetailの設定
    /// </summary>
    /// <param name="placementCharaSelectPop"></param>
    /// <param name="charaData"></param>
    public void SetUpSelectCharaDetail(PlacementCharaSelectPopUp placementCharaSelectPop, CharaData charaData){
        this.placementCharaSelectPop = placementCharaSelectPop;
        this.charaData = charaData;


        //TODO ボタンを押せない状態に切り替える


        imgChara.sprite = this.charaData.charaSprite;


        //TODO カレンシーの値が更新される度にコストが支払えるか確認する監視処理


        //ボタンにメソッドを登録
        btnSelectCharaDetail.onClick.AddListener(OnClickSelectCharaDetail);


        //TODO コストに応じてボタンを押せるかどうかを切り替える

    }

/// <summary>
/// SelectCharaDetailを押したの処理
/// </summary>
private void OnClickSelectCharaDetail()
{

    //TODO アニメ演出

    //タップしたSelectCharaDetailの情報をポップアップに送る
    placementCharaSelectPop.SetSelectCharaDetail(charaData);
}
}
