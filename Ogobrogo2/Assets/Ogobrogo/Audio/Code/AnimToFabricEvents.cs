﻿using Fabric;
using UnityEngine;

namespace ClubPenguin.Audio
{
    internal class AnimToFabricEvents : MonoBehaviour 
	{
#pragma warning disable 0649
        public bool Mute;
		public GameObject OverrideSoundSource;
#pragma warning restore 0649

        private GameObject getSoundSource()
		{
			return (OverrideSoundSource != null) ? OverrideSoundSource : gameObject;
		}

		public void FabricPlaySound(string name)
		{
			if (!Mute)
			{
				EventManager.Instance.PostEvent(name, EventAction.PlaySound, getSoundSource());
			}
		}

		public void FabricPauseSound(string name)
		{
			if (!Mute)
			{
				EventManager.Instance.PostEvent(name, EventAction.PauseSound, getSoundSource());
			}
		}

		public void FabricUnpauseSound(string name)
		{
			if (!Mute)
			{
				EventManager.Instance.PostEvent(name, EventAction.UnpauseSound, getSoundSource());
			}
		}

		public void FabricStopSound(string name)
		{
			if (!Mute)
			{
				EventManager.Instance.PostEvent(name, EventAction.StopSound, getSoundSource());
			}
		}

		public void FabricStopAllSound(string name)
		{
			if (!Mute)
			{
				EventManager.Instance.PostEvent(name, EventAction.StopAll, getSoundSource());
			}
		}

		public void FabricSetVolume(string name, float volume)
		{
			if (!Mute)
			{
				EventManager.Instance.PostEvent(name, EventAction.SetVolume, volume, getSoundSource());
			}
		}

		public void FabricSetPitch(string name, float pitch)
		{
			if (!Mute)
			{
				EventManager.Instance.PostEvent(name, EventAction.SetPitch, pitch, getSoundSource());
			}
		}
	}
}