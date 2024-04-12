using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    [SerializeField] private AudioClipRefsSO audioClipRefsSO;
    private float volume=1f;
    private const string PLAYER_PREFS_SOUND_VOLUME = "PlayerPrefsSoundVolume";
    private void Awake()
    {
        Instance = this;
        volume=PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_VOLUME,1f);
    }
    private void Start()
    {

        DeliveryManage.instance.OnDeliverySuccess += Delivery_OnDeliverySuccess;
        DeliveryManage.instance.OnDeliveryFail += Delivery_OnDeliveryFail;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        BaseCounter.OnObjectPlace += BaseCounter_OnObjectPlace;
        Player.Intance.OnObjectPickUp += Player_OnObjectPickUp;
        TrashCounter.OnTrash += TrashCounter_OnTrash;
    }

    private void TrashCounter_OnTrash(object sender, System.EventArgs e)
    {
        TrashCounter trashCounter=sender as TrashCounter;
        PlaySound(audioClipRefsSO.trash, trashCounter.transform.position);

    }

    private void Player_OnObjectPickUp(object sender, System.EventArgs e)
    {

        PlaySound(audioClipRefsSO.ObjectPickup, Player.Intance.transform.position);
    }

    private void BaseCounter_OnObjectPlace(object sender, System.EventArgs e)
    {
        BaseCounter baseCounter=sender as BaseCounter;
        PlaySound(audioClipRefsSO.objectDrop, baseCounter.transform.position);
    }

    private void CuttingCounter_OnAnyCut(object sender, System.EventArgs e)
    {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySound(audioClipRefsSO.chop, cuttingCounter.transform.position);
    }

    private void Delivery_OnDeliveryFail(object sender, System.EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipRefsSO.deliveryFail,deliveryCounter.transform.position);
    }

    private void Delivery_OnDeliverySuccess(object sender, System.EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;

        PlaySound(audioClipRefsSO.deliverySuccess, deliveryCounter.transform.position);

    }
    public void PlaySoundFootstep(Player player, float volume = 1f)
    {
        PlaySound(audioClipRefsSO.footstep, player.transform.position,volume);

    }
    public void PlaySound(AudioClip[] audioClip, Vector3 position, float volume = 1f)
    {
        PlaySound(audioClip[Random.Range(0,audioClip.Length)], position, volume);
    }
    public void PlaySound(AudioClip audioClip,Vector3 position,float volumeModified=1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volumeModified*volume);
    }
    public void InscreaseSoundVolume()
    {
        volume += .1f;
        if(volume > 1f)
        {
            volume = 0f;
        }
        PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_VOLUME, volume);
        PlayerPrefs.Save();
        Debug.Log(PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_VOLUME));
    }
    public float GetVolume() { return volume; } 
}
