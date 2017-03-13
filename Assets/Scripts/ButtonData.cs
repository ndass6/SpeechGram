public enum ButtonType { Register, Navigation, Reply, Close }

public struct ButtonData
{
    public static readonly ButtonData[,] Buttons = new ButtonData[,]
    {
        {
            new ButtonData(ButtonType.Reply, "z", message: "Yes"),
            new ButtonData(ButtonType.Reply, "x", message: "Yes"),
            new ButtonData(ButtonType.Reply, "c", message: "Yes"),
            new ButtonData(ButtonType.Navigation, "Menu 2", destination: 1),
            new ButtonData(ButtonType.Reply, "Yes", message: "Yes"),
            new ButtonData(ButtonType.Reply, "Yes", message: "Yes"),
            new ButtonData(ButtonType.Reply, "Yes", message: "Yes"),
            new ButtonData(ButtonType.Navigation, "Menu 3", destination: 2),
            new ButtonData(ButtonType.Reply, "Yes", message: "Yes"),
            new ButtonData(ButtonType.Reply, "Yes", message: "Yes"),
            new ButtonData(ButtonType.Reply, "Yes", message: "Yes"),
            new ButtonData(ButtonType.Navigation, "Menu 4", destination: 3)
        },
        {
            new ButtonData(ButtonType.Reply, "2", message: "Yes"),
            new ButtonData(ButtonType.Reply, "v", message: "Yes"),
            new ButtonData(ButtonType.Reply, "b", message: "Yes"),
            new ButtonData(ButtonType.Reply, "Yes", message: "Yes"),
            new ButtonData(ButtonType.Reply, "Yes", message: "Yes"),
            new ButtonData(ButtonType.Reply, "Yes", message: "Yes"),
            new ButtonData(ButtonType.Reply, "Yes", message: "Yes"),
            new ButtonData(ButtonType.Reply, "Yes", message: "Yes"),
            new ButtonData(ButtonType.Reply, "Yes", message: "Yes"),
            new ButtonData(ButtonType.Reply, "Yes", message: "Yes"),
            new ButtonData(ButtonType.Reply, "Yes", message: "Yes"),
            new ButtonData(ButtonType.Reply, "Yes", message: "Yes")
        },
        {
            new ButtonData(ButtonType.Reply, "3", message: "Yes"),
            new ButtonData(ButtonType.Reply, "Yes", message: "Yes"),
            new ButtonData(ButtonType.Reply, "Yes", message: "Yes"),
            new ButtonData(ButtonType.Reply, "Yes", message: "Yes"),
            new ButtonData(ButtonType.Reply, "Yes", message: "Yes"),
            new ButtonData(ButtonType.Reply, "Yes", message: "Yes"),
            new ButtonData(ButtonType.Reply, "Yes", message: "Yes"),
            new ButtonData(ButtonType.Reply, "Yes", message: "Yes"),
            new ButtonData(ButtonType.Reply, "Yes", message: "Yes"),
            new ButtonData(ButtonType.Reply, "Yes", message: "Yes"),
            new ButtonData(ButtonType.Reply, "Yes", message: "Yes"),
            new ButtonData(ButtonType.Reply, "Yes", message: "Yes")
        },
        {
            new ButtonData(ButtonType.Reply, "4", message: "Yes"),
            new ButtonData(ButtonType.Reply, "Yes", message: "Yes"),
            new ButtonData(ButtonType.Reply, "Yes", message: "Yes"),
            new ButtonData(ButtonType.Reply, "Yes", message: "Yes"),
            new ButtonData(ButtonType.Reply, "Yes", message: "Yes"),
            new ButtonData(ButtonType.Reply, "Yes", message: "Yes"),
            new ButtonData(ButtonType.Reply, "Yes", message: "Yes"),
            new ButtonData(ButtonType.Reply, "Yes", message: "Yes"),
            new ButtonData(ButtonType.Reply, "Yes", message: "Yes"),
            new ButtonData(ButtonType.Reply, "Yes", message: "Yes"),
            new ButtonData(ButtonType.Reply, "Yes", message: "Yes"),
            new ButtonData(ButtonType.Reply, "Yes", message: "Yes")
        }
    };

    public ButtonType Type;
    public string Text;

    // Used for navigation buttons
    public int Destination;

    // Used for reply buttons
    public string Message;

    public ButtonData(ButtonType type, string text, int destination = 0, string message = null)
    {
        Type = type;
        Text = text;
        Destination = destination;
        Message = message;
    }
}
