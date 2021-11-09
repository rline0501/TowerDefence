using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaController : MonoBehaviour
{
    [SerializeField, Header("�U����")]
    private int attackPower = 1;

    [SerializeField, Header("�U������܂ł̑ҋ@����")]
    private float intervalAttackTime = 60.0f;

    [SerializeField]
    private bool isAttack;

    [SerializeField]
    private EnemyController enemy;

    [SerializeField, Header("�c��U����")]
    private int attackCount = 3;

    [SerializeField]
    private UnityEngine.UI.Text txtAttackCount;

   private void OnTriggerStay2D(Collider2D collision)
   {
        //�U���͈͗p�̃R���C�_�[�ɐN�������Q�[���I�u�W�F�N�g��Tag��Enemy�ł���ꍇ�ŁA���U�����ł͂Ȃ��ꍇ�ŁA���G�̏��𖢎擾�ł���ꍇ
        if (collision.tag == "Enemy" && !isAttack && !enemy)

        //�U�����łȂ��ꍇ�ŁA���G�̏��𖢎擾�ł���ꍇ
        //if(!isAttack && !enemy)

        //�U���͈͗p�̃R���C�_�[�ɐN�������Q�[���I�u�W�F�N�g��Tag��Enemy�ł���ꍇ
        //if(collision.tag == "Enemy")
        {
            Debug.Log("�G����");

            //Destroy(collision.gameObject);�i�폜�j


            //�G�̏��iEnemyController�j���擾����
            //EnemyController���A�^�b�`����Ă���Q�[���I�u�W�F�N�g�𔻕ʂ��Ă���̂�
            //�����ō��܂ł�Tag�ɂ�锻��Ɠ�������Ŕ��肪�s���܂�
            //���̂��߁��̏�������Tag�̏������폜���Ă��܂�
            if (collision.gameObject.TryGetComponent(out enemy) == true)
            {
                //TODO �����擾�ł�����U����Ԃɂ���
                isAttack = true;

                //TODO �U�������ɓ���
                StartCoroutine(PrepareteAttack());

            }
        }
    }


    public IEnumerator PrepareteAttack()
    {
        Debug.Log("�U���J�n");

            int timer = 0;

        //�U�����̊Ԃ������[�v�������J��Ԃ�
        while (isAttack)
        {
            //TODO�@�Q�[���v���C���̂ݍU������


            timer++;

            //�U���̂��߂̑ҋ@���Ԃ��o�߂�����
            if(timer > intervalAttackTime)
            {
                //���̍U���ɔ����đҋ@���Ԃ̃^�C�}�[�����Z�b�g
                timer = 0;

                //�U��
                Attack();

                //TODO �U���񐔊֘A�̏����������ɋL�q����
                attackCount--;

                //TODO �c��U���񐔂̕\���X�V
                UpdateDisplayAttackCount();

                //�U���񐔂������Ȃ�����
                if(attackCount <= 0)
                {
                    //�L�����j��
                    Destroy(gameObject);
                }
            }

            //�P�t���[�������𒆒f����(���̏����������Y���Ɓ����[�v�ɂȂ���Unity����)
            yield return null;

        }
    }

    /// <summary>
    /// �U��
    /// </summary>
    private void Attack()
    {
        Debug.Log("�U��");

        //TODO�L�����̏�ɍU���G�t�F�N�g����


        //�G�L�������ɗp�ӂ����_���[�W�v�Z�p���\�b�h���Ăяo���ēG�Ƀ_���[�W��^����
        enemy.CulcDamage(attackPower);

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            Debug.Log("�G�Ȃ�");

            isAttack = false;
            enemy = null;
        }
    }

    /// <summary>
    /// �c��U���񐔂̕\���X�V
    /// </summary>
    private void UpdateDisplayAttackCount()
    {
        txtAttackCount.text = attackCount.ToString();
    }

}
