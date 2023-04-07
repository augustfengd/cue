namespace CUE.Parser

open System.IO
open System.Text
open CUE.Utilities

module Lexer =
    let read (sr : StreamReader) : char option =
        match sr.Peek () with
        | -1 -> None
        | _ -> Some (sr.Read () |> char)

    let skip (sr: StreamReader) : unit =
        sr.Read() |> ignore

    let trim (sr: StreamReader) (c: char) : unit =
        while sr.Peek () = int c do sr.Read () |> ignore

    let peek (sr: StreamReader) : char option =
        match sr.Peek () with
        | -1 -> None
        | c -> Some(char c)

    let tokenize (sr: StreamReader) =
        let rec build (accumulator : StringBuilder) : StringBuilder option =
            match peek sr with
            | None -> Some accumulator
            | Some c -> option {
                match c with
                | ' ' ->
                    do trim sr ' '
                    return! build accumulator
                | ':' ->
                    do skip sr
                    return accumulator.Append ':'
                | c ->
                    do skip sr
                    return! build (accumulator.Append c)
            }
        seq {
            while peek sr <> None do
                match build (StringBuilder ()) with
                | Some token -> yield token
                | None -> ()
        } |> Seq.toList
