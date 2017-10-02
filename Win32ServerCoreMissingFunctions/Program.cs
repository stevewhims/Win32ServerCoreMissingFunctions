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
		private Log notPublishedMissingWin32FunctionsLog = null;
		private Log publishedMissingWin32FunctionsLog = null;

		// Data
		//private Dictionary<string, string> uniqueKeyMap = null;
		private Dictionary<string, List<string>> functionsInDesktopExperienceButNotInServerCore = null;

		static int Main(string[] args)
		{
			return (new Program()).Run();
		}

		protected override void OnRun()
		{
			this.notPublishedMissingWin32FunctionsLog = new Log()
			{
				Filename = "NotPublishedMissingWin32Functions_Log.txt",
				AnnouncementStyle = ConsoleWriteStyle.Default,
				Headers = new string[] { "binaryName", "functionName" }
			};
			this.RegisterLog(this.notPublishedMissingWin32FunctionsLog);

			this.publishedMissingWin32FunctionsLog = new Log()
			{
				Filename = "PublishedMissingWin32Functions_Log.txt",
				AnnouncementStyle = ConsoleWriteStyle.Default,
				Headers = new string[] { "binaryName", "functionName" }
			};
			this.RegisterLog(this.publishedMissingWin32FunctionsLog);

			//this.uniqueKeyMap = this.LoadUniqueKeyMap("uniqueKeyMap.txt");
			this.functionsInDesktopExperienceButNotInServerCore = this.LoadNonUniqueKeyMap("functionsInDesktopExperienceButNotInServerCore.txt");

			int totalValidMissingWin32FunctionNames = 0;
			int notPublishedMissingWin32FunctionNames = 0;
			int publishedMissingWin32FunctionNames = 0;

			var apiRefModelWin32 = ApiRefModelWin32.GetApiRefModelWin32(Platform.Win32DesktopAndWsuaDotTxt);

			foreach (var binaryName in this.functionsInDesktopExperienceButNotInServerCore.Keys)
			{
				totalValidMissingWin32FunctionNames += this.functionsInDesktopExperienceButNotInServerCore[binaryName].Count;

				foreach (var functionName in this.functionsInDesktopExperienceButNotInServerCore[binaryName])
				{
					FunctionWin32InDocs foundFunctionWin32 = null;
					apiRefModelWin32.GetFunctionWin32ByName(functionName, ref foundFunctionWin32);

					if (foundFunctionWin32 != null)
					{
						++publishedMissingWin32FunctionNames;
						this.publishedMissingWin32FunctionsLog.AddEntry(binaryName, functionName);
					}
					else
					{
						++notPublishedMissingWin32FunctionNames;
						this.notPublishedMissingWin32FunctionsLog.AddEntry(binaryName, functionName);
					}
				}
			}

			ProgramBase.ConsoleWrite(string.Format("{0} valid names in functionsInDesktopExperienceButNotInServerCore.txt", totalValidMissingWin32FunctionNames));
			ProgramBase.ConsoleWrite(string.Format("{0} of those are published", publishedMissingWin32FunctionNames));
			ProgramBase.ConsoleWrite(string.Format("{0} are not published", notPublishedMissingWin32FunctionNames));
		}
	}
}