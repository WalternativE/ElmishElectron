module FsReactstrap

open Fable.Core
open Fable.Helpers.React
open Fable.Import.React
open Fable.Core.JsInterop
open Fable.Helpers.React.Props

[<AutoOpen>]
module Props =
    [<StringEnum>]
    type Color =
        | Primary
        | Secondary
        | Success
        | Danger
        | Warning
        | Info
        | Light
        | Dark

    type RSButtonProps =
        | Active of bool
        | Block of bool
        | Color of Color
            interface IProp

    [<StringEnum>]
    type Fluid =
        | [<CompiledName("container-fluid")>] ContainerFluid
        | Container

    type RSContainerProps =
        | Fluid of bool
            interface IProp

    type RSAlertProps =
        | CloseClassName of string
        | Color of Color
        | IsOpen of bool
        | Toggle of (MouseEvent -> unit)
        | Tag of string // TODO find out what the optional function type could mean
        // TODO implement Fade component
            interface IProp

    type RSBadgeProps =
        | Color of Color
        | Pill of bool
            interface IProp

let inline alert (props : IProp list) (elems : ReactElement list) : ReactElement =
    ofImport "Alert" "reactstrap" (keyValueList CaseRules.LowerFirst props) elems

let inline button (props : IProp list) (elems : ReactElement list) : ReactElement =
    ofImport "Button" "reactstrap" (keyValueList CaseRules.LowerFirst props) elems

let inline container (props : IProp list) (elems : ReactElement list) : ReactElement =
    ofImport "Container" "reactstrap" (keyValueList CaseRules.LowerFirst props) elems

let inline badge (props : IProp list) (elems : ReactElement list) : ReactElement =
    ofImport "Badge" "reactstrap" (keyValueList CaseRules.LowerFirst props) elems