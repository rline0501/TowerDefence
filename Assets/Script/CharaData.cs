using UnityEngine;

/// <summary>
/// �L�����̏ڍ׃f�[�^
/// </summary>
[System.Serializable]
public class CharaData
{
    //�ʂ��ԍ�
    public int CharaNo;

    //�R�X�g
    public int cost;

    //�摜
    public Sprite charaSprite;
    
    //���O
    public string charaName;

    //�U����
    public int attackPower;
    
    //�U���͈�
    public AttackRangeType attackRange;
    
    //�U���܂ł̑ҋ@����
    public float intervalAttackTime;
    
    //�U����
    public int maxAttackCount;

    [Multiline]
    //�ڍ׏��
    public string discription;

    //TODO���ɂ�����Βǉ�
}
