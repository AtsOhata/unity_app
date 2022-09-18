using UnityEngine;
using UnityEngine.UI;

/// <summary>Home�V�[����Option��ʂ̉���</summary>
public class HomeOptionSound : MonoBehaviour, OptionInterface
{
    /// <summary>����</summary>
    Slider AmountSlider;

    public void Initialize()
    {
        AmountSlider = transform.Find("Amount").GetComponent<Slider>();
        AmountSlider.onValueChanged.AddListener(ee);
        AmountSlider.value = UserOption.Instance.SoundValue;
    }

    public void Act() { }

    /// <summary>�X���C�_�[�ŉ��ʒ��������ꍇ�ɔ������Đݒ�ۑ�</summary>
    void ee(float value)
    {
        UserOption.Instance.SoundValue = value;
        UserOption.Save();
    }

}
