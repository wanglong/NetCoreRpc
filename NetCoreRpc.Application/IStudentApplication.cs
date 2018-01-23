using NetCoreRpc.Serializing.Attributes;
using System;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreRpc.Application
{
    public interface IStudentApplication
    {
        int Age();

        bool IsYongPeople(int age);

        void Say(string msg);

        Task Sleep();

        Task<int> RunAsync(int sleepTime);

        void Say(byte[] msg);

        byte[] Say();

        [BinarySerializer]
        TestModel Test();
    }

    public class StudentApplication : IStudentApplication
    {
        public int Age()
        {
            return 10;
        }

        public bool IsYongPeople(int age)
        {
            return age < 18;
        }

        public Task<int> RunAsync(int sleepTime)
        {
            //await Task.Delay(sleepTime);
            //return sleepTime;
            return Task.FromResult(sleepTime);
        }

        public void Say(string msg)
        {
            Console.WriteLine($"Say:{msg}");
        }

        public Task Sleep()
        {
            return Task.Delay(10);
        }

        public void Say(byte[] msg)
        {
            Console.WriteLine(Encoding.UTF8.GetString(msg));
        }

        public byte[] Say()
        {
            return Encoding.UTF8.GetBytes("Good Job!");
        }

        public TestModel Test()
        {
            return new TestModel
            {
                Age = 10,
                Msg = Encoding.UTF8.GetBytes("Hello")
            };
        }
    }

    [Serializable]
    public class TestModel
    {
        public int Age { get; set; }

        public byte[] Msg { get; set; }

        public override string ToString()
        {
            return $"{Age}|{Encoding.UTF8.GetString(Msg)}";
        }
    }
}