<Query Kind="Statements" />

var datestrs = new string[] {
	"2015-05-09T23:45:59.500-05:00", 
	"2015-05-09T03:45:59",
	"2015-05-09T03:45:59+00:00",
	"2015-06-09",
	"2015-06-18T09:30:15Z",
	"2015-06-09T04:45:59+01:00",
};
	
datestrs
	.Select(ds => new {
		ds, 
		DateRoundTrip=XmlConvert.ToDateTime(ds, XmlDateTimeSerializationMode.RoundtripKind),
		DateRoundUtc=XmlConvert.ToDateTime(ds, XmlDateTimeSerializationMode.Utc),
		ToStringRoundTrip=XmlConvert.ToString(XmlConvert.ToDateTime(ds, XmlDateTimeSerializationMode.RoundtripKind), XmlDateTimeSerializationMode.RoundtripKind),
		ToStringUtc=XmlConvert.ToString(XmlConvert.ToDateTime(ds, XmlDateTimeSerializationMode.RoundtripKind), XmlDateTimeSerializationMode.Utc),
		ToStringUnspecified = XmlConvert.ToString(XmlConvert.ToDateTime(ds, XmlDateTimeSerializationMode.Unspecified), XmlDateTimeSerializationMode.Unspecified)		
		}) 
		
	.Dump()	
	;
	
	
datestrs
	.Select(ds => XmlConvert.ToDateTimeOffset(ds))
	.Dump()
	.Select(dt => new {UTC =dt.UtcDateTime, Kind=dt.DateTime.Kind, DateTime = dt.DateTime})
	.Dump()
	;