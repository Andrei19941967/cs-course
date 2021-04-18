using System;

namespace andrej_OOP
{
    public class ReminderItem
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

        public virtual void WriteProperties()
        {
            Console.WriteLine("ReminderItem");
            Console.WriteLine("Alarm date: " + AlarmDate.ToString());
            Console.WriteLine("Alarm message: " + AlarmMessage);
            Console.WriteLine("Time to alarm: " + TimeToAlarm.ToString());
            Console.WriteLine("Is out dated: " + IsOutdated);
        }
    }

    public class PhoneReminderItem : ReminderItem
    {
        public string PhoneNumber { get; set; }

        public PhoneReminderItem(DateTimeOffset dateTime, string message, string phone) : base(dateTime, message)
        {
            PhoneNumber = phone;
        }

        public override void WriteProperties()
        {
            Console.WriteLine("PhoneReminderItem");
            Console.WriteLine("Alarm date: " + AlarmDate.ToString());
            Console.WriteLine("Alarm message: " + AlarmMessage);
            Console.WriteLine("Time to alarm: " + TimeToAlarm.ToString());
            Console.WriteLine("Is out dated: " + IsOutdated);
            Console.WriteLine("Phone Number: " + PhoneNumber);
        }
    }

    public class ChatReminderItem : ReminderItem
    {
        public string ChatName { get; set; }
        public string AccountName { get; set; }

        public ChatReminderItem(DateTimeOffset dateTime, string message,
            string chatName, string accountName) : base(dateTime, message)
        {
            ChatName = chatName;
            AccountName = accountName;
        }

        public override void WriteProperties()
        {
            Console.WriteLine("ChatReminderItem");
            Console.WriteLine("Alarm date: " + AlarmDate.ToString());
            Console.WriteLine("Alarm message: " + AlarmMessage);
            Console.WriteLine("Time to alarm: " + TimeToAlarm.ToString());
            Console.WriteLine("Is out dated: " + IsOutdated);
            Console.WriteLine("Chat name: " + ChatName);
            Console.WriteLine("Account name: " + AccountName);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {

            DateTimeOffset d1 = new DateTimeOffset(2032, 1, 20, 22, 50, 30, TimeSpan.FromHours(0));
            DateTimeOffset d2 = new DateTimeOffset(2000, 1, 20, 22, 50, 30, TimeSpan.FromHours(0));
            DateTimeOffset d3 = new DateTimeOffset(2022, 3, 18, 11, 34, 17, TimeSpan.FromHours(0));

            ReminderItem[] arr = new ReminderItem[3];
            arr[0] = new ReminderItem(d1, "Wake up!");
            arr[1] = new PhoneReminderItem(d2, "Hello", "88005553535");
            arr[2] = new ChatReminderItem(d3, "Hi!!", "First chat", "Liling");

            for (int i = 0; i < arr.Length; i++)
            {
                arr[i].WriteProperties();
                Console.WriteLine();
            }
        }
    }
}