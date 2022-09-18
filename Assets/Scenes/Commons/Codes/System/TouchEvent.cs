using UnityEngine;

public class TouchEvent : MonoBehaviour
{

    /// <summary> �^�b�`���W </summary>
    private static Vector3 TouchPosition = Vector3.zero;


    /// <summary>
    /// �^�b�`���BUnityEngine.TouchPhase �� None �̏���ǉ��g���B
    /// </summary>
    public enum TouchInfo
    {
        /// <summary>
        /// �^�b�`�Ȃ�
        /// </summary>
        None = 99,

        // �ȉ��� UnityEngine.TouchPhase �̒l�ɑΉ�
        /// <summary>
        /// �^�b�`�J�n
        /// </summary>
        Began = 0,
        /// <summary>
        /// �^�b�`�ړ�
        /// </summary>
        Moved = 1,
        /// <summary>
        /// �^�b�`�Î~
        /// </summary>
        Stationary = 2,
        /// <summary>
        /// �^�b�`�I��
        /// </summary>
        Ended = 3,
        /// <summary>
        /// �^�b�`�L�����Z��
        /// </summary>
        Canceled = 4,
    }


    // �G�f�B�^�[�œ��삷��
    static private bool IsEditor = true;
    /// <summary>
    /// 
    /// �^�b�`�����擾(�G�f�B�^�Ǝ��@���l��)
    /// </summary>
    /// <returns>�^�b�`���B�^�b�`����Ă��Ȃ��ꍇ�� null</returns>
    public static TouchInfo GetTouch()
    {
        if (IsEditor)
        {
            if (Input.GetMouseButtonDown(0)) { return TouchInfo.Began; }
            if (Input.GetMouseButton(0)) { return TouchInfo.Moved; }
            if (Input.GetMouseButtonUp(0)) { return TouchInfo.Ended; }
        }
        else
        {
            if (Input.touchCount > 0)
            {
                return (TouchInfo)((int)Input.GetTouch(0).phase);
            }
        }
        return TouchInfo.None;
    }

    /// <summary>
    /// �^�b�`�|�W�V�������擾(�G�f�B�^�Ǝ��@���l��)
    /// </summary>
    /// <returns>�^�b�`�|�W�V�����B�^�b�`����Ă��Ȃ��ꍇ�� (0, 0, 0)</returns>
    public static Vector3 GetTouchPosition()
    {
        if (IsEditor)
        {
            TouchInfo touch = GetTouch();
            if (touch != TouchInfo.None) { return Input.mousePosition; }
        }
        else
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                TouchPosition.x = touch.position.x;
                TouchPosition.y = touch.position.y;
                return TouchPosition;
            }
        }
        return Vector3.zero;
    }

    /// <summary>
    /// �^�b�`���[���h�|�W�V�������擾(�G�f�B�^�Ǝ��@���l��)
    /// </summary>
    /// <param name='camera'>�J����</param>
    /// <returns>�^�b�`���[���h�|�W�V�����B�^�b�`����Ă��Ȃ��ꍇ�� (0, 0, 0)</returns>
    public static Vector3 GetTouchWorldPosition(Camera camera)
    {
        return camera.ScreenToWorldPoint(GetTouchPosition());
    }



}
