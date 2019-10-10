/*
 * Created by SharpDevelop.
 * User: Bitl
 * Date: 10/10/2019
 * Time: 7:06 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

	public class CryptoRandom : RandomNumberGenerator
	{
		private static RandomNumberGenerator r;
	
		public CryptoRandom()
	 	{ 
	  		r = RandomNumberGenerator.Create();
	 	}
	
	 	///<param name=”buffer”>An array of bytes to contain random numbers.</param>
	 	public override void GetBytes(byte[] buffer)
	 	{
	  		r.GetBytes(buffer);
	 	}
	 	
		public override void GetNonZeroBytes(byte[] data)
		{
			r.GetNonZeroBytes(data);
		}
	 	public double NextDouble()
	 	{
	  		byte[] b = new byte[4];
	  		r.GetBytes(b);
	  		return (double)BitConverter.ToUInt32(b, 0) / UInt32.MaxValue;
	 	}
	
	 	///<param name=”minValue”>The inclusive lower bound of the random number returned.</param>
	 	///<param name=”maxValue”>The exclusive upper bound of the random number returned. maxValue must be greater than or equal to minValue.</param>
	 	public int Next(int minValue, int maxValue)
	 	{
	  		return (int)Math.Round(NextDouble() * (maxValue - minValue - 1)) + minValue;
	 	}
	 	public int Next()
	 	{
	  		return Next(0, Int32.MaxValue);
	 	}
	
	 	///<param name=”maxValue”>The inclusive upper bound of the random number returned. maxValue must be greater than or equal 0</param>
	 	public int Next(int maxValue)
	 	{
	  		return Next(0, maxValue);
	 	}
	}