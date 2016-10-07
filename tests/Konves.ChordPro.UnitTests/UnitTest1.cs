using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace Konves.ChordPro.UnitTests
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void TestXParser()
		{
			// Arrange
			var assembly = Assembly.GetExecutingAssembly();
			var resourceName = "Konves.ChordPro.UnitTests.Data.swing-low.cho";
			List<ILine> result;

			using (Stream stream = assembly.GetManifestResourceStream(resourceName))
			using (TextReader reader = new StreamReader(stream))
			{
				Parser sut = new Parser(reader);

				// Act
				result = sut.Parse().ToList();
			}

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
