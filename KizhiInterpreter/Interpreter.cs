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

            case CommandType.Subtract:
                _operationResult = _writer.SubVariable(com);
                break;

            case CommandType.Print:
                _operationResult = _writer.PrintVariable(com);
                break;

            case CommandType.Remove:
                _operationResult = _writer.RemoveVariable(com);
                break;

            case CommandType.Unknown:
                Console.WriteLine("Некорретный ввод. Попробуйте еще раз.");
                break;

            default:
                Console.WriteLine("Неправильная команда");
                break;
        }
        if (!_operationResult)
            Console.WriteLine("Переменная отсутствует в памяти");
    }

}