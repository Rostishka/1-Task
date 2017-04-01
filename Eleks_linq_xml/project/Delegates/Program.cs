using System;

namespace Delegates
{
    delegate void Del(string str);

    public delegate void AccountStateHandler(string message);

    class Program
    {
        AccountStateHandler del;
        static void Main(string[] args)
        {
            #region варіант використання 1
            Del notifyDel = new Del(Notify);
            notifyDel("Oleh");
            #endregion

            #region варіант використання 2
            Del notifyDel2 = Notify;
            notifyDel2("Anton");           
            #endregion

            #region варіант використання 3 (анонімний метод)
            Del notifyDel3 = delegate (string name)
            {
                Console.WriteLine("Hello {0}!", name);
            };
            notifyDel3("Taras");            
         
            #endregion

            #region варіант використання 4 (з лямбда виразами)
            Del notifyDel4 = name => { Console.WriteLine("Hello {0}!", name); };
            notifyDel4("Ihor");
            #endregion
            
            Console.ReadLine();
            Console.Clear();

            #region варіант використання 5 (register and unregister)
            // створюємо банківський рахунок
            Account account = new Account(200, 6);
            
            // Додаємо в делегат посилання на метод Show_Message
            // а сам делегат передаєтся в якості параметра методу RegisterHandler
            account.RegisterHandler(new Account.AccountStateHandler(Show_Message));
            
            // Два рази підряд пробуємо зняти гроші
            account.Withdraw(100);
            account.Withdraw(150);

            // Видаляємо делегат
            account.UnregisterHandler(Show_Message);
            account.Withdraw(50);
            #endregion

            Console.ReadLine();
        }

        private static void Show_Message(String message)
        {
            Console.WriteLine(message);
        }

        static void Notify(string name)
        {
            Console.WriteLine("Hello {0}!", name);
        }       
    }

    class Account
    {
        int _sum;
        int _percentage;

        
        public delegate void AccountStateHandler(string message);
        AccountStateHandler del;        

        public Account(int sum, int percentage)
        {
            _sum = sum;
            _percentage = percentage;
        }

        // Реєструємо делегат
        public void RegisterHandler(AccountStateHandler _del)
        {
            del = _del;
        }

        // Відміна реєстрації делегата
        public void UnregisterHandler(AccountStateHandler _del)
        {
            Delegate mainDel = System.Delegate.Remove(del, _del);
            del = mainDel as AccountStateHandler;
        }

        public int CurrentSum
        {
            get { return _sum; }
        }

        public void Put(int sum)
        {
            _sum += sum;
        }

        public void Withdraw(int sum)
        {
            if (sum <= _sum)
            {
                _sum -= sum;

                if (del != null)
                    del("Сумма " + sum.ToString() + " снята со счета");
            }
            else
            {
                if (del != null)
                    del("Недостаточно денег на счете");
            }
        }

        public int Percentage
        {
            get { return _percentage; }
        }        
    }
}
