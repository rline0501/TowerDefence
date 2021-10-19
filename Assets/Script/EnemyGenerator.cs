using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField]
    private EnemyController enemyControllerPrefab;

    [SerializeField]
    private PathData pathData;

    private GameManager gameManager;

    //GameManager変数追加の代わりに削除
    //public bool isEnemyGenerate;
    //public int generateIntervalTime;
    //public int generateEnemyCount;
    //public int maxEnemyCount;

    //void Start(){
        //生成の許可をする
        //isEnemyGenerate = true;

        //敵の生成準備
        //StartCoroutine(PreparateEnemyGenerate());
    //}

    /// <summary>
    /// 敵の生成準備
    /// </summary>
    /// <returns></returns>
    public IEnumerator PreparateEnemyGenerate(GameManager gameManager)
    {
        //引数を通じて届いた情報を自分のクラスにある変数に代入している
        this.gameManager = gameManager;

        //生成用のタイマー用意
        int timer = 0;

        //isEnemyGenetate が true の間はループする
        while (gameManager.isEnemyGenerate)
        {

            //タイマーを加算
            timer++;

            //タイマーの値が敵の生成待機時間を超えたら
            if (timer > gameManager.generateIntervalTime)
            {
                //次の生成のためにタイマーをリセット
                timer = 0;

                //敵の生成
                GenerateEnemy();

                // 敵の生成数のカウントアップとListへの追加
                gameManager.AddEnemyList();

                // 最大生成数を超えたら生成停止
                gameManager.JudgeGenerateEnemysEnd();

                //↑で代わりに行うため削除
                //生成した数をカウントアップ
                //generateEnemyCount++;
                //敵の最大生成数を超えたら
                //if (generateEnemyCount >= maxEnemyCount){
                    //生成停止
                    //isEnemyGenerate = false;
                //}
            }

            //1フレーム中断
            yield return null;
        }

        // TODO生成終了後の処理を記述する

    }

    /// <summary>
    /// 敵の生成
    /// </summary>
    public void GenerateEnemy()
    {
        //指定した位置に敵を生成
        EnemyController enemyController = Instantiate(enemyControllerPrefab, pathData.generateTran.position, Quaternion.identity);

        //TODO 実装したい処理を日本語のコメントとして残しておくようにする

    }
}
