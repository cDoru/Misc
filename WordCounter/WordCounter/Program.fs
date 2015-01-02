open System
open System.IO
open System.Text

let CountWords file =
    let nonLetters = 
        [0..255]
            |> Seq.map(fun x -> Convert.ToChar x)
            |> Seq.filter(fun x -> Char.IsLetter(x) = false)
            |> Seq.toArray
    seq { use fileReader = new StreamReader(File.OpenRead(file))
        while not fileReader.EndOfStream do
            yield fileReader.ReadLine() }
            |> Seq.collect (fun line -> line.Split(nonLetters, StringSplitOptions.RemoveEmptyEntries))
            |> Seq.countBy (fun x -> x)
            |> Seq.sortBy (fun x -> -snd x)
            |> Seq.toArray

let ConvertToText wordCounts =
    let sb = new StringBuilder()
    for t in wordCounts do
        ("{0} - {1}", fst t, snd t)
            |> String.Format
            |> sb.AppendLine
            |> ignore
    sb.ToString()

[<EntryPoint>]
let main argv = 
     let words = CountWords "DemoText.txt"
     Console.SetBufferSize(Console.BufferHeight, words.Length + 1)
     words
        |> ConvertToText
        |> Console.Write
        |> ignore
     Console.ReadLine() |> ignore
     0