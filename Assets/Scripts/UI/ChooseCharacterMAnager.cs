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
            Debug.Log("unlockingCharacter");
            GirlBlock.SetActive(false);
            return;
        }

        if (IsUnlocked== false)
        {
            Debug.Log("lockingcharacter");
            PlayerPrefs.SetInt("IsUnlocked", 0);
        }
    }

    public void ChooseBoy()
    {
        Boy.SetActive(true);
        ChooseUI.SetActive(false);
        Time.timeScale = 1f;
        _camera.Target.TrackingTarget = Boy.transform;
        _positionComposer.CameraDistance = 4.83f;
    }

    public void ChooseGirl()
    {
        Girl.SetActive(true);
        ChooseUI.SetActive(false);
        Time.timeScale = 1f;
        _camera.Target.TrackingTarget = Girl.transform;
        _positionComposer.CameraDistance = 7.3f;
    }

    public void UnlockGirl()
    {
        Debug.Log("unlock score reached");
        IsUnlocked = true;
        PlayerPrefs.SetInt("IsUnlocked", 1);
    }

}
