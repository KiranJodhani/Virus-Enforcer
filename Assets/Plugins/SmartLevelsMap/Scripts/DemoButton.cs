using System;
using UnityEngine;

namespace Assets.Plugins.SmartLevelsMap.Scripts
{
    public class DemoButton : MonoBehaviour 
    {

        public event EventHandler Click;

        public void OnMouseUpAsButton()
        {
            if (Click != null)
                Click(this, EventArgs.Empty);
        }

    }
}
