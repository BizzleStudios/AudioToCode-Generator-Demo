using UnityEngine;

namespace AudioToCodeGeneratorStudio.Samples
{
    [RequireComponent(typeof(AudioSource))]
    public class BasicAudioExample : MonoBehaviour
    {
        [Header("Audio Settings")]
        public AudioSource audioSource;

        [Header("Clip Settings")]
        public int sampleRate = 44100;

        private AudioClip cachedJumpClip;
        private AudioClip cachedCoinClip;
        private string statusMessage = "Click a button to test procedural audio!";

        void Awake()
        {
            if (audioSource == null)
            {
                audioSource = GetComponent<AudioSource>();
            }

            cachedJumpClip = CreateProceduralJumpClip();
            cachedCoinClip = CreateProceduralCoinClip();
        }

        public void PlayJumpSound()
        {
            PlayClip(cachedJumpClip, "Playing Procedural Jump SFX!");
        }

        public void PlayCoinSound()
        {
            PlayClip(cachedCoinClip, "Playing Procedural Coin SFX!");
        }

        private void PlayClip(AudioClip clip, string message)
        {
            if (audioSource != null && clip != null)
            {
                audioSource.PlayOneShot(clip);
                statusMessage = message;
            }
            else
            {
                Debug.LogError("AudioSource or AudioClip is missing!");
            }
        }

        private AudioClip CreateProceduralJumpClip()
        {
            int totalSamples = (int)(sampleRate * 0.25f);
            float[] samples = new float[totalSamples];
            float phase = 0f;

            for (int i = 0; i < totalSamples; i++)
            {
                float t = (float)i / totalSamples;
                float frequency = Mathf.Lerp(180f, 650f, t);
                phase += 2f * Mathf.PI * frequency / sampleRate;
                samples[i] = Mathf.Sin(phase) * (1f - t) * 0.5f;
            }

            AudioClip clip = AudioClip.Create("Demo_JumpSFX", totalSamples, 1, sampleRate, false);
            clip.SetData(samples, 0);
            return clip;
        }

        private AudioClip CreateProceduralCoinClip()
        {
            int totalSamples = (int)(sampleRate * 0.35f);
            float[] samples = new float[totalSamples];
            float phase = 0f;

            for (int i = 0; i < totalSamples; i++)
            {
                float t = (float)i / totalSamples;
                float frequency = (t < 0.25f) ? 987.77f : 1318.51f;
                phase += 2f * Mathf.PI * frequency / sampleRate;

                float wave = Mathf.Sin(phase) > 0f ? 0.35f : -0.35f;
                samples[i] = wave * (1f - t);
            }

            AudioClip clip = AudioClip.Create("Demo_CoinSFX", totalSamples, 1, sampleRate, false);
            clip.SetData(samples, 0);
            return clip;
        }

        void OnGUI()
        {
            GUILayout.BeginArea(new Rect(20, 20, 280, 175), "Audio to Code Generator - Demo", GUI.skin.window);

            GUILayout.Space(10);

            GUIStyle labelStyle = new GUIStyle(GUI.skin.label) { wordWrap = true };
            GUILayout.Label(statusMessage, labelStyle);

            GUILayout.Space(10);

            if (GUILayout.Button("▶ Play Retro Jump (Code)", GUILayout.Height(35)))
            {
                PlayJumpSound();
            }

            if (GUILayout.Button("▶ Play Retro Coin (Code)", GUILayout.Height(35)))
            {
                PlayCoinSound();
            }

            GUILayout.EndArea();
        }
    }
}