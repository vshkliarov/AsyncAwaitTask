namespace AsyncAwaitTask.App;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("-> {0} is start Main()", Thread.CurrentThread.ManagedThreadId);
        MakeBreakfastAsync();
        Console.WriteLine("-> {0} is finish Main()", Thread.CurrentThread.ManagedThreadId);
        Console.ReadLine(); 
    }

    static async void MakeBreakfastAsync()
    {
        Console.WriteLine("-> {0} is start MakeBreakfastAsync()", Thread.CurrentThread.ManagedThreadId);

        Task<bool> isCoffeeReadyTask = PourCoffee();
        Console.WriteLine("-> {0} is MakeBreakfastAsync() -> BEFORE isCoffeeReadyTask", Thread.CurrentThread.ManagedThreadId);
        var isCoffeeReady = await isCoffeeReadyTask;
        Console.WriteLine("-> {0} is MakeBreakfastAsync() -> AFTER isCoffeeReadyTask", Thread.CurrentThread.ManagedThreadId);

        Task voidTask1 = Task.Delay(2000);
        Console.WriteLine("-> {0} is MakeBreakfastAsync() -> BEFORE voidTask1", Thread.CurrentThread.ManagedThreadId);
        await voidTask1;
        Console.WriteLine("-> {0} is MakeBreakfastAsync() -> AFTER voidTask1", Thread.CurrentThread.ManagedThreadId);

        Task voidTask2 = Task.Delay(2000);
        Console.WriteLine("-> {0} is MakeBreakfastAsync() -> BEFORE voidTask2", Thread.CurrentThread.ManagedThreadId);
        await voidTask2;
        Console.WriteLine("-> {0} is MakeBreakfastAsync() -> AFTER voidTask2", Thread.CurrentThread.ManagedThreadId);

        Console.WriteLine("-> {0} is finish MakeBreakfastAsync()", Thread.CurrentThread.ManagedThreadId);
    }

    static Task<bool> PourCoffee()
    {
        Console.WriteLine("-> {0} is start PourCoffee()", Thread.CurrentThread.ManagedThreadId);
        Func<bool> pouringCoffee = new Func<bool>(pouringCoffeeImpl); 
        var task = Task.Run(pouringCoffee);
        Console.WriteLine("-> {0} is finish PourCoffee()", Thread.CurrentThread.ManagedThreadId);
        // Console.WriteLine("The Status of task is {0}", task.Status.ToString());
        // Console.WriteLine("The type of task is {0}", task.GetType().ToString());
        return task;
    }

    static bool pouringCoffeeImpl()
    {
        Console.WriteLine("-> {0} is start pouringCoffeeImpl()", Thread.CurrentThread.ManagedThreadId);
        Console.Write("POURING COFFEE: ");
        for (int i = 0; i < 10; i++)
        {
            Random r = new Random();
            Thread.Sleep(1000 * r.Next(5));
            Console.Write("{0}-{1}, ", i, Thread.CurrentThread.ManagedThreadId);
        }
        Console.WriteLine();
        Console.WriteLine("-> {0} is finish pouringCoffeeImpl()", Thread.CurrentThread.ManagedThreadId);
        return true;
    }

    // static Task<bool> PourCoffee()
    // {
    //     Console.WriteLine("start pour coffe");       
    //     return Task.Run(() => 
    //     {
    //         Console.WriteLine("start pouring coffee...");
    //         Thread.Sleep(2000);
    //         Console.WriteLine("...finish pouring coffee");
    //         return true;
    //     });
    // }

    // static bool PourCoffee()
    // {
    //     Console.WriteLine("start pouring coffee...");
    //     Thread.Sleep(2000);
    //     Console.WriteLine("...finish pouring coffee");
    //     return true;
    // }
}