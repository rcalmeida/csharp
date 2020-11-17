		public bool ValidateInput(string inputValue, int inputType, int minLength, int maxLength)
		{
			/*
			inputType: any string to match against the regex library below
			inputType:
				0 - words and basic punctuation only - generic use,
				1 - letters only - case insentive,
				2 - numbers only - positive integers,
				3 - numbers only - positive or negative integers,
				4 - numbers only - positive decimals,
				5 - numbers only - positive or negative decimals,
				6 - phone numbers
				7 - canadian postal code
				8 - email address
			minLength: minimum lenght of the resulting string for validation purposes
			maxLength: maximum lenght of the resulting string for validation purposes
			*/

			string[] regexString = new string[] {
				@"[\w\'\,\.\/\-\(\)\:\ ]", 
				@"[A-Za-z]", 
				@"[0-9]",
				@"\-?[0-9]",
				@"([0-9]*\.?[0-9]*)?[0-9]",
				@"(\-?[0-9]*\.?[0-9]*)?[0-9]",
				@"(\+?[1-9]{1,3})?[0-9]{3}[0-9]{3}[0-9]{4}",
				@"([ABCEGHJKLMNPRSTVXY]{1}[0-9]{1}[ABCEGHJKLMNPRSTVWXYZ]{1})\s?([0-9]{1}[ABCEGHJKLMNPRSTVWXYZ]{1}[0-9]{1})",
				@"[A-Za-z0-9\.\-\_]{3,32}\@[A-Za-z0-9\.\-]{3,32}\.[AZ-a-z]{2,3}"
			};
			RegexOptions regexConfig = RegexOptions.IgnoreCase | RegexOptions.Multiline;

			string cleanValue = Regex.Replace(inputValue, regexString[inputType], string.Empty, regexConfig );

			if (cleanValue.Length >= minLength && cleanValue.Length <= maxLength)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
