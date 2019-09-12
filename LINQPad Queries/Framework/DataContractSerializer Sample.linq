<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Runtime.Serialization.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Runtime.Serialization.Json.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Runtime.Serialization.Xml.dll</Reference>
  <Namespace>System.Runtime.Serialization</Namespace>
  <Namespace>System.Runtime.Serialization.Json</Namespace>
</Query>

void Main()
{
	Record record1 = new Record(1, 2.00d, "+", 3, Position.Employee);
	record1.AanmaakDatumTijd = DateTime.Now;
	
	//string.Format("Original record: {0}", record1.ToString()).Dump();
	
	MemoryStream stream1 = new MemoryStream();

	//Serialize the Record object to a memory stream using DataContractSerializer.
	DataContractSerializer serializer = new DataContractSerializer(typeof(Record));
	serializer.WriteObject(stream1, record1);
	
	stream1.Position = 0;

	//Deserialize the Record object back into a new record object.
	Record record2 = (Record)serializer.ReadObject(stream1);	

	//Console.WriteLine("Deserialized record: {0}", record2.ToString());
	
	stream1.Position = 0;
	
	//new XDocument(stream1)).Dump("Dump as Xml");
	XDocument.Load(stream1)
		.Dump("Record")
		;	
	
//	MemoryStream stream2 = new MemoryStream();
//	DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Record));
//	ser.WriteObject(stream2, record1);	
//	Encoding.Default.GetString(stream2.ToArray()).Dump("JSON");
	
	var document = new Document() { Bovk = new BetalingsOpdrachtVanKIB() { Bankomgeving = 10, Valuta = "EUR" } };
	MemoryStream stream2 = new MemoryStream();
	DataContractSerializer ser = new DataContractSerializer(typeof(Document));
	ser.WriteObject(stream2, document);	
	XDocument.Parse(Encoding.Default.GetString(stream2.ToArray()).Dump("Xml")).Dump();
	
	VerzoekToestemmingRequest request = new VerzoekToestemmingRequest() {RekNr="123", BankOmgevingId=1};	
	MemoryStream stream3 = new MemoryStream();
	DataContractJsonSerializer ser2 = new DataContractJsonSerializer(typeof(VerzoekToestemmingRequest));
	ser2.WriteObject(stream3, request);	
	stream3.Position = 0;
	using (StreamReader reader = new StreamReader(stream3))
	{
		reader.ReadToEnd().Dump("VerzoekToestemmingRequest");
	}	
	
}

    [DataContract(Name = "Document")]
    public class VerzoekToestemmingRequest
    {
        [DataMember(Name = "RekNr", Order = 0)]
        public string RekNr { get; set; }

        [DataMember(Name = "BankOmgevingId", Order = 1)]
        public int BankOmgevingId { get; set; }
    }


// Define other methods and classes here

[DataContract]
public enum Position
{
    [EnumMember(Value = "Emp")]
    Employee,
    [EnumMember(Value = "Mgr")]
    Manager,
    [EnumMember(Value = "Ctr")]
    Contractor,
    NotASerializableEnumeration
}


[CollectionDataContract(ItemName="FamilyMember", Namespace="http://Microsoft.ServiceModel.Samples")]
public class FamilyList: List<string>{}

[DataContract(Namespace="http://Microsoft.ServiceModel.Samples", Name="Document")]
internal class Record
{
    private double n1;
    private double n2;
    private string operation;
    private double result;

    internal Record(double n1, double n2, string operation, double result, Position position)
    {
        this.n1 = n1;
        this.n2 = n2;
        this.operation = operation;
        this.result = result;
		this.Position = position;
		this.Options = StringSplitOptions.RemoveEmptyEntries;
		this.Values = new FamilyList {"Broer", "Zus", "Neef", "Nicht"};
		//this.AanmaakDatumTijd =  new DateTime (2015, 5, 11, 1, 59, 0, DateTimeKind.Utc);
		this.AanmaakDatumTijd =  new DateTime (2015, 5, 11);
    }
	
	[DataMember]	
	internal Position Position {get;set;}
	
	[DataMember]
	internal StringSplitOptions Options {get;set;}
	
	[DataMember]
	internal DateTime AanmaakDatumTijd {get; set;}

    [DataMember]
    internal double OperandNumberOne
    {
        get { return n1; }
        set { n1 = value; }
    }

    [DataMember]
    internal double OperandNumberTwo
    {
        get { return n2; }
        set { n2 = value; }
    }

    [DataMember]
    internal string Operation
    {
        get { return operation; }
        set { operation = value; }
    }

    [DataMember]
    internal double Result
    {
        get { return result; }
        set { result = value; }
    }	
	
	[DataMember (Name="FamilyMembers")]
	internal FamilyList Values
	{
		get;set;
	}

    public override string ToString()
    {
        return string.Format("Record: {0} {1} {2} = {3}", n1, operation, n2, result);
    }
}

    /// <summary>
    /// Wrapper om Document tag te genereren 
    /// </summary>
    [DataContract(Name = "Document", Namespace = Ns)]
    public class Document
    {
        public const string Ns = "urn:iso:std:iso:20222:tech:xsd:bovk.001.001.01";
        [DataMember]
        public BetalingsOpdrachtVanKIB Bovk { get; set; }
    }
	
	[DataContract]
	public class BetalingsOpdrachtVanKIBBase
    {
		[DataMember]
        public int Bankomgeving { get; set; }
		[DataMember]
        public string Valuta { get; set; }
    }

    /// <summary>
    /// Betalingsopdracht van KIB (Bovk)
    /// </summary>
	[KnownType(typeof(BetalingsOpdrachtVanKIBBase))]
	[DataContract(Namespace = Document.Ns)]
    public class BetalingsOpdrachtVanKIB : BetalingsOpdrachtVanKIBBase
    {
    }
	
	[DataContract(Namespace = "urn:iso:std:iso:20222:tech:xsd:iovk.001.001.01")]
    public class IncassoOpdrachtVanKIB : BetalingsOpdrachtVanKIBBase
    {
    }