using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using WDCMLSDK;
using WDCMLSDKBase;

namespace Win32ServerCoreMissingFunctions
{
}

namespace WDCMLSDKDerived
{
	using Win32ServerCoreMissingFunctions;

	/// <summary>
	/// See the xml docs for ProgramBase.
	/// </summary>
	internal class Program : ProgramBase
	{
		// Logs
		private Log exceptionLog = null;
		private Log libraryNamesLog = null;

		// Data
		//private Dictionary<string, string> uniqueKeyMap = null;
		private Dictionary<string, List<string>> functionsInDesktopExperienceButNotInServerCore = null;

		static int Main(string[] args)
		{
			return (new Program()).Run();
		}

		protected override void OnRun()
		{
			var apiRefModelWin32 = ApiRefModelWin32.GetApiRefModelWin32(Platform.Win32DesktopAndWsuaDotTxt);

			//this.uniqueKeyMap = this.LoadUniqueKeyMap("uniqueKeyMap.txt");
			this.functionsInDesktopExperienceButNotInServerCore = this.LoadNonUniqueKeyMap("functionsInDesktopExperienceButNotInServerCore.txt");
			foreach (var binaryName in this.functionsInDesktopExperienceButNotInServerCore.Keys)
			{
				foreach (var functionName in this.functionsInDesktopExperienceButNotInServerCore[binaryName])
				{
					FunctionWin32InDocs foundFunctionWin32 = null;
					apiRefModelWin32.GetFunctionWin32ByName(functionName, ref foundFunctionWin32);
				}
			}
		}
	}
}