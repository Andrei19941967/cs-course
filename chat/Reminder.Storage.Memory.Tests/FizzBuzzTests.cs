using NUnit.Framework;

namespace Reminder.Storage.Memory.Tests
{
	// FizzBuzz
	// Написать метод, который принимает число (int)
	// Этот метод возвращает строку:
	//   Если число делится на 3, то строку "Fizz"
	//   Если число делится на 5, то строку "Buzz"
	//   Если число делится на 3 и на 5, то строку "FizzBuzz"
	//   Иначе - строку в виде числа (ToString)

	public class FizzBuzzTests
	{
		[Test]
		public void Given3_WhenFizzBuzz_ThenFizzReturned()
		{
			Assert.AreEqual("Fizz", FizzBuzz(3));
		}

		[Test]
		public void Given5_WhenFizzBuzz_ThenBuzzReturned()
		{
			Assert.AreEqual("Buzz", FizzBuzz(5));
		}

		[TestCase("FizzBuzz", 15)]
		[TestCase("16", 16)]
		public void FizzBuzz(string expectedString, int number)
		{
			Assert.AreEqual(expectedString, FizzBuzz(number));
		}

		public string FizzBuzz(int number) =>
			(number % 3 == 0, number % 5 == 0) switch
			{
				(true, true) => "FizzBuzz",
				(true, false) => "Fizz",
				(false, true) => "Buzz",
				_ => number.ToString()
			};
	}
}