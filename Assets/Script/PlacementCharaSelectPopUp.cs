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
    [SerializeField]
    private Image imgPickupChara;


    [SerializeField]
    private Text txtPickupCharaName;

    [SerializeField]
    private Text txtPickupCharaAttackPower;

    [SerializeField]
    private Text txtPickupCharaAttackRangeType;

    [SerializeField]
    private Text txtPickupCharaCost;

    [SerializeField]
    private Text txtPickupCharaMaxAttackCount;

    //�L�����̃{�^���p�̃v���t�@�u���A�T�C������
    [SerializeField]
    private SelectCharaDetail selectCharaDetailPrefab;

    //�L�����̃{�^���𐶐�����ʒu���A�T�C������
    [SerializeField]
    private Transform selectCharaDetailTran;

    //���������L�����̃{�^�����Ǘ�����
    [SerializeField]
    private List<SelectCharaDetail> selectCharaDetailList = new List<SelectCharaDetail>();

    //���ݑI�����Ă���L�����̏����Ǘ�����
    private CharaData chooseCharaData;


    /// <summary>
    /// �|�b�v�A�b�v�̐ݒ�
    /// </summary>
    /// <param name="charaGenerator"></param>
    public void SetUpPlacementCharaSelectPopUp(CharaGenerator charaGenerator, List<CharaData> haveCharaDataList)
    {

        this.charaGenerator = charaGenerator;

        //TODO ���ɐݒ荀�ڂ���������ǉ�����


        //��x�����Ȃ���Ԃɂ��Ă���
        canvasGroup.alpha = 0;

        //���X�Ƀ|�b�v�A�b�v��\������
        HidePopUp();

        //�e�{�^���̑���������Ȃ���Ԃɂ���
        SwitchActiveButtons(false);


        //SO�ɓo�^����Ă���L�������i�����Ŏ󂯎�������j�𗘗p����
        for (int i = 0; i < haveCharaDataList.Count; i++)
        {
            //TODO�@SO�ɓo�^����Ă���L�������̃{�^���̃Q�[���I�u�W�F�N�g�𐶐�
            SelectCharaDetail selectCharaDetail = Instantiate(selectCharaDetailPrefab, selectCharaDetailTran, false);

            //�{�^���̃Q�[���I�u�W�F�N�g�̐ݒ�iCharaData��ݒ肷��j
            selectCharaDetail.SetUpSelectCharaDetail(this, haveCharaDataList[i]);

            //List�ɒǉ�
            selectCharaDetailList.Add(selectCharaDetail);

            //TODO �ŏ��ɐ��������{�^���̏ꍇ
            if (i == 0)
            {
                //TODO �I�����Ă���L�����Ƃ��ď����l�ɐݒ�
                SetSelectCharaDetail(haveCharaDataList[i]);
            }
        }

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
        charaGenerator.CreateChooseChara(chooseCharaData);

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

    /// <summary>
    /// �I�����ꂽSelectCharaDetail�̏����|�b�v�A�b�v���̃s�b�N�A�b�v�ɕ\������
    /// </summary>
    /// <param name="charaData"></param>
    public void SetSelectCharaDetail(CharaData charaData)
    {
        //�e�l�̐ݒ�
        imgPickupChara.sprite = charaData.charaSprite;

        txtPickupCharaName.text = charaData.charaName;

        txtPickupCharaAttackPower.text = charaData.attackPower.ToString();

        txtPickupCharaAttackRangeType.text = charaData.attackRange.ToString();

        txtPickupCharaCost.text = charaData.cost.ToString();

        txtPickupCharaMaxAttackCount.text = charaData.maxAttackCount.ToString();
    }

}
