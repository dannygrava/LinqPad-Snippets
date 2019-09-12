<Query Kind="Program">
  <Namespace>Microsoft.Win32</Namespace>
</Query>

void Main()
{
	new Hyperlinq("https://msdn.microsoft.com/en-us/library/hh925568(v=vs.110).aspx", "How to: Determine Which .NET Framework Versions Are Installed").Dump();
	
	GetVersionFromRegistry4AndLower();
	Get45or451FromRegistry();
	const int VERSION452 = 379893;
	CheckFor45DotVersion(VERSION452).Dump();
}

// Define other methods and classes here
private static void GetVersionFromRegistry4AndLower()
{
     // Opens the registry key for the .NET Framework entry.
        using (RegistryKey ndpKey = 
            RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, "").
            OpenSubKey(@"SOFTWARE\Microsoft\NET Framework Setup\NDP\"))
        {
            // As an alternative, if you know the computers you will query are running .NET Framework 4.5 
            // or later, you can use:
            // using (RegistryKey ndpKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, 
            // RegistryView.Registry32).OpenSubKey(@"SOFTWARE\Microsoft\NET Framework Setup\NDP\"))
        foreach (string versionKeyName in ndpKey.GetSubKeyNames())
        {
            if (versionKeyName.StartsWith("v"))
            {

                RegistryKey versionKey = ndpKey.OpenSubKey(versionKeyName);
                string name = (string)versionKey.GetValue("Version", "");
                string sp = versionKey.GetValue("SP", "").ToString();
                string install = versionKey.GetValue("Install", "").ToString();
                if (install == "") //no install info, must be later.
                    Console.WriteLine(versionKeyName + "  " + name);
                else
                {
                    if (sp != "" && install == "1")
                    {
                        Console.WriteLine(versionKeyName + "  " + name + "  SP" + sp);
                    }

                }
                if (name != "")
                {
                    continue;
                }
                foreach (string subKeyName in versionKey.GetSubKeyNames())
                {
                    RegistryKey subKey = versionKey.OpenSubKey(subKeyName);
                    name = (string)subKey.GetValue("Version", "");
                    if (name != "")
                        sp = subKey.GetValue("SP", "").ToString();
                    install = subKey.GetValue("Install", "").ToString();
                    if (install == "") //no install info, must be later.
                        Console.WriteLine(versionKeyName + "  " + name);
                    else
                    {
                        if (sp != "" && install == "1")
                        {
                            Console.WriteLine("  " + subKeyName + "  " + name + "  SP" + sp);
                        }
                        else if (install == "1")
                        {
                            Console.WriteLine("  " + subKeyName + "  " + name);
                        }
                    }
                }
            }
        }
    }
}

private static void Get45or451FromRegistry()
{
	using (RegistryKey ndpKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey("SOFTWARE\\Microsoft\\NET Framework Setup\\NDP\\v4\\Full\\")) {
		int releaseKey = Convert.ToInt32(ndpKey.GetValue("Release"));
		if (true) {
			Console.WriteLine("Version: " + CheckFor45DotVersion(releaseKey));
		}
	}
}

// Checking the version using >= will enable forward compatibility, 
// however you should always compile your code on newer versions of
// the framework to ensure your app works the same.
private static string CheckFor45DotVersion(int releaseKey)
{
   if (releaseKey >= 393295) {
      return "4.6 or later";
   }
   if ((releaseKey >= 379893)) {
		return "4.5.2 or later";
	}
	if ((releaseKey >= 378675)) {
		return "4.5.1 or later";
	}
	if ((releaseKey >= 378389)) {
		return "4.5 or later";
	}
	// This line should never execute. A non-null release key should mean
	// that 4.5 or later is installed.
	return "No 4.5 or later version detected";
}