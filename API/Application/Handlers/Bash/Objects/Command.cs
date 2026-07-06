namespace Application.Handlers.Bash.Objects
{
    public record Command(string Name, string[] Args)
    {
        public static Command Parse(string input)
        {
            var parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            return new Command(parts[0], [.. parts.Skip(1)]);
        }
    }
}
