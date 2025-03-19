using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CityPeople
{
    public class CityPeople : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Overrides palette materials, skips other objects")]
        private Material PaletteOverride;
        public string CurrentPaletteName { get; private set; }

        private Animator animator;
        private List<Renderer> _paletteMeshes;
        private static readonly int IsWalk = Animator.StringToHash("isWalk");

        private void Awake()
        {
            var allRenderers = GetComponentsInChildren<Renderer>();
            _paletteMeshes = new List<Renderer>();

            foreach (Renderer r in allRenderers)
            {
                if (r.sharedMaterial.name.StartsWith("people_pal"))
                {
                    _paletteMeshes.Add(r);
                }
            }

            if (_paletteMeshes.Count > 0)
            {
                CurrentPaletteName = _paletteMeshes[0].sharedMaterial.name;
            }

            if (PaletteOverride != null)
            {
                SetPalette(PaletteOverride);
            }
        }

        void Start()
        {
            animator = GetComponent<Animator>();

            if (animator != null)
            {
                animator.SetBool(IsWalk, false); // Запускаємо Idle-анімацію
            }
        }

        public void SetPalette(Material mat)
        {
            if (mat != null && mat.name.StartsWith("people_pal"))
            {
                CurrentPaletteName = mat.name;
                foreach (Renderer r in _paletteMeshes)
                {
                    r.material = mat;
                }
            }
            else
            {
                Debug.LogWarning("Material name should start with 'people_pal...'.");
            }
        }

        /// <summary>
        /// Увімкнути або вимкнути анімацію руху.
        /// </summary>
        public void SetWalkState(bool isWalking)
        {
            if (animator != null)
            {
                animator.SetBool(IsWalk, isWalking);
            }
        }

    }
}
