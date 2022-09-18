using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PleaseWait : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(ColorCoroutine());  // �_�ł�����
    }

    /// <summary>
    /// �_�ł�����
    /// </summary>
    IEnumerator ColorCoroutine()
    {
        Color color = gameObject.GetComponent<Text>().color;
        while (true)
        {
            yield return new WaitForEndOfFrame();

            color.a = Mathf.Sin(Time.time * 3) / 2 + 0.5f;

            gameObject.GetComponent<Text>().color = color;
        }
    }
}
