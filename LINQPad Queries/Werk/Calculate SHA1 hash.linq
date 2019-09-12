<Query Kind="Statements">
  <Reference>&lt;RuntimeDirectory&gt;\System.Windows.Forms.dll</Reference>
  <Namespace>System.Security.Cryptography</Namespace>
</Query>

dynamic outlook = Activator.CreateInstance(Type.GetTypeFromProgID("Outlook.Application"));
const string AanvraagSubject = "Aanvraag Licentie uitbreiding";
const int olFolderInbox = 6;
const int olFolderDrafts = 16;
dynamic Inbox = outlook.GetNamespace("Mapi").GetDefaultFolder(olFolderDrafts);
dynamic InboxItems = Inbox.Items;
foreach (dynamic Mailobject in InboxItems)
{
	if (Mailobject.Subject == AanvraagSubject)
	{		
		string input = Mailobject.Body;
		
		var pattern = @"(?m)^\s*(?'name'\w+)\s*= *(?'value'.*)[ ]*\r$";
		const string FIXED_SECRET_SUFFIX = "!@#$";		
		
		var matches = Regex.Matches(input, pattern);
		
		string toHash = "";
		string checksum = "";
		foreach (Match m in matches)
		{	
			if (m.Groups["name"].Value == "Checksum")
				checksum = m.Groups["value"].Value;
			else if (new string []{"Opmerking", "BesteldDoor", "BevestigingNaar" }.Contains(m.Groups["name"].Value))		
				continue;
			else		
				toHash = toHash + m.Groups["value"].Value;
		}
		
		toHash = toHash + FIXED_SECRET_SUFFIX;
		
		toHash.Dump("String to hash found in mail message");
		
		//var asciiEncoding = new ASCIIEncoding();
		//asciiEncoding.GetBytes(toHash).Dump();
		
		checksum.Dump("Checksum found in mail message");
		
		SHA1 sha = new SHA1CryptoServiceProvider(); // This is one implementation of the abstract class SHA1.
		//var result = sha.ComputeHash(Encoding.UTF8.GetBytes(toHash));
		var result = sha.ComputeHash(Encoding.Default.GetBytes(toHash));
		
		StringBuilder sb = new StringBuilder();
			foreach (byte b in result)
				sb.Append(b.ToString("X2"));
		
		sb.ToString().Dump("SHA1 hash");
		//BitConverter.ToString(result).Dump("SHA1 hash");		
		(checksum == sb.ToString()).Dump("Same hash?");
	}
}