using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class EnemyController : MonoBehaviour
{
    [SerializeField, Header("�ړ��o�H�̏��")]
    private PathData pathData;

    [SerializeField, Header("�ړ����x")]
    private float moveSpeed;

    [SerializeField, Header("�ő�HP")]
    private int maxHp;

    [SerializeField]
    private int hp;



    private Tween tween;

    //�ړ�����e�n�_�������邽�߂̔z��
    private Vector3[] paths;

    //Animator�R���|�[�l���g�̎擾�p
    private Animator anim;

    //�G�L�����̌��݂̈ʒu���
    //private Vector3 currentPos;

    void Start()
    {
        hp = maxHp;

        //Animator�R���|�[�l���g���擾����anim�ϐ��ɑ��
        TryGetComponent(out anim);

        //�ړ�����n�_���擾���邽�߂̔z��̏�����
        //paths = new Vector3[pathData.pathTranArray.Length];

        //�ړ�����ʒu�������Ԃɔz��Ɏ擾
        //for(int i = 0; i < pathData.pathTranArray.Length; i++)
        {
            //paths[i] = pathData.pathTranArray[i].position;
        }

        //�ړ�����n�_���擾�i��Ɠ����j
        paths = pathData.pathTranArray.Select(x => x.position).ToArray();

        //�e�n�_�Ɍ����Ĉړ�
        //transform.DOPath(paths, 1000 / moveSpeed).SetEase(Ease.Linear);

        //�e�n�_�Ɍ����Ĉړ��i���t�@�N�^�����O���ǉ��C���ō폜�j
        //�R�ڂ̃��\�b�h��ǉ�
        //transform.DOPath(paths, 1000 / moveSpeed).SetEase(Ease.Linear).OnWaypointChange(ChangeAnimeDirection);


        // �e�n�_�Ɍ����Ĉړ��B���ケ�̏����𐧌䂷�邽��Tween�^�̕ϐ���DOPath���\�b�h�̏����������Ă���
        tween = transform.DOPath(paths, 1000 / moveSpeed).SetEase(Ease.Linear).OnWaypointChange(ChangeAnimeDirection);    //  <=  DOPath �̏����� tween �ϐ��ɑ�����܂�

    }



    //void Update()
    //{
    // �G�̐i�s�������擾�i���t�@�N�^�����O�ō폜�j
    //ChangeAnimeDirection();
    //}


    /// <summary>
    /// �G�̐i�s�������擾���Ĉړ��A�j���Ɠ���
    /// </summary>
    //private void ChangeAnimeDirection()
    //{

    //if (transform.position.x < currentPos.x)
    //{

    //anim.SetFloat("Y", 0f);
    //anim.SetFloat("X", -1.0f);

    //Debug.Log("������");
    //}
    //else if (transform.position.y > currentPos.y)
    //{
    //anim.SetFloat("X", 0f);
    //anim.SetFloat("Y", 1.0f);

    //Debug.Log("�㍶��");
    //}
    //else if (transform.position.y < currentPos.y)
    //{
    //anim.SetFloat("X", 0f);
    //anim.SetFloat("Y", -1.0f);

    //Debug.Log("������");
    //}
    //else
    //{
    //anim.SetFloat("Y", 0f);
    //anim.SetFloat("X", 1.0f);

    //Debug.Log("�E����");
    //}

    //���݂̈ʒu����ێ�
    //currentPos = transform.position;
    //}



    ///<summary>
    ///�G�̐i�s�������擾���Ĉړ��A�j���Ɠ���
    ///</summary>
    private void ChangeAnimeDirection(int index)�@//�@������ǉ����܂�
    {�@�@�@�@�@
        Debug.Log(index); //�A��������if���S����ǉ����܂�

        //���̈ړ���̒n�_���Ȃ��ꍇ�ɂ́A�����ŏ������I������
        if (index >= paths.Length)
        {
            return;
        }�@              //�A�����܂�

        //�ڕW�̈ʒu�ƌ��݂̈ʒu�Ƃ̋����ƕ������擾���A���K���������s���A�P�ʃx�N�g���Ƃ���(�����̏��͎����A�����ɂ�鑬�x�����Ȃ����Ĉ��l�ɂ���)
        Vector3 direction = (transform.position - paths[index]).normalized;
        Debug.Log(direction);

        //�A�j���[�V������ Palameter �̒l���X�V���A�ړ��A�j����BlendTree �𐧌䂵�Ĉړ��̕����ƈړ��A�j���𓯊�
        anim.SetFloat("X", direction.x);
        anim.SetFloat("Y", direction.y);


        //�ȉ����t�@�N�^�����O�ō폜
        //if (transform.position.x > paths[index].x)
        //{//�B�������̉E�ӂ�ύX���܂��B���Z�q�̕����ɒ��ӂ��Ă�������
            //anim.SetFloat("Y", 0f);
            //anim.SetFloat("X", -1.0f);

            //Debug.Log("������");

        //}
        //else if (transform.position.y < paths[index].y)
        //{//�C�������̉E�ӂ�ύX���܂��B���Z�q�̕����ɒ��ӂ��Ă�������
            //anim.SetFloat("X", 0f);
            //anim.SetFloat("Y", 1.0f);

            //Debug.Log("�㍶��");

        //}
        //else if (transform.position.y > paths[index].y)
        //{//�D�������̉E�ӂ�ύX���܂��B���Z�q�̕����ɒ��ӂ��Ă�������
            //anim.SetFloat("X", 0f);
            //anim.SetFloat("Y", -1.0f);

            //Debug.Log("������");

        //}
        //else
        //{
            //anim.SetFloat("Y", 0f);
            //anim.SetFloat("X", 1.0f);

            //Debug.Log("�E����");
        //}

    }

    /// <summary>
    /// �_���[�W�v�Z
    /// </summary>
    /// <param name="amount"></param>
    public void CulcDamage(int amount)
    {

        // Hp �̒l�����Z�������ʒl���A�Œ�l�ƍő�l�͈͓̔��Ɏ��܂�悤�ɂ��čX�V
        hp = Mathf.Clamp(hp -= amount, 0, maxHp);

        Debug.Log("�c��HP : " + hp);

        // Hp �� 0 �ȉ��ɂȂ����ꍇ
        if (hp <= 0)
        {

            // �j�󏈗������s���郁�\�b�h���Ăяo��
            DestroyEnemy();
        }

        // TODO ���o�p�̃G�t�F�N�g����


        // TODO �q�b�g�X�g�b�v���o

    }

    /// <summary>
    /// �G�j�󏈗�
    /// </summary>
    public void DestroyEnemy()
    {

        // Kill���\�b�h�����s��tween�ϐ��ɑ������Ă��鏈��(DOPath�̏���)���I������
        tween.Kill();

        //TODO SE�̏���


        //TODO �j�󎞂̃G�t�F�N�g�̐�����֘A���鏈��


        //�G�L�����̔j��
        Destroy(gameObject);
    }
}
