
using System;

namespace andrej_OOP
{
    class ReminderItem
    {
        private DateTimeOffset alarmDate;
        private string alarmMessage;


        public ReminderItem(DateTimeOffset dateTime, string message)
        {
            alarmDate = dateTime;
            alarmMessage = message;
        }


        public DateTimeOffset AlarmDate
        {
            get
            {
                return alarmDate;
            }

            set
            {
                alarmDate = value;
            }
        }

        public string AlarmMessage
        {
            get
            {
                return alarmMessage;
            }

            set
            {
                alarmMessage = value;
            }
        }

        public TimeSpan TimeToAlarm
        {
            get
            {
                return alarmDate - DateTimeOffset.Now;
            }
        }

        public bool IsOutdated
        {
            get
            {
                if (TimeToAlarm.Milliseconds < 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public void WriteProperties()
        {
            Console.WriteLine("Alarm date: " + alarmDate.ToString());
            Console.WriteLine("Alarm message: " + alarmMessage);
            Console.WriteLine("Time to alarm: " + TimeToAlarm.ToString());
            Console.WriteLine("Is out dated: " + IsOutdated);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {

            DateTimeOffset d1 = new DateTimeOffset(2032, 1, 20, 22, 50, 30, TimeSpan.FromHours(0));
            string m1 = "Wake up!";

            ReminderItem r1 = new ReminderItem(d1, m1);
            r1.WriteProperties();

            Console.WriteLine();

            DateTimeOffset d2 = new DateTimeOffset(2000, 1, 20, 22, 50, 30, TimeSpan.FromHours(0));
            string m2 = "Wake up, dear!";

            ReminderItem r2 = new ReminderItem(d2, m2);
            r2.WriteProperties();
        }
    }
}
