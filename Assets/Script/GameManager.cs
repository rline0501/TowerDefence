using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private EnemyGenerator enemyGenerator;

    [SerializeField]
    private CharaGenerator charaGenerator;

    //������EnemyGenerator�X�N���v�g���̕ϐ����S�ڊǂ��܂�
    public bool isEnemyGenerate;

    public int generateIntervalTime;

    public int generateEnemyCount;

    public int maxEnemyCount;

    public enum GameState
    {
        Preparate,//�Q�[���J�n�O�̏�����
        Play,//�Q�[���v���C��
        Stop,//�Q�[�����̏����̈ꎞ��~��
        GameUp//�Q�[���I���i�Q�[���I�[�o�[�A�N���A�����j
    }


    //���݂�GameState�̏�ԁB��L����1������������B���Ƃ͋������Ȃ�
    public GameState currentGameState;

    [SerializeField]
    //�G�̏����ꌳ�����ĊǗ����邽�߂̕ϐ��B
    //EnemyController�ɂ��郁�\�b�h���g����������EnemyController�^�ň���
    private List<EnemyController> enemiesList = new List<EnemyController>();

    //�j�󂵂��G�̃J�E���g�p
    private int destroyEnemyCount;

    public UIManager uIManager;

    [SerializeField]
    //�z�u�����L�����̏����ꌳ�����ĊǗ����邽�߂̕ϐ��B
    //CharaController �^�ň���
    private List<CharaController> charasList = new List<CharaController>();


    void Start()
    {
        //�Q�[���̐i�s��Ԃ��������ɐݒ�
        SetGameState(GameState.Preparate);

        //TODO �Q�[���f�[�^��������


        //TODO �X�e�[�W�̐ݒ�{�X�e�[�W���Ƃ�PathData��ݒ�


        //�L�����z�u�p�|�b�v�A�b�v�̐����Ɛݒ�
        StartCoroutine(charaGenerator.SetUpCharaGenerator(this));

        //TODO ���_�̐ݒ�


        //TODO �I�[�v�j���O���o�Đ�



        isEnemyGenerate = true;

        //�G�̐��������J�n
        StartCoroutine(enemyGenerator.PreparateEnemyGenerate(this));

        //�Q�[���̐i�s��Ԃ��v���C���ɕύX
        currentGameState = GameState.Play;

        //TODO �J�����V�[�̎����l�������̊J�n
        StartCoroutine(TimeToCurrency());
    }

    /// <summary>
    /// �G�̏���List�ɒǉ�
    /// </summary>
    public void AddEnemyList(EnemyController enemy)
    {    //TODO�G�̏���List�ɒǉ�����ۂɁA������ǉ�

        //TODO�G�̏���List�ɒǉ�
        enemiesList.Add(enemy);

        //�G�̐��������J�E���g�A�b�v
        generateEnemyCount++;
    }

    /// <summary>
    /// �G�̐������~���邩����
    /// </summary>
    public void JudgeGenerateEnemysEnd()
    {
        if (generateEnemyCount >= maxEnemyCount)
        {
            isEnemyGenerate = false;
        }
    }
    
    /// <summary>
    /// GameState�̕ύX�@nextGameState�������Ƃ��āA�����ɑ������GameState�ւƕύX����
    /// </summary>
    /// <param name="nextGameState"></param>
    public void SetGameState(GameState nextGameState)
    {
        currentGameState = nextGameState;
    }

    /// <summary>
    /// ���ׂĂ̓G�̈ړ����ꎞ��~
    /// </summary>
    public void PauseEnemies()
    {
        for(int i = 0; i < enemiesList.Count; i++)
        {
            enemiesList[i].PauseMove();
        }
    }

    /// <summary>
    /// ���ׂĂ̓G�̈ړ����ĊJ
    /// </summary>
    public void ResumeEnemies()
    {
        for(int i = 0; i < enemiesList.Count; i++)
        {
            enemiesList[i].ResumeMove();
        }
    }

    /// <summary>
    /// �G�̏������X�g����폜
    /// </summary>
    /// <param name="removeEnemy"></param>
    public void RemoveEnemyList(EnemyController removeEnemy)
    {
        enemiesList.Remove(removeEnemy);
    }

    /// <summary>
    /// �j�󂵂��G�̐����J�E���g(���̃��\�b�h���O���̃N���X������s���Ă��炤)
    /// </summary>
    public void CountUpDestoryEnemyCount(EnemyController enemyController)
    {

        //���X�g����j�󂳂ꂽ�G�̏����폜
        RemoveEnemyList(enemyController);

        //�G��j�󂵂��������Z
        destroyEnemyCount++;

        Debug.Log("�j�󂵂��G�̐� : " + destroyEnemyCount);

        //�Q�[���N���A����
        JudgeGameClear();
    }


    /// <summary>
    /// �Q�[���N���A����
    /// </summary>
    public void JudgeGameClear()
    {
        //�������𒴂��Ă��邩
        if (destroyEnemyCount >= maxEnemyCount)
        {

            Debug.Log("�Q�[���N���A");


            //TODO �Q�[���N���A�̏�����ǉ�

        }
    }


    public IEnumerator TimeToCurrency()
    {
        int timer = 0;

        //�Q�[���v���C���̂݉��Z
        while (currentGameState == GameState.Play)
        {
            timer++;

            //����̎��Ԃ��o�߂��A�J�����V�[���ő�łȂ����
            if (timer > GameData.instance.maxCurrencyIntervalTime && GameData.instance.currency < GameData.instance.maxCurrency)
            {
                timer = 0;

                //�ő�l�ȉ��ɂȂ�悤�ɃJ�����V�[�����Z
                GameData.instance.currency = Mathf.Clamp(GameData.instance.currency += GameData.instance.addCurrencyPoint, 0, GameData.instance.maxCurrency);

                //�J�����V�[�̉�ʕ\�����X�V
                uIManager.UpdateDisplayCurrency();
            }

            yield return null;
        }

    }

    /// <summary>
    /// �I�������L�����̏������X�g�ɒǉ�
    /// </summary>
    public void AddCharasList(CharaController chara)
    {
        charasList.Add(chara);
    }

    /// <summary>
    /// �I�������L������j�����A�������X�g ����폜
    /// </summary>
    /// <param name="chara"></param>
    public void RemoveCharasList(CharaController chara)
    {
        Destroy(chara.gameObject);
        charasList.Remove(chara);
    }

    /// <summary>
    /// ���݂̔z�u���Ă���L�����̐��̎擾
    /// </summary>
    /// <returns></returns>
    public int GetPlacementCharaCount()
    {
        return charasList.Count;
    }
}
