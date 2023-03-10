namespace KizhiInterpreter.Commands;

public class Command
{
    public CommandType CommandType;
    public Variable Variable = new();

    public Command(string command)
    {
        LexCommand(command);
    }

    private void LexCommand(string command)
    {
        var cells = command.Split(" ");
        switch (cells.Length)
        {
            case 3:
                int.TryParse(cells[2], out Variable.Value);
                goto case 2;
          
            case 2:
                CommandType = GetCommand(cells[0]);
                if (IsLettersOnly(cells[1]))
                    Variable.Name = cells[1];
                else goto default;
                break;
           
            default:
                CommandType = CommandType.Unknown;
                break;
        }
    }

    private CommandType GetCommand(string text)
    {
        CommandType = text switch
        {
            "set" => CommandType.Set,
            "sub" => CommandType.Subtract,
            "print" => CommandType.Print,
            "rem" => CommandType.Remove,
            _ => CommandType.Unknown
        };
        return CommandType;
    }

    private bool IsLettersOnly(string text) => text.All(c => c < '0' || c > '9');

}