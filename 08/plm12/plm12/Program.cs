using System;
using System.Collections.Generic;

namespace plm12
{
    
    class Program
    {
 
        public static void Main()
        {
            string s= Console.ReadLine();

            Stack<char> st = new Stack<char>();
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '(' || s[i] == '[')
                {
                    st.Push(s[i]);
                }
                else if (st.Count != 0)
                {
                    if (s[i] == ')' && st.Peek() == '(')
                    {
                        st.Pop();
                    }
                    else if (s[i] == ']' && st.Peek() == '[')
                    {
                        st.Pop();
                    }
                    else
                    {
                        Console.WriteLine("False");
                        return;
                    }
                }
                else
                {
                    Console.WriteLine("False");
                    return;
                }
            }
            if (st.Count == 0) Console.WriteLine("True");
            else Console.WriteLine("False");
        }
    }
}
     