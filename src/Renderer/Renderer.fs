module Renderer

open Elmish
open Elmish.React
open Elmish.HMR
open Fable.Core.JsInterop
open Fable.Import.React

importAll "../../node_modules/bootstrap/dist/css/bootstrap.min.css"

type Model = int

type Message =
    | Increase
    | Decrease

let init _ = 42

let update (msg : Message) (model : Model) =
    match msg with
    | Increase -> model + 1
    | Decrease -> model - 1

module R = Fable.Helpers.React
module RP = Fable.Helpers.React.Props
module RS = FsReactstrap
module RSP = FsReactstrap.Props

let increase (dispatch : Message -> unit) (event : MouseEvent) : unit =
    printfn "We do an increase"
    dispatch Increase

let view (model : Model) (dispatch : Message -> unit) =
    RS.container [ RSP.Fluid(true) ] [
        R.h1 [] [ R.str (sprintf "The count is %i" model) ]
        R.br []
        RS.button [ RP.OnClick (increase dispatch); RSP.Color(RSP.Primary) ] [ R.str "Increase" ]
        R.span [] [ R.str " "]
        RS.button [ RP.OnClick (fun _ -> dispatch Decrease); RSP.Color(RSP.Secondary) ] [ R.str "Decrease" ]
        R.span [] [ R.str " "]
        RS.button [ RSP.Color(RSP.Success) ] [ R.str "Another" ]
    ]

Program.mkSimple init update view
|> Program.withHMR
|> Program.withReact "app"
|> Program.withConsoleTrace
|> Program.run