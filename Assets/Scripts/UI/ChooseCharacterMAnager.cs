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
    public GameObject GirlUI;
    public GameObject BoyUI;
    public StudioEventEmitter StartSoundEmitter;
    public CinemachineCamera _camera;
    public CinemachinePositionComposer _positionComposer;
    private float Dist = 4.83f; // boy 4.83 girl 7.3
    private GameObject Char;

    private void Awake()
    {
        Time.timeScale = 0f;
    }
    private  void Start()
    {
        Char = Boy;
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

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space) && ChooseUI.activeInHierarchy) 
        {
            if (Char != null)
            {
                ConfirmChoice(Char, Dist);
            }
        }
    }
    public void ChooseBoy()
    {
        BoyUI.SetActive(true); 
        GirlUI.SetActive(false);
        Char = Boy;
        Dist = 4.83f;
    }

    public void ChooseGirl()
    {
        GirlUI.SetActive(true);
        BoyUI.SetActive(false);
        Char = Girl;
        Dist = 7.3f;
    }

    public void ConfirmChoice(GameObject character,float cameraDistance)
    {
        character.SetActive(true);
        ChooseUI.SetActive(false);
        Time.timeScale = 1f;
        StartSoundEmitter.enabled = true;
        _camera.Target.TrackingTarget =character.transform;
        _positionComposer.CameraDistance = cameraDistance;
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
