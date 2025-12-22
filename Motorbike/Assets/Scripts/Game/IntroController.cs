using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

namespace HazzardGameworks.ProjectInfrastructure.Game
{
    [RequireComponent (typeof (VideoPlayer))]
    public class IntroController : MonoBehaviour
    {
        //This classes controls playing and skipping the intro videos.

        //Intro videos to be played are stored in this array.
        [SerializeField]
        private VideoClip[] clips;
        //Used to determine which video clip in the array to play.
        [SerializeField]
        private int currentClipIndex = 0;
        //Reference to the video player component which plays the video
        //clips.
        [SerializeField]
        private VideoPlayer videoPlayer;

        //Volume of the audio source component associated with the video
        //player.
        [SerializeField]
        [Range (0f, 10f)]
        private float videoVolume;

        private void Start()
        {
            //Assign the video player component reference.
            videoPlayer = GetComponent<VideoPlayer>();
            //Choose a video using the array index.
            PlayVideo(currentClipIndex);
            //Subscribe to a delegate invoked by the video player component
            //when the current video clip ends.
            videoPlayer.loopPointReached += PlayNextVideo;
            
        }

        //Plays a video clip from the array based on the provided index.
        public void PlayVideo(int videoIndex)
        {
            //Makes sure there's an available video player component.
            if (videoPlayer != null)
            {
                //Checks that the provided index isn't outside the array.
                if(clips.Length !> 0)
                {
                    //Assigns the video clip to the video player component,
                    //sets the provided index as the current index,
                    //sets the audio source volume and play the clip.
                    videoPlayer.clip = clips[videoIndex];
                    currentClipIndex = videoIndex;
                    videoPlayer.SetDirectAudioVolume(0, videoVolume / 10);
                    videoPlayer.Play();
                }
                else
                {
                    //Otherwise, ends the intro.
                    Debug.LogWarning("No VideoClips.");
                    EndIntro();
                }
            }
            else
            {
                //Otherwise, ends the intro.
                Debug.LogWarning("No VideoPlayer.");
                EndIntro();
            }
        }
        //Receives a message from the PlayerInput component to skip
        //the current video.
        public void OnSkip()
        {
            SkipVideo();
        }
        //Skips the current video, ending the intro if there are no
        //more video clips to play.
        public void SkipVideo()
        {
            //Iterates the clip index.
            currentClipIndex++;

            if (currentClipIndex >= clips.Length)
            {
                //If the index is outside the array, ends the intro.
                EndIntro();
            }
            else
            {
                //Otherwise, plays the next intro video from the array.
                videoPlayer.Stop();
                PlayVideo(currentClipIndex);
            }
        }
        //Plays the next intro video
        public void PlayNextVideo(UnityEngine.Video.VideoPlayer vp)
        {
            SkipVideo();
        }
        //Ends the intro video scene.
        public void EndIntro()
        {
            Debug.Log("Intro ended.");
            SceneController.ChangeScene(3);
        }
        //Unsubscribes from the video player loop point delegate.
        private void OnDisable()
        {
            videoPlayer.loopPointReached -= PlayNextVideo;
        }
    }
}
