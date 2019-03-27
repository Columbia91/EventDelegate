using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event
{
    delegate void AccountStateHandler(string message);
    class Account
    {
        int _sum;
        //AccountStateHandler _del;
        public event AccountStateHandler Added;
        public event AccountStateHandler Adding;
        public event AccountStateHandler Withdrawn;
        //public void RegisterHandler(AccountStateHandler del)
        //{
        //    _del += del;
        //}
        //public void UnRegisterHandler(AccountStateHandler del)
        //{
        //    _del -= del;
        //}
        public Account(int sum)
        {
            _sum = sum;
        }
        public void Put(int sum)
        {
            if(Adding != null)
                Adding($"На счет добавляется {sum}");
            _sum += sum;
            if (Added != null)
                Added($"На счет пришло {sum}");
        }
        public void Withdraw(int sum)
        {
            if(_sum >= sum)
            {
                _sum -= sum;
                if(Withdrawn != null)
                    Withdrawn($"Со счета снято {sum}");
            }
            else
            {
                if (Withdrawn != null)
                    Withdrawn("Недостаточно средств для списания");
            }
        }
    }
    class Program
    {
        delegate void Message();
        delegate int Operation(int x, int y);
        static void Main(string[] args)
        {
            Account account = new Account(100);
            account.Added += new AccountStateHandler(Display);
            account.Withdrawn += Display;
            account.Withdrawn += ColorDisplay;
            //account.RegisterHandler(Display);
            //account.RegisterHandler(ColorDisplay);
            account.Put(100);
            account.Withdraw(100);
            account.Withdrawn -= ColorDisplay;
            //account.UnRegisterHandler(ColorDisplay);
            account.Withdraw(150);
            //DoOperation(4, 5, Add);
            //DoOperation(4, 5, Multiply);

            Console.ReadLine();
        }
        static void DoOperation(int x, int y, Operation operation)
        {
            int res = operation(x, y);
            Console.WriteLine(res);
        }
        static int Add(int x, int y)
        {
            return x + y;
        }
        static int Multiply(int x,int y)
        {
            return x * y;
        }
        static void ColorDisplay(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }
        static void Display(string message)
        {
            Console.WriteLine(message);
        }
    }
}
