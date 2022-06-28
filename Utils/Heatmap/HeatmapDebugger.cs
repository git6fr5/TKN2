/* --- Libraries --- */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TKN2;

namespace TKN2 {

    ///<summary>
    ///
    ///<summary>
    public class HeatmapDebugger : MonoBehaviour {

        // Components.
        [HideInInspector] private SpriteRenderer m_SpriteRenderer;
        
        // Shader.
        [SerializeField] private ComputeShader m_Shader;

        // Texture.
        [HideInInspector] private RenderTexture m_OutputRenderTexture;
        [HideInInspector] private Texture2D m_OutputTexture;

        // Active Heatmap.
        [SerializeField] private DataTracker m_DataTracker;
        [HideInInspector] private Heatmap m_ActiveHeatmap;
        [SerializeField] private bool m_DebugClicks;

        void Start() {
            m_SpriteRenderer = GetComponent<SpriteRenderer>();
        }

        void Update() {
            GetActiveMap();
            if (m_DataTracker != null && m_ActiveHeatmap != null) {
                ResetTexture();
                ComputeHeatmap();
            }
        }

        private void GetActiveMap() {
            if (m_DataTracker != null) {
                if (m_DebugClicks) {
                    m_ActiveHeatmap = m_DataTracker.ClickMap;
                }
                else {
                    m_ActiveHeatmap = m_DataTracker.HoverMap;
                }
                transform.localScale = new Vector3((float)m_ActiveHeatmap.Cols / Screen.ScreenSize.x, (float)m_ActiveHeatmap.Rows / Screen.ScreenSize.y, 1f);
                Vector3 scale = transform.localScale;
                transform.localPosition = new Vector3(-scale.x / 2f, -scale.y / 2f, 0f);
            }
        }

        private void ResetTexture() {
            if (m_ActiveHeatmap == null) {
                return;
            }
            m_OutputRenderTexture = new RenderTexture(m_ActiveHeatmap.Cols, m_ActiveHeatmap.Rows, 16, RenderTextureFormat.ARGB32);
            m_OutputRenderTexture.enableRandomWrite = true;
            m_OutputRenderTexture.Create();
            m_OutputTexture = new Texture2D(m_ActiveHeatmap.Cols, m_ActiveHeatmap.Rows, TextureFormat.ARGB32, true);
        }

        private void ComputeHeatmap() {
            int kernel = m_Shader.FindKernel("ComputeHeatmap");
            
            m_Shader.SetTexture(kernel, "heatmap", m_OutputRenderTexture);
            m_Shader.SetInt("rows", m_ActiveHeatmap.Rows);
            m_Shader.SetInt("cols", m_ActiveHeatmap.Cols);
            
            int[] grid = new int[m_ActiveHeatmap.Rows * m_ActiveHeatmap.Cols];
            int max = 1;
            for (int i = 0; i < m_ActiveHeatmap.Rows; i++) {
                for (int j = 0; j < m_ActiveHeatmap.Cols; j++) {
                    grid[m_ActiveHeatmap.Cols * i + j] = m_ActiveHeatmap.Grid[i][j];
                    if (m_ActiveHeatmap.Grid[i][j] > max) {
                        max = m_ActiveHeatmap.Grid[i][j];
                    }
                }
            }
            // m_Shader.SetInt("max", (int)Mathf.Max(1, max));
            m_Shader.SetInt("maxVal", 1);

            ComputeBuffer heatBuffer = new ComputeBuffer(grid.Length, sizeof(int));
            heatBuffer.SetData(grid);
            m_Shader.SetBuffer(kernel, "heat", heatBuffer);

            m_Shader.Dispatch(kernel, m_ActiveHeatmap.Rows, m_ActiveHeatmap.Cols, 1);
            StartCoroutine(IEReadRenderTexture());

            heatBuffer.Release();
        }

        public IEnumerator IEReadRenderTexture() {
            yield return new WaitForEndOfFrame();
            RenderTexture.active = m_OutputRenderTexture;
            m_OutputTexture.ReadPixels(new Rect(0, 0, m_OutputRenderTexture.width, m_OutputRenderTexture.height), 0, 0);
            m_OutputTexture.Apply();
            Sprite sprite = Sprite.Create(m_OutputTexture, new Rect(0, 0, m_OutputTexture.width, m_OutputTexture.height), Vector2.zero);
            m_SpriteRenderer.sprite = sprite;
            yield return null;
        }

    }

}