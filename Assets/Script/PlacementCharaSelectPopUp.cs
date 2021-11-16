using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlacementCharaSelectPopUp : MonoBehaviour
{
    [SerializeField]
    private Button btnClosePopUp;

    [SerializeField]
    private Button btnChooseChara;

    [SerializeField]
    private CanvasGroup canvasGroup;

    private CharaGenerator charaGenerator;

    //TODO ������s�������e�R���|�[�l���g�̏����A�T�C�����邽�߂̕ϐ��Q��ǉ�����

   /// <summary>
   /// �|�b�v�A�b�v�̐ݒ�
   /// </summary>
   /// <param name="charaGenerator"></param>
    public void SetUpPlacementCharaSelectPopUp(CharaGenerator charaGenerator)
    {

        this.charaGenerator = charaGenerator;

        //TODO ���ɐݒ荀�ڂ���������ǉ�����


        //��x�����Ȃ���Ԃɂ��Ă���
        canvasGroup.alpha = 0;

        //���X�Ƀ|�b�v�A�b�v��\������
        HidePopUp();

        //�e�{�^���̑���������Ȃ���Ԃɂ���
        SwitchActiveButtons(false);


        //TODO�@SO�ɓo�^����Ă���L�������̃{�^���̃Q�[���I�u�W�F�N�g�𐶐�


        //TODO �ŏ��ɐ��������{�^���̏ꍇ


        //TODO �I�����Ă���L�����Ƃ��ď����l�ɐݒ�


        //�e�{�^���Ƀ��\�b�h��o�^
        btnChooseChara.onClick.AddListener(OnClickSubmitChooseChara);

        btnClosePopUp.onClick.AddListener(OnClickClosePopUp);

        //�e�{�^�����������Ԃɂ���
        SwitchActiveButtons(true);
    }
    /// <summary>
    /// �e�{�^���̃A�N�e�B�u��Ԃ̐؂�ւ�
    /// </summary>
    /// <param name="isSwitch"></param>
    public void SwitchActiveButtons(bool isSwitch)
    {
        btnChooseChara.interactable = isSwitch;
        btnClosePopUp.interactable = isSwitch;
    }
    
    /// <summary>
    /// �|�b�v�A�b�v�̕\��
    /// </summary>
    public void ShowPopUp()
    {
        //TODO �e�L�����̃{�^���̐���


        //�|�b�v�A�b�v�̕\��
        canvasGroup.DOFade(1.0f, 1.5f);
    }

    /// <summary>
    /// �I�����Ă���L������z�u����{�^���𐄂����ۂ̏���
    /// </summary>
    private void OnClickSubmitChooseChara()
    {
        //TODO �R�X�g�̎x�������\���ŏI�m�F


        //�I�����Ă���L�����̐���


        //�|�b�v�A�b�v�̔�\��
        HidePopUp();
    }

    /// <summary>
    /// �z�u���~�߂�{�^���𐄂����ۂ̏���
    /// </summary>
    private void OnClickClosePopUp()
    {

        //�|�b�v�A�b�v�̔�\��
        HidePopUp();
    }

    /// <summary>
    /// �|�b�v�A�b�v�̔�\��
    /// </summary>
    private void HidePopUp()
    {

        //TODO �e�L�����̃{�^���̐���


        //�|�b�v�A�b�v�̔�\��
        canvasGroup.DOFade(0, 0.5f).OnComplete(() => charaGenerator.InactivatePlacementCharaSelectPopUp());
    }


}
