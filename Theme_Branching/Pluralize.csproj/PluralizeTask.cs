public static class PluralizeTask
{
	public static string PluralizeRubles(int count)
	{
		if (count % 100 >= 11 && count % 100 <= 20 || count % 10 == 0 || count % 10 >= 5 && count % 10 <= 10)
			return "рублей";
		if (count % 10 >= 2 && count % 10 <= 4)
			return "рубля";
		return "рубль";
	}
}