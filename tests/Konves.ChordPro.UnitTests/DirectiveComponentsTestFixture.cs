using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konves.ChordPro.UnitTests
{
	[TestClass]
	public class DirectiveComponentsTestFixture
	{
		[TestMethod]
		public void TryParseTest()
		{
			DoTryParseTest("{asdf:qwerty}", true, "asdf", string.Empty, "qwerty");
			DoTryParseTest("  {  asdf  :  qwerty  }  ", true, "asdf", string.Empty, "qwerty");
			DoTryParseTest("{asdf abc:qwerty}", true, "asdf", "abc", "qwerty");
			DoTryParseTest("  {  asdf   abc  :  qwerty  asdf  }  ", true, "asdf", "abc", "qwerty  asdf");
			DoTryParseTest("{asdf:qwerty}#Comment", true, "asdf", string.Empty, "qwerty");
			DoTryParseTest("  {  asdf  :  qwerty  }  # Comment", true, "asdf", string.Empty, "qwerty");
			DoTryParseTest("{asdf abc:qwerty}# Comment", true, "asdf", "abc", "qwerty");
			DoTryParseTest("  {  asdf   abc  :  qwerty  asdf  }  # Comment", true, "asdf", "abc", "qwerty  asdf");

			DoTryParseTest("{}", false, null, null, null);
		}

		private void DoTryParseTest(string input, bool expectedResult, string expectedKey, string expectedSubKey, string expectedValue)
		{
			// Arrange
			DirectiveComponents components;

			// Act
			bool result = DirectiveComponents.TryParse(input, out components);

			// Assert
			Assert.AreEqual(expectedResult, result);
			Assert.AreEqual(expectedKey, components?.Key);
			Assert.AreEqual(expectedSubKey, components?.SubKey);
			Assert.AreEqual(expectedValue, components?.Value);
		}
	}
}
