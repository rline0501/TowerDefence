using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text txtCost;



    /// <summary>
    /// �J�����V�[�̕\���X�V
    /// </summary>
    public void UpdateDisplayCurrency()
    {
        txtCost.text = GameData.instance.currency.ToString();
    }
}
