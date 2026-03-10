using System.Collections;
using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour
{
    [SerializeField] private float duration = 1.0f;
    [SerializeField] private float riseSpeed = 2.0f;
    [SerializeField] private float startSize = 0.3f;
    [SerializeField] private float endSize = 1.2f;

    private TextMeshPro tmp;

    void Awake()
    {
        tmp = GetComponent<TextMeshPro>();
    }

    public void SetText(string text)
    {
        tmp.text = "<color=green>" + text + "</color>";
        StartCoroutine(Animate());
    }

    IEnumerator Animate()
    {
        float elapsed = 0f;
        Color color = tmp.color;
        transform.localScale = Vector3.one * startSize;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            transform.localScale = Vector3.one * Mathf.Lerp(startSize, endSize, t);
            color.a = Mathf.Lerp(1f, 0f, t);
            tmp.color = color;
            transform.position += Vector3.up * riseSpeed * Time.deltaTime;

            // Face the camera
            transform.rotation = Camera.main.transform.rotation;

            yield return null;
        }

        Destroy(gameObject);
    }
}
