using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class ContainerCounterVisual : MonoBehaviour
    {
        private Animator animator;
        private const string OPEN_CLOSE = "OpenClose";

        [SerializeField] ContainerCounter containerCounter;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        private void Start()
        {
            containerCounter.OnPlayerGrabbedObject += ContainerCounter_OnPlayerGrabbedObject;
        }

        private void ContainerCounter_OnPlayerGrabbedObject(object sender, EventArgs e)
        {
            animator.SetTrigger(OPEN_CLOSE);
        }
    }
}