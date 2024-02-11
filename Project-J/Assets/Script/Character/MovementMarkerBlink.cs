using DG.Tweening;
using UnityEngine;

public class PlayerMarkerBlink : MonoBehaviour
{
    [SerializeField] private GameObject playerAurora;
    [SerializeField] private SpriteRenderer playerAuroraSprite;

    private Sequence blinkSequence;

    private void Start()
    {
        blinkSequence = DOTween.Sequence();
        blinkSequence.AppendInterval(0.5f);
        blinkSequence.Join(playerAurora.transform.DOScale(3, 1));
        blinkSequence.Join(playerAuroraSprite.DOFade(0, 1));
        blinkSequence.SetLoops(-1);
    }

    private void OnDestroy()
    {
        if (blinkSequence != null)
            blinkSequence.Kill();
    }
}