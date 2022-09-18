using UnityEngine;

public class TouchDetect : MonoBehaviour
{

    /// <summary> �^�b�`�󂯕t���̈捶����W </summary>
    public Vector2 LeftTopLimit;

    /// <summary> �^�b�`�󂯕t���̈�E�����W </summary>
    public Vector2 RightBottomLimit;

    SpriteRenderer sr;
    RectTransform rt;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>(); // SpriteRenderer ������ꍇ����Ŕ���
        rt = GetComponent<RectTransform>();  // RectTransform ������ꍇ����Ŕ���
    }

    /// <summary>�^�b�`����</summary>
    public bool DetectTouch()
    {
        if (TouchEvent.GetTouch() == TouchEvent.TouchInfo.Ended)  // �^�b�`�I����
        {
            // �^�b�`�n�_�ƏI�_����������Ă��鎞����
            if (Vector2.Distance(TouchData.TouchStartCoordinates, TouchData.TouchEndCoordinates) > 5f) return false;

            // �ȉ��^�b�`�I�_�݂̂Ŕ���

            // ���ƍ����̎擾
            float width, height;
            if (sr != null) { width = sr.bounds.size.x;  height = sr.bounds.size.y; }
            else if(rt != null) { width = rt.rect.width * rt.localScale.x; ;  height = rt.rect.height * rt.localScale.y; }
            else { width = transform.lossyScale.x;  height = transform.lossyScale.y; }
            
            // ������W(v1)�ƉE�����W(v2)�̎擾
            Vector2 v1 = new Vector2(transform.position.x - width / 2f, transform.position.y + height / 2f);
            Vector2 v2 = new Vector2(transform.position.x + width / 2f, transform.position.y - height / 2f);

            // �^�b�`�󂯕t���̈捶����W�ƃ^�b�`�󂯕t���̈�E�����W���K�؂ɐݒ肳��Ă���Ƃ��^�b�`���W�����̗̈�����łȂ���Βe��
            if (LeftTopLimit.x < RightBottomLimit.x
                && LeftTopLimit.y > RightBottomLimit.y)
            {
                if (TouchData.TouchEndCoordinates.x < LeftTopLimit.x
                    || TouchData.TouchEndCoordinates.x > RightBottomLimit.x
                    || TouchData.TouchEndCoordinates.y > LeftTopLimit.y
                    || TouchData.TouchEndCoordinates.y < RightBottomLimit.y)
                    return false;
            }
            // �Ώۂ��^�b�`����Ă��邩�ǂ�������
            if (TouchData.TouchEndCoordinates.x > v1.x
                && TouchData.TouchEndCoordinates.x < v2.x
                && TouchData.TouchEndCoordinates.y < v1.y
                && TouchData.TouchEndCoordinates.y > v2.y
                )
                return true;
        }

        return false;
    }

}
