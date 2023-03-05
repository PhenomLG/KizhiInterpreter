namespace KizhiInterpreter.Commands;

public class Command
{
    public CommandType CommandType;
    public Variable Variable;

    public Command(string command)
    {
        LexCommand(command);
    }

    private void LexCommand(string command)
    {
        var cells = command.Split(" ");
        if (cells.Length >= 2)
        {
            CommandType = GetCommand(cells[0]);
            Variable = new Variable(cells[1]);
        }
        if (cells.Length == 3)
            int.TryParse(cells[2], out Variable.Value);
    }

    private CommandType GetCommand(string text)
    {
        CommandType = text switch
        {
            "set" => CommandType.Set,
            "sub" => CommandType.Sub,
            "print" => CommandType.Print,
            "rem" => CommandType.Rem,
        };
        return CommandType;
    }
}