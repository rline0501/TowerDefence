using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private EnemyGenerator enemyGenerator;

    [SerializeField]
    private CharaGenerator charaGenerator;

    //ここにEnemyGeneratorスクリプト側の変数を４つ移管します
    public bool isEnemyGenerate;

    public int generateIntervalTime;

    public int generateEnemyCount;

    public int maxEnemyCount;

    public enum GameState
    {
        Preparate,//ゲーム開始前の準備中
        Play,//ゲームプレイ中
        Stop,//ゲーム内の処理の一時停止中
        GameUp//ゲーム終了（ゲームオーバー、クリア両方）
    }


    //現在のGameStateの状態。上記から1つだけ代入される。他とは競合しない
    public GameState currentGameState;

    [SerializeField]
    //敵の情報を一元化して管理するための変数。
    //EnemyControllerにあるメソッドを使いたいためEnemyController型で扱う
    private List<EnemyController> enemiesList = new List<EnemyController>();

    //破壊した敵のカウント用
    private int destroyEnemyCount;

    public UIManager uIManager;

    [SerializeField]
    //配置したキャラの情報を一元化して管理するための変数。
    //CharaController 型で扱う
    private List<CharaController> charasList = new List<CharaController>();


    void Start()
    {
        //ゲームの進行状態を準備中に設定
        SetGameState(GameState.Preparate);

        //TODO ゲームデータを初期化


        //TODO ステージの設定＋ステージごとのPathDataを設定


        //キャラ配置用ポップアップの生成と設定
        StartCoroutine(charaGenerator.SetUpCharaGenerator(this));

        //TODO 拠点の設定


        //TODO オープニング演出再生



        isEnemyGenerate = true;

        //敵の生成準備開始
        StartCoroutine(enemyGenerator.PreparateEnemyGenerate(this));

        //ゲームの進行状態をプレイ中に変更
        currentGameState = GameState.Play;

        //TODO カレンシーの自動獲得処理の開始
        StartCoroutine(TimeToCurrency());
    }

    /// <summary>
    /// 敵の情報をListに追加
    /// </summary>
    public void AddEnemyList(EnemyController enemy)
    {    //TODO敵の情報をListに追加する際に、引数を追加

        //TODO敵の情報をListに追加
        enemiesList.Add(enemy);

        //敵の生成数をカウントアップ
        generateEnemyCount++;
    }

    /// <summary>
    /// 敵の生成を停止するか判定
    /// </summary>
    public void JudgeGenerateEnemysEnd()
    {
        if (generateEnemyCount >= maxEnemyCount)
        {
            isEnemyGenerate = false;
        }
    }
    
    /// <summary>
    /// GameStateの変更　nextGameStateを引数として、そこに代入したGameStateへと変更する
    /// </summary>
    /// <param name="nextGameState"></param>
    public void SetGameState(GameState nextGameState)
    {
        currentGameState = nextGameState;
    }

    /// <summary>
    /// すべての敵の移動を一時停止
    /// </summary>
    public void PauseEnemies()
    {
        for(int i = 0; i < enemiesList.Count; i++)
        {
            enemiesList[i].PauseMove();
        }
    }

    /// <summary>
    /// すべての敵の移動を再開
    /// </summary>
    public void ResumeEnemies()
    {
        for(int i = 0; i < enemiesList.Count; i++)
        {
            enemiesList[i].ResumeMove();
        }
    }

    /// <summary>
    /// 敵の情報をリストから削除
    /// </summary>
    /// <param name="removeEnemy"></param>
    public void RemoveEnemyList(EnemyController removeEnemy)
    {
        enemiesList.Remove(removeEnemy);
    }

    /// <summary>
    /// 破壊した敵の数をカウント(このメソッドを外部のクラスから実行してもらう)
    /// </summary>
    public void CountUpDestoryEnemyCount(EnemyController enemyController)
    {

        //リストから破壊された敵の情報を削除
        RemoveEnemyList(enemyController);

        //敵を破壊した数を加算
        destroyEnemyCount++;

        Debug.Log("破壊した敵の数 : " + destroyEnemyCount);

        //ゲームクリア判定
        JudgeGameClear();
    }


    /// <summary>
    /// ゲームクリア判定
    /// </summary>
    public void JudgeGameClear()
    {
        //生成数を超えているか
        if (destroyEnemyCount >= maxEnemyCount)
        {

            Debug.Log("ゲームクリア");


            //TODO ゲームクリアの処理を追加

        }
    }


    public IEnumerator TimeToCurrency()
    {
        int timer = 0;

        //ゲームプレイ中のみ加算
        while (currentGameState == GameState.Play)
        {
            timer++;

            //既定の時間が経過し、カレンシーが最大でなければ
            if (timer > GameData.instance.maxCurrencyIntervalTime && GameData.instance.currency < GameData.instance.maxCurrency)
            {
                timer = 0;

                //最大値以下になるようにカレンシーを加算
                GameData.instance.currency = Mathf.Clamp(GameData.instance.currency += GameData.instance.addCurrencyPoint, 0, GameData.instance.maxCurrency);

                //カレンシーの画面表示を更新
                uIManager.UpdateDisplayCurrency();
            }

            yield return null;
        }

    }

    /// <summary>
    /// 選択したキャラの情報をリストに追加
    /// </summary>
    public void AddCharasList(CharaController chara)
    {
        charasList.Add(chara);
    }

    /// <summary>
    /// 選択したキャラを破棄し、情報をリスト から削除
    /// </summary>
    /// <param name="chara"></param>
    public void RemoveCharasList(CharaController chara)
    {
        Destroy(chara.gameObject);
        charasList.Remove(chara);
    }

    /// <summary>
    /// 現在の配置しているキャラの数の取得
    /// </summary>
    /// <returns></returns>
    public int GetPlacementCharaCount()
    {
        return charasList.Count;
    }
}
