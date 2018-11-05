module Renderer

open Elmish
open Elmish.React
open Fable.Core.JsInterop
open Fable.Import.React

importAll "./assets/styles/app.scss"

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
module RC = FsCoreuiReact
module RCP = FsCoreuiReact.Props

let viewContent model dispatch =
    match model.PageModel with
    | CounterPage cpm ->
        R.fragment [] [
            Counter.view cpm (CounterMsg >> dispatch)
        ]
    | AlertsPage ->
        R.fragment [] [
            R.h1 [] [ R.str "Hello from alerts" ]
        ]

let brandImage = importAll<ReactElement> "./assets/img/brand/logo.svg"
let smallBrandImage = importAll<ReactElement> "./assets/img/brand/sygnet.svg"

let headerFragment =
    let fullBrandProps : RCP.BrandOptions =
        { src = brandImage
          width = 89
          height = 25
          alt = "CoreUI Logo" }

    let minimizedBrandProps : RCP.BrandOptions =
        { src = smallBrandImage
          width = 30
          height = 30
          alt = "CoreUI Logo" }

    R.fragment [] [
        RC.sidebarToggler
            [ RCP.SidebarTogglerProps.ClassName "d-lg-none"
              RCP.SidebarTogglerProps.Display "md"
              RCP.SidebarTogglerProps.Mobile true ] []
        RC.navbarBrand [ RCP.Brand fullBrandProps; RCP.Minimized minimizedBrandProps ] []
        RC.sidebarToggler
            [ RCP.SidebarTogglerProps.ClassName "d-md-down-none"
              RCP.SidebarTogglerProps.Display "lg" ] []
        
        RS.nav [ RP.ClassName "d-md-down-none"; RSP.Navbar ] [
            RS.navItem [ RP.ClassName "px-3" ] [
                RS.navLink [ Routing.href Routing.Counter ] [ R.str "Dashboard" ]
            ]
            RS.navItem [ RP.ClassName "px-3" ] [
                RS.navLink [ Routing.href Routing.Alerts ] [ R.str "Users" ]
            ]
            RS.navItem [ RP.ClassName "px-3" ] [
                RS.navLink [ RP.Href "#" ] [ R.str "Settings" ]
            ]
        ]

        RS.nav [ RP.ClassName "ml-auto"; RSP.Navbar ] [
            RS.navItem [ RP.ClassName "d-md-down-none" ] [
                RS.navLink [ RP.Href "#" ] [
                    R.i [ RP.ClassName "icon-bell" ] []
                    RS.badge [ RSP.Pill true; RSP.RSBadgeProps.Color RSP.Danger ] [ R.str "5" ]
                ]
            ]
            RS.navItem [ RP.ClassName "d-md-down-none" ] [
                RS.navLink [ RP.Href "#" ] [
                    R.i [ RP.ClassName "icon-list" ] []
                ]
            ]
            RS.navItem [ RP.ClassName "d-md-down-none" ] [
                RS.navLink [ RP.Href "#" ] [
                    R.i [ RP.ClassName "icon-location-pin" ] []
                ]
            ]

            RC.headerDropdown [ RCP.Direction "down" ] [
                RS.dropdownToggle [ RSP.RSDropdownToggleProps.Nav ] [
                    R.img [ RP.Src "https://api.adorable.io/avatars/150/bobben.png"; RP.ClassName "img-avatar" ]
                ]
                RS.dropdownMenu [ RSP.Right ] [
                    RS.dropdownItem [ RSP.Header; RSP.RSDropdownItemProps.Tag "div"; RSP.RSDropdownItemProps.ClassName "text-center" ] [
                        R.strong [] [ R.str "Account" ]
                    ]
                    RS.dropdownItem [ ] [
                        R.i [ RP.ClassName "fa fa-bell-o" ] []
                        R.str " Updates"
                        RS.badge [ RSP.RSBadgeProps.Color RSP.Info ] [ R.str "42" ]
                    ]
                ]
            ]
        ]
    ]

let view (model : Model) (dispatch : Msg -> unit) =
    R.div [ RP.ClassName "app" ] [
        RC.header [] [ headerFragment ]
        R.div [ RP.ClassName "app-body" ] [
            RC.sidebar [] [
                RC.sidebarHeader [] []
                RC.sidebarForm [] []
                RC.sidebarFooter [] []
                RC.sidebarMinimizer [] []
            ]
            R.main [ RP.ClassName "main" ] [
                RS.container [ RSP.Fluid true; RP.Style [ RP.PaddingTop 20 ] ] [
                    viewContent model dispatch
                ]
            ]
            RC.aside [ RCP.Fixed true ] []
        ]
        RC.footer [] []
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
