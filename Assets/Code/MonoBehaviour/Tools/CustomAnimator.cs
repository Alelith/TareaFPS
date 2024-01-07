using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomAnimator : MonoBehaviour
{
    [SerializeField]
    private Image gunImage;
    [SerializeField]
    private List<Sprite> frames;
    [SerializeField]
    private float frameRate;

    public List<Sprite> Frames { get => frames; }

    public void Play()
    {
        StartCoroutine(Animate());
    }

    private IEnumerator Animate()
    {
        foreach (Sprite sprite in frames)
        {
            gunImage.sprite = sprite;
            yield return new WaitForSeconds(frameRate);
        }
        gunImage.sprite = frames[0];
    }

    public void SetupAnimator(List<Sprite> sprites)
    {
        this.frames = sprites;
        gunImage.sprite = frames[0];
    }

    private void Start()
    {
        gunImage.sprite = frames[0];
    }
}
