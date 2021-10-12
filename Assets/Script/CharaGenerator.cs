using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps; //タイルマップの機能を扱うために必要

public class CharaGenerator : MonoBehaviour
{
    //キャラのプレファブの登録用
    [SerializeField]
    private GameObject charaPrefab;

    //タイルマップの座標を取得するための情報。Grid_Base側のGridを指定する 
    [SerializeField]
    private Grid grid;

    //Walk側のTileMapを指定する
    [SerializeField]
    private Tilemap tilemaps;

    //タイルマップのタイルのセル座標の保持用
    private Vector3Int gridPos;

    void Update()
    {
        //TODO 配置できる最大キャラ数に達している場合には配置できない

        //画面をタップ(マウスクリック)したら
        if (Input.GetMouseButtonDown(0))
        {
            //タップ(マウスクリック)の位置を取得してワールド座標に変換し、それをさらにタイルのセル座標に変換
            gridPos = grid.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));

            //タップした位置のタイルのコライダーの情報を確認し、それがNoneであるな
            if (tilemaps.GetColliderType(gridPos) == Tile.ColliderType.None)
            {
                //キャラ生成処理をメソッド化
                CreateChara(gridPos);

                //TODO 配置キャラ選択用ポップアップの表示
            }

            //タップした位置にキャラを生成して配置
            GameObject chara = Instantiate(charaPrefab, gridPos, Quaternion.identity);

            //キャラの位置がタイルの左下を 0,0 として生成しているので、タイルの中央にくるように位置を調整
            chara.transform.position = new Vector2(chara.transform.position.x + 0.5f, chara.transform.position.y + 0.5f);
        }
    }

    /// <summary>
    /// キャラ生成
    /// </summary>
    /// <param name="gridPos"></param>
    private void CreateChara(Vector3Int gridPos)
    {
        //タップした位置にキャラを生成して配置
        GameObject chara = Instantiate(charaPrefab, gridPos, Quaternion.identity);

        //キャラの位置がタイルの左下を0,0として生成しているので、タイルの中央にくるように位置を調整
        chara.transform.position = new Vector2(chara.transform.position.x + 0.5f, chara.transform.position.y + 0.5f);
    }
}
