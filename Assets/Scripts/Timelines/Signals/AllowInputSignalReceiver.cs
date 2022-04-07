using UnityEngine;
using UnityEngine.Playables;

public class AllowInputSignalReceiver : MonoBehaviour, INotificationReceiver
{
    public void OnNotify(Playable origin, INotification notification, object context)
    {
        AllowInputMarker marker = notification as AllowInputMarker;
        if (marker != null)
        {
            //GameManager.Instance.Controller.SetAllowedInput(marker.allowedInput);
        }
    }
}
