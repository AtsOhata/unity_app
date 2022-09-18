using UnityEngine;
using UnityEngine.UI;

/// <summary>HomeシーンのOption画面の音量</summary>
public class HomeOptionSound : MonoBehaviour, OptionInterface
{
    /// <summary>音量</summary>
    Slider AmountSlider;

    public void Initialize()
    {
        AmountSlider = transform.Find("Amount").GetComponent<Slider>();
        AmountSlider.onValueChanged.AddListener(ee);
        AmountSlider.value = UserOption.Instance.SoundValue;
    }

    public void Act() { }

    /// <summary>スライダーで音量調整した場合に反応して設定保存</summary>
    void ee(float value)
    {
        UserOption.Instance.SoundValue = value;
        UserOption.Save();
    }

}
