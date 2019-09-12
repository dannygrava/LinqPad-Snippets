<Query Kind="Statements" />

dynamic outlook = Activator.CreateInstance(Type.GetTypeFromProgID("Outlook.Application"));
/*
dynamic msg = outlook.CreateItem(0);

msg.To = "dq@King.Eu";
msg.Subject = "Outlook automation from C#";
msg.Body = "Dit is een test.";
msg.Display(); //msg.Send();
*/

new Hyperlinq("http://msdn.microsoft.com/en-us/library/bb208072(v=office.12).aspx").Dump("OlDefaultFolders Enumeration");

//const string AanvraagSubject = "Nieuwe kopieermachines";
//const string AanvraagSubject = "Aanvraag Licentie uitbreiding";
const string AanvraagSubject = "Traktatie";
//const string AanvraagSubject = "RE: Werken bij King Software";


const int olFolderInbox = 6;
const int olFolderDrafts = 16;
const int olHTML = 0x5;
dynamic Inbox = outlook.GetNamespace("Mapi").GetDefaultFolder(olFolderInbox);
dynamic InboxItems = Inbox.Items;
foreach (dynamic Mailobject in InboxItems)
{
	//LINQPad.Extensions.Dump(Mailobject.Subject, "");
	if (Mailobject.Subject == AanvraagSubject)
	{
		LINQPad.Extensions.Dump(Mailobject.Body, string.Format("Message Body of mail with subject {0}", Mailobject.Subject));		
		//LINQPad.Extensions.Dump(Mailobject.HtmlBody, string.Format("Html Message Body of mail with subject {0}", Mailobject.Subject));		
		//Mailobject.SaveAs("Traktatie.html".FromDownloadFolder(), olHTML);
//		var asciiEncoding = new ASCIIEncoding();
//		LINQPad.Extensions.Dump(asciiEncoding.GetString(Mailobject.RtfBody), string.Format("Rtf Message Body of mail with subject {0}", Mailobject.Subject));		
	}
}