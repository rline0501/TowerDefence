using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps; //�^�C���}�b�v�̋@�\���������߂ɕK�v

public class CharaGenerator : MonoBehaviour
{
    //�L�����̃v���t�@�u�̓o�^�p
    [SerializeField]
    private GameObject charaPrefab;

    //�^�C���}�b�v�̍��W���擾���邽�߂̏��BGrid_Base����Grid���w�肷�� 
    [SerializeField]
    private Grid grid;

    //Walk����TileMap���w�肷��
    [SerializeField]
    private Tilemap tilemaps;

    //PlacementCharaSelectPopUp�v���t�@�u�Q�[���I�u�W�F�N�g���A�T�C���p
    [SerializeField]
    private PlacementCharaSelectPopUp placementCharaSelectPopUpPrefab;

    //PlacementCharaSelectPopUp�Q�[���I�u�W�F�N�g�̐����ʒu�̓o�^�p
    [SerializeField]
    private Transform canvasTran;

    //�������ꂽPlacementCharaSelectPopUp�Q�[���I�u�W�F�N�g�������邽�߂̕ϐ�
    private PlacementCharaSelectPopUp placementCharaSelectPopUp;

    private GameManager gameManager;

    //�^�C���}�b�v�̃^�C���̃Z�����W�̕ێ��p
    private Vector3Int gridPos;

    void Update()
    {
        //TODO �z�u�ł���ő�L�������ɒB���Ă���ꍇ�ɂ͔z�u�ł��Ȃ�

        //��ʂ��^�b�v�i�}�E�X�N���b�N�j���A���z�u�L�����|�b�v�A�b�v����\����ԂȂ�
        if (Input.GetMouseButtonDown(0) && !placementCharaSelectPopUp.gameObject.activeSelf)
        {

            //��ʂ��^�b�v(�}�E�X�N���b�N)������
            if (Input.GetMouseButtonDown(0))
            {
                //�^�b�v(�}�E�X�N���b�N)�̈ʒu���擾���ă��[���h���W�ɕϊ����A���������Ƀ^�C���̃Z�����W�ɕϊ�
                gridPos = grid.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));

                //�^�b�v�����ʒu�̃^�C���̃R���C�_�[�̏����m�F���A���ꂪNone�ł����
                if (tilemaps.GetColliderType(gridPos) == Tile.ColliderType.None)
                {
                    //�L�����������������\�b�h��
                    //CreateChara(gridPos); �^�b�v�ő����ɃL�������쐬����Ȃ��悤�Ƀ��t�@�N�^�����O

                    //�z�u�L�����I��p�|�b�v�A�b�v�̕\��
                    ActivatePlacementCharaSelectPopUp();
                }

                //�^�b�v�����ʒu�ɃL�����𐶐����Ĕz�u
                //GameObject chara = Instantiate(charaPrefab, gridPos, Quaternion.identity);

                //�L�����̈ʒu���^�C���̍����� 0,0 �Ƃ��Đ������Ă���̂ŁA�^�C���̒����ɂ���悤�Ɉʒu�𒲐�
                //chara.transform.position = new Vector2(chara.transform.position.x + 0.5f, chara.transform.position.y + 0.5f);
            }
        }
    }

    /// <summary>
    /// �L��������
    /// </summary>
    /// <param name="gridPos"></param>
    private void CreateChara(Vector3Int gridPos)
    {
        //�^�b�v�����ʒu�ɃL�����𐶐����Ĕz�u
        GameObject chara = Instantiate(charaPrefab, gridPos, Quaternion.identity);

        //�L�����̈ʒu���^�C���̍�����0,0�Ƃ��Đ������Ă���̂ŁA�^�C���̒����ɂ���悤�Ɉʒu�𒲐�
        chara.transform.position = new Vector2(chara.transform.position.x + 0.5f, chara.transform.position.y + 0.5f);
    }

    /// <summary>
    /// �ݒ�
    /// </summary>
    /// <param name="gameManager"></param>
    /// <returns></returns>
    public IEnumerator SetUpCharaGenerator(GameManager gameManager)
    {
        this.gameManager = gameManager;

        //TODO �X�e�[�W�̃f�[�^���擾

        //TODO �L�����̃f�[�^�����X�g��

        //�L�����z�u�p�̃|�b�v�A�b�v�̐���
        yield return StartCoroutine(CreatePlacementCharaSelectPopUp());
    }


    /// <summary>
    /// �z�u�L�����I��p�̃|�b�v�A�b�v����
    /// </summary>
    /// <returns></returns>
    private IEnumerator CreatePlacementCharaSelectPopUp()
    {
        ///�|�b�v�A�b�v�𐶐�
        placementCharaSelectPopUp = Instantiate(placementCharaSelectPopUpPrefab, canvasTran, false);

        //�|�b�v�A�b�v�̐ݒ�
        //TODO ���ƂŃL�����ݒ�p�̏����n��
        placementCharaSelectPopUp.SetUpPlacementCharaSelectPopUp(this);


        //�|�b�v�A�b�v���\���ɂ���
        placementCharaSelectPopUp.gameObject.SetActive(false);

        yield return null;
    }

    public void ActivatePlacementCharaSelectPopUp()
    {
        //�Q�[���̐i�s��Ԃ��Q�[����~�ɕύX

        //TODO ���ׂĂ̓G�̈ړ����ꎞ��~

        //�z�u�L�����I��p�̃|�b�v�A�b�v�̕\��
        placementCharaSelectPopUp.gameObject.SetActive(true);
        placementCharaSelectPopUp.ShowPopUp();

    }


    /// <summary>
    /// �z�u�L�����I��p�̃|�b�v�A�b�v�̔�\��
    /// </summary>
    public void InactivatePlacementCharaSelectPopUp()
    {
        ///�z�u�L�����I��p�̃|�b�v�A�b�v�̔�\��
        placementCharaSelectPopUp.gameObject.SetActive(false);

        //TODO �Q�[���I�[�o�[��Q�[���N���A�ł͂Ȃ��ꍇ

        //TODO �Q�[���̐i�s��Ԃ��v���C���ɕύX���āA�Q�[���ĊJ

        //TODO ���ׂĂ̓G�̈ړ����ĊJ

        //TODO �J�����V�[�̉��Z�������ĊJ
    }

}
