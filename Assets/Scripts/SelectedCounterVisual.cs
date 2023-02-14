using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class SelectedCounterVisual : MonoBehaviour
    {
        [SerializeField] private BaseCounter baseCounter;
        [SerializeField] private GameObject[] visualGameObjectArray;
        private void Start()
        {
            Player.Instance.onSelectedCounterChanged += Player_onSelectedCounterChanged;
        }

        private void Player_onSelectedCounterChanged(object sender, Player.onSelectedCounterChangedEventArgs e)
        {
            if(e.selectedCounter == baseCounter)
                Show();
            else
                Hide();
          
        }

        private void Show()
        {
            foreach (GameObject VisualGameObject in visualGameObjectArray)
            {
                VisualGameObject.SetActive(true);
            }
        }

        private void Hide()
        {
            foreach (GameObject VisualGameObject in visualGameObjectArray)
            {
                VisualGameObject.SetActive(false);
            }
        }
    }
}