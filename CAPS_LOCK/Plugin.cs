using BepInEx;
using Smash;
using System.Runtime.InteropServices;
using UnityEngine;

namespace CAPS_LOCK
{
    [BepInPlugin("com.steven.slapcity.capslock", "CAPS_LOCK", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        bool capslock = false;
        Vector3 targetSize = Vector3.one;
        Vector3[] velocity = new Vector3[4];


        void Update()
        {
            if (GameManager.StateLastSwitchedTo == GameManager.MacroState.Game)
            {
                capslock = (((ushort)GetKeyState(0x14)) & 0xffff) != 0;

                targetSize = Vector3.one * ((capslock) ? 2 : 1);

                for (int i = 0; i < SmashLoader.Instance.NumberOfPlayers; i++)
                {
                    var player = SmashLoader.Instance.GetPlayer(i);
                    if (player != null && player.character != null)
                    {
                        var transform = player.character.transform;
                        transform.localScale = Vector3.SmoothDamp(transform.localScale, targetSize, ref velocity[i], 0.1f);
                    }
                }
            }
        }

        [DllImport("user32.dll")]
        static extern short GetKeyState(int keyCode);
    }
}