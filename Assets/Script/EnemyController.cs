using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class EnemyController : MonoBehaviour
{
    [SerializeField, Header("移動経路の情報")]
    private PathData pathData;

    [SerializeField, Header("移動速度")]
    private float moveSpeed;

    //移動する各地点を代入するための配列
    private Vector3[] paths;

    //Animatorコンポーネントの取得用
    private Animator anim;

    //敵キャラの現在の位置情報
    //private Vector3 currentPos;

    void Start()
    {
        //Animatorコンポーネントを取得してanim変数に代入
        TryGetComponent(out anim);

        //移動する地点を取得するための配列の初期化
        //paths = new Vector3[pathData.pathTranArray.Length];

        //移動する位置情報を順番に配列に取得
        //for(int i = 0; i < pathData.pathTranArray.Length; i++)
        {
            //paths[i] = pathData.pathTranArray[i].position;
        }

        //移動する地点を取得（上と同じ）
        paths = pathData.pathTranArray.Select(x => x.position).ToArray();

        //各地点に向けて移動
        //transform.DOPath(paths, 1000 / moveSpeed).SetEase(Ease.Linear);

        //各地点に向けて移動（リファクタリング）
        //３つ目のメソッドを追加
        transform.DOPath(paths, 1000 / moveSpeed).SetEase(Ease.Linear).OnWaypointChange(ChangeAnimeDirection);
    }


    //void Update()
    //{
    // 敵の進行方向を取得（リファクタリングで削除）
    //ChangeAnimeDirection();
    //}


    /// <summary>
    /// 敵の進行方向を取得して移動アニメと同期
    /// </summary>
    //private void ChangeAnimeDirection()
    //{
    
    //if (transform.position.x < currentPos.x)
    //{

    //anim.SetFloat("Y", 0f);
    //anim.SetFloat("X", -1.0f);

    //Debug.Log("左方向");
    //}
    //else if (transform.position.y > currentPos.y)
    //{
    //anim.SetFloat("X", 0f);
    //anim.SetFloat("Y", 1.0f);

    //Debug.Log("上左向");
    //}
    //else if (transform.position.y < currentPos.y)
    //{
    //anim.SetFloat("X", 0f);
    //anim.SetFloat("Y", -1.0f);

    //Debug.Log("下方向");
    //}
    //else
    //{
    //anim.SetFloat("Y", 0f);
    //anim.SetFloat("X", 1.0f);

    //Debug.Log("右方向");
    //}

    //現在の位置情報を保持
    //currentPos = transform.position;
    //}

   

    ///<summary>
    ///敵の進行方向を取得して移動アニメと同期
    ///</summary>
    private void ChangeAnimeDirection(int index)　//①引数を追加します
    {　　　　　
        Debug.Log(index); //②ここからif文全文を追加します

        //次の移動先の地点がない場合には、ここで処理を終了する
        if (index >= paths.Length)
        {
            return;
        }　              //②ここまで

        //目標の位置と現在の位置との距離と方向を取得し、正規化処理を行い、単位ベクトルとする(方向の情報は持ちつつ、距離による速度差をなくして一定値にする)
        Vector3 direction = (transform.position - paths[index]).normalized;
        Debug.Log(direction);

        //アニメーションの Palameter の値を更新し、移動アニメのBlendTree を制御して移動の方向と移動アニメを同期
        anim.SetFloat("X", direction.x);
        anim.SetFloat("Y", direction.y);


        //以下リファクタリングで削除
        //if (transform.position.x > paths[index].x)
        //{//③条件式の右辺を変更します。演算子の方向に注意してください
            //anim.SetFloat("Y", 0f);
            //anim.SetFloat("X", -1.0f);

            //Debug.Log("左方向");

        //}
        //else if (transform.position.y < paths[index].y)
        //{//④条件式の右辺を変更します。演算子の方向に注意してください
            //anim.SetFloat("X", 0f);
            //anim.SetFloat("Y", 1.0f);

            //Debug.Log("上左向");

        //}
        //else if (transform.position.y > paths[index].y)
        //{//⑤条件式の右辺を変更します。演算子の方向に注意してください
            //anim.SetFloat("X", 0f);
            //anim.SetFloat("Y", -1.0f);

            //Debug.Log("下方向");

        //}
        //else
        //{
            //anim.SetFloat("Y", 0f);
            //anim.SetFloat("X", 1.0f);

            //Debug.Log("右方向");
        //}

    }
}
