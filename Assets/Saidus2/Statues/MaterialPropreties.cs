using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Saidus2 {
    public class MaterialPropreties : MonoBehaviour
    {
        SkinnedMeshRenderer m_Renderer;
        private void Start()
        {
            m_Renderer = GetComponent<SkinnedMeshRenderer>();
        }
        // Update is called once per frame
        void Update()
        {
            m_Renderer.material.SetFloat("_Transparency", GameManager.Instance.SanityNormalized);
        }
    } }
