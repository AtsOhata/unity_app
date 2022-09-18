using UnityEngine;

public class TouchDetect : MonoBehaviour
{

    /// <summary> タッチ受け付け領域左上座標 </summary>
    public Vector2 LeftTopLimit;

    /// <summary> タッチ受け付け領域右下座標 </summary>
    public Vector2 RightBottomLimit;

    SpriteRenderer sr;
    RectTransform rt;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>(); // SpriteRenderer がある場合それで判定
        rt = GetComponent<RectTransform>();  // RectTransform がある場合それで判定
    }

    /// <summary>タッチ判定</summary>
    public bool DetectTouch()
    {
        if (TouchEvent.GetTouch() == TouchEvent.TouchInfo.Ended)  // タッチ終了時
        {
            // タッチ始点と終点がかけ離れている時無視
            if (Vector2.Distance(TouchData.TouchStartCoordinates, TouchData.TouchEndCoordinates) > 5f) return false;

            // 以下タッチ終点のみで判定

            // 幅と高さの取得
            float width, height;
            if (sr != null) { width = sr.bounds.size.x;  height = sr.bounds.size.y; }
            else if(rt != null) { width = rt.rect.width * rt.localScale.x; ;  height = rt.rect.height * rt.localScale.y; }
            else { width = transform.lossyScale.x;  height = transform.lossyScale.y; }
            
            // 左上座標(v1)と右下座標(v2)の取得
            Vector2 v1 = new Vector2(transform.position.x - width / 2f, transform.position.y + height / 2f);
            Vector2 v2 = new Vector2(transform.position.x + width / 2f, transform.position.y - height / 2f);

            // タッチ受け付け領域左上座標とタッチ受け付け領域右下座標が適切に設定されているときタッチ座標がその領域内かでなければ弾く
            if (LeftTopLimit.x < RightBottomLimit.x
                && LeftTopLimit.y > RightBottomLimit.y)
            {
                if (TouchData.TouchEndCoordinates.x < LeftTopLimit.x
                    || TouchData.TouchEndCoordinates.x > RightBottomLimit.x
                    || TouchData.TouchEndCoordinates.y > LeftTopLimit.y
                    || TouchData.TouchEndCoordinates.y < RightBottomLimit.y)
                    return false;
            }
            // 対象がタッチされているかどうか判定
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
