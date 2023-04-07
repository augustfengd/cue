module Tests

open System.IO
open Xunit
open FsUnit.Xunit

module Helper =
    let openFile path =
      Path.Combine [| __SOURCE_DIRECTORY__ ; "data" ; path |] |> File.OpenText


module Lexer =
  open CUE.Parser
  [<Fact>]
  let ``helloworld`` () =
      let fs = Helper.openFile "helloworld/foobar.cue"
      Lexer.tokenize fs |> should equal ["foobar" ; ":" ; "helloworld"]
