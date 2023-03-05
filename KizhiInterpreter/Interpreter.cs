using KizhiInterpreter.Commands;

namespace KizhiInterpreter;

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
                _operationResult = _writer.PrintVariable(com);
                break;

            case CommandType.Rem:
                _operationResult = _writer.RemoveVariable(com);
                break;

            default:
                throw new Exception("Нет такой команды");
        }
        if (!_operationResult)
            Console.WriteLine("Переменная отсутствует в памяти");
    }

}