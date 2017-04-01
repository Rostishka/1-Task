using System;

namespace Events
{
    delegate void PushPrintButton(); // оголошення делегата - step 1
    class Program
    {
        static void Main(string[] args)
        {
            Button button = new Button();

            // підписуємось на подію з вказанням конкретного метода
            button.Click += new PushPrintButton(button.OnButtonClick);

            bool flag = true;

            while(flag)
            {
                Console.WriteLine("press press any button    ");
                var enteredButton = Console.ReadKey().Key.ToString().ToLower();
                if (enteredButton == "q")
                {
                    flag = false;
                    button.Click -= new PushPrintButton(button.OnButtonClick); // відписуємось від події
                    button.DoEvent();
                }
                else
                    button.DoEvent();
            }

            Console.ReadLine();
        }
    }

    class Button
    {
        public event PushPrintButton Click; // оголошення події з типом делегата - step 2

        public void DoEvent()
        {
            if(Click != null)
                Click(); // виконати
        }

        public void OnButtonClick()
        {
            Console.WriteLine("\nВiдбулось натиснення кнопки... \n\n");
        }
    }
}
