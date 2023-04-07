module CUE.Utilities

type OptionBuilder () =
    member _.Bind (m,f) =
        match m with
        | Some v -> f v
        | None -> None

    member _.Return (v) = Some v

    member _.ReturnFrom(v) = v

    member _.Zero() = ()


let option = OptionBuilder ()
