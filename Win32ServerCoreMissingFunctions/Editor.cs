using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using WDCMLSDK;
using WDCMLSDKBase;

namespace WDCMLSDKDerived
{
	using Win32ServerCoreMissingFunctions;

	/// <summary>
	/// See the xml docs for EditorBase.
	/// </summary>
	internal class Editor : EditorBase
	{
		public Editor(FileInfo fileInfo, XNamespace xNamespace = null) : base(fileInfo, xNamespace) { }

		// Methods that don't modify.

		// Methods that modify. Set this.IsDirty to true only you modify the document directly, not
		// if you call a method that already does so.
	}
}