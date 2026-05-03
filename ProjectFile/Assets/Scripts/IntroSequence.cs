using UnityEngine;
using DG.Tweening;

public class IntroSequence : MonoBehaviour
{

    public GameObject dropPod;
    public GameObject playerController;
    public GameObject scannerObject;

    void Start()
    {
        playerController.GetComponent<PlayerMovement>().enabled = false;
        scannerObject.SetActive(false);
        playerController.transform.DOMoveY(1.125f, 30, false)
        .SetEase(Ease.OutCubic);
        dropPod.transform.DOMoveY(0, 30, false)
        .SetEase(Ease.OutCubic)
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
        playerController.GetComponent<PlayerMovement>().enabled = true;
        scannerObject.SetActive(true);
    }
}
