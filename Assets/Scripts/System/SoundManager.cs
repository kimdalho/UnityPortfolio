using System;
using System.Collections.Generic;
using UnityEngine;

public enum eBGMType
{
    LobbySoundTrack = 1,
    GameSoundTrack = 2,
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField]   
    private List<AudioClip> gameSound;

    private Dictionary<eBGMType, AudioClip> keyValuePairs;
    public AudioSource audioSource;

    public float effectSoundVol;


    

        

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Setup();
    }

    private void Setup()
    {
        keyValuePairs = new Dictionary<eBGMType, AudioClip> ();

        //열거타입 eBGMType를 배열로 가져온다.
        eBGMType[] types = (eBGMType[])Enum.GetValues(typeof(eBGMType));

        //배열의 요소값을 i로 가져온다.
        for (int i = 0; i < types.Length; i++)
        {           
            keyValuePairs[types[i]] = gameSound[i];
        }

        audioSource.volume = PlayerPrefs.GetFloat("Prf_BGM", 1f);
        effectSoundVol = PlayerPrefs.GetFloat("Prf_effect", 1f);
    }

    public void PlayBGM(eBGMType sound)
    {
        audioSource.clip = keyValuePairs[sound];
        audioSource.Play();
    }
}
