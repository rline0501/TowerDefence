using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private EnemyGenerator enemyGenerator;

    //ここにEnemyGeneratorスクリプト側の変数を４つ移管します
    public bool isEnemyGenerate;

    public int generateIntervalTime;

    public int generateEnemyCount;

    public int maxEnemyCount;


    void Start()
    {
        isEnemyGenerate = true;

        //敵の生成準備開始
        StartCoroutine(enemyGenerator.PreparateEnemyGenerate(this));
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
}
