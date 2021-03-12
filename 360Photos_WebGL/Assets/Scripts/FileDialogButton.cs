using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
 
public class FileDialogButton : MonoBehaviour {
    [DllImport("__Internal")]
    private static extern void onPointerDown();
 
    public void PointerDown () {
        onPointerDown ();
    }
}