using UI.Menus;
using UnityEngine;
using Utilities;

namespace Weapons
{
    public class PlayerWeapons : WeaponHandler
    {
        [SerializeField] private Camera playerCam;

        private void Update()
        {
            if (GamePause.IsPaused) return;
            Fire(CurrentWeapon.IsAutomatic
                    ? Input.GetMouseButton(0)
                    : Input.GetMouseButtonDown(0),
                playerCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)));
        }
        
        private void OnEnable()
        {
            InputFilter.Instance.OnNumericValueReceived += SwitchTo;
        }
        
        private void OnDisable()
        {
            if (InputFilter.HasInstance())
            {
                InputFilter.Instance.OnNumericValueReceived -= SwitchTo;
            }   
        }

        public void Collect(int index)
        {
            ConfirmedGuns[index-1].IsUnlocked = true;
            SwitchTo(index);
        }
    }
}