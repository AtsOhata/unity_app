using UnityEngine;

/// <summary>
/// �Œ�l�������ɏ��� �������I�ɊO���t�@�C������ǂݍ��߂�悤�ɂ���H
/// </summary>
public class GameOption : MonoBehaviour
{
    // �f�o�b�O���[�h�@ON�ɂ���ƌʃV�[���̋N�������ł���悤�ɂȂ����肷��
    static public readonly bool IsDebugMode = false;

    // �A�v���̑薼


    // *****
    // ����
    // *****
    /// <summary>�P�̃Z���ɕ����̏�񂪓����Ă��鎞���Ɏg�p�����؂蕶��</summary>
    static public readonly char SEPARATOR = '#';

    // --------------------
    // Resources�z���̃p�X
    // --------------------
    // QBInfo
    /// <summary>QBInfo.csv�̃p�X</summary>
    static public readonly string QB_INFO_PATH = "QBInfo";
    /// <summary>��蒠</summary>
    static public readonly string QB_INFO_COLUMN_PB = "��蒠";
    /// <summary>��蒠����</summary>
    static public readonly string QB_INFO_COLUMN_SUBTITLE = "��蒠����";
    /// <summary>�摜�t�@�C����</summary>
    static public readonly string QB_INFO_COLUMN_IMAGE_FILENAME = "�摜�t�@�C����";
    /// <summary>��胊�X�g</summary>
    static public readonly string QB_INFO_COLUMN_QL = "��胊�X�g";
    /// <summary>��胊�X�g_�t�@�C����</summary>
    static public readonly string QB_INFO_COLUMN_QL_FILE_NAME = "��胊�X�g_�t�@�C����";

    // QLInfo
    /// <summary>QLInfo.csv�̃p�X</summary>
    static public readonly string QL_INFO_PATH = "QLInfo";
    /// <summary>��胊�X�g</summary>
    static public readonly string QL_INFO_COLUMN_PL = "��胊�X�g";
    /// <summary>��胊�X�g_�t�@�C����</summary>
    static public readonly string QL_INFO_COLUMN_PL_FILENAME = "��胊�X�g_�t�@�C����";

    // ��胊�X�g
    /// <summary>��胊�X�g�t�H���_�[�̃p�X</summary>
    static public readonly string QL_FOLDER_PATH = "QL";
    /// <summary>���</summary>
    static public readonly string QL_COLUMN_PROBLEM = "���";
    /// <summary>�����v�[��</summary>
    static public readonly string QL_COLUMN_CORRECT_ANSWER_POOL = "�����v�[��";
    /// <summary>�ԈႢ�v�[��</summary>
    static public readonly string QL_COLUMN_WRONG_ANSWER_POOL = "�ԈႢ�v�[��";
    /// <summary>����䗦</summary>
    static public readonly string QL_COLUMN_CORRECT_WRONG_RATIO = "����䗦";
    // ���p�x���X�g
    /// <summary>���p�x���X�g.csv�̃p�X</summary>
    static public readonly string QUESTION_PROBABILITY_LIST = "QuestionProbabilityList";
    /// <summary>���</summary>
    static public readonly string QUESTIONFREQUENCYLIST_COLUMN_PROBLEM = "���";
    /// <summary>�o��m��</summary>
    static public readonly string QUESTIONFREQUENCYLIST_COLUMN_PROBLEM_FREQUENCY = "�o��m��";


    // --------------------
    // �Q�[���̐ݒ�
    // --------------------
    /// <summary>�o�萔</summary>
    static public readonly int QUESTION_QUANTITY = 10;
    /// <summary>�f�t�H���g����䗦</summary>
    static public readonly string DEFAULT_CORRECT_WRONG_RATIO = "1:3";
}
