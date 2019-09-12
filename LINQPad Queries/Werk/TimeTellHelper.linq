<Query Kind="Statements" />

double werkweek = 40d; // 40 urige werkweek
double saldoVrijdagOchtend = 31.67; // Saldo aan het begin van de ochtend   
TimeSpan begonnen = new TimeSpan(8, 18, 0); // Tijd ingeklokt 's ochtends
TimeSpan pauzeStart = new TimeSpan(12, 08, 0); // Tijd uitgeklokt pauze
TimeSpan pauzeEind = new TimeSpan(12, 37, 0); // Tijd ingeklokt pauze

double tekort = werkweek - saldoVrijdagOchtend;
TimeSpan teWerken = new TimeSpan((int) Math.Truncate(tekort), (int) ((tekort- (int) Math.Truncate(tekort)) * 60), 0);

TimeSpan tijdWeg = begonnen + teWerken + (pauzeEind - pauzeStart).Dump("Pauze tijd");
tijdWeg.Dump("8 uur volgemaakt om");

// Variant 2
TimeSpan gestopt= new TimeSpan(17, 14, 0); // Tijd gestopt

var gewerkt = ((pauzeStart - begonnen) + (gestopt - pauzeEind)).Dump();
decimal gewerktInDec = gewerkt.Hours + (gewerkt.Minutes / 60m);
$"{gewerktInDec:N2}".Dump("Dagtijd in decimalen");