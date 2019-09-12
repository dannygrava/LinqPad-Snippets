<Query Kind="Statements" />

new Hyperlinq ("http://stackoverflow.com/questions/3555317/linqpad-extension-methods", "LingPad extension methods").Dump();

Util.RawHtml (new XElement ("h1", "This is a big heading")).Dump();

Util.HorizontalRun (true, "Check out", new Hyperlinq ("http://stackoverflow.com", "this site"), "for answers to programming questions.").Dump();

//AppDomain.CurrentDomain.GetAssemblies().Where(ass => ass.Name == "Util").SelectMany(s => s.GetTypes()).Dump();
typeof(Util).GetMethods().Select(m=> m.Name).Dump();


typeof (LINQPad.Extensions).GetMethods().Select(m=> m.Name).Dump();

//Util.GetTypes().Dump();