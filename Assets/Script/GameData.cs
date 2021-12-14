using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData instance;

    [Header("�R�X�g�p�̒ʉ�")]
    public int currency;

    [Header("�J�����V�[�̍ő�l")]
    public int maxCurrency;

    [Header("���Z�܂ł̑ҋ@����")]
    public int maxCurrencyIntervalTime;

    [Header("���Z�l")]
    public int addCurrencyPoint;

    //�z�u�ł���L�����̏����
    public int maxCharaPlacementCount;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }else{

            Destroy(gameObject);
        }
    }
}