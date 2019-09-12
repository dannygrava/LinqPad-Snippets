<Query Kind="Statements">
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.dll</Reference>
  <Namespace>System.Globalization</Namespace>
  <Namespace>System.Net</Namespace>
</Query>

const string licGenExe = @"D:\King_Trunk\Run\KingLicGen.exe";
const string parameters = "LICNUMMER 25000 LICVERSIE Factureren LICAANTALGEB 11 LICOPTIES Factuurhistorie " + 
	"LICADRES \"Zwaarddans 53\" LICNAAM \"DANNY GRAVA\" LICPLAATS \"2907AS CAPELLE AD IJSSEL\"";

System.Diagnostics.Process.Start(licGenExe + " " + parameters);