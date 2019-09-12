<Query Kind="Statements">
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.dll</Reference>
  <Namespace>System.Globalization</Namespace>
  <Namespace>System.Net</Namespace>
</Query>

new Hyperlinq("http://test.rebex.net/", "test.rebex.net address is used for testing of FTP components.").Dump();

FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://test.rebex.net:21/readme.txt");
request.Credentials = new NetworkCredential("demo", "password");
//request.EnableSsl = true;
//request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
//request.Method = WebRequestMethods.Ftp.ListDirectory;
request.Method = WebRequestMethods.Ftp.DownloadFile;

using (FtpWebResponse response = (FtpWebResponse) request.GetResponse())
{
    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
    {
        //Console.WriteLine(reader.ReadToEnd());
		reader.ReadToEnd().Dump("Contents of " + request.RequestUri);
    }
}
