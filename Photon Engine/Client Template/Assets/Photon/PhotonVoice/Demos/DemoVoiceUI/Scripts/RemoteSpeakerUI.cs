namespace Photon.Voice.Unity.Demos.DemoVoiceUI
{
    using ExitGames.Client.Photon;
    using Unity;
    using UnityEngine;
    using UnityEngine.UI;
    using Realtime;

    [RequireComponent(typeof(Speaker))]
    public class RemoteSpeakerUI : MonoBehaviour, IInRoomCallbacks
    {
        #pragma warning disable 649
        [SerializeField]
        private Text nameText;
        [SerializeField]
        protected Image remoteIsMuting;
        [SerializeField]
        private Image remoteIsTalking;
        [SerializeField]
        private InputField playbackDelayInputField;
        [SerializeField]
        private Text bufferLagText;
        #pragma warning restore 649
        protected Speaker speaker;

        protected VoiceConnection voiceConnection;

        protected virtual void Start()
        {
            this.speaker = this.GetComponent<Speaker>();
            this.playbackDelayInputField.text = this.speaker.PlayDelayMs.ToString();
            this.playbackDelayInputField.SetSingleOnEndEditCallback(this.OnPlaybackDelayChanged);
            this.SetNickname();
            this.SetMutedState();
        }

        private void OnPlaybackDelayChanged(string newPlaybackDelayString)
        {
            int newPlaybackDelayValue;
            if (int.TryParse(newPlaybackDelayString, out newPlaybackDelayValue) && newPlaybackDelayValue > 0)
            {
                this.speaker.PlayDelayMs = newPlaybackDelayValue;
            }
            else
            {
                this.playbackDelayInputField.text = this.speaker.PlayDelayMs.ToString();
            }
        }

        private void Update()
        {
            // TODO: It would be nice, if we could show if a user is actually talking right now (Voice Detection)
            this.remoteIsTalking.enabled = this.speaker.IsPlaying;
            this.bufferLagText.text = string.Concat("Buffer Lag: ", this.speaker.Lag);
        }

        private void OnDestroy()
        {
            this.voiceConnection.Client.RemoveCallbackTarget(this);
        }

        private void SetNickname()
        {
            string nick = this.speaker.name;
            if (this.speaker.Actor != null)
            {
                nick = this.speaker.Actor.NickName;
                if (string.IsNullOrEmpty(nick))
                {
                    nick = string.Concat("user ", this.speaker.Actor.ActorNumber);
                }
            }
            this.nameText.text = nick;
        }

        private void SetMutedState()
        {
            this.SetMutedState(this.speaker.Actor.IsMuted());
        }

        protected virtual void SetMutedState(bool isMuted)
        {
            this.remoteIsMuting.enabled = isMuted;
        }

        protected virtual void OnActorPropertiesChanged(Player targetPlayer, Hashtable changedProps)
        {
            if (targetPlayer.ActorNumber == this.speaker.Actor.ActorNumber)
            {
                this.SetMutedState();
                this.SetNickname();
            }
        }

        public virtual void Init(VoiceConnection vC)
        {
            this.voiceConnection = vC;
            this.voiceConnection.Client.AddCallbackTarget(this);
        }

        #region IInRoomCallbacks

        void IInRoomCallbacks.OnPlayerEnteredRoom(Player newPlayer)
        {
        }

        void IInRoomCallbacks.OnPlayerLeftRoom(Player otherPlayer)
        {
        }

        void IInRoomCallbacks.OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
        {
        }

        void IInRoomCallbacks.OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
        {
            this.OnActorPropertiesChanged(targetPlayer, changedProps);
        }

        void IInRoomCallbacks.OnMasterClientSwitched(Player newMasterClient)
        {
        }

        #endregion
    }
}