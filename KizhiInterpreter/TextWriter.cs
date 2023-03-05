using KizhiInterpreter.Commands;

namespace KizhiInterpreter;

public class TextWriter
{
    public static List<Variable> variables = new();


    public bool SetVariable(Command command)
    {
        var variable = variables.FirstOrDefault(u => u.Name == command.Variable.Name);
        if (variable is null)
            variables.Add(command.Variable);
        else
            variable.Value = command.Variable.Value;
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