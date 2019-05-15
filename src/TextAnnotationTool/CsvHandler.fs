module CsvHandler

open System.IO
open CsvHelper
open CsvHelper.Configuration.Attributes

type Annotation = {
    [<Name("File"); Index(0)>]
    File : string; 
    [<Name("Line"); Index(1)>]
    Line : int; 
    [<Name("Begin Offset"); Index(2)>]
    BeginOffset : int; 
    [<Name("End Offset"); Index(3)>]
    EndOffset : int; 
    [<Name("Type"); Index(4)>]
    Type : string;
}

let write (path : string) records =
    use writer = new StreamWriter(path)
    use csv = new CsvWriter(writer)
    csv.WriteRecords(records)