using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CaptureProgressBar : MonoBehaviour
{
    [SerializeField] private GameObject _anchor;
    [SerializeField] private Image _progressBar;
    [SerializeField] private float _resetWinTime;
    private BaseCapture _baseCapture;

    private void Awake()
    {
        EventSystem.AddEventListener<BaseCapture>(EContentEventType.CaptureAdded, OnCaptureAdded);
    }

    private void Start()
    {
        ResetBar();
    }

    private void OnCaptureAdded(BaseCapture baseCapture)
    {
        _baseCapture = baseCapture;
        baseCapture.CaptureStarted += OnCaptureStarted;
        baseCapture.Capture += OnCaptureProgress;
        baseCapture.CaptureFailed += OnCaptureFailed;
        baseCapture.CaptureCompleted += OnCaptureCompleted;
    }

    private void OnCaptureStarted()
    {
        _anchor.SetActive(true);
    }

    private void OnCaptureProgress(float progressAmount)
    {
        _progressBar.fillAmount = progressAmount;
    }

    private void OnCaptureFailed()
    {
        ResetBar();
    }

    private void OnCaptureCompleted()
    {
        StartCoroutine(FinishCapture());
    }

    private IEnumerator FinishCapture()
    {
        yield return new WaitForSeconds(_resetWinTime);
        ResetBar();
    }

    private void ResetBar()
    {
        _anchor.SetActive(false);
        _progressBar.fillAmount = 0;
    }

    private void OnDestroy()
    {
        EventSystem.RemoveEventListener<BaseCapture>(EContentEventType.CaptureAdded, OnCaptureAdded);

        if(_baseCapture != null)
        {
            _baseCapture.CaptureStarted -= OnCaptureStarted;
            _baseCapture.Capture -= OnCaptureProgress;
            _baseCapture.CaptureFailed -= OnCaptureFailed;
            _baseCapture.CaptureCompleted -= OnCaptureCompleted;
        }
    }
}
