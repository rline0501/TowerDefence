using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaController : MonoBehaviour
{
    [SerializeField, Header("攻撃力")]
    private int attackPower = 1;

    [SerializeField, Header("攻撃するまでの待機時間")]
    private float intervalAttackTime = 60.0f;

    [SerializeField]
    private bool isAttack;

    [SerializeField]
    private EnemyController enemy;

    [SerializeField, Header("残り攻撃回数")]
    private int attackCount = 3;

    [SerializeField]
    private UnityEngine.UI.Text txtAttackCount;

    [SerializeField]
    private BoxCollider2D attackRangeArea;

    [SerializeField]
    private CharaData charaData;

    private GameManager gameManager;

    //private SpriteRenderer spriteRenderer;

    private Animator anim;

    private string overrideClipName = "Chara_0";

    private AnimatorOverrideController overrideController;

    private void OnTriggerStay2D(Collider2D collision)
   {
        //攻撃範囲用のコライダーに侵入したゲームオブジェクトのTagがEnemyである場合で、かつ攻撃中ではない場合で、かつ敵の情報を未取得である場合
        if (collision.tag == "Enemy" && !isAttack && !enemy)

        //攻撃中でない場合で、かつ敵の情報を未取得である場合
        //if(!isAttack && !enemy)

        //攻撃範囲用のコライダーに侵入したゲームオブジェクトのTagがEnemyである場合
        //if(collision.tag == "Enemy")
        {
            Debug.Log("敵発見");

            //Destroy(collision.gameObject);（削除）


            //敵の情報（EnemyController）を取得する
            //EnemyControllerがアタッチされているゲームオブジェクトを判別しているので
            //ここで今までのTagによる判定と同じ動作で判定が行えます
            //そのため↑の処理からTagの処理を削除しています
            if (collision.gameObject.TryGetComponent(out enemy) == true)
            {
                //TODO 情報を取得できたら攻撃状態にする
                isAttack = true;

                //TODO 攻撃準備に入る
                StartCoroutine(PrepareteAttack());

            }
        }
    }


    public IEnumerator PrepareteAttack()
    {
        Debug.Log("攻撃開始");

            int timer = 0;

        //攻撃中の間だけループ処理を繰り返す
        while (isAttack)
        {
            //TODO　ゲームプレイ中のみ攻撃する
            if (gameManager.currentGameState == GameManager.GameState.Play)
            {

                timer++;

            //攻撃のための待機時間が経過したら
            if(timer > intervalAttackTime)
            {
                //次の攻撃に備えて待機時間のタイマーをリセット
                timer = 0;

                //攻撃
                Attack();

                //TODO 攻撃回数関連の処理をここに記述する
                attackCount--;

                //TODO 残り攻撃回数の表示更新
                UpdateDisplayAttackCount();

                //攻撃回数が無くなったら
                if(attackCount <= 0)
                {
                    //キャラ破壊
                    Destroy(gameObject);

                        //キャラのリストから情報を削除
                        gameManager.RemoveCharasList(this);
                }
                
            }
           
            }

            //１フレーム処理を中断する(この処理を書き忘れると∞ループになってUnity壊れる)
            yield return null;

        }
    }

    /// <summary>
    /// 攻撃
    /// </summary>
    private void Attack()
    {
        Debug.Log("攻撃");

        //TODOキャラの上に攻撃エフェクト生成


        //敵キャラ側に用意したダメージ計算用メソッドを呼び出して敵にダメージを与える
        enemy.CulcDamage(attackPower);

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            Debug.Log("敵なし");

            isAttack = false;
            enemy = null;
        }
    }

    /// <summary>
    /// 残り攻撃回数の表示更新
    /// </summary>
    private void UpdateDisplayAttackCount()
    {
        txtAttackCount.text = attackCount.ToString();
    }

    /// <summary>
    /// キャラの設定
    /// </summary>
    /// <param name="charaData"></param>
    /// <param name="gameManager"></param>
    public void SetUpChara(CharaData charaData, GameManager gameManager)
    {
        this.charaData = charaData;
        this.gameManager = gameManager;

        //各値をCharaDataから取得して設定
        attackPower = this.charaData.attackPower;

        intervalAttackTime = this.charaData.intervalAttackTime;

        //DataBaseManagerに登録されているAttackRangeSizeSOスクリプタブル・オブジェクトのデータと照合を行い
        //CharaDataのAttackRangeTypeの情報を元にSizeを設定
        attackRangeArea.size = DataBaseManager.instance.GetAttackRangeSize(this.charaData.attackRange);

        attackCount = this.charaData.maxAttackCount;

        //残りの攻撃回数の表示更新
        UpdateDisplayAttackCount();

        //キャラ画像の設定。アニメを利用するようになったら、この処理はやらない
        //if (TryGetComponent(out spriteRenderer))
        //{

            //画像を配置したキャラの画像に差し替える
            //spriteRenderer.sprite = this.charaData.charaSprite;
        //}

        //TODO キャラごとの AnimationClip を設定
        SetUpAnimation();

    }

    /// <summary>
    /// Motionに登録されているAnimationClipを変更
    /// </summary>
    private void SetUpAnimation()
    {
        if (TryGetComponent(out anim))
        {

            overrideController = new AnimatorOverrideController();

            overrideController.runtimeAnimatorController = anim.runtimeAnimatorController;
            anim.runtimeAnimatorController = overrideController;

            AnimatorStateInfo[] layerInfo = new AnimatorStateInfo[anim.layerCount];

            for (int i = 0; i < anim.layerCount; i++)
            {
                layerInfo[i] = anim.GetCurrentAnimatorStateInfo(i);
            }

            overrideController[overrideClipName] = this.charaData.charaAnim;

            anim.runtimeAnimatorController = overrideController;

            anim.Update(0.0f);

            for (int i = 0; i < anim.layerCount; i++)
            {
                anim.Play(layerInfo[i].fullPathHash, i, layerInfo[i].normalizedTime);
            }
        }
    }
}
