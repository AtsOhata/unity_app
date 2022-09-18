using UnityEngine;

/// <summary>
/// �^�b�`��񓝊��N���X
/// </summary>
public class TouchData : MonoBehaviour
{
    /// <summary> �^�b�`�J�n���W </summary>
    public static Vector2 TouchStartCoordinates { get; private set; } = new Vector2();

    /// <summary> �^�b�`�I�����W </summary>
    public static Vector2 TouchEndCoordinates { get; private set; } = new Vector2();

    /// <summary> �O��^�b�`���W </summary>
    public static Vector2 LastTouchCoordinates { get; private set; } = new Vector2();

    /// <summary> ����^�b�`���W-�O��^�b�`���W </summary>
    public static Vector2 TouchDelta { get; private set; } = new Vector2();


    void Update()
    {
        //�^�b�`���̎擾
        if (TouchEvent.GetTouch() == TouchEvent.TouchInfo.Began) // �^�b�`�����u��
        {
            LastTouchCoordinates = TouchStartCoordinates = TouchEvent.GetTouchWorldPosition(Camera.main);
        }
        else if (TouchEvent.GetTouch() == TouchEvent.TouchInfo.Moved)  // ��������
        {
            Vector2 v = TouchEvent.GetTouchWorldPosition(Camera.main);
            TouchDelta = v - LastTouchCoordinates;
            LastTouchCoordinates = v;
        }
        else if (TouchEvent.GetTouch() == TouchEvent.TouchInfo.Ended)  // �^�b�`�I����
        {
            TouchEndCoordinates = TouchEvent.GetTouchWorldPosition(Camera.main);
            TouchDelta = new Vector2();
        }
    }
}
