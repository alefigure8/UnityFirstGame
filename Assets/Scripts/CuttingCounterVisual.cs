using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class CuttingCounterVisual : MonoBehaviour
    {
        private Animator animator;
        private const string CUT = "Cut";

        [SerializeField] CuttingCounter cuttingCounter;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        private void Start()
        {
            cuttingCounter.OnCut += CuttingCounter_OnCut;
        }

        private void CuttingCounter_OnCut(object sender, EventArgs e)
        {
            animator.SetTrigger(CUT);
        }
    }
}