namespace DependencyInjection
{
	class DependencyInjection
	{
		float Triangle_Area(float b, float h)
		{
			if (b <= 0 || h <= 0)
				return 0;
			else
				return 1/2 * b * h;
		}

		public static void Main(string[] args)
		{

        }
	}
}