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
        Preparate,
        Play,
        Stop,
        GameUp
    }


    //現在のGameStateの状態。上記から1つだけ代入される
    public GameState currentGameState;

    public UIManager uIManager;


    void Start()
    {
        //キャラ配置用ポップアップの生成と設定
        StartCoroutine(charaGenerator.SetUpCharaGenerator(this));

        isEnemyGenerate = true;

        //敵の生成準備開始
        StartCoroutine(enemyGenerator.PreparateEnemyGenerate(this));

        currentGameState = GameState.Play;

        //TODO カレンシーの自動獲得処理の開始
        StartCoroutine(TimeToCurrency());
    }

    /// <summary>
    /// 敵の情報をListに追加
    /// </summary>
    public void AddEnemyList()
    {    //TODO敵の情報をListに追加する際に、引数を追加

        //TODO敵の情報をListに追加

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
}
