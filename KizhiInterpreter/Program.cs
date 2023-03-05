
TextWriter writer = new();
Interpreter interpreter = new(writer);
interpreter.ExecuteLine(Console.ReadLine());
interpreter.ExecuteLine(Console.ReadLine());



public class Interpreter
{
    private TextWriter _writer;

    public Interpreter(TextWriter writer)
    {
        _writer = writer;
    }

    public void ExecuteLine(string command)
    {
        Command com = new Command(command);
        switch (com.Token.MyType)
        {
            case CommandType.Set:
                _writer.SetVariable(com);
                break;

            case CommandType.Sub:
                _writer.SubVariable(com);
                break;
            case CommandType.Print:
                _writer.PrintVariable(com);
                break;
            case CommandType.Rem:
                _writer.RemoveVariable(com);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

}

public class TextWriter
{
    public List<Command> commands = new();

    public void SetVariable(Command command)
    {
        if (commands.FirstOrDefault(u => u.Variable == command.Variable) is null)
            commands.Add(command);
        else 
            commands.Where(u => u.Variable == command.Variable).Select(u => u.Value = command.Value);
    }

    public void SubVariable(Command command)
    {

    }

    public void PrintVariable(Command command)
    {

    }

    public void RemoveVariable(Command command)
    {

    }
}

public class Command
{
    public Token Token;
    public string Variable;
    public int Value;
    private int _commandLength;

    public Command(string command)
    {
        LexCommand(command);
    }

    private void LexCommand(string command)
    {
        var cells = command.Split(" ");
        _commandLength = cells.Length;
        if (_commandLength >= 2)
        {
            Token = new Token(cells[0]);
            Variable = cells[1];
        }

        if (_commandLength == 3)
            int.TryParse(cells[2], out Value);

    }

    public override string ToString()
    {
        switch (_commandLength)
        {
            case 2:
                return $"{Token.ToString().ToLower()} {Variable}";
            case 3:
                return $"{Token.ToString().ToLower()} {Variable} {Value}";
            default: throw new ArgumentException(message: "Wrong command.");
        }
    }
}
public class Token
{

    public CommandType MyType;

    public Token(string token)
    {
        MyType = GetCommandToken(token);
    }

    private CommandType GetCommandToken(string text)
    {
        MyType = text switch
        {
            "set" => CommandType.Set,
            "sub" => CommandType.Sub,
            "print" => CommandType.Print,
            "rem" => CommandType.Rem,
        };
        return MyType;
    }

    public override string ToString()
    {
        return MyType.ToString().ToLower();
    }

}
public enum CommandType
{
    Set,
    Sub,
    Print,
    Rem
}

