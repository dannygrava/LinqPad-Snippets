<Query Kind="Statements" />

dynamic app = Activator.CreateInstance(Type.GetTypeFromProgID("Excel.Application"));
app.Visible = true;
dynamic workbook = app.Workbooks.Open("Top-2000-alle jaren.xlsx".FromDownloadFolder());
dynamic worksheet = workbook.Worksheets[1];


//LINQPad.Extensions.Dump(workbook.Worksheets[1].Columns[1].Value);
dynamic row = workbook.Worksheets[1].Rows[2].Value;
LINQPad.Extensions.Dump(row);
//LINQPad.Extensions.Dump(worksheet.Cell[1, 1]);

workbook.Close();
app.Quit();