using System.Collections;
using UnityEngine;
using TMPro;

public class PowerupText : MonoBehaviour
{
    public TextMeshProUGUI text;

    [SerializeField] private float holdDuration = 0.8f;
    [SerializeField] private float fadeDuration = 0.4f;
    [SerializeField] private float scaleAmount = 1.3f;

    private Coroutine activeCoroutine;

    public void Show(string message, Color color)
    {
        if (activeCoroutine != null)
            StopCoroutine(activeCoroutine);
        gameObject.SetActive(true);
        activeCoroutine = StartCoroutine(Animate(message, color));
    }

    IEnumerator Animate(string message, Color color)
    {
        text.text = message;
        text.color = color;
        transform.localScale = Vector3.one * scaleAmount;

        yield return new WaitForSeconds(holdDuration);

        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float a = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
            text.color = new Color(color.r, color.g, color.b, a);
            transform.localScale = Vector3.one * Mathf.Lerp(scaleAmount, 1f, elapsed / fadeDuration);
            yield return null;
        }

        gameObject.SetActive(false);
    }
}
