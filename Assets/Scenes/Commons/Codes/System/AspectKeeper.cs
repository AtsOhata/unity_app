using UnityEngine;

[ExecuteAlways]
public class AspectKeeper : MonoBehaviour
{
    [SerializeField]
    private Camera targetCamera;  // 対象とするカメラ

    [SerializeField]
    private Vector2 aspectVec; //目的解像度

    void Update()
    {
        float screenAspect = (float)Screen.width / (float)Screen.height; //画面のアスペクト比
        float targetAspect = (float)aspectVec.x / (float)aspectVec.y; //目的のアスペクト比

        float magRate = targetAspect / screenAspect; //目的アスペクト比にするための倍率

        Rect viewportRect = new Rect(0, 0, 1, 1); //Viewport初期値でRectを作成

        if (magRate < 1)
        {
            viewportRect.width = magRate; //使用する横幅を変更
            viewportRect.x = 0.5f - viewportRect.width * 0.5f;//中央寄せ
        }
        else
        {
            viewportRect.height = 1 / magRate; //使用する縦幅を変更
            viewportRect.y = 0.5f - viewportRect.height * 0.5f;//中央寄せ
        }

        targetCamera.rect = viewportRect; //カメラのViewportに適用
    }

}
