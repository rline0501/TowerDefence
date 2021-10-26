using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField]
    private EnemyController enemyControllerPrefab;

    [SerializeField]
    private PathData pathData;

    [SerializeField]
    private DrawPathLine pathLinePrefab;

    private GameManager gameManager;

    //GameManager�ϐ��ǉ��̑���ɍ폜
    //public bool isEnemyGenerate;
    //public int generateIntervalTime;
    //public int generateEnemyCount;
    //public int maxEnemyCount;

    //void Start(){
        //�����̋�������
        //isEnemyGenerate = true;

        //�G�̐�������
        //StartCoroutine(PreparateEnemyGenerate());
    //}

    /// <summary>
    /// �G�̐�������
    /// </summary>
    /// <returns></returns>
    public IEnumerator PreparateEnemyGenerate(GameManager gameManager)
    {
        //������ʂ��ē͂������������̃N���X�ɂ���ϐ��ɑ�����Ă���
        this.gameManager = gameManager;

        //�����p�̃^�C�}�[�p��
        int timer = 0;

        //isEnemyGenetate �� true �̊Ԃ̓��[�v����
        while (gameManager.isEnemyGenerate)
        {

            //�^�C�}�[�����Z
            timer++;

            //�^�C�}�[�̒l���G�̐����ҋ@���Ԃ𒴂�����
            if (timer > gameManager.generateIntervalTime)
            {
                //���̐����̂��߂Ƀ^�C�}�[�����Z�b�g
                timer = 0;

                //�G�̐���
                GenerateEnemy();

                // �G�̐������̃J�E���g�A�b�v��List�ւ̒ǉ�
                gameManager.AddEnemyList();

                // �ő吶�����𒴂����琶����~
                gameManager.JudgeGenerateEnemysEnd();

                //���ő���ɍs�����ߍ폜
                //�������������J�E���g�A�b�v
                //generateEnemyCount++;
                //�G�̍ő吶�����𒴂�����
                //if (generateEnemyCount >= maxEnemyCount){
                    //������~
                    //isEnemyGenerate = false;
                //}
            }

            //1�t���[�����f
            yield return null;
        }

        // TODO�����I����̏������L�q����

    }

    /// <summary>
    /// �G�̐���
    /// </summary>
    public void GenerateEnemy()
    {
        //�w�肵���ʒu�ɓG�𐶐�
        EnemyController enemyController = Instantiate(enemyControllerPrefab, pathData.generateTran.position, Quaternion.identity);

        //TODO ������������������{��̃R�����g�Ƃ��Ďc���Ă����悤�ɂ���

        //�ړ�����n�_���擾(<=���܂܂�EnemyController�X�N���v�g���ōs���Ă���������������Ɉړ����܂�)
        Vector3[] paths = pathData.pathTranArray.Select(x => x.position).ToArray();

        //�G�L�����̏����ݒ���s���A�ړ����ꎞ��~���Ă���
        enemyController.SetUpEnemyController(paths);

        //�G�̈ړ��o�H�̃��C���\���𐶐��̏���
        StartCoroutine(PreparateCreatePathLine(paths, enemyController));

    }

     ///<summary>
    ///���C�������̏���
    ///</summary>
    ///<param name="paths"></param>
    ///<returns></returns>
    private IEnumerator PreparateCreatePathLine(Vector3[] paths, EnemyController enemyController)
    {

        //���C���̐����ƍ폜�B���̏������I������܂ł́A���̏�����艺�̏����͎��s����Ȃ�
        yield return StartCoroutine(CreatePathLine(paths));

        //�G�̈ړ����ĊJ
        enemyController.ResumeMove();
    }

    /// <summary>
    /// �ړ��o�H�p�̃��C���̐����Ɣj��
    /// </summary>
    /// <param name="paths"></param>
    /// <returns></returns>
    private IEnumerator CreatePathLine(Vector3[] paths)
    {
        //List�̐錾�Ə�����
        List<DrawPathLine> drawPathLinesList = new List<DrawPathLine>();

        //�P��Path���ƂɂP�����ԂɃ��C���𐶐�
        for(int i = 0; i < paths.Length -1; i++)
        {
            DrawPathLine drawPathLine = Instantiate(pathLinePrefab, transform.position, Quaternion.identity);

            Vector3[] drawPaths = new Vector3[2] { paths[i], paths[i + 1] };

            drawPathLine.CreatePathLine(drawPaths);

            drawPathLinesList.Add(drawPathLine);

            yield return new WaitForSeconds(1.0f);
        }

        //���ׂẴ��C���𐶐����đҋ@
        yield return new WaitForSeconds(1.0f);

        //�P�̃��C�������Ԃɍ폜����
        for(int i = 0; i < drawPathLinesList.Count; i++)
        {
            Destroy(drawPathLinesList[i].gameObject);

            yield return new WaitForSeconds(0.1f);
        }
    }
}
