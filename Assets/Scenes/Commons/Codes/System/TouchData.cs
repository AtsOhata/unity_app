using UnityEngine;

/// <summary>
/// タッチ情報統括クラス
/// </summary>
public class TouchData : MonoBehaviour
{
    /// <summary> タッチ開始座標 </summary>
    public static Vector2 TouchStartCoordinates { get; private set; } = new Vector2();

    /// <summary> タッチ終了座標 </summary>
    public static Vector2 TouchEndCoordinates { get; private set; } = new Vector2();

    /// <summary> 前回タッチ座標 </summary>
    public static Vector2 LastTouchCoordinates { get; private set; } = new Vector2();

    /// <summary> 今回タッチ座標-前回タッチ座標 </summary>
    public static Vector2 TouchDelta { get; private set; } = new Vector2();


    void Update()
    {
        //タッチ情報の取得
        if (TouchEvent.GetTouch() == TouchEvent.TouchInfo.Began) // タッチした瞬間
        {
            LastTouchCoordinates = TouchStartCoordinates = TouchEvent.GetTouchWorldPosition(Camera.main);
        }
        else if (TouchEvent.GetTouch() == TouchEvent.TouchInfo.Moved)  // 動いた時
        {
            Vector2 v = TouchEvent.GetTouchWorldPosition(Camera.main);
            TouchDelta = v - LastTouchCoordinates;
            LastTouchCoordinates = v;
        }
        else if (TouchEvent.GetTouch() == TouchEvent.TouchInfo.Ended)  // タッチ終了時
        {
            TouchEndCoordinates = TouchEvent.GetTouchWorldPosition(Camera.main);
            TouchDelta = new Vector2();
        }
    }
}
