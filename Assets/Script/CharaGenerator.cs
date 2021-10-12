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

    //�^�C���}�b�v�̃^�C���̃Z�����W�̕ێ��p
    private Vector3Int gridPos;

    void Update()
    {
        //TODO �z�u�ł���ő�L�������ɒB���Ă���ꍇ�ɂ͔z�u�ł��Ȃ�

        //��ʂ��^�b�v(�}�E�X�N���b�N)������
        if (Input.GetMouseButtonDown(0))
        {
            //�^�b�v(�}�E�X�N���b�N)�̈ʒu���擾���ă��[���h���W�ɕϊ����A���������Ƀ^�C���̃Z�����W�ɕϊ�
            gridPos = grid.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));

            //�^�b�v�����ʒu�̃^�C���̃R���C�_�[�̏����m�F���A���ꂪNone�ł����
            if (tilemaps.GetColliderType(gridPos) == Tile.ColliderType.None)
            {
                //�L�����������������\�b�h��
                CreateChara(gridPos);

                //TODO �z�u�L�����I��p�|�b�v�A�b�v�̕\��
            }

            //�^�b�v�����ʒu�ɃL�����𐶐����Ĕz�u
            //GameObject chara = Instantiate(charaPrefab, gridPos, Quaternion.identity);

            //�L�����̈ʒu���^�C���̍����� 0,0 �Ƃ��Đ������Ă���̂ŁA�^�C���̒����ɂ���悤�Ɉʒu�𒲐�
            //chara.transform.position = new Vector2(chara.transform.position.x + 0.5f, chara.transform.position.y + 0.5f);
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
}
