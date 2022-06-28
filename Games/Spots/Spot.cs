/* --- Libraries --- */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TKN2;

namespace TKN2 {

    ///<summary>
    ///
    ///<summary>
    [RequireComponent(typeof(SpriteRenderer))]
    public class Spot : MonoBehaviour {

        [HideInInspector] private SpriteRenderer m_SpriteRenderer;

        // The spot's name.
        public string SpotName => gameObject.name;
        public Vector2 Position => transform.position;

        // The threshold around which this can be found.
        [SerializeField] private float m_SelectionThreshold;
        public float Radius => m_SelectionThreshold;

        // Whether this spot has been selected on it.
        [SerializeField, ReadOnly] private bool m_Selected = false;
        public bool Selected => m_Selected;

        // The time that this has found.
        [SerializeField, ReadOnly] private float m_TimeFound = -1f;
        public float TimeFound => m_TimeFound;

        public AudioClip m_FoundSound;

        // Runs once before the first frame.
        void Start() {
            m_SpriteRenderer = GetComponent<SpriteRenderer>();
            m_SpriteRenderer.enabled = false;
            m_TimeFound = -1f;
            m_Selected = false;
            transform.localScale = new Vector3(1f, 1f, 1f);
        }

        void Update() {

            if (m_Selected) {
                return;
            }

            if (Input.GetMouseButtonDown(0)) {
                Vector3 mousePosiion = Screen.MousePosition;
                float squareDistance = (mousePosiion - transform.position).sqrMagnitude;
                if (squareDistance < m_SelectionThreshold * m_SelectionThreshold) {
                    Select();
                }

            }

        }

        public void Select() {
            m_SpriteRenderer.enabled = true;
            m_TimeFound = DataTracker.Ticks;
            m_Selected = true;
            SpotUI.MainSpotUI.Add();
            SoundManager.PlaySound(m_FoundSound);
        }

        void OnDrawGizmos() {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, m_SelectionThreshold);
        }

    }

}
