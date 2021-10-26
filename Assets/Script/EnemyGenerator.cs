using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField]
    private EnemyController enemyControllerPrefab;

    [SerializeField]
    private PathData pathData;

    [SerializeField]
    private DrawPathLine pathLinePrefab;

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

        //移動する地点を取得(<=いままでEnemyControllerスクリプト内で行っていた処理をこちらに移動します)
        Vector3[] paths = pathData.pathTranArray.Select(x => x.position).ToArray();

        //敵キャラの初期設定を行い、移動を一時停止しておく
        enemyController.SetUpEnemyController(paths);

        //敵の移動経路のライン表示を生成の準備
        StartCoroutine(PreparateCreatePathLine(paths, enemyController));

    }

     ///<summary>
    ///ライン生成の準備
    ///</summary>
    ///<param name="paths"></param>
    ///<returns></returns>
    private IEnumerator PreparateCreatePathLine(Vector3[] paths, EnemyController enemyController)
    {

        //ラインの生成と削除。この処理が終了するまでは、この処理より下の処理は実行されない
        yield return StartCoroutine(CreatePathLine(paths));

        //敵の移動を再開
        enemyController.ResumeMove();
    }

    /// <summary>
    /// 移動経路用のラインの生成と破棄
    /// </summary>
    /// <param name="paths"></param>
    /// <returns></returns>
    private IEnumerator CreatePathLine(Vector3[] paths)
    {
        //Listの宣言と初期化
        List<DrawPathLine> drawPathLinesList = new List<DrawPathLine>();

        //１つのPathごとに１つずつ順番にラインを生成
        for(int i = 0; i < paths.Length -1; i++)
        {
            DrawPathLine drawPathLine = Instantiate(pathLinePrefab, transform.position, Quaternion.identity);

            Vector3[] drawPaths = new Vector3[2] { paths[i], paths[i + 1] };

            drawPathLine.CreatePathLine(drawPaths);

            drawPathLinesList.Add(drawPathLine);

            yield return new WaitForSeconds(1.0f);
        }

        //すべてのラインを生成して待機
        yield return new WaitForSeconds(1.0f);

        //１つのラインずつ順番に削除する
        for(int i = 0; i < drawPathLinesList.Count; i++)
        {
            Destroy(drawPathLinesList[i].gameObject);

            yield return new WaitForSeconds(0.1f);
        }
    }
}
