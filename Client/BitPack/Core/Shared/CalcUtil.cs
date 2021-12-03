using System;

namespace BitPack.Core.Shared
{
	public static class CalcUtil
	{
		public static int CumulativeSum(int index, int offset, int amount)
		{
			int ret = 0;
			for (int i = 2; i <= index; i++)
			{
				ret += (amount * i) + offset;
			}

			return ret;
		}

		public static double CalcProgress(double total, double collected)
		{
			if (total <= 0) return 100;

			double ret = Math.Floor(collected / total * 100);
			return ret;
		}
	}
}
