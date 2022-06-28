/* --- Libraries --- */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TKN2;

namespace TKN2 {

    ///<summary>
    ///
    ///<summary>
    public class Rating : MonoBehaviour {

        public static float DefaultRating = 0.5f;

        [SerializeField, ReadOnly] private int m_MaxRating;
        [SerializeField, ReadOnly] private int m_Rating;

        [SerializeField] private Text m_Text;
        public Text RatingText => m_Text;
        
        [SerializeField] private UnityEngine.UI.Slider m_Slider;
        public Slider RatingSlider => m_Slider;

        [SerializeField] private UnityEngine.UI.Text m_ValueDisplay;
        public Text ValueDisplay => m_ValueDisplay;

        [SerializeField] private AudioClip m_ValueChangeSound;

        public void Init(string question, int maxRating) {
            m_Text.text = question;
            m_MaxRating = maxRating;
            m_Slider.maxValue = maxRating;
            m_Slider.value = (int)Mathf.Round(DefaultRating * m_MaxRating);
            gameObject.SetActive(true);

            // Set the slider size.

            // Set the slider images.

        }

        void Update() {
            if (m_Slider.value != m_Rating) {
                OnValueChanged();
            } 
        }

        public virtual void OnValueChanged() {
            m_Rating = (int) m_Slider.value;
            if (m_ValueDisplay != null) {
                m_ValueDisplay.text = m_Rating.ToString();
            }
            SoundManager.PlaySound(m_ValueChangeSound);
        }

    }
}