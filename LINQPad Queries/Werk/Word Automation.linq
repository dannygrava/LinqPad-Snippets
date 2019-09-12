<Query Kind="Statements" />

dynamic app = Activator.CreateInstance(Type.GetTypeFromProgID("Word.Application"));
app.Visible = true;
dynamic doc = app.Documents.Add();
dynamic selection = app.Selection;
selection.TypeText ("Hello, world");
selection.TypeParagraph();

