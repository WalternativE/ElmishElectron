module FsReactstrap

open Fable.Core
open Fable.Helpers.React
open Fable.Import.React
open Fable.Core.JsInterop

[<AutoOpen>]
module Props =
    [<StringEnum>]
    type Color =
        | Secondary

    type RSButtonProps =
        | Active of bool
        | Block of bool
        | Color of Color

let inline button (props : RSButtonProps list) (elems : ReactElement list) : ReactElement =
    ofImport "Button" "reactstrap" (keyValueList CaseRules.LowerFirst props) elems