using DotNetEnv;

namespace ExcelReader;

class Program
{
    static void Main(string[] args)
    {
        var bulkInsertion = new InsertBulkData(10000, 734991);

        CancellationTokenSource ctn = new CancellationTokenSource();
        Thread t = new Thread(() => PrintWaitingStatement(ctn.Token));
        
        
        t.Start();
        
        bulkInsertion.InsertData();
        
        ctn.Cancel();
        t.Join();
        
        Console.WriteLine("Finished Insertion");
    }


    static void PrintWaitingStatement(CancellationToken cnt)
    {
        char[] spins = { '|', '/', '-', '\\' };
        int n = 4;

        int cntr = 0;
        while (!cnt.IsCancellationRequested)
        {
            Console.WriteLine($"Inserting Data {spins[cntr]}");
            Thread.Sleep(250);
            cntr = (cntr + 1) % n;
            Console.Clear();
        }
        
        Console.Clear(); // Clear spinner
    }
}