namespace geektrust
{
    class Program
    {
        static void Main(string[] args)
        {
            Family family = new Family();
            family.Run(args[0]);
        }
    }
}
