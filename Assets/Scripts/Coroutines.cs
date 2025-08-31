using System.Collections;
using UnityEngine;

public static class Coroutines
{
    public static IEnumerator Shake(Transform target, Vector3 originalPosition, float duration = 0.25f, float amplitude = 0.25f, float frequency = 4f)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            float angle = t * duration * frequency * Mathf.PI * 2f;
            float x = Mathf.Sin(angle) * amplitude;
            target.localPosition = new Vector3(originalPosition.x + x, originalPosition.y, originalPosition.z);
            elapsed += Time.deltaTime;
            yield return null;
        }

        target.localPosition = originalPosition;
    }

    public static IEnumerator ShiftForward(Transform target, Vector3 originalPosition, float duration = 0.25f, float amplitude = 0.25f)
    {
        float elapsed = 0f;
        float phase = Mathf.PI;

        while (elapsed < duration)
        {
            float angle = elapsed * Mathf.PI * 2f + phase;
            float y = Mathf.Sin(angle) * amplitude;

            target.localPosition = new Vector3(
                originalPosition.x,
                originalPosition.y + y,
                originalPosition.z
            );

            elapsed += Time.deltaTime;
            yield return null;
        }

        target.localPosition = originalPosition;
    }

    public static IEnumerator FadeRedBlack(SpriteRenderer spriteRenderer, float duration = 1f, float frequency = 4f)
    {
        Color red = Color.red;
        Color black = Color.black;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            float wave = (Mathf.Sin(elapsed * frequency * Mathf.PI * 2f) + 1f) / 2f;
            spriteRenderer.color = Color.Lerp(black, red, wave);
            elapsed += Time.deltaTime;
            yield return null;
        }

        spriteRenderer.color = Color.red;
    }
}
