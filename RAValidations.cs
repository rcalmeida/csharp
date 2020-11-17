using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace RAValidations
{
	public class RAStrings
	{
		/* https://docs.microsoft.com/en-us/dotnet/api/system.globalization.textinfo.totitlecase?view=netframework-4.7 */
		public static bool TitleGrammar(ref string inputString)
		{
			TextInfo setTextCase = CultureInfo.CurrentCulture.TextInfo;
			inputString = inputString.Trim().ToLower();

			string[] regexStrings = new string[] { @"^[a-zA-Z0-9 ]*$", @"[^a-zA-Z0-9 ]" };
			//Regex regexPattern = new Regex(regexStrings[0], RegexOptions.IgnoreCase);
			inputString = Regex.Replace(inputString, regexStrings[1], string.Empty);
			if (string.IsNullOrEmpty(inputString))
			{
				inputString = string.Empty;
				return false;
			}
			else
			{
				string[] tempTitle = inputString.Split(" ");
				inputString = string.Empty;
				foreach (string tempWord in tempTitle)
				{
					inputString += setTextCase.ToTitleCase(tempWord) + " ";
				}
				inputString = inputString.Trim();
				return true;
			}
		}//method TitleGrammar

		/* https://docs.microsoft.com/en-ca/archive/blogs/dboyle/checking-a-string-for-a-valid-isbn-number
		*  https://www.oreilly.com/library/view/regular-expressions-cookbook/9781449327453/ch04s13.html */
		public static bool ISBN(ref string inputString)
		{
			string regexString = @"[^0-9]";
			//Regex regexPattern = new Regex(regexString, RegexOptions.IgnoreCase);
			int isbn13Length = 13;
			inputString = Regex.Replace(inputString, regexString, string.Empty);

			if (inputString.Length == isbn13Length)
			{
				return true;
			}
			else if (string.IsNullOrEmpty(inputString))
			{
				inputString = string.Empty;
				return false;
			}
			else
			{
				return false;
			}
		}//method ISBN

		public static bool PhoneNumber(ref string inputString)
		{
			int phoneNumberLength = 10;
			string[] regexStrings = new string[] { @"[^0-9]", @"(\d{3})(\d{3})(\d{4})", @"$1-$2-$3" };
			//Regex regexPattern = new Regex(regexStrings[0], RegexOptions.IgnoreCase);
			inputString = Regex.Replace(inputString, regexStrings[0], string.Empty);
			if (inputString.Length == phoneNumberLength)
			{
				inputString = Regex.Replace(inputString, regexStrings[1], regexStrings[2]);
				return true;
			}
			else if (string.IsNullOrEmpty(inputString))
			{
				inputString = string.Empty;
				return false;
			}
			else
			{
				return false;
			}
		}//method PhoneNumber

		public static bool CanadianPostalCode(ref string inputString)
		{
			int postalCodeLenght = 6;
			string regexString = @"([ABCEGHJKLMNPRSTVXY]{1}[0-9]{1}[ABCEGHJKLMNPRSTVWXYZ]{1})([0-9]{1}[ABCEGHJKLMNPRSTVWXYZ]{1}[0-9]{1})";
			Regex regexPattern = new Regex(regexString, RegexOptions.IgnoreCase);

			inputString = Regex.Replace(inputString, regexString, string.Empty);
			if (regexPattern.IsMatch(inputString))
			{
				if (inputString.Length == postalCodeLenght)
				{
					inputString = inputString.Insert(3, " ").ToUpper();
				}
				return true;
			}
			else if (string.IsNullOrEmpty(inputString))
			{
				inputString = string.Empty;
				return false;
			}
			else
			{
				return false;
			}
		}//method CanadianPostalCode

		/*	https://docs.microsoft.com/en-us/dotnet/standard/base-types/how-to-verify-that-strings-are-in-valid-email-format
			https://docs.microsoft.com/en-us/dotnet/api/system.net.mail.mailaddress.address?view=netframework-4.8 */
		public static bool EmailAddress(ref string inputString)
		{
			string regexString = @"^[A-Za-z0-9.-_]@[A-Za-z0-9.-]\.[A-Za-z]+$";
			//Regex regexPattern = new Regex(regexString, RegexOptions.IgnoreCase);
			inputString = Regex.Replace(inputString, regexString, string.Empty);
			inputString = inputString.ToLower();

			if (string.IsNullOrEmpty(inputString))
			{
				inputString = string.Empty;
				return false;
			}
			else
			{
				try
				{
					MailAddress tempEmailAddress = new MailAddress(inputString);
					inputString = tempEmailAddress.ToString();
					return true;
				}
				catch
				{
					return false;
				}
			}
		}//method EmailAddress

		/* https://stackoverflow.com/questions/15491894/regex-to-validate-date-format-dd-mm-yyyy */
		public static bool DateValidation(ref string inputString)
		{
			if (string.IsNullOrEmpty(inputString))
			{
				inputString = string.Empty;
				return false;
			}
			else
			{
				try
				{
					//DateTime.TryParse(inputString, out _);
					inputString = DateTime.Parse(inputString).ToString();
					return true;
				}
				catch
				{
					return false;
				}
			}
		}//method DateValidation
	}//class RAStrings
}//namespace RAValidations
