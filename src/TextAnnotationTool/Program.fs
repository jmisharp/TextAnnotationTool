open System.IO

[<EntryPoint>]
let main argv =
    let dir = DirectoryInfo(argv.[0])
    let entityType = dir.Name
    
    let data = dir.FullName
                |> Directory.EnumerateFiles 
                |> Seq.map(fun f -> (f, f |> File.ReadAllText))

    let entityData = Path.Combine(dir.Parent.FullName, sprintf "%s.txt" entityType) |> File.ReadAllText
    let entities = entityData.Split(',')

    let annotations = data |> Seq.collect(fun (file, s) -> s |> Annotations.annotate file entityType entities |> Seq.toList)
    annotations |> CsvHandler.write argv.[1]

    0 // return an integer exit code
