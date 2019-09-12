<Query Kind="Statements" />

var test = @"01. LA MENDOZINA (Juan Alais)
02. EL GAUCHO ARGENTINO (Justo Tomas Morales)
03. EL GATO CORDOBES (Andres Chazarreta)
04. AIRES DE MI PATRIA (Justo Tomas Morales)
05. ECOS DE MI PAMPA (Pedro Quijano)
06. CAPRICHO PAMPEANO (Manuel Lopez)
07. LA RINCONADA (Antonio Sinopoli)
08. ÑO JUAN CARLOS (Justo Tomas Morales)
09. ARPA Y GUITARRA (Pedro Herrera)
10. DE REGRESO (Carmelo Rizzuti)
11. ALMA EN PENA (Abel Fleury)
12. DE CLAVEL EN LA OREJA (Abel Fleury)

";
System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(test.ToLower()).Dump();