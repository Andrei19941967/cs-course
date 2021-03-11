using System;
class HelloWorld
{
    static void Main()
    {
        int zp_rus_rub = 174000, zp_denmark_kron = 174000;
        if (zp_rus_rub <= zp_denmark_kron)
        {
            Console.WriteLine("Ура, все едем в Д~А~Н~И~Ю на заработки");
        }
        if(zp_rus_rub >= zp_denmark_kron)
        {
            Console.WriteLine("Ура, все едем в РФ на заработки");

        }
        else
        {
            Console.WriteLine("КУДА ЕХАТЬ?");
        }

    }
}