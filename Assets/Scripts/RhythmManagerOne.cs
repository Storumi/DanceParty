using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RhythmManagerOne : MonoBehaviour {
    public static RhythmManagerOne rm1 { get; private set; }
    public List<FloorTileScript> floorTiles = new List<FloorTileScript>();
    public List<PlayerScript> playerScripts = new List<PlayerScript>();
    [SerializeField] public List<Color> colorList;
    [SerializeField] AK.Wwise.Event musicEvent;
    uint playingID;

    [SerializeField] float beatDuration;
    [SerializeField] float barDuration;
    [SerializeField] bool durationSet = false;


    #region Additional Parameters
    [SerializeField] SpawnObjectOne spawnObject;

    [SerializeField] Transform spawnLocation;

    public int OkWindowMillis = 200;
    public int GoodWindowMillis = 100;
    public int PerfectWindowMillis = 50;

    // public List<SpawnObjectOne> spawnObjectList = new List<SpawnObjectOne>();

    // [SerializeField] SpriteRenderer testCirlce;

    #endregion

    #region Setup
    void Awake()
    {
        // Check if an instance already exists
        if (rm1 != null && rm1 != this)
        {
            Debug.LogWarning("Another instance of rm1 found, destroying this one.");
            Destroy(gameObject); // Destroy this duplicate instance
        }
        else
        {
            // Assign the instance to this script if it doesn't exist
            rm1 = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
    }

    private void Start() {
        playingID = musicEvent.Post(gameObject, 
            (uint)(AkCallbackType.AK_MusicSyncAll | AkCallbackType.AK_EnableGetMusicPlayPosition 
                                                  | AkCallbackType.AK_MusicSyncUserCue 
                                                  | AkCallbackType.AK_MIDIEvent), CallbackFunction);
    }
    #endregion


    void CallbackFunction(object in_cookie, AkCallbackType in_type, AkCallbackInfo in_info) {
        AkMusicSyncCallbackInfo musicInfo;
        AkMIDIEventCallbackInfo midiInfo;

        if (in_info is AkMusicSyncCallbackInfo) {
            musicInfo = (AkMusicSyncCallbackInfo)in_info;

            switch (in_type) {

                case AkCallbackType.AK_MusicSyncUserCue:
                    //Debug.Log("My User Cue");
                    //Debug.Log($"This cue has the name: {musicInfo.userCueName}");
                    //ManageUserCue(musicInfo.userCueName);
                   break;

                case AkCallbackType.AK_MusicSyncBeat:
                    //HERE IS WHERE YOU CAN DO SOMETHING ON THE BEAT
                    OnTheBeat();
                    break;

                case AkCallbackType.AK_MusicSyncBar:
                    //HERE IS WHERE YOU CAN DO SOMETHING ON THE BAR
                    OnTheBar();
                    break;
            }

            if (in_type is AkCallbackType.AK_MusicSyncBar) {
                if (!durationSet) {
                    beatDuration = musicInfo.segmentInfo_fBeatDuration;
                    barDuration = musicInfo.segmentInfo_fBarDuration;
                    durationSet = true;
                }
            }
        }


        if (in_info is AkMIDIEventCallbackInfo) {
            midiInfo = (AkMIDIEventCallbackInfo)in_info;
            Debug.Log($"Midi Info Callback }} Note: {midiInfo.byOnOffNote}");

            switch (midiInfo.byType) {
                case AkMIDIEventTypes.NOTE_ON:
                    NoteOnEvents(midiInfo);
                    break;

                case AkMIDIEventTypes.NOTE_OFF:
                    
                    break;

                case AkMIDIEventTypes.PITCH_BEND:
                    break;
            }
        }
    }


    void NoteOnEvents(AkMIDIEventCallbackInfo b) {
        Debug.Log($"Note: {b.byOnOffNote}");

        switch (b.byOnOffNote) {



        }

    }

    void ManageUserCue(string s) {
        switch (s) {
            case "A":
                Debug.Log("Looks like we're at A!");
                break;
            case "B":
                Debug.Log("It's a B! Run!");
                break;
            case "C":
                Debug.Log("C you later!!");
                break;
        }
    }

    /// <summary>
    /// Here is an example function of a place where you can do something that reacts on the beat
    /// </summary>
    void OnTheBeat() {
        Debug.Log("Here is a beat event!");
        
        foreach (var tile in floorTiles)
        {
            tile.CycleColor();
        }
        
        foreach (var player in playerScripts)
        {
            player.GrantMove();
        }
        
        //testCirlce.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
    }

    /// <summary>
    /// Here is an example function of a place where you can do something that reacts on the bar
    /// </summary>
    void OnTheBar() {
        Debug.Log("Here is a bar event!");
        //testCirlce.transform.localScale = new Vector3(Random.Range(2.0f, 4.0f), Random.Range(2.0f, 4.0f), Random.Range(0.0f, 1.0f));
    }

    #region Other
    public int GetMusicTimeInMS() {
        AkSegmentInfo segmentInfo = new AkSegmentInfo();
        AkSoundEngine.GetPlayingSegmentInfo(playingID, segmentInfo, true);
        return segmentInfo.iCurrentPosition;
    }

    //We're going to call this when we spawn a gem, in order to determine when it's crossing time should be
    //Crossing time is based on the current playback position, our beat duration, and our beat offset
    // public int SetCrossingTimeInMS(int beatOffset) {
    //     AkSegmentInfo segmentInfo = new AkSegmentInfo();
    //     AkSoundEngine.GetPlayingSegmentInfo(playingID, segmentInfo, true);
    //     int offsetTime = Mathf.RoundToInt(1000 * beatDuration * beatOffset);
    //     //Debug.Log($"Crossing time: {segmentInfo.iCurrentPosition + offsetTime}");
    //     return segmentInfo.iCurrentPosition + offsetTime;
    // }

    //void SpawnItem(AkMusicSyncCallbackInfo info) {
    //    int spawnTime = GetMusicTimeInMS();
    //    SpawnObjectOne s = Instantiate(spawnObject, new Vector3(spawnLocation.position.x, 0.0f, spawnLocation.position.z), Quaternion.identity);
    //    s.SetSpawnItemInfo(this, spawnTime, SetCrossingTimeInMS(1));
    //    s.name = $"{s.name} | {spawnTime}";
    //    spawnObjectList.Insert(0, s);
    //}
    #endregion

}

public enum HitTiming { None, Perfect, Good, Okay, Late, Miss }