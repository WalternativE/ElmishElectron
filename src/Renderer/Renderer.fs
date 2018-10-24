﻿module Renderer

open Elmish
open Elmish.React
open Elmish.HMR
open Fable.Core.JsInterop

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

let view (model : Model) (dispatch : Message -> unit) =
    R.div [] [
        R.h1 [] [ R.str (sprintf "The count is %i" model) ]
        R.br []
        R.button [ RP.OnClick (fun _ -> dispatch Increase) ] [ R.str "Increase" ]
        R.button [ RP.OnClick (fun _ -> dispatch Decrease) ] [ R.str "Decrease" ]
        RS.button [ RSP.Color(RSP.Secondary) ] [ R.str "Blub" ]
    ]

Program.mkSimple init update view
|> Program.withReact "app"
|> Program.withConsoleTrace
|> Program.withHMR
|> Program.run