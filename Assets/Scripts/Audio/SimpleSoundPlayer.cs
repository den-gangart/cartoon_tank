using UnityEngine;

public class SimpleSoundPlayer : MonoBehaviour
{
    public void PlayGameSound(string soundName)
    {
        AudioHandler.PlayGameSound(soundName, gameObject);
    }
}
