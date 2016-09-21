using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntlrHelper
{
	class Program
	{
		static void Main(string[] args)
		{
			//Console.Write(args[0]);
			//Console.Write(args[1]);

			using (Stream inputStream = new FileStream(args[0], FileMode.Open))
			using (StreamReader inputReader = new StreamReader(inputStream))
			using (Stream outputStream = new FileStream(args[1], FileMode.Create))
			using (StreamWriter outputWriter = new StreamWriter(outputStream))
				outputWriter.Write(inputReader.ReadToEnd().Replace("public partial class", "internal partial class"));
		}
	}
}
