using FMODUnity;
using System.Collections.Concurrent;
using Unity.Cinemachine;
using UnityEngine;

public class ChooseCharacterMAnager : MonoBehaviour
{
    public GameObject ChooseUI;
    public static ChooseCharacterMAnager instance;
    public bool IsUnlocked = false;
    public GameObject Boy;
    public GameObject Girl;
    public GameObject GirlBlock;
    public StudioEventEmitter StartSoundEmitter;
    public CinemachineCamera _camera;
    public CinemachinePositionComposer _positionComposer;

    private void Awake()
    {
        Time.timeScale = 0f;
    }
    private  void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        if (PlayerPrefs.GetInt("IsUnlocked") == 1)
        {
            GirlBlock.SetActive(false);
            return;
        }

        if (IsUnlocked== false)
        {
            PlayerPrefs.SetInt("IsUnlocked", 0);
        }
    }

    public void ChooseBoy()
    {
        Boy.SetActive(true);
        ChooseUI.SetActive(false);
        Time.timeScale = 1f;
        StartSoundEmitter.enabled = true;
        _camera.Target.TrackingTarget = Boy.transform;
        _positionComposer.CameraDistance = 4.83f;
    }

    public void ChooseGirl()
    {
        Girl.SetActive(true);
        ChooseUI.SetActive(false);
        Time.timeScale = 1f;
        StartSoundEmitter.enabled = true;
        _camera.Target.TrackingTarget = Girl.transform;
        _positionComposer.CameraDistance = 7.3f;
    }

    public void UnlockGirl()
    {
        IsUnlocked = true;
        PlayerPrefs.SetInt("IsUnlocked", 1);
    }

    public void ResteCharacters()
    {
        IsUnlocked = true;
        PlayerPrefs.SetInt("IsUnlocked", 0);
    }

}
