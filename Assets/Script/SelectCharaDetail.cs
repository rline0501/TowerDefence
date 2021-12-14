using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SelectCharaDetail : MonoBehaviour
{
    [SerializeField]
    private Button btnSelectCharaDetail;

    [SerializeField]
    private Image imgChara;

    private PlacementCharaSelectPopUp placementCharaSelectPop;

    private CharaData charaData;

    //������s�������e�R���|�[�l���g�̏����A�T�C�����邽�߂̕ϐ��Q��ǉ�����
   
   


    /// <summary>
    /// SelectCharaDetail�̐ݒ�
    /// </summary>
    /// <param name="placementCharaSelectPop"></param>
    /// <param name="charaData"></param>
    public void SetUpSelectCharaDetail(PlacementCharaSelectPopUp placementCharaSelectPop, CharaData charaData){
        this.placementCharaSelectPop = placementCharaSelectPop;
        this.charaData = charaData;


        //TODO �{�^���������Ȃ���Ԃɐ؂�ւ���
        ChangeActivateButton(false);

        imgChara.sprite = this.charaData.charaSprite;


        //TODO �J�����V�[�̒l���X�V�����x�ɃR�X�g���x�����邩�m�F����Ď�����
        JudgePermissionCost(GameData.instance.currency);


        //�{�^���Ƀ��\�b�h��o�^
        btnSelectCharaDetail.onClick.AddListener(OnClickSelectCharaDetail);


        //TODO �R�X�g�ɉ����ă{�^���������邩�ǂ�����؂�ւ���
        ChangeActivateButton(true);
    }

 /// <summary>
 /// SelectCharaDetail���������̏���
 /// </summary>
 private void OnClickSelectCharaDetail()
 {

    //TODO �A�j�����o

    //�^�b�v����SelectCharaDetail�̏����|�b�v�A�b�v�ɑ���
    placementCharaSelectPop.SetSelectCharaDetail(charaData);
 }

    /// <summary>
    /// �{�^�����������Ԃ̐؂�ւ�
    /// </summary>
    public void ChangeActivateButton(bool isSwitch)
    {
        btnSelectCharaDetail.interactable = isSwitch;
    }

    /// <summary>
    /// �R�X�g���x�����邩�m�F����
    /// </summary>
    public bool JudgePermissionCost(int value)
    {

        //Debug.Log("�R�X�g�m�F");

        // �R�X�g���x������ꍇ
        if (charaData.cost <= value)
        {

            // �{�^�����������Ԃɂ���
            ChangeActivateButton(true);
            return true;
        }
        return false;
    }

    /// <summary>
    /// �{�^���̏�Ԃ̎擾(����̂��߂Ɏ���)
    /// </summary>
    /// <returns></returns>
    public bool GetActivateButtonState()
    {
        return btnSelectCharaDetail.interactable;
    }

    /// <summary>
    /// CharaData �̎擾(����̂��߂Ɏ���)
    /// </summary>
    /// <returns></returns>
    public CharaData GetCharaData()
    {
        return charaData;
    }


}


