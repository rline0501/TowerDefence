using UnityEngine;

/// <summary>
/// キャラの詳細データ
/// </summary>
[System.Serializable]
public class CharaData
{
    //通し番号
    public int CharaNo;

    //コスト
    public int cost;

    //画像
    public Sprite charaSprite;
    
    //名前
    public string charaName;

    //攻撃力
    public int attackPower;
    
    //攻撃範囲
    public AttackRangeType attackRange;
    
    //攻撃までの待機時間
    public float intervalAttackTime;
    
    //攻撃回数
    public int maxAttackCount;

    [Multiline]
    //詳細情報
    public string discription;

    //TODO他にもあれば追加
}
