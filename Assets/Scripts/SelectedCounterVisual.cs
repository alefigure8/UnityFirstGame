using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class SelectedCounterVisual : MonoBehaviour
    {
        [SerializeField] private ClearCounter clearCounter;
        [SerializeField] private GameObject visualGameObject;
        private void Start()
        {
            Player.Instance.onSelectedCounterChanged += Player_onSelectedCounterChanged;
        }

        private void Player_onSelectedCounterChanged(object sender, Player.onSelectedCounterChangedEventArgs e)
        {
            if(e.selectedCounter == clearCounter)
                Show();
            else
                Hide();
          
        }

        private void Show()
        {
            visualGameObject.SetActive(true);
        }

        private void Hide()
        {
            visualGameObject.SetActive(false);
        }
    }
}