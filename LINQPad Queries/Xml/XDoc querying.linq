<Query Kind="Statements" />

	XDocument doc = new XDocument(
		new XElement("Results",
			new XElement("Player", 
				new XElement("Naam", "Mjb"),
				new XElement("RatAfter", "1480"),
				new XElement("RatBefore", "1479"),
				new XElement("Rank", "2"),
				new XElement("Score", "22"),
				new XElement("SB", "222")
				),
			new XElement("Player", 
				new XElement("Naam", "Bvp"),
				new XElement("RatAfter", "1082"),
				new XElement("RatBefore", "1079"),
				new XElement("Rank", "16"),
				new XElement("Score", "4"),
				new XElement("SB", "22")
				),			
			new XElement("Player", 
				new XElement("Naam", "Mjb"),
				new XElement("RatAfter", "1480"),
				new XElement("RatBefore", "1479"),
				new XElement("Rank", "2"),
				new XElement("Score", "22"),
				new XElement("SB", "222")
				),
			new XElement("Player", 
				new XElement("Naam", "Bvp"),
				new XElement("RatAfter", "1082"),
				new XElement("RatBefore", "1079"),
				new XElement("Rank", "16"),
				new XElement("Score", "4"),
				new XElement("SB", "22")
				)		
			)
		);
		
	//doc.Dump();
	doc.Element("Results").Elements("Player").Where(e=>e.Element("Naam").Value=="Bvp").Last().Dump();
	
	
	
	
