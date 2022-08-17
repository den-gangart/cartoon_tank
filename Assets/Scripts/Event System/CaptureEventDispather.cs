using UnityEngine;

[RequireComponent(typeof(BaseCapture))]
public class CaptureEventDispather : MonoBehaviour
{
    void Start()
    {
        EventSystem.Broadcast(EContentEventType.CaptureAdded, GetComponent<BaseCapture>());
    }
}
