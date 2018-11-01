module Renderer

open Elmish
open Elmish.React
open Fable.Core.JsInterop

importAll "../../node_modules/bootstrap/dist/css/bootstrap.min.css"
importAll "./assets/renderer.css"

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
module RP = Fable.Helpers.React.Props
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
        RS.navbar
            [ RSP.RSNavbarProps.Dark true
              RSP.Color(RSP.Dark)
              RSP.Fixed(RSP.Top)
              RP.ClassName "flex-md-nowrap shadow navbar-expand-md" ] [
            RS.navbarBrand [ Routing.href Routing.Counter; RP.ClassName "col-sm-3 col-md-2 mr-0" ] [ R.str "ElmishReact" ]
            RS.input [ RSP.Type(RSP.Text); RP.ClassName "form-control-dark w-100" ]
            RS.nav [ RSP.RSNavProps.Navbar; RP.Class "px-3" ] [
                RS.navItem [ RP.ClassName "text-nowrap" ] [
                    RS.navLink [ RP.Href "#" ] [ R.str "Login" ]
                ]
            ]
        ]
        RS.container [ RSP.Fluid(true) ] [
            RS.row [] [
                RS.nav [ RP.ClassName "col-md-2 d-none d-md-block bg-light sidebar"; RSP.Tag "div" ] [
                    R.div [ RP.ClassName "sidebar-sticky" ] [
                        RS.nav [ RP.ClassName "flex-column"] [
                            RS.navItem [ ] [
                                RS.navLink [ Routing.href Routing.Counter ] [ R.str "Counter" ]
                            ]
                            RS.navItem [ ] [
                                RS.navLink [ Routing.href Routing.Alerts] [ R.str "Alerts" ]
                            ]
                        ]
                    ]
                ]
                R.main [ RP.ClassName "col-md-9 ml-sm-auto col-lg-10 px-4"; RP.Role "main" ] [
                    viewContent model dispatch
                ]
            ]
        ]
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