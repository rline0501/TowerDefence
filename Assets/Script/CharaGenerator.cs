using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps; //タイルマップの機能を扱うために必要

public class CharaGenerator : MonoBehaviour
{
    //キャラのプレファブの登録用
    //[SerializeField]
    //private GameObject charaPrefab;

    //新しくCharaCotroller型で変数を宣言。アサインするプレファブは↑と同じもの。
    [SerializeField]
    private CharaController charaControllerPrefab;

    //タイルマップの座標を取得するための情報。Grid_Base側のGridを指定する 
    [SerializeField]
    private Grid grid;

    //Walk側のTileMapを指定する
    [SerializeField]
    private Tilemap tilemaps;

    //PlacementCharaSelectPopUpプレファブゲームオブジェクトをアサイン用
    [SerializeField]
    private PlacementCharaSelectPopUp placementCharaSelectPopUpPrefab;

    //PlacementCharaSelectPopUpゲームオブジェクトの生成位置の登録用
    [SerializeField]
    private Transform canvasTran;

    [SerializeField, Header("キャラのデータリスト")]
    private List<CharaData> charaDatasList = new List<CharaData>();

    //生成されたPlacementCharaSelectPopUpゲームオブジェクトを代入するための変数
    private PlacementCharaSelectPopUp placementCharaSelectPopUp;

    private GameManager gameManager;

    //タイルマップのタイルのセル座標の保持用
    private Vector3Int gridPos;

    void Update()
    {
        //TODO 配置できる最大キャラ数に達している場合には配置できない

        //画面をタップ（マウスクリック）し、かつ配置キャラポップアップが非表示状態なら
        if (Input.GetMouseButtonDown(0) && !placementCharaSelectPopUp.gameObject.activeSelf)
        {

            //画面をタップ(マウスクリック)したら
            if (Input.GetMouseButtonDown(0))
            {
                //タップ(マウスクリック)の位置を取得してワールド座標に変換し、それをさらにタイルのセル座標に変換
                gridPos = grid.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));

                //タップした位置のタイルのコライダーの情報を確認し、それがNoneであるな
                if (tilemaps.GetColliderType(gridPos) == Tile.ColliderType.None)
                {
                    //キャラ生成処理をメソッド化
                    //CreateChara(gridPos); タップで即座にキャラを作成されないようにリファクタリング

                    //配置キャラ選択用ポップアップの表示
                    ActivatePlacementCharaSelectPopUp();
                }

                //タップした位置にキャラを生成して配置
                //GameObject chara = Instantiate(charaPrefab, gridPos, Quaternion.identity);

                //キャラの位置がタイルの左下を 0,0 として生成しているので、タイルの中央にくるように位置を調整
                //chara.transform.position = new Vector2(chara.transform.position.x + 0.5f, chara.transform.position.y + 0.5f);
            }
        }
    }

    /// <summary>
    /// キャラ生成
    /// </summary>
    /// <param name="gridPos"></param>
    ///private void CreateChara(Vector3Int gridPos)
    ///{
        //タップした位置にキャラを生成して配置
        ///GameObject chara = Instantiate(charaPrefab, gridPos, Quaternion.identity);

        //キャラの位置がタイルの左下を0,0として生成しているので、タイルの中央にくるように位置を調整
        ///chara.transform.position = new Vector2(chara.transform.position.x + 0.5f, chara.transform.position.y + 0.5f);
    ///}



    /// <summary>
    /// 設定
    /// </summary>
    /// <param name="gameManager"></param>
    /// <returns></returns>
    public IEnumerator SetUpCharaGenerator(GameManager gameManager)
    {
        this.gameManager = gameManager;

        //TODO ステージのデータを取得

        //TODO キャラのデータをリスト化
        CreateHaveCharaDatasList();

        //キャラ配置用のポップアップの生成
        yield return StartCoroutine(CreatePlacementCharaSelectPopUp());
    }


    /// <summary>
    /// 配置キャラ選択用のポップアップ生成
    /// </summary>
    /// <returns></returns>
    private IEnumerator CreatePlacementCharaSelectPopUp()
    {
        ///ポップアップを生成
        placementCharaSelectPopUp = Instantiate(placementCharaSelectPopUpPrefab, canvasTran, false);

        //ポップアップの設定
        //TODO あとでキャラ設定用の情報も渡す
        placementCharaSelectPopUp.SetUpPlacementCharaSelectPopUp(this, charaDatasList);


        //ポップアップを非表示にする
        placementCharaSelectPopUp.gameObject.SetActive(false);

        yield return null;
    }

    public void ActivatePlacementCharaSelectPopUp()
    {
        //ゲームの進行状態をゲーム停止に変更

        //TODO すべての敵の移動を一時停止

        //配置キャラ選択用のポップアップの表示
        placementCharaSelectPopUp.gameObject.SetActive(true);
        placementCharaSelectPopUp.ShowPopUp();

    }


    /// <summary>
    /// 配置キャラ選択用のポップアップの非表示
    /// </summary>
    public void InactivatePlacementCharaSelectPopUp()
    {
        ///配置キャラ選択用のポップアップの非表示
        placementCharaSelectPopUp.gameObject.SetActive(false);

        //TODO ゲームオーバーやゲームクリアではない場合

        //TODO ゲームの進行状態をプレイ中に変更して、ゲーム再開

        //TODO すべての敵の移動を再開

        //TODO カレンシーの加算処理を再開
    }

    /// <summary>
    /// キャラのデータをリスト化
    /// </summary>
    private void CreateHaveCharaDatasList()
    {
        //CharaDataSOスクリプタブル・オブジェクト内のCharaDataを１つずつリストに追加
        //TODO スクリプタブル。オブジェクトではなく、実際にプレイヤーが所持しているキャラの番号を元にキャラのデータのリストを作成
        for(int i = 0; i < DataBaseManager.instance.charaDataSO.charaDatasList.Count; i++)
        {
            charaDatasList.Add(DataBaseManager.instance.charaDataSO.charaDatasList[i]);
        }

    }

    /// <summary>
    /// 選択したキャラを生成して配置
    /// </summary>
    /// <param name="charaData"></param>
    public void CreateChooseChara(CharaData charaData)
    {

        //TODO コスト支払い


        //キャラをタップした位置に生成
        CharaController chara = Instantiate(charaControllerPrefab, gridPos, Quaternion.identity);

        //位置が左下を0,0としているので、中央にくるように調整
        chara.transform.position = new Vector2(chara.transform.position.x + 0.5f, chara.transform.position.y + 0.5f);

        //TODO キャラの設定
        chara.SetUpChara(charaData, gameManager);

        //選択しているキャラのデータが届いているのかを確認
        //Debug.Log(charaData.name);

        //TODO キャラをListに追加

    }
}
