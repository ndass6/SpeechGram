public enum ButtonType { Back, Register, Close, Navigation, Reply }

public struct ButtonData
{
    public static readonly ButtonData[,] Buttons = new ButtonData[,]
    {
        {
            new ButtonData(ButtonType.Reply, "Yes", message: "Yes"),
            new ButtonData(ButtonType.Reply, "No", message: "No"),
            new ButtonData(ButtonType.Reply, "Maybe", message: "Maybe"),
            new ButtonData(ButtonType.Reply, "I'm not sure", message: "I'm not sure"),
            new ButtonData(ButtonType.Reply, "Please introduce yourself", message: "Could you please tell me about yourself?"),
            new ButtonData(ButtonType.Reply, "Okay", message: "Okay"),
            new ButtonData(ButtonType.Reply, "I agree", message: "I agree"),
            new ButtonData(ButtonType.Reply, "I disagree", message: "I disagree"),
            new ButtonData(ButtonType.Navigation, "Common responses", destination: 1),
            new ButtonData(ButtonType.Navigation, "Common questions", destination: 2),
            new ButtonData(ButtonType.Navigation, "Numbers", destination: 3),
            new ButtonData(ButtonType.Navigation, "Time", destination: 4)
        },
        {
            new ButtonData(ButtonType.Reply, "Oh, that’s good to hear!", message: "Oh, that’s good to hear!"),
            new ButtonData(ButtonType.Reply, "Good luck!", message: "Good luck!"),
            new ButtonData(ButtonType.Reply, "You look nice today!", message: "You look nice today!"),
            new ButtonData(ButtonType.Reply, "I like it!", message: "I like it!"),
            new ButtonData(ButtonType.Reply, "Yes, please!", message: "Yes, please!"),
            new ButtonData(ButtonType.Reply, "Thank you", message: "Thank you"),
            new ButtonData(ButtonType.Reply, "You’re welcome", message: "You’re welcome"),
            new ButtonData(ButtonType.Reply, "Let me check", message: "Let me check"),
            new ButtonData(ButtonType.Reply, "Be right back", message: "Be right back"),
            new ButtonData(ButtonType.Reply, "Tell me a joke", message: "Tell me a joke"),
            new ButtonData(ButtonType.Reply, "That’s funny!", message: "That’s funny!"),
            new ButtonData(ButtonType.Navigation, "Salutations", destination: 5)
        },
        {
            new ButtonData(ButtonType.Reply, "How is youor day going?", message: "How is youor day going?"),
            new ButtonData(ButtonType.Reply, "What are you doing?", message: "What are you doing?"),
            new ButtonData(ButtonType.Reply, "What do you think?", message: "What do you think?"),
            new ButtonData(ButtonType.Reply, "Can you elaborate?", message: "Can you elaborate?"),
            new ButtonData(ButtonType.Reply, "Can you help me with something?", message: "Can you help me with something?"),
            new ButtonData(ButtonType.Reply, "Do you need help?", message: "Do you need help?"),
            new ButtonData(ButtonType.Reply, "Are you sure?", message: "Are you sure?"),
            new ButtonData(ButtonType.Reply, "What are your hobbies?", message: "What are your hobbies?"),
            new ButtonData(ButtonType.Reply, "How much does this cost?", message: "How much does this cost?"),
            new ButtonData(ButtonType.Reply, "What time is it?", message: "What time is it?"),
            new ButtonData(ButtonType.Navigation, "Do you want to ____?", destination: 6),
            new ButtonData(ButtonType.Navigation, "What are you doing ____?", destination: 7),
        },
        {
            new ButtonData(ButtonType.Reply, "1", message: "1"),
            new ButtonData(ButtonType.Reply, "2", message: "2"),
            new ButtonData(ButtonType.Reply, "3", message: "3"),
            new ButtonData(ButtonType.Reply, "4", message: "4"),
            new ButtonData(ButtonType.Reply, "5", message: "5"),
            new ButtonData(ButtonType.Reply, "10", message: "10"),
            new ButtonData(ButtonType.Reply, "15", message: "15"),
            new ButtonData(ButtonType.Reply, "20", message: "20"),
            new ButtonData(ButtonType.Reply, "30", message: "30"),
            new ButtonData(ButtonType.Reply, "45", message: "45"),
            new ButtonData(ButtonType.Reply, "60", message: "60"),
            new ButtonData(ButtonType.Reply, "100", message: "100")
        },
        {
            new ButtonData(ButtonType.Reply, "Seconds", message: "Seconds"),
            new ButtonData(ButtonType.Reply, "Minutes", message: "Minutes"),
            new ButtonData(ButtonType.Reply, "Hours", message: "Hours"),
            new ButtonData(ButtonType.Reply, "Days", message: "Days"),
            new ButtonData(ButtonType.Reply, "Weeks", message: "Weeks"),
            new ButtonData(ButtonType.Reply, "Months", message: "Months"),
            new ButtonData(ButtonType.Reply, "Years", message: "Years"),
            new ButtonData(ButtonType.Reply, "Noon", message: "Noon"),
            new ButtonData(ButtonType.Reply, "Midnight", message: "Midnight"),
            new ButtonData(ButtonType.Reply, "Yesterday", message: "Yesterday"),
            new ButtonData(ButtonType.Reply, "Today", message: "Today"),
            new ButtonData(ButtonType.Reply, "Tomorrow", message: "Tomorrow")
        },
        {
            new ButtonData(ButtonType.Reply, "Good morning", message: "Good morning"),
            new ButtonData(ButtonType.Reply, "Good afternoon", message: "Good afternoon"),
            new ButtonData(ButtonType.Reply, "Good evening", message: "Good evening"),
            new ButtonData(ButtonType.Reply, "Good night", message: "Good night"),
            new ButtonData(ButtonType.Reply, "Hi", message: "Hi"),
            new ButtonData(ButtonType.Reply, "Hello", message: "Hello"),
            new ButtonData(ButtonType.Reply, "Hey", message: "Hey"),
            new ButtonData(ButtonType.Reply, "Bye", message: "Bye"),
            new ButtonData(ButtonType.Reply, "See you later", message: "See you later"),
            new ButtonData(ButtonType.Reply, "See you tomorrow", message: "See you tomorrow"),
            new ButtonData(ButtonType.Reply, "See you soon", message: "See you soon"),
            new ButtonData(ButtonType.Reply, "Goodbye", message: "Goodbye")
        },
        {
            new ButtonData(ButtonType.Reply, "... be friends?", message: "Do you want to be friends?"),
            new ButtonData(ButtonType.Reply, "... eat food?", message: "Do you want to eat food?"),
            new ButtonData(ButtonType.Reply, "... watch a movie?", message: "Do you want to watch a movie?"),
            new ButtonData(ButtonType.Reply, "... play a game?", message: "Do you want to play a game?"),
            new ButtonData(ButtonType.Reply, "... take a walk?", message: "Do you want to take a walk?"),
            new ButtonData(ButtonType.Reply, "... dance?", message: "Do you want to dance?"),
            new ButtonData(ButtonType.Reply, "... study together?", message: "Do you want to study together?"),
            new ButtonData(ButtonType.Reply, "... here a joke?", message: "Do you want to hear a joke?"),
            new ButtonData(ButtonType.Reply, "... watch the news?", message: "Do you want to watch the news?"),
            new ButtonData(ButtonType.Reply, "... play a sport?", message: "Do you want to play a sport?"),
            new ButtonData(ButtonType.Reply, "... go shopping?", message: "Do you want to go shopping?"),
            new ButtonData(ButtonType.Reply, "... read a book?", message: "Do you want to read a book?")
        },
        {
            new ButtonData(ButtonType.Reply, "... today?", message: "What are you doing today?"),
            new ButtonData(ButtonType.Reply, "... tomorrow?", message: "What are you doing tomorrow?"),
            new ButtonData(ButtonType.Reply, "... on Monday?", message: "What are you doing on Monday?"),
            new ButtonData(ButtonType.Reply, "... on Tuesday?", message: "What are you doing on Tuesday?"),
            new ButtonData(ButtonType.Reply, "... on Wednesday?", message: "What are you doing on Wednesday?"),
            new ButtonData(ButtonType.Reply, "... on Thursday?", message: "What are you doing on Thursday?"),
            new ButtonData(ButtonType.Reply, "... on Friday?", message: "What are you doing on Friday?"),
            new ButtonData(ButtonType.Reply, "... on Saturday?", message: "What are you doing on Saturday?"),
            new ButtonData(ButtonType.Reply, "... on Sunday?", message: "What are you doing on Sunday?"),
            new ButtonData(ButtonType.Reply, "... this weekend?", message: "What are you doing this weekend?"),
            new ButtonData(ButtonType.Reply, "... this week?", message: "What are you doing this week?"),
            new ButtonData(ButtonType.Reply, "... next week?", message: "What are you doing next week?")
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
