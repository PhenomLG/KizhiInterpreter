using KizhiInterpreter;
using KizhiInterpreter.Commands;
using TextWriter = KizhiInterpreter.TextWriter;

TextWriter writer = new();
Interpreter interpreter = new(writer);

while (true)
{
    interpreter.ExecuteLine(Console.ReadLine());
}

