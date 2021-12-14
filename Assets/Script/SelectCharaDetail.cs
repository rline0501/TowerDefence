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
        ChangeActivateButton(false);

        imgChara.sprite = this.charaData.charaSprite;


        //TODO カレンシーの値が更新される度にコストが支払えるか確認する監視処理
        JudgePermissionCost(GameData.instance.currency);


        //ボタンにメソッドを登録
        btnSelectCharaDetail.onClick.AddListener(OnClickSelectCharaDetail);


        //TODO コストに応じてボタンを押せるかどうかを切り替える
        ChangeActivateButton(true);
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

    /// <summary>
    /// ボタンを押せる状態の切り替え
    /// </summary>
    public void ChangeActivateButton(bool isSwitch)
    {
        btnSelectCharaDetail.interactable = isSwitch;
    }

    /// <summary>
    /// コストが支払えるか確認する
    /// </summary>
    public bool JudgePermissionCost(int value)
    {

        //Debug.Log("コスト確認");

        // コストが支払える場合
        if (charaData.cost <= value)
        {

            // ボタンを押せる状態にする
            ChangeActivateButton(true);
            return true;
        }
        return false;
    }

    /// <summary>
    /// ボタンの状態の取得(今後のために実装)
    /// </summary>
    /// <returns></returns>
    public bool GetActivateButtonState()
    {
        return btnSelectCharaDetail.interactable;
    }

    /// <summary>
    /// CharaData の取得(今後のために実装)
    /// </summary>
    /// <returns></returns>
    public CharaData GetCharaData()
    {
        return charaData;
    }


}


