using UnityEngine;
using DG.Tweening;

public class IntroSequence : MonoBehaviour
{

    public GameObject dropPod;
    public GameObject playerController;
    public Rigidbody playerRigidbody;

    void Start()
    {
        playerController.transform.DOMoveY(1.125f, 20, false)
        .SetEase(Ease.Linear);
        dropPod.transform.DOMoveY(0, 20, false)
        .SetEase(Ease.Linear)
        .OnComplete(CutsceneEnd);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            DOTween.CompleteAll(true);
        }
    }

    public void CutsceneEnd()
    {
        //Allow player movement, unlock camera etc.
    }
}
