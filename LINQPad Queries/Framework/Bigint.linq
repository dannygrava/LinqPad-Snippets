<Query Kind="Statements">
  <Reference>&lt;RuntimeDirectory&gt;\System.Numerics.dll</Reference>
  <Namespace>System.Numerics</Namespace>
</Query>

// Ten behoeven van controle nummer check
// http://rosettacode.org/wiki/IBAN#C.23

BigInteger bigint = BigInteger.Parse("1289128391823918398129381928391283918239819238918391391839189381298391283918293");
bigint.Dump();

(bigint % 97).Dump();