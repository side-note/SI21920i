/*
*   ISEL-ADEETC-SI2
*   ND 2014-2019
*
*   Material didático para apoio 
*   à unidade curricular de 
*   Sistemas de Informação II
*
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace APADO.NET
{
	#region DO_NOT_CHANGE_NOTHING_IN_THIS_REGION_
	class Credentials
	{
		public string Username {
			get;
			private set;
		}
		public string Password {
			get;
			private set;
		}
		public Credentials(string username,string password) 
		{
			Username = username;
			Password = password;
		}
		
	};
	
	class App
	{
		private enum Option
		{
			Unknown=-1,
			Exit,
			ListStudent,
			ListCourse,
			RegisterStudent,
			EnrolStudent
		}
		private static App __instance;
		private App()
		{
			__dbMethods = new Dictionary<Option, DBMethod>();
			__dbMethods.Add (Option.ListStudent, ListStudent );
			__dbMethods.Add (Option.ListCourse, ListCourse );
			__dbMethods.Add (Option.RegisterStudent, RegisterStudent);
			__dbMethods.Add (Option.EnrolStudent,EnrolStudent);
			
		}		
		public static App Instance
		{
			get 
			{
				if(__instance == null) 
					__instance = new App(); 
				return __instance;  
			}
			private set {}
		}
		
		private Option DisplayMenu()
		{ 
		  Option option=Option.Unknown;
		  try
		  {
				Console.WriteLine("Course management");
				Console.WriteLine();
				Console.WriteLine("1. List students");
				Console.WriteLine("2. List courses");
				Console.WriteLine("3. Register student");
				Console.WriteLine("4. Enrol student");
				Console.WriteLine("0. Exit");
				var result = Console.ReadLine();
				option = (Option)Enum.Parse(typeof(Option), result);
			}
			catch(ArgumentException ex)
			{
				//nothing to do. User press select no option and press enter.
			}
			
			return option;
			
		}
		private void Login()
		{
			using(SqlConnection con = new SqlConnection(ConnectionString))
			{ 
				con.Open();
			}
			
		}
		private delegate void DBMethod();
		private System.Collections.Generic.Dictionary<Option, DBMethod> __dbMethods;
		public string ConnectionString {
			get;
			set;
		}
		
		public void Run()
		{
			Login ();
			Option userInput = Option.Unknown;
			do
			{
				Console.Clear();	
			  	userInput = DisplayMenu();
			  	Console.Clear();	
				try
				{		
				  	__dbMethods[userInput]();				
					Console.ReadKey();	
				}
				catch(KeyNotFoundException ex)
				{
					//Nothing to do. The option was not a valid one. Read another.
				}
			  
			}while(userInput!=Option.Exit);
		}
#endregion		
#region TO_IMPLEMENT
		private void printResults(IEnumerator results)
		{
            while(results.MoveNext())
            {
                Console.WriteLine(results.Current);
            }
		}
		private void ListStudent()
		{
			//TODO: Implement
			Console.WriteLine("ListStudent()");

		}
		private void ListCourse()
		{
			//TODO: Implement
			Console.WriteLine("ListCourse()");
		}
		private void RegisterStudent()
		{
			//TODO: Implement
			Console.WriteLine("RegisterStudent()");
		}
		private void EnrolStudent()
		{
			//TODO: Implement
			Console.WriteLine("EnrolStudent()");
		}
#endregion

	}
	class MainClass
	{
#region DO_NOT_CHANGE_NOTHING_IN_THIS_REGION__				
		public static Credentials getCredentials()
		{
			Console.Write("Enter your username: ");
			string username = Console.ReadLine();
			string password = "";
			Console.Write("Enter your password: ");
			
			ConsoleKeyInfo key;
			
			do
			{
			    key = Console.ReadKey(true);
			
			    // Backspace Should Not Work
			    if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
				{
				   password += key.KeyChar;
				   Console.Write("*");
				}
				else
				{
				   if (key.Key == ConsoleKey.Backspace && password.Length > 0)
				   {
				      password = password.Substring(0, (password.Length - 1));
				      Console.Write("\b \b");
				   }
				}
			}
			while (key.Key != ConsoleKey.Enter);
			
			return new Credentials(username,password);
		}
#endregion		
		public static void Main (string[] args)
		{
			Credentials cr = getCredentials();
            //TODO: Pass credentials to App
            App.Instance.ConnectionString = @"Data Source=10.62.73.95;Initial Catalog=" + cr.Username + "; User Id =" + cr.Username + "; Password =" + cr.Password + "; Max Pool Size = 10;";
            App.Instance.Run();
		}
		
	}
}
