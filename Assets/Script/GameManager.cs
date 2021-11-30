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
        Preparate,
        Play,
        Stop,
        GameUp
    }


    //���݂�GameState�̏�ԁB��L����1������������
    public GameState currentGameState;

    public UIManager uIManager;


    void Start()
    {
        //�L�����z�u�p�|�b�v�A�b�v�̐����Ɛݒ�
        StartCoroutine(charaGenerator.SetUpCharaGenerator(this));

        isEnemyGenerate = true;

        //�G�̐��������J�n
        StartCoroutine(enemyGenerator.PreparateEnemyGenerate(this));

        currentGameState = GameState.Play;

        //TODO �J�����V�[�̎����l�������̊J�n
        StartCoroutine(TimeToCurrency());
    }

    /// <summary>
    /// �G�̏���List�ɒǉ�
    /// </summary>
    public void AddEnemyList()
    {    //TODO�G�̏���List�ɒǉ�����ۂɁA������ǉ�

        //TODO�G�̏���List�ɒǉ�

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
}
