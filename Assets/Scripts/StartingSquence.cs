using UnityEngine;
using TMPro;
using System.Collections;

public class StartingSquence : MonoBehaviour
{
    public TextMeshProUGUI CounterUI;
    public MusicPlayer Mplayer;
    public bool StartSequenceEnded;
    void Start()
    {
        StartCoroutine(StartingSequence());
    }
    public IEnumerator StartingSequence()
    {
        Debug.Log("TIme before " + Time.deltaTime);
        Time.timeScale = 0f;
        CounterUI.text = "3";
        yield return new WaitForSecondsRealtime(1);
        CounterUI.text = "2";
        yield return new WaitForSecondsRealtime(1);
        CounterUI.text = "1";
        yield return new WaitForSecondsRealtime(1);
        CounterUI.text = "0";
        yield return new WaitForSecondsRealtime(1);
        CounterUI.text = "";
        Time.timeScale = 1f;
        yield return null;
        yield return null;
        Mplayer.PlayRandomTrack();
        Enemy_Mouvement.soundInstance.start();
    }
}
