<Query Kind="FSharpProgram" />

let pathArtikelen = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"King\Exports\Artikelen_DemoArt.txt")
pathArtikelen |> Dump

// in F# there are no anonymous types; 
// see http://stackoverflow.com/questions/8144184/name-tuples-anonymous-types-in-f
type Artikel = {
	Code : string
	ZoekCode : string
	Oms : string
	Groep : int
	KostprijsEx : decimal
	Leveranciernummer : string	
}

let artikelen = 
	File.ReadAllLines(pathArtikelen) 
	|> Array.toList
	|> List.tail // return the list without first element
	|> List.map (fun line -> line.Split(';')) 
	// decimal, float, int operators parse from string with InvariantCulture
	|> List.map (fun line -> {Code = line.[0]; ZoekCode = line.[1]; Oms = line.[2]; Groep = int line.[3]; KostprijsEx = System.Convert.ToDecimal(line.[4]); Leveranciernummer = line.[5]})	
	
//artikelen |> Dump

// Gesorteerd op Groep , LeverancierNummer beide oplopend
List.sortBy (fun artikel -> (artikel.Groep, artikel.Leveranciernummer)) artikelen |> ignore
// Gesorteerd op Groep , LeverancierNummer beide aflopend
artikelen |> List.sortBy (fun artikel -> artikel.Groep, artikel.Leveranciernummer) |> List.rev |> Dump
// Gesorteerd op Groep oplopend , LeverancierNummer aflopend -> lukt me niet zonder Linq SortByDescending 

// Alleen artikelen binnen groep 40
artikelen |> List.filter (fun artikel -> artikel.Groep = 40) |> Dump
