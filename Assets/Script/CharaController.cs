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

    [SerializeField]
    private BoxCollider2D attackRangeArea;

    [SerializeField]
    private CharaData charaData;

    private GameManager gameManager;

    //private SpriteRenderer spriteRenderer;

    private Animator anim;

    private string overrideClipName = "Chara_0";

    private AnimatorOverrideController overrideController;

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
            if (gameManager.currentGameState == GameManager.GameState.Play)
            {

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

                        //�L�����̃��X�g��������폜
                        gameManager.RemoveCharasList(this);
                }
                
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

    /// <summary>
    /// �L�����̐ݒ�
    /// </summary>
    /// <param name="charaData"></param>
    /// <param name="gameManager"></param>
    public void SetUpChara(CharaData charaData, GameManager gameManager)
    {
        this.charaData = charaData;
        this.gameManager = gameManager;

        //�e�l��CharaData����擾���Đݒ�
        attackPower = this.charaData.attackPower;

        intervalAttackTime = this.charaData.intervalAttackTime;

        //DataBaseManager�ɓo�^����Ă���AttackRangeSizeSO�X�N���v�^�u���E�I�u�W�F�N�g�̃f�[�^�Əƍ����s��
        //CharaData��AttackRangeType�̏�������Size��ݒ�
        attackRangeArea.size = DataBaseManager.instance.GetAttackRangeSize(this.charaData.attackRange);

        attackCount = this.charaData.maxAttackCount;

        //�c��̍U���񐔂̕\���X�V
        UpdateDisplayAttackCount();

        //�L�����摜�̐ݒ�B�A�j���𗘗p����悤�ɂȂ�����A���̏����͂��Ȃ�
        //if (TryGetComponent(out spriteRenderer))
        //{

            //�摜��z�u�����L�����̉摜�ɍ����ւ���
            //spriteRenderer.sprite = this.charaData.charaSprite;
        //}

        //TODO �L�������Ƃ� AnimationClip ��ݒ�
        SetUpAnimation();

    }

    /// <summary>
    /// Motion�ɓo�^����Ă���AnimationClip��ύX
    /// </summary>
    private void SetUpAnimation()
    {
        if (TryGetComponent(out anim))
        {

            overrideController = new AnimatorOverrideController();

            overrideController.runtimeAnimatorController = anim.runtimeAnimatorController;
            anim.runtimeAnimatorController = overrideController;

            AnimatorStateInfo[] layerInfo = new AnimatorStateInfo[anim.layerCount];

            for (int i = 0; i < anim.layerCount; i++)
            {
                layerInfo[i] = anim.GetCurrentAnimatorStateInfo(i);
            }

            overrideController[overrideClipName] = this.charaData.charaAnim;

            anim.runtimeAnimatorController = overrideController;

            anim.Update(0.0f);

            for (int i = 0; i < anim.layerCount; i++)
            {
                anim.Play(layerInfo[i].fullPathHash, i, layerInfo[i].normalizedTime);
            }
        }
    }
}
