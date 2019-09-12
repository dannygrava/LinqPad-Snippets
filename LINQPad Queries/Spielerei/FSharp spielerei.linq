<Query Kind="FSharpProgram" />

let treatLine (line: string) =
    let stringParts = line.Split(';')
    stringParts
    
let data = 
    File.ReadAllLines(@"RoundRobinResults.csv".FromLinqpadDataFolder())
    //File.ReadAllLines(@"d:\users\dg\documents\linqpad_data\_RoundRobinResults_22112012_FormatV1.csv")
    |> Array.map treatLine

let myAdd = fun x y -> x + y
myAdd 10 5 |> Dump

let raisePowerTwo x = x ** 2.0
raisePowerTwo 8.0 |> Dump

let n = 10
let add a b = a + b
let result = add n 4
printfn "%i" (result)

let ``more?`` = "style"
``more?`` |> Dump
printfn "%s" (``more?``)

// Function that returns a function to
let calculatePrefixFunction prefix = 
    // calculate prefix
    let prefix' = Printf.sprintf "[%s]: " prefix
    // Define function to perform prefixing
    let prefixFunction appendee = 
        Printf.sprintf "%s%s" prefix' appendee
    // return function
    prefixFunction
    
// Create the prefix function
let prefixer = calculatePrefixFunction "DEBUG"

// use the prefix function
printfn "%s" (prefixer "My message")

// a recursive function to generate Fibonacci numbers
let rec fib x = 
    match x with
        | 1 -> 1
        | 2 -> 1
        | x -> fib (x-1) + fib (x-2)
        
let x = 8		
printfn "fib %i" (fib x)

let res = add (add 4 5) (add 6 7)

let result1 = List.map ((+) 1) [1; 2; 3]
printfn "List.map ((+) 1) [1; 2; 3] = %A" result1