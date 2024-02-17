namespace Framework;

public static class DateTime : object
{
	static DateTime()
	{
	}
		public static System.DateTimeOffset Now
	{
		get
		{
			var result =
				System.DateTime.Now;
			return result;
		}
	}

	public static bool TokenExpired(System.DateTime CreationDate)
	{

		if (CreationDate.AddMinutes(2) >= System.DateTime.Now)
		{
			return false;
		}
		else
		{
			return true;
		}
	}
}
