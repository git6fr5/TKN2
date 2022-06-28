/* --- Libraries --- */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TKN2;

namespace TKN2 {

    public class Twine : MonoBehaviour {

        [SerializeField] private bool m_PlayOnStart;
        [SerializeField] private Prompt m_Prompt;
        [SerializeField] private Response[] m_Responses;

        // The current text being presented.
        [SerializeField, ReadOnly] private string m_Text;
        public string PrintText => m_Text;

        void Start() {
            if (m_PlayOnStart) {
                Play();
            }
        }

        public void Play() {
            StartCoroutine(IERead());
        }

        // Read the prompt out over time.
        private IEnumerator IERead() {
            // Set the chat to follow this prompt.
            ChatUI.SetTwine(this);

            // Read through the lines in the prompt.
            for (int i = 0; i < m_Prompt.Lines.Length; i++) {
                
                if (i != 0) {
                    // Wait for a click to start the next line.
                    yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

                }
                
                // Print the line letter by letter.
                m_Text = "";
                for (int j = 0; j < m_Prompt.Lines[i].Length; j++) {
                    m_Text += m_Prompt.Lines[i][j];
                    if (j % 2 == 0) {
                        SoundManager.PlaySound(ChatUI.MainChatUI.PrintLetterSoundA);
                    }
                    else {
                        SoundManager.PlaySound(ChatUI.MainChatUI.PrintLetterSoundB);
                    }
                    yield return new WaitForSeconds(Prompt.PrintInterval);

                }

                // Wait a frame to reset the inputs.
                yield return new WaitForEndOfFrame();
            }

            // Present the responses once this is done reading.
            PresentResponses();

            yield return null;
        }

        // Present the responses.
        private void PresentResponses() {
            for (int i = 0; i < m_Responses.Length; i++) {
                ChatUI.CreateResponseButton(m_Responses[i], i, m_Responses.Length);
            }
        }

    }

}