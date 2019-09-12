<Query Kind="Statements" />

Uri quadrant = new Uri ("http://intranet.quadrant.local/");
Uri quadrantWiki = new Uri ("http://intranet.quadrant.local/wiki/");
Uri myPage = new Uri ("http://intranet.quadrant.local/wiki/index.php?title=Gebruiker:Dg");

//myPage.Dump();
quadrant.MakeRelativeUri (myPage).ToString().Dump("Relatieve Uri Quadrant");
quadrantWiki.MakeRelativeUri (myPage).ToString().Dump("Relatieve Uri QuadrantWiki");

Uri relative = new Uri ("index.php?title=Gebruiker:Dg", UriKind.RelativeOrAbsolute);

relative.IsAbsoluteUri.Dump("Is absolute");

new Uri(quadrantWiki, relative).Dump();
new Uri(quadrantWiki, "index.php?title=Gebruiker:Dg").Dump();
