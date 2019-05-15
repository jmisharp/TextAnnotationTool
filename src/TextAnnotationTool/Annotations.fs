module Annotations

open System.Linq
open System.Text.RegularExpressions
open CsvHandler

let private specialCharRegex = Regex(@"[^\w\s]")
let private wordRegex = Regex(@"(\w+)", RegexOptions.IgnoreCase)

let removeSpecialChars s =
    specialCharRegex.Replace(s, "")

let private extractWords s =
    let matches = wordRegex.Matches(s)
    
    matches |> Seq.collect(fun m -> m.Captures |> Seq.map (fun c -> (c.Value, c.Index, c.Length + c.Index)))

let private intersect (s2 : seq<string>) (s1 : seq<string * int * int>) = 
    s1 |> Seq.choose(fun (value, indexStart, indexEnd) -> 
                        match s2.Contains(value) with
                        | true -> Some(value, indexStart, indexEnd)
                        | false -> None)

let annotate (file : string) (t : string) entities s = 
    s |> extractWords 
    |> intersect entities 
    |> Seq.map (fun (value, indexStart, indexEnd) -> { File = file; Line = 0; BeginOffset = indexStart; EndOffset = indexEnd; Type = t; })