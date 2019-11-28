using System;
using Coursework.DataAccess;
using Coursework.Types;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            PatientController controller = new PatientController();
            controller.AddPatient(new Patient());

            Console.WriteLine(controller.GetPatients());

            Console.ReadLine();
        }
    }
}
