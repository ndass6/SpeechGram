using UnityEngine;

public enum ButtonType { Register, Reply, Close }

public class MenuButton : MonoBehaviour
{
    public ButtonType Type;
    public string Message;
}
