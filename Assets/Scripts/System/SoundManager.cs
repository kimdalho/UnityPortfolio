using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum eBGMType
{
    LobbySoundTrack = 1,
    GameSoundTrack = 2,
    BossTrack =3,
}


public enum eEffectType
{
    Hit = 1,
    Shoot = 2,
    Gameover = 3,
    Jump = 4,
    oop = 5,
    levelup = 6,
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField]   
    private List<AudioClip> gameSound;
    private Dictionary<eBGMType, AudioClip> dic_bgm;    
    public AudioSource audioSource;

    [SerializeField]
    private List<AudioClip> gameeffectSound;
    private Dictionary<eEffectType, AudioClip> dic_effect;
    [SerializeField]
    private List<SoundEffector> effectors;
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
        dic_bgm = new Dictionary<eBGMType, AudioClip> ();
        dic_effect = new Dictionary<eEffectType, AudioClip>();
        //열거타입 eBGMType를 배열로 가져온다.
        eBGMType[] types = (eBGMType[])Enum.GetValues(typeof(eBGMType));
        eEffectType[] types2 = (eEffectType[])Enum.GetValues(typeof(eEffectType));

        //배열의 요소값을 i로 가져온다.
        for (int i = 0; i < types.Length; i++)
        {           
            dic_bgm[types[i]] = gameSound[i];
        }

        //배열의 요소값을 i로 가져온다.
        for (int i = 0; i < types2.Length; i++)
        {
            dic_effect[types2[i]] = gameeffectSound[i];
        }

        audioSource.volume = PlayerPrefs.GetFloat("Prf_BGM", 1f);
        effectSoundVol = PlayerPrefs.GetFloat("Prf_effect", 1f);
    }

    public void PlayBGM(eBGMType sound)
    {
        audioSource.clip = dic_bgm[sound];
        audioSource.Play();
    }

    public void PlayEffect(eEffectType effect)
    {
        effectors[0].audioSource.clip = dic_effect[effect];
        effectors[0].audioSource.volume = effectSoundVol;
        effectors[0].audioSource.Play();
    }

    public void PlayEffect(eEffectType effect,Transform _transform)
    {
        effectors[0].audioSource.clip = dic_effect[effect];
        effectors[0].audioSource.volume = effectSoundVol;
        effectors[0].transform.position = _transform.position;
        effectors[0].audioSource.Play();
    }
}
