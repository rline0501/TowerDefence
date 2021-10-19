using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private EnemyGenerator enemyGenerator;

    //������EnemyGenerator�X�N���v�g���̕ϐ����S�ڊǂ��܂�
    public bool isEnemyGenerate;

    public int generateIntervalTime;

    public int generateEnemyCount;

    public int maxEnemyCount;


    void Start()
    {
        isEnemyGenerate = true;

        //�G�̐��������J�n
        StartCoroutine(enemyGenerator.PreparateEnemyGenerate(this));
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
}
