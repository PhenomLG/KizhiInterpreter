
TextWriter writer = new();
Interpreter interpreter = new(writer);

while (true)
{
    interpreter.ExecuteLine(Console.ReadLine());
}



public class Interpreter
{
    private TextWriter _writer;
    private bool _operationResult = true;


    public Interpreter(TextWriter writer)
    {
        _writer = writer;
    }

    public void ExecuteLine(string command)
    {
        Command com = new Command(command);
        switch (com.CommandType)
        {
            case CommandType.Set:
                _operationResult = _writer.SetVariable(com);
                break;

            case CommandType.Sub:
                _operationResult = _writer.SubVariable(com);
                break;

            case CommandType.Print:
                _operationResult =_writer.PrintVariable(com);
                break;

            case CommandType.Rem:
                _operationResult = _writer.RemoveVariable(com);
                break;

            default:
                throw new Exception("Нет такой команды");
        }
        if(!_operationResult)
            Console.WriteLine("Переменная отсутствует в памяти");
    }

}

public class Variable
{
    public string Name;
    public int Value;

    public Variable(string name)
    {
        Name = name;
    }
}



public class TextWriter
{
    public static List<Variable> variables = new();


    public bool SetVariable(Command command)
    {
        if (variables.FirstOrDefault(u => u.Name == command.Variable.Name) is null)
            variables.Add(command.Variable);
        else 
            variables.Where(u => u.Name == command.Variable.Name).Select(u => u.Value = command.Variable.Value);
        return true;
    }

    public bool SubVariable(Command command)
    {
        var variable = variables.FirstOrDefault(u => u.Name == command.Variable.Name);
        if (variable is null) 
            return false;

        variable.Value -= command.Variable.Value;
        return true;
    }

    public bool PrintVariable(Command command)
    {
        var variable = variables.FirstOrDefault(u => u.Name == command.Variable.Name);
        if (variable is null) 
            return false;

        Console.WriteLine(variable.Value);
        return true;
    }

    public bool RemoveVariable(Command command)
    {
        var variable = variables.FirstOrDefault(u => u.Name == command.Variable.Name);
        if (variable is null) 
            return false;

        variables.RemoveAll(u => u.Name == command.Variable.Name);
        return true;
    }
}

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

public enum CommandType
{
    Set,
    Sub,
    Print,
    Rem
}

