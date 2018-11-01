module Renderer

open Elmish
open Elmish.React
open Fable.Core.JsInterop

importAll "../../node_modules/bootstrap/dist/css/bootstrap.min.css"

type PageModel =
    | CounterPage of Counter.Model
    | AlertsPage

type Model =
    { PageModel : PageModel }

type Msg =
    | CounterMsg of Counter.Message

let urlUpdate (result : Routing.Route option) model =
    match result with
    | Some Routing.Counter ->
        let mdl = Counter.init ()
        { model with PageModel = CounterPage mdl }, Cmd.none
    | Some Routing.Alerts ->
        { model with PageModel = AlertsPage }, Cmd.none
    | None ->
        model, Routing.newUrl Routing.Counter

let init result =
    let cm = Counter.init ()
    let model = { PageModel = CounterPage cm }

    urlUpdate result model

let update (msg : Msg) (model : Model) =
    match msg, model.PageModel with
    | CounterMsg msg, CounterPage mdl ->
        let cm = Counter.update msg mdl
        { model with PageModel = CounterPage cm}, Cmd.none
    | CounterMsg msg, _ -> model, Cmd.none

module R = Fable.Helpers.React
module RS = FsReactstrap
module RSP = FsReactstrap.Props

let viewContent model dispatch =
    match model.PageModel with
    | CounterPage cpm ->
        Counter.view cpm (CounterMsg >> dispatch)
    | AlertsPage ->
        R.h1 [] [ R.str "Hello from alerts" ]

let view (model : Model) (dispatch : Msg -> unit) =
    R.div [] [
        R.div [] [
            RS.button [ RSP.Color(RSP.Primary); Routing.href Routing.Counter ] [ R.str "Go to Counter" ]
            R.span [] [ R.str " " ]
            RS.button [ RSP.Color(RSP.Primary); Routing.href Routing.Alerts ] [ R.str "Go to Alerts" ]
        ]
        R.hr []
        viewContent model dispatch
    ]

#if DEBUG
open Elmish.HMR
open Elmish.Debug
#endif

open Elmish.Browser.Navigation
open Elmish.Browser.UrlParser

Program.mkProgram init update view
|> Program.toNavigable (parseHash Routing.route) urlUpdate
#if DEBUG
|> Program.withHMR
|> Program.withDebugger
#endif
|> Program.withReact "app"
#if DEBUG
|> Program.withConsoleTrace
#endif
|> Program.run