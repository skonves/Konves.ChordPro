using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using System.IO;
using Antlr4.Runtime;
using static Konves.ChordPro.ChordProParser;
using System.Text;

namespace Konves.ChordPro.UnitTests
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void TestMethod1()
		{
			// Arrange
			var assembly = Assembly.GetExecutingAssembly();
			var resourceName = "Konves.ChordPro.UnitTests.Data.love-me-tender.cho";

			AntlrInputStream inputStream;

			using (Stream stream = assembly.GetManifestResourceStream(resourceName))
			{
				inputStream = new AntlrInputStream(stream);
			}

			ChordProLexer lexer = new ChordProLexer(inputStream);
			CommonTokenStream commonTokenStream = new CommonTokenStream(lexer);
			ChordProParser parser = new ChordProParser(commonTokenStream);

			parser.RemoveErrorListeners();

			// Act
			DocumentContext result = parser.document();

			// Assert
			Assert.IsNotNull(result);
		}

		[TestMethod]
		public void TestMethod2()
		{
			// Arrange
			var assembly = Assembly.GetExecutingAssembly();
			var resourceName = "Konves.ChordPro.UnitTests.Data.everybody-hurts.cho";

			using (Stream stream = assembly.GetManifestResourceStream(resourceName))
			{
				// Act
				Document result = ChordProSerializer.Deserialize(stream);

				// Assert
				Assert.IsNotNull(result);
			}
		}

		[TestMethod]
		public void TestMethod3()
		{
			// Arrange
			var assembly = Assembly.GetExecutingAssembly();
			var resourceName = "Konves.ChordPro.UnitTests.Data.everybody-hurts.cho";

			using (Stream stream = assembly.GetManifestResourceStream(resourceName))
			{
				Document doc = ChordProSerializer.Deserialize(stream);
				StringBuilder sb = new StringBuilder();
				TextWriter writer = new StringWriter(sb);

				// Act
				ChordProSerializer.Serialize(doc, writer);
				string result = sb.ToString();

				// Assert
				Assert.IsNotNull(result);
			}
		}
	}
}
